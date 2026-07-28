// Randomizer Compatibility Patches by: MottZilla
// These patches allow various aspects of randomizers to function as intended. Because the game is recompiled from the
// original unmodified game all code changes made by the randomizer are missing. Here we patch various functions to read from
// the patched overlay data to match the behavior that is intended. Complex presets would need large amounts of code re-implemented.
//
// Currently we are just doing the basics for simple presets as well as for the built-in randomizer feature.

using System.Data.SqlTypes;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using RecompOne.Runtime.Context;
using RecompOne.Runtime.Dispatch;
using RecompOne.Runtime.Hle;
using RecompOne.Runtime.Memory;
using Silk.NET.Vulkan;
using SixLabors.ImageSharp;

namespace Recompiled;

public enum PresetId : byte { None, Lycanthrope, Nimble, NimbleLite, Expedition, Warlock, BountyHunter, Hitman, Unknown, Integrated }

public static partial class RandoPatch
{
    static bool _initialized;
    static byte LastPresetStringLength = 0;

    static void EnsureInitialized()
    {
        if (_initialized) return;
        _initialized = true;
    }

    // This is used by patched menu functions to know if you have either JP Familar Card
    public static bool HasJPCard(IMemory m)
    {
        if (m.ReadU8(0x8009797B) > 0 || m.ReadU8(0x8009797C) > 0)
            return true;
        return false;
    }

    // Helper Function for comparing in game memory preset string to given parameter string
    public static bool PresetNameIs(CpuContext c, IMemory m, string CheckString)
    {
        UInt32 PresetBaseOffset = 0x801A78E5;
        byte StringIndex = 0;
        byte ReadIndex = 0;
        byte ReadByte = 0;

        LastPresetStringLength = 0;

        // Console.WriteLine("Comparing Preset Name to: " + CheckString);

        if (m.ReadU32(0x801A78B4) == 0x75706E49)     // Detect Original game via the String ( Input "RICHTER" to play )
        {
            m.WriteU8(0x8000C000, (byte)PresetId.None);
            return false;
        }

        while (true)
        {
            // Console.WriteLine("Preset Name Checking Position" + ReadIndex);
            ReadByte = m.ReadU8(PresetBaseOffset + ReadIndex);  // Read a byte from preset string in game memory
            ReadIndex++;

            LastPresetStringLength = ReadIndex;     // This exists to detect between nimble and nimble-lite.

            if (ReadByte == 0 || ReadByte == 0x20)  // preset name terminates on 0x00 or 0x20.
                break;

            // Console.WriteLine("Preset Name ReadByte equals " + $"Hex: {ReadByte:X}" + "CheckString Byte equals " + $"Hex: {(int)CheckString[StringIndex]:X}");

            if (ReadByte == 0x81) // Detecting hyphens
            {
                if (m.ReadU8(PresetBaseOffset + ReadIndex) == 0x7C)
                {
                    ReadByte = 0x2D;
                    ReadIndex++;
                }
            }

            if (StringIndex < CheckString.Length && ReadByte != CheckString[StringIndex])
            {
                // Console.WriteLine("Failed Preset Match on position:" + ReadIndex);
                return false;
            }
            if (StringIndex < CheckString.Length && CheckString[StringIndex] == 0)
                break;

            StringIndex++;
        }

        // Console.WriteLine("Found Preset Name Match for: " + CheckString);
        return true;
    }

    public static bool PreHandleGravityBootsMP(CpuContext c, IMemory m)
    {
        byte CUR_PRESET = m.ReadU8(0x8000C000);

        if (CUR_PRESET == (byte)PresetId.NimbleLite)    // Gravity Boots free to use in this preset
        {
            c.V0 = 0;       // Set Return Value to 0
            return false;   // Do not Execute HandleGravityBootsMP
        }

        return true;        // Execute HandleGravityBootsMP normally.
    }

    // Handles various presets discounted transformation MP costs
    public static void PreHandleTransformationMP(CpuContext c, IMemory m)
    {
        byte CUR_PRESET = m.ReadU8(0x8000C000);
        UInt32 g_GameTimer = m.ReadU32(0x8003c8c4);
        UInt32 CUR_MP = m.ReadU32(0x80097BB0);

        if (CUR_PRESET == (byte)PresetId.Lycanthrope)
        {
            if (c.A0 == 2 && c.A1 == 1 && g_GameTimer % 120 == 0)    // A0 == WOLF, A1 = Reduce MP
            {
                CUR_MP++;   // Offset MP about to be consumed
                m.WriteU32(0x80097BB0, CUR_MP);
            }
        }
        if (CUR_PRESET == (byte)PresetId.Warlock)
        {
            if (m.ReadU8(0x8009796C) > 1 && c.A0 == 1 && c.A1 == 1 && g_GameTimer % 30 == 0)    // Power of Mist Active A0 == MIST, A1 = Reduce MP
            {
                CUR_MP += 2;   // Offset MP about to be consumed
                m.WriteU32(0x80097BB0, CUR_MP);
            }
            else
            {
                if (m.ReadU8(0x8009796C) < 2 && c.A0 == 1 && c.A1 == 1 && g_GameTimer % 8 == 0)    // Power of Mist not Active A0 == MIST, A1 = Reduce MP
                {
                    CUR_MP += 10;   // Offset MP about to be consumed
                    m.WriteU32(0x80097BB0, CUR_MP);
                }
            }
        }
    }

    // Gives starting relics for various presets.
    public static void SetupStartingRelics(CpuContext c, IMemory m)
    {
        byte CUR_PRESET;

        CUR_PRESET = m.ReadU8(0x8000C000);

        if (CUR_PRESET == (byte)PresetId.Lycanthrope)
        {
            m.WriteU32(0x80097964 + 4, 0x00030303); // Soul of Wolf, Power of Wolf, Skill of Wolf
        }
        if (CUR_PRESET == (byte)PresetId.Nimble || CUR_PRESET == (byte)PresetId.NimbleLite || CUR_PRESET == (byte)PresetId.Expedition)
        {
            m.WriteU8(0x80097964, 0x03);    // Soul of Bat
            m.WriteU16(0x80097970, 0x0303); // Gravity Boots & Leap Stone
        }
        if (CUR_PRESET == (byte)PresetId.Warlock)
        {
            m.WriteU8(0x80097964 + 7, 0x03);    // Form of Mist
            m.WriteU8(0x80097BB8, 0x01);    // 1 STR
            m.WriteU8(0x80097BBC, 0x01);    // 1 CON
            m.WriteU8(0x80097BC0, 0x99);    // 99 INT
            m.WriteU8(0x80097BC4, 0x01);    // 1 LCK
        }
        if (CUR_PRESET == (byte)PresetId.BountyHunter)
        {
            m.WriteU8(0x80097964 + 0xF, 0x03);
            m.WriteU8(0x80097BC4, 0x63);    // 99 LCK
        }
        if (CUR_PRESET == (byte)PresetId.Hitman)
        {
            m.WriteU8(0x80097964 + 0x0F, 0x03);
            m.WriteU8(0x80097964 + 0x12, 0x01);
            m.WriteU8(0x80097964 + 0x13, 0x01);
            m.WriteU8(0x80097964 + 0x14, 0x01);
            m.WriteU8(0x80097964 + 0x15, 0x01);
            m.WriteU8(0x80097964 + 0x16, 0x01);
            m.WriteU8(0x80097BC4, 0x63);    // 99 LCK
        }
    }

    // Detects randomizer preset by reading game memory and sets an identifier in game memory to be used by other functions.
    public static void DetectPreset(CpuContext c, IMemory m)
    {
        byte CUR_PRESET = m.ReadU8(0x8000C000);

        if (CUR_PRESET != 0)
            return;

        // Lycanthrope
        if (PresetNameIs(c, m, "lycanthrope"))
        {
            m.WriteU8(0x8000C000, (byte)PresetId.Lycanthrope);
        }
        // Nimble
        if (PresetNameIs(c, m, "nimble"))
        {
            m.WriteU8(0x8000C000, (byte)PresetId.Nimble);
        }
        // Nimble-Lite
        if (PresetNameIs(c, m, "nimble-lite") && LastPresetStringLength > 7)
        {
            m.WriteU8(0x8000C000, (byte)PresetId.NimbleLite);
        }
        // Warlock
        if (PresetNameIs(c, m, "warlock"))
        {
            m.WriteU8(0x8000C000, (byte)PresetId.Warlock);
        }
        // Expedition
        if (PresetNameIs(c, m, "expedition"))
        {
            m.WriteU8(0x8000C000, (byte)PresetId.Expedition);
        }
        // Bounty Hunter
        if (PresetNameIs(c, m, "bounty-hunter"))
        {
            m.WriteU8(0x8000C000, (byte)PresetId.BountyHunter);
        }
        // Hitman
        if (PresetNameIs(c, m, "hitman"))
        {
            m.WriteU8(0x8000C000, (byte)PresetId.Hitman);
        }

        // If no Preset matched yet and we don't see vanilla ( Input "RICHTER" to play ) string
        if (m.ReadU8(0x8000C000) == 0 && m.ReadU32(0x801A78B4) != 0x75706E49)
        {
            m.WriteU8(0x8000C000, (byte)PresetId.Unknown);      // We do this so we know some unsupported preset is being used. 
        }
    }

    public static void InitStatsAndGear(CpuContext c, IMemory m)
    {
        // Modified to read Overlay to get updated Starting Gear and updated Prologue Reward Items.
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x38D0u));
        c.SP = c.SP - 0x18u;
        m.WriteU32((c.SP + 0x14u), c.RA);
        if (c.V0 == 0u)
        {
            m.WriteU32((c.SP + 0x10u), c.S0);
            goto L800FF7E8;
        }
        m.WriteU32((c.SP + 0x10u), c.S0);
        c.RA = 0x800FF7D8u;
        SoTN.func_800F53A4(c, m);
        c.RA = 0x800FF7E0u;
        SoTN.UpdateCapePalette(c, m);
        goto L8010073C;
    L800FF7E8:;
        c.V0 = 0u | 0x0001u;
        if (c.A0 != c.V0)
        {
            c.S0 = 0u | 0x07FFu;
            goto L800FF9D8;
        }
        c.S0 = 0u | 0x07FFu;
        c.A0 = 0x80090000u;
        c.A0 = c.A0 + 0x7C00u;
        c.V0 = m.ReadU32(c.A0);
        //c.V1 = 0u | 0x007Bu;              // 7B = Alucard Sword
        c.V1 = m.ReadU16(0x800FF800);       // Read Right Hand Starting Weapon Value from Overlay
        if (c.V0 != c.V1)                   // If Right Hand Doesn't have (default) Alucard Sword...
        {
            goto L800FF814;
        }
        m.WriteU32(c.A0, 0u);
        goto L800FF85C;
    L800FF814:;
        c.V0 = 0x80090000u;
        c.V0 = m.ReadU32((c.V0 + 0x7C04u));
        if (c.V0 != c.V1)
        {
            goto L800FF838;
        }
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7C04u), 0u);
        goto L800FF854;
    L800FF838:;
        c.V0 = 0x80090000u;
        //c.V0 = m.ReadU8((c.V0 + 0x7A05u));
        c.V0 = m.ReadU8((c.V0 + m.ReadU16(0x800FF83C)));
        if (c.V0 == 0u)
        {
            c.V0 = c.V0 - 0x1u;
            goto L800FF854;
        }
        c.V0 = c.V0 - 0x1u;
        c.At = 0x80090000u;
        //m.WriteU8((c.At + 0x7A05u), (byte)c.V0);
        m.WriteU8((c.At + m.ReadU16(0x800FF850)), (byte)c.V0);
    L800FF854:;
        c.A0 = 0x80090000u;
        c.A0 = c.A0 + 0x7C00u;
    L800FF85C:;
        c.V0 = m.ReadU32(c.A0);
        //c.V1 = 0u | 0x0010u;              // 10 = Alucard Shield
        c.V1 = m.ReadU16(0x800FF860);       // Read Left Hand Starting Weapon Value from Overlay
        if (c.V0 != c.V1)
        {
            goto L800FF874;
        }
        m.WriteU32(c.A0, 0u);
        goto L800FF8B4;
    L800FF874:;
        c.V0 = 0x80090000u;
        c.V0 = m.ReadU32((c.V0 + 0x7C04u));
        if (c.V0 != c.V1)
        {
            goto L800FF898;
        }
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7C04u), 0u);
        goto L800FF8B4;
    L800FF898:;
        c.V0 = 0x80090000u;
        //c.V0 = m.ReadU8((c.V0 + 0x799Au));                // Read Inventory Count of Alucard Shields
        c.V0 = m.ReadU8((c.V0 + m.ReadU16(0x800FF89C)));
        if (c.V0 == 0u)
        {
            c.V0 = c.V0 - 0x1u;
            goto L800FF8B4;
        }
        c.V0 = c.V0 - 0x1u;
        c.At = 0x80090000u;
        //m.WriteU8((c.At + 0x799Au), (byte)c.V0);          // Write back new Count
        m.WriteU8((c.At + m.ReadU16(0x800FF8B0)), (byte)c.V0);
    L800FF8B4:;
        c.A0 = 0x80090000u;
        c.A0 = c.A0 + 0x7C08u;
        c.V1 = m.ReadU32(c.A0);
        //c.V0 = 0u | 0x002Du;              // 2D = Dragon Helm
        c.V0 = m.ReadU16(0x800FF8C0);       // Read Starting Helm from Overlay
        if (c.V1 != c.V0)
        {
            c.V0 = 0u | 0x001Au;
            goto L800FF8D4;
        }
        c.V0 = 0u | 0x001Au;
        m.WriteU32(c.A0, c.V0);
        goto L800FF8F0;
    L800FF8D4:;
        c.V0 = 0x80090000u;
        //c.V0 = m.ReadU8((c.V0 + 0x7A60u));
        c.V0 = m.ReadU8((c.V0 + m.ReadU16(0x800FF8D8)));
        if (c.V0 == 0u)
        {
            c.V0 = c.V0 - 0x1u;
            goto L800FF8F0;
        }
        c.V0 = c.V0 - 0x1u;
        c.At = 0x80090000u;
        //m.WriteU8((c.At + 0x7A60u), (byte)c.V0);
        m.WriteU8((c.At + m.ReadU16(0x800FF8EC)), (byte)c.V0);
    L800FF8F0:;
        c.A0 = 0x80090000u;
        c.A0 = c.A0 + 0x7C0Cu;
        c.V1 = m.ReadU32(c.A0);
        //c.V0 = 0u | 0x000Fu;              // 0F = Alucard Mail
        c.V0 = m.ReadU16(0x800FF8FC);       // Read Starting Body Armor Value from Overlay
        if (c.V1 != c.V0)
        {
            goto L800FF910;
        }
        m.WriteU32(c.A0, 0u);
        goto L800FF92C;
    L800FF910:;
        c.V0 = 0x80090000u;
        //c.V0 = m.ReadU8((c.V0 + 0x7A42u));
        c.V0 = m.ReadU8((c.V0 + m.ReadU16(0x800FF914)));
        if (c.V0 == 0u)
        {
            c.V0 = c.V0 - 0x1u;
            goto L800FF92C;
        }
        c.V0 = c.V0 - 0x1u;
        c.At = 0x80090000u;
        //m.WriteU8((c.At + 0x7A42u), (byte)c.V0);
        m.WriteU8((c.At + m.ReadU16(0x800FF928)), (byte)c.V0);
    L800FF92C:;
        c.A0 = 0x80090000u;
        c.A0 = c.A0 + 0x7C10u;
        c.V1 = m.ReadU32(c.A0);
        //c.V0 = 0u | 0x0038u;              // 38 = Twilight Cloak
        c.V0 = m.ReadU16(0x800FF938);       // Read Starting Cape Value from Overlay
        if (c.V1 != c.V0)
        {
            c.V0 = 0u | 0x0030u;
            goto L800FF954;
        }
        c.V0 = 0u | 0x0030u;
        m.WriteU32(c.A0, c.V0);
        c.RA = 0x800FF94Cu;
        SoTN.UpdateCapePalette(c, m);
        goto L800FF970;
    L800FF954:;
        c.V0 = 0x80090000u;
        //c.V0 = m.ReadU8((c.V0 + 0x7A6Bu));    // Item Count
        c.V0 = m.ReadU8((c.V0 + m.ReadU16(0x800FF958)));
        if (c.V0 == 0u)
        {
            c.V0 = c.V0 - 0x1u;
            goto L800FF970;
        }
        c.V0 = c.V0 - 0x1u;
        c.At = 0x80090000u;
        //m.WriteU8((c.At + 0x7A6Bu), (byte)c.V0);
        m.WriteU8((c.At + m.ReadU16(0x800FF96C)), (byte)c.V0);
    L800FF970:;
        c.A0 = 0x80090000u;
        c.A0 = c.A0 + 0x7C14u;
        c.V0 = m.ReadU32(c.A0);
        //c.V1 = 0u | 0x004Eu;              // 4E = Necklace of J
        c.V1 = m.ReadU16(0x800FF97C);       // Read Starting Acc1 Value from Overlay
        if (c.V0 != c.V1)
        {
            c.V0 = 0u | 0x0039u;
            goto L800FF990;
        }
        c.V0 = 0u | 0x0039u;
        m.WriteU32(c.A0, c.V0);
        goto L80100734;
    L800FF990:;
        c.V0 = 0x80090000u;
        c.V0 = m.ReadU32((c.V0 + 0x7C18u));
        if (c.V0 != c.V1)
        {
            c.V0 = 0u | 0x0039u;
            goto L800FF9B4;
        }
        c.V0 = 0u | 0x0039u;
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7C18u), c.V0);
        goto L80100734;
    L800FF9B4:;
        c.V0 = 0x80090000u;
        //c.V0 = m.ReadU8((c.V0 + 0x7A81u));      // Read Necklace of J Item Count
        c.V0 = m.ReadU8((c.V0 + m.ReadU16(0x800FF9B8)));
        if (c.V0 == 0u)
        {
            c.V0 = c.V0 - 0x1u;
            goto L80100734;
        }
        c.V0 = c.V0 - 0x1u;
        c.At = 0x80090000u;
        //m.WriteU8((c.At + 0x7A81u), (byte)c.V0);  // Update item Count
        m.WriteU8((c.At + m.ReadU16(0x800FF9CC)), (byte)c.V0);
        goto L80100734;
    L800FF9D8:;
        c.V0 = 0x80070000u;
        c.V0 = c.V0 - 0x3C8Du;
    L800FF9E0:;
        m.WriteU8(c.V0, (byte)0u);
        c.S0 = c.S0 - 0x1u;
        if ((int)c.S0 >= 0)
        {
            c.V0 = c.V0 - 0x1u;
            goto L800FF9E0;
        }
        c.V0 = c.V0 - 0x1u;
        c.S0 = 0u | 0x0003u;
        c.V0 = 0x80090000u;
        c.V0 = c.V0 + 0x7BF8u;
        c.V1 = c.V0 - 0x24u;
        c.At = 0x80040000u;
        m.WriteU32((c.At - 0x38A0u), 0u);
        m.WriteU32(c.V0, 0u);
    L800FFA0C:;
        m.WriteU32(c.V1, 0u);
        c.S0 = c.S0 - 0x1u;
        if ((int)c.S0 >= 0)
        {
            c.V1 = c.V1 - 0x4u;
            goto L800FFA0C;
        }
        c.V1 = c.V1 - 0x4u;
        c.S0 = 0u + 0u;
        c.A0 = 0u | 0x0001u;
        c.V1 = 0u + 0u;
        c.V0 = 0u | 0x0001u;
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7BECu), 0u);
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7BE8u), c.V0);
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7BF4u), 0u);
    L800FFA44:;
        c.At = 0x80090000u;
        c.At = c.At + c.V1;
        m.WriteU32((c.At + 0x7C44u), c.A0);
        c.At = 0x80090000u;
        c.At = c.At + c.V1;
        m.WriteU32((c.At + 0x7C48u), 0u);
        c.At = 0x80090000u;
        c.At = c.At + c.V1;
        m.WriteU32((c.At + 0x7C4Cu), 0u);
        c.S0 = c.S0 + 0x1u;
        c.V0 = (int)c.S0 < 7 ? 1u : 0u;
        if (c.V0 != 0u)
        {
            c.V1 = c.V1 + 0xCu;
            goto L800FFA44;
        }
        c.V1 = c.V1 + 0xCu;
        c.S0 = 0u + 0u;
    L800FFA7C:;
        c.At = 0x80090000u;
        c.At = c.At + c.S0;
        m.WriteU8((c.At + 0x798Au), (byte)0u);
        c.At = 0x80090000u;
        c.At = c.At + c.S0;
        m.WriteU8((c.At + 0x7A8Du), (byte)c.S0);
        c.S0 = c.S0 + 0x1u;
        c.V0 = (int)c.S0 < 169 ? 1u : 0u;
        if (c.V0 != 0u)
        {
            goto L800FFA7C;
        }
        c.S0 = 0u + 0u;
    L800FFAA8:;
        c.At = 0x80090000u;
        c.At = c.At + c.S0;
        m.WriteU8((c.At + 0x7A33u), (byte)0u);
        c.At = 0x80090000u;
        c.At = c.At + c.S0;
        m.WriteU8((c.At + 0x7B36u), (byte)c.S0);
        c.S0 = c.S0 + 0x1u;
        c.V0 = (int)c.S0 < 90 ? 1u : 0u;
        if (c.V0 != 0u)
        {
            c.V0 = 0u | 0x0001u;
            goto L800FFAA8;
        }
        c.V0 = 0u | 0x0001u;
        c.S0 = 0u | 0x0007u;
        c.V1 = 0x80090000u;
        c.V1 = c.V1 + 0x798Au;
        c.A0 = c.V1 - 0x1u;
        m.WriteU8(c.V1, (byte)c.V0);
        c.At = 0x80090000u;
        m.WriteU8((c.At + 0x7A4Du), (byte)c.V0);
        c.At = 0x80090000u;
        m.WriteU8((c.At + 0x7A33u), (byte)c.V0);
        c.At = 0x80090000u;
        m.WriteU8((c.At + 0x7A63u), (byte)c.V0);
        c.At = 0x80090000u;
        m.WriteU8((c.At + 0x7A6Cu), (byte)c.V0);
    L800FFB04:;
        m.WriteU8(c.A0, (byte)0u);
        c.S0 = c.S0 - 0x1u;
        if ((int)c.S0 >= 0)
        {
            c.A0 = c.A0 - 0x1u;
            goto L800FFB04;
        }
        c.A0 = c.A0 - 0x1u;
        c.A0 = 0x80090000u;
        c.A0 = c.A0 + 0x7B9Cu;
        c.V1 = 0x80090000u;
        c.V1 = m.ReadU32((c.V1 + 0x74A0u));
        c.V0 = 0u | 0x001Fu;
        if (c.V1 == c.V0)
        {
            m.WriteU32(c.A0, 0u);
            goto L800FFB44;
        }
        m.WriteU32(c.A0, 0u);
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x3660u));
        if (c.V0 == 0u)
        {
            goto L800FFD60;
        }
    L800FFB44:;
        c.V1 = 0u | 0x0001u;
        c.S0 = 0u | 0x001Du;
        c.V0 = c.A0 - 0x21Bu;
    L800FFB50:;
        m.WriteU8(c.V0, (byte)c.V1);
        c.S0 = c.S0 - 0x1u;
        if ((int)c.S0 >= 0)
        {
            c.V0 = c.V0 - 0x1u;
            goto L800FFB50;
        }
        c.V0 = c.V0 - 0x1u;
        c.S0 = 0u | 0x001Fu;
        c.A1 = 0x80040000u;
        c.A1 = c.A1 - 0x355Cu;
        c.A0 = 0x80090000u;
        c.A0 = c.A0 + 0x796Eu;
        c.V0 = m.ReadU8(c.A0);
        c.V1 = 0x80090000u;
        c.V1 = m.ReadU8((c.V1 + 0x796Fu));
        c.V0 = c.V0 | 0x0002u;
        m.WriteU8(c.A0, (byte)c.V0);
        c.V0 = 0x80090000u;
        c.V0 = m.ReadU8((c.V0 + 0x7973u));
        c.V1 = c.V1 | 0x0002u;
        c.At = 0x80090000u;
        m.WriteU8((c.At + 0x796Fu), (byte)c.V1);
        c.V1 = 0x80090000u;
        c.V1 = m.ReadU8((c.V1 + 0x7974u));
        c.V0 = c.V0 | 0x0002u;
        c.V1 = c.V1 | 0x0002u;
        c.At = 0x80090000u;
        m.WriteU8((c.At + 0x7973u), (byte)c.V0);
        c.At = 0x80090000u;
        m.WriteU8((c.At + 0x7974u), (byte)c.V1);
    L800FFBBC:;
        m.WriteU32(c.A1, 0u);
        c.S0 = c.S0 - 0x1u;
        if ((int)c.S0 >= 0)
        {
            c.A1 = c.A1 - 0x4u;
            goto L800FFBBC;
        }
        c.A1 = c.A1 - 0x4u;
        c.S0 = 0x80090000u;
        c.S0 = c.S0 + 0x7BFCu;
        c.V1 = 0x80090000u;
        c.V1 = m.ReadU32((c.V1 + 0x74A0u));
        c.V0 = 0u | 0x001Fu;
        c.At = 0x80040000u;
        m.WriteU32((c.At - 0x3500u), 0u);
        c.At = 0x80040000u;
        m.WriteU32((c.At - 0x34FCu), 0u);
        if (c.V1 == c.V0)
        {
            m.WriteU32(c.S0, 0u);
            goto L800FFC3C;
        }
        m.WriteU32(c.S0, 0u);
        c.V0 = 0u | 0x0041u;
        if (c.V1 == c.V0)
        {
            goto L800FFC3C;
        }
        c.RA = 0x800FFC0Cu;
        SoTN.rand(c, m);
        c.V1 = 0x38E30000u;
        c.V1 = c.V1 | 0x8E39u;
        { var _r = (long)(int)c.V0 * (int)c.V1; c.LO = (uint)_r; c.HI = (uint)(_r >> 32); }
        c.V1 = (uint)((int)c.V0 >> 31);
        c.A0 = c.HI;
        c.A0 = (uint)((int)c.A0 >> 1);
        c.A0 = c.A0 - c.V1;
        c.V1 = c.A0 << 3;
        c.V1 = c.V1 + c.A0;
        c.V0 = c.V0 - c.V1;
        c.V0 = c.V0 + 0x1u;
        m.WriteU32(c.S0, c.V0);
    L800FFC3C:;
        c.V1 = 0x80090000u;
        c.V1 = m.ReadU32((c.V1 + 0x74A0u));
        c.V0 = 0u | 0x0032u;
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7BA0u), c.V0);
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7BA4u), c.V0);
        c.V0 = 0u | 0x001Eu;
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7BA8u), c.V0);
        c.V0 = 0u | 0x0063u;
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7BACu), c.V0);
        c.V0 = 0u | 0x0014u;
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7BB4u), c.V0);
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7BB0u), c.V0);
        c.V0 = 0u | 0x000Au;
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7BB8u), c.V0);
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7BBCu), c.V0);
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7BC0u), c.V0);
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7BC4u), c.V0);
        c.V0 = 0u | 0x001Au;
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7C08u), c.V0);
        c.V0 = 0u | 0x0030u;
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7C10u), c.V0);
        c.V0 = 0u | 0x0039u;
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7C14u), c.V0);
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7C18u), c.V0);
        c.V0 = 0u | 0x0041u;
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7BF0u), 0u);
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7C00u), 0u);
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7C04u), 0u);
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7C0Cu), 0u);
        if (c.V1 != c.V0)
        {
            c.A0 = 0u | 0x001Au;
            goto L800FFD38;
        }
        c.A0 = 0u | 0x001Au;
        c.A1 = 0u | 0x0001u;
        c.RA = 0x800FFD08u;
        SoTN.TimeAttackController(c, m);
        c.A0 = 0u | 0x0009u;
        c.A1 = 0u | 0x0001u;
        c.RA = 0x800FFD14u;
        SoTN.TimeAttackController(c, m);
        c.A0 = 0u | 0x0004u;
        c.A1 = 0u | 0x0001u;
        c.RA = 0x800FFD20u;
        SoTN.TimeAttackController(c, m);
        c.A0 = 0u | 0x000Eu;
        c.A1 = 0u | 0x0001u;
        c.RA = 0x800FFD2Cu;
        SoTN.TimeAttackController(c, m);
        c.A0 = 0u | 0x000Cu;
        c.A1 = 0u | 0x0001u;
        c.RA = 0x800FFD38u;
        SoTN.TimeAttackController(c, m);
    L800FFD38:;
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7C30u), 0u);
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7C34u), 0u);
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7C38u), 0u);
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7C3Cu), 0u);
        goto L80100734;
    L800FFD60:;
        c.V0 = 0u | 0x0041u;
        if (c.V1 != c.V0)
        {
            c.S0 = 0u | 0x001Fu;
            goto L8010031C;
        }
        c.S0 = 0u | 0x001Fu;
        c.S0 = 0u | 0x001Du;
        c.V1 = c.A0 - 0x21Bu;
        c.V0 = 0u | 0x0006u;
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7BB8u), c.V0);
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7BBCu), c.V0);
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7BC0u), c.V0);
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7BC4u), c.V0);
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7BF0u), 0u);
    L800FFDA0:;
        m.WriteU8(c.V1, (byte)0u);
        c.S0 = c.S0 - 0x1u;
        if ((int)c.S0 >= 0)
        {
            c.V1 = c.V1 - 0x1u;
            goto L800FFDA0;
        }
        c.V1 = c.V1 - 0x1u;
        c.V0 = 0x80140000u;
        c.V0 = m.ReadU32((c.V0 - 0x6804u));
        if (c.V0 == 0u)
        {
            c.A0 = 0u | 0x009Fu;
            goto L800FFDD4;
        }
        //c.A0 = 0u | 0x009Fu;                  // 9F = Potion
        c.A0 = 0u | m.ReadU16(0x800FFDC0);      // Read Reward Id from Overlay Data
        c.A1 = 0u + 0u;
        c.RA = 0x800FFDCCu;
        SoTN.AddToInventory(c, m);
        c.S0 = 0u | 0x0003u;
        goto L800FFE94;
    L800FFDD4:;
        c.A0 = 0x80090000u;
        c.A0 = m.ReadU32((c.A0 + 0x7BA0u));
        c.V1 = 0x80090000u;
        c.V1 = m.ReadU32((c.V1 + 0x7BA4u));
        if (c.A0 != c.V1)
        {
            c.V0 = c.V1 >> 31;
            goto L800FFE48;
        }
        c.V0 = c.V1 >> 31;
        c.V0 = 0x80090000u;
        c.V0 = m.ReadU32((c.V0 + 0x7BB8u));
        c.V1 = 0x80090000u;
        c.V1 = m.ReadU32((c.V1 + 0x7BBCu));
        c.V0 = c.V0 + 0x1u;
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7BB8u), c.V0);
        c.V0 = 0x80090000u;
        c.V0 = m.ReadU32((c.V0 + 0x7BC0u));
        c.V1 = c.V1 + 0x1u;
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7BBCu), c.V1);
        c.V1 = 0x80090000u;
        c.V1 = m.ReadU32((c.V1 + 0x7BC4u));
        c.V0 = c.V0 + 0x1u;
        c.V1 = c.V1 + 0x1u;
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7BC0u), c.V0);
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7BC4u), c.V1);
        c.S0 = 0u + 0u;
        goto L800FFE94;
    L800FFE48:;
        c.V0 = c.V1 + c.V0;
        c.V0 = (uint)((int)c.V0 >> 1);
        c.V0 = (int)c.A0 < (int)c.V0 ? 1u : 0u;
        if (c.V0 != 0u)
        {
            c.S0 = 0u | 0x0002u;
            goto L800FFE7C;
        }
        c.S0 = 0u | 0x0002u;
        c.V0 = 0x80090000u;
        c.V0 = m.ReadU32((c.V0 + 0x7BB8u));
        c.V0 = c.V0 + 0x1u;
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7BB8u), c.V0);
        c.S0 = 0u | 0x0001u;
        goto L800FFE94;
    L800FFE7C:;
        c.V0 = 0x80090000u;
        c.V0 = m.ReadU32((c.V0 + 0x7BBCu));
        c.V0 = c.V0 + 0x1u;
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7BBCu), c.V0);
    L800FFE94:;
        c.V0 = 0x80090000u;
        c.V0 = m.ReadU32((c.V0 + 0x7BA8u));
        if (c.V0 != 0u)
        {
            c.V0 = (int)c.S0 < 3 ? 1u : 0u;
            goto L800FFEB8;
        }
        c.V0 = (int)c.S0 < 3 ? 1u : 0u;
        if (c.V0 == 0u)
        {
            c.A0 = 0u | 0x008Eu;
            goto L800FFEB8;
        }
        //c.A0 = 0u | 0x008Eu;              // 8E = Heart Refresh
        c.A0 = 0u | m.ReadU16(0x800FFEAC);  // Read Reward ID from Overlay Data
        c.A1 = 0u + 0u;
        c.RA = 0x800FFEB8u;
        SoTN.AddToInventory(c, m);
    L800FFEB8:;
        c.V1 = 0x80090000u;
        c.V1 = c.V1 + 0x7BA4u;
        c.V0 = 0u | 0x0046u;
        if (c.S0 != 0u)
        {
            m.WriteU32(c.V1, c.V0);
            goto L800FFED4;
        }
        m.WriteU32(c.V1, c.V0);
        c.V0 = 0u | 0x004Bu;
        m.WriteU32(c.V1, c.V0);
    L800FFED4:;
        c.V0 = 0u | 0x000Au;
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7BA8u), c.V0);
        c.V0 = 0u | 0x0032u;
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7BACu), c.V0);
        c.V0 = 0x80140000u;
        c.V0 = m.ReadU32((c.V0 - 0x6FF8u));
        c.V1 = 0u | 0x0014u;
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7BB4u), c.V1);
        c.V0 = (int)c.V0 < 41 ? 1u : 0u;
        if (c.V0 != 0u)
        {
            c.A0 = 0u | 0x0047u;
            goto L800FFF34;
        }
        //c.A0 = 0u | 0x0047u;              // 47 = Neutron Bomb
        c.A0 = 0u | m.ReadU16(0x800FFF08);  // Read Reward ID from Overlay Data
        c.A1 = 0u + 0u;
        c.RA = 0x800FFF14u;
        SoTN.AddToInventory(c, m);
        c.V0 = 0x80090000u;
        c.V0 = m.ReadU32((c.V0 + 0x7BC0u));
        c.V0 = c.V0 + 0x1u;
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7BC0u), c.V0);
        goto L800FFF4C;
    L800FFF34:;
        c.V0 = 0x80090000u;
        c.V0 = m.ReadU32((c.V0 + 0x7BB8u));
        c.V0 = c.V0 + 0x1u;
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7BB8u), c.V0);
    L800FFF4C:;
        c.V1 = 0x80090000u;
        c.V1 = m.ReadU32((c.V1 + 0x7BFCu));
        c.V0 = 0u | 0x0004u;
        if (c.V1 != c.V0)
        {
            c.V0 = 0u | 0x0003u;
            goto L800FFF9C;
        }
        c.V0 = 0u | 0x0003u;
        c.V0 = (int)c.S0 < 3 ? 1u : 0u;
        if (c.V0 == 0u)
        {
            c.A0 = 0u + 0u;
            goto L80100084;
        }
        c.A0 = 0u + 0u;
        c.V0 = 0x80090000u;
        c.V0 = m.ReadU32((c.V0 + 0x7BACu));
        c.V1 = 0x80090000u;
        c.V1 = m.ReadU32((c.V1 + 0x7BB4u));
        c.V0 = c.V0 + 0x5u;
        c.V1 = c.V1 + 0x5u;
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7BACu), c.V0);
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7BB4u), c.V1);
        goto L80100080;
    L800FFF9C:;
        if (c.V1 != c.V0)
        {
            c.V0 = 0u | 0x0001u;
            goto L800FFFE0;
        }
        c.V0 = 0u | 0x0001u;
        c.V0 = (int)c.S0 < 2 ? 1u : 0u;
        if (c.V0 == 0u)
        {
            c.A0 = 0u + 0u;
            goto L80100084;
        }
        c.A0 = 0u + 0u;
        c.V0 = 0x80090000u;
        c.V0 = m.ReadU32((c.V0 + 0x7BACu));
        c.V1 = 0x80090000u;
        c.V1 = m.ReadU32((c.V1 + 0x7BC0u));
        c.V0 = c.V0 + 0x5u;
        c.V1 = c.V1 + 0x1u;
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7BACu), c.V0);
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7BC0u), c.V1);
        goto L80100080;
    L800FFFE0:;
        if (c.S0 == c.V0)
        {
            c.V0 = (int)c.S0 < 2 ? 1u : 0u;
            goto L80100050;
        }
        c.V0 = (int)c.S0 < 2 ? 1u : 0u;
        if (c.V0 == 0u)
        {
            goto L80100000;
        }
        if (c.S0 == 0u)
        {
            c.A0 = 0u + 0u;
            goto L80100014;
        }
        c.A0 = 0u + 0u;
        goto L80100084;
    L80100000:;
        c.V0 = 0u | 0x0002u;
        if (c.S0 == c.V0)
        {
            c.A0 = 0u + 0u;
            goto L80100068;
        }
        c.A0 = 0u + 0u;
        goto L80100084;
    L80100014:;
        c.V0 = 0x80090000u;
        c.V0 = m.ReadU32((c.V0 + 0x7BC4u));
        c.V1 = 0x80090000u;
        c.V1 = m.ReadU32((c.V1 + 0x7BBCu));
        c.V0 = c.V0 + 0x5u;
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7BC4u), c.V0);
        c.V0 = 0x80090000u;
        c.V0 = m.ReadU32((c.V0 + 0x7BC0u));
        c.V1 = c.V1 + 0x1u;
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7BBCu), c.V1);
        c.V0 = c.V0 + 0x1u;
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7BC0u), c.V0);
    L80100050:;
        c.V1 = 0x80090000u;
        c.V1 = c.V1 + 0x7BA4u;
        c.V0 = m.ReadU32(c.V1);
        c.V0 = c.V0 + 0x5u;
        m.WriteU32(c.V1, c.V0);
    L80100068:;
        c.V1 = 0x80090000u;
        c.V1 = c.V1 + 0x7BB8u;
        c.V0 = m.ReadU32(c.V1);
        c.V0 = c.V0 + 0x1u;
        m.WriteU32(c.V1, c.V0);
    L80100080:;
        c.A0 = 0u + 0u;
    L80100084:;
        c.A1 = 0u + 0u;
        c.RA = 0x8010008Cu;
        SoTN.TimeAttackController(c, m);
        c.V1 = c.V0 + 0u;
        c.V0 = (int)c.V1 < 101 ? 1u : 0u;
        if (c.V0 == 0u)
        {
            c.V0 = (int)c.V1 < 201 ? 1u : 0u;
            goto L80100134;
        }
        c.V0 = (int)c.V1 < 201 ? 1u : 0u;
        c.V1 = 0x80090000u;
        c.V1 = c.V1 + 0x7BA4u;
        c.V0 = m.ReadU32(c.V1);
        c.V0 = c.V0 + 0x5u;
        m.WriteU32(c.V1, c.V0);
        c.V0 = 0x80090000u;
        c.V0 = m.ReadU32((c.V0 + 0x7BB4u));
        c.V1 = 0x80090000u;
        c.V1 = m.ReadU32((c.V1 + 0x7BACu));
        c.V0 = c.V0 + 0x5u;
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7BB4u), c.V0);
        c.V0 = 0x80090000u;
        c.V0 = m.ReadU32((c.V0 + 0x7BB8u));
        c.V1 = c.V1 + 0x5u;
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7BACu), c.V1);
        c.V1 = 0x80090000u;
        c.V1 = m.ReadU32((c.V1 + 0x7BBCu));
        c.V0 = c.V0 + 0x5u;
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7BB8u), c.V0);
        c.V0 = 0x80090000u;
        c.V0 = m.ReadU32((c.V0 + 0x7BC0u));
        c.V1 = c.V1 + 0x5u;
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7BBCu), c.V1);
        c.V1 = 0x80090000u;
        c.V1 = m.ReadU32((c.V1 + 0x7BC4u));
        c.V0 = c.V0 + 0x5u;
        c.V1 = c.V1 + 0x5u;
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7BC0u), c.V0);
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7BC4u), c.V1);
        c.S0 = 0u + 0u;
        goto L80100190;
    L80100134:;
        if (c.V0 == 0u)
        {
            goto L80100150;
        }
        c.V1 = 0x80090000u;
        c.V1 = c.V1 + 0x7BC4u;
        c.V0 = m.ReadU32(c.V1);
        c.V0 = c.V0 + 0x2u;
        goto L80100188;
    L80100150:;
        c.V0 = (int)c.V1 < 301 ? 1u : 0u;
        if (c.V0 == 0u)
        {
            c.V0 = (int)c.V1 < 1000 ? 1u : 0u;
            goto L8010016C;
        }
        c.V0 = (int)c.V1 < 1000 ? 1u : 0u;
        c.V1 = 0x80090000u;
        c.V1 = c.V1 + 0x7BC4u;
        goto L8010017C;
    L8010016C:;
        if (c.V0 != 0u)
        {
            c.S0 = 0u + 0u;
            goto L80100190;
        }
        c.S0 = 0u + 0u;
        c.V1 = 0x80090000u;
        c.V1 = c.V1 + 0x7BBCu;
    L8010017C:;
        c.V0 = m.ReadU32(c.V1);
        c.V0 = c.V0 + 0x1u;
    L80100188:;
        m.WriteU32(c.V1, c.V0);
        c.S0 = 0u + 0u;
    L80100190:;
        c.A1 = 0x800A0000u;
        c.A1 = m.ReadU32((c.A1 + 0x300Cu));
        c.V1 = 0x80090000u;
        c.V1 = m.ReadU32((c.V1 + 0x7BA4u));
        c.A0 = 0x80090000u;
        c.A0 = m.ReadU32((c.A0 + 0x7BB4u));

        // Preset Setup
        SetupStartingRelics(c, m);

        // Starting Equipment Setup

        //c.V0 = 0u | 0x007Bu;              // 7B = Alucard Sword
        c.V0 = m.ReadU16(0x801001A8);       // Read Right Hand Value from Overlay
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7C00u), c.V0);
        //c.V0 = 0u | 0x0010u;              // 10 = Alucard Shield
        c.V0 = m.ReadU16(0x801001B4);       // Read Left Hand Value from Overlay
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7C04u), c.V0);
        //c.V0 = 0u | 0x002Du;              // 2D = Dragon Helm
        c.V0 = m.ReadU16(0x801001C0);       // Read Head Item Value from Overlay
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7C08u), c.V0);
        //c.V0 = 0u | 0x000Fu;              // 0F = Alucard Mail
        c.V0 = m.ReadU16(0x801001CC);       // Read Body Armor Value from Overlay
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7C0Cu), c.V0);
        //c.V0 = 0u | 0x0038u;              // 38 = Twilight Cape
        c.V0 = m.ReadU16(0x801001D8);       // Read Cape Value from Overlay
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7C10u), c.V0);
        //c.V0 = 0u | 0x004Eu;              // 4E = Necklace of J
        c.V0 = m.ReadU16(0x801001E4);       // Read Acc1 Value from Overlay
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7C14u), c.V0);
        //c.V0 = 0u | 0x0039u;              // 39 = No Accessory
        c.V0 = m.ReadU16(0x801001F0);       // Read Acc2 Value from Overlay
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7BFCu), 0u);
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7C18u), c.V0);
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7BA0u), c.V1);
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7BB0u), c.A0);
    L80100214:;
        c.At = 0x80090000u;
        c.At = c.At + c.S0;
        c.V1 = m.ReadU8((c.At + 0x7B90u));
        c.V0 = m.ReadU8(c.A1);
        if (c.V1 != c.V0)
        {
            c.A1 = c.A1 + 0x1u;
            goto L80100240;
        }
        c.A1 = c.A1 + 0x1u;
        c.S0 = c.S0 + 0x1u;
        c.V0 = (int)c.S0 < 8 ? 1u : 0u;
        if (c.V0 != 0u)
        {
            goto L80100214;
        }
    L80100240:;
        c.V0 = 0u | 0x0008u;
        if (c.S0 != c.V0)
        {
            c.V1 = 0u | 0x0001u;
            goto L801002B4;
        }
        c.V1 = 0u | 0x0001u;
        c.V0 = 0u | 0x0063u;
        c.A0 = 0u | 0x0019u;
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7BC4u), c.V0);
        c.V0 = 0u | 0x0005u;
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7BA8u), c.V0);
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7BACu), c.V0);
        c.V0 = 0u | 0x0046u;
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7BB8u), c.V1);
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7BBCu), 0u);
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7BC0u), 0u);
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7BA4u), c.A0);
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7BB4u), c.V1);
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7BA0u), c.A0);
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7BB0u), c.V1);
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7C18u), c.V0);
    L801002B4:;
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x4220u));
        if (c.V0 == 0u)
        {
            goto L80100734;
        }
        c.A1 = 0x800A0000u;
        c.A1 = m.ReadU32((c.A1 + 0x3010u));
        c.S0 = 0u + 0u;
    L801002D4:;
        c.At = 0x80090000u;
        c.At = c.At + c.S0;
        c.V1 = m.ReadU8((c.At + 0x7B90u));
        c.V0 = m.ReadU8(c.A1);
        if (c.V1 != c.V0)
        {
            c.A1 = c.A1 + 0x1u;
            goto L80100300;
        }
        c.A1 = c.A1 + 0x1u;
        c.S0 = c.S0 + 0x1u;
        c.V0 = (int)c.S0 < 8 ? 1u : 0u;
        if (c.V0 != 0u)
        {
            goto L801002D4;
        }
    L80100300:;
        c.V0 = 0u | 0x0008u;
        if (c.S0 != c.V0)
        {
            c.A0 = 0u | 0x0019u;
            goto L80100734;
        }
        c.A0 = 0u | 0x0019u;
        c.A1 = 0u | 0x0002u;
        c.RA = 0x80100314u;
        SoTN.AddToInventory(c, m);
        goto L80100734;
    L8010031C:;
        c.V0 = 0x80040000u;
        c.V0 = c.V0 - 0x355Cu;
    L80100324:;
        m.WriteU32(c.V0, 0u);
        c.S0 = c.S0 - 0x1u;
        if ((int)c.S0 >= 0)
        {
            c.V0 = c.V0 - 0x4u;
            goto L80100324;
        }
        c.V0 = c.V0 - 0x4u;
        c.V1 = 0x00070000u;
        c.V1 = c.V1 | 0xA120u;
        c.A1 = 0x80090000u;
        c.A1 = c.A1 + 0x7BB8u;
        c.V0 = 0u | 0x0006u;
        m.WriteU32(c.A1, c.V0);
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7BBCu), c.V0);
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7BC0u), c.V0);
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7BC4u), c.V0);
        c.V0 = 0u | 0x0046u;
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7BA4u), c.V0);
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7BA0u), c.V0);
        c.V0 = 0u | 0x000Au;
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7BA8u), c.V0);
        c.V0 = 0u | 0x0032u;
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7BF0u), c.V1);
        c.V1 = 0x80090000u;
        c.V1 = m.ReadU32((c.V1 + 0x74A0u));
        c.A0 = 0u | 0x0014u;
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7BACu), c.V0);
        c.V0 = 0u | 0x04D2u;
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7BA8u), c.V0);
        c.V0 = 0u | 0x07D0u;
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7BACu), c.V0);
        c.V0 = 0u | 0x2AF8u;
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7BB4u), c.A0);
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7BB0u), c.A0);
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7BECu), c.V0);
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7BE8u), c.A0);
        c.V1 = c.V1 & 0x0020u;
        if (c.V1 == 0u)
        {
            c.A3 = 0u | 0x0003u;
            goto L801003FC;
        }
        c.A3 = 0u | 0x0003u;
        c.V0 = 0x00010000u;
        c.V0 = c.V0 | 0xADB0u;
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7BECu), c.V0);
    L801003FC:;
        c.A2 = 0u | 0x0001u;
        c.V1 = c.A1 - 0x254u;
        c.A0 = 0u + 0u;
        c.A1 = c.A1 - 0x236u;
    L8010040C:;
        m.WriteU8(c.V1, (byte)c.A3);
        c.At = 0x800B0000u;
        c.At = c.At + c.A0;
        c.V0 = m.ReadU32((c.At - 0x78D4u));
        if (c.V0 == 0u)
        {
            c.A0 = c.A0 + 0x10u;
            goto L8010042C;
        }
        c.A0 = c.A0 + 0x10u;
        m.WriteU8(c.V1, (byte)c.A2);
    L8010042C:;
        c.V1 = c.V1 + 0x1u;
        c.V0 = (int)c.V1 < (int)c.A1 ? 1u : 0u;
        if (c.V0 != 0u)
        {
            goto L8010040C;
        }
        c.V1 = 0u | 0x0032u;
        c.S0 = 0u | 0x00A8u;
        c.V0 = 0x80090000u;
        c.V0 = c.V0 + 0x7A32u;
    L8010044C:;
        m.WriteU8(c.V0, (byte)c.V1);
        c.S0 = c.S0 - 0x1u;
        if ((int)c.S0 >= 0)
        {
            c.V0 = c.V0 - 0x1u;
            goto L8010044C;
        }
        c.V0 = c.V0 - 0x1u;
        c.V1 = 0u | 0x0001u;
        c.S0 = 0u | 0x0059u;
        c.V0 = 0x80090000u;
        c.V0 = c.V0 + 0x7A8Cu;
    L8010046C:;
        m.WriteU8(c.V0, (byte)c.V1);
        c.S0 = c.S0 - 0x1u;
        if ((int)c.S0 >= 0)
        {
            c.V0 = c.V0 - 0x1u;
            goto L8010046C;
        }
        c.V0 = c.V0 - 0x1u;
        c.A0 = 0u | 0x006Fu;
        c.V0 = 0u | 0x0013u;
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7C00u), c.V0);
        c.V0 = 0u | 0x0005u;
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7C04u), c.V0);
        c.V0 = 0u | 0x001Au;
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7C08u), c.V0);
        c.V0 = 0u | 0x0002u;
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7C0Cu), c.V0);
        c.V0 = 0u | 0x0030u;
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7C10u), c.V0);
        c.V0 = 0u | 0x0039u;
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7C14u), c.V0);
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7C18u), c.V0);
        c.V0 = 0u | 0x0003u;
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7C30u), 0u);
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7C34u), 0u);
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7C38u), 0u);
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7C3Cu), 0u);
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7BFCu), 0u);
        c.At = 0x80090000u;
        m.WriteU8((c.At + 0x796Eu), (byte)c.V0);
        c.At = 0x80090000u;
        m.WriteU8((c.At + 0x796Fu), (byte)c.V0);
        c.At = 0x80090000u;
        m.WriteU8((c.At + 0x7973u), (byte)c.V0);
        c.At = 0x80090000u;
        m.WriteU8((c.At + 0x7964u), (byte)c.V0);
        c.At = 0x80090000u;
        m.WriteU8((c.At + 0x7965u), (byte)c.V0);
        c.At = 0x80090000u;
        m.WriteU8((c.At + 0x7968u), (byte)c.V0);
        c.At = 0x80090000u;
        m.WriteU8((c.At + 0x7969u), (byte)c.V0);
        c.At = 0x80090000u;
        m.WriteU8((c.At + 0x796Au), (byte)c.V0);
        c.At = 0x80090000u;
        m.WriteU8((c.At + 0x796Bu), (byte)c.V0);
        c.At = 0x80090000u;
        m.WriteU8((c.At + 0x7970u), (byte)c.V0);
        c.At = 0x80090000u;
        m.WriteU8((c.At + 0x7971u), (byte)c.V0);
        c.A1 = 0u + 0u;
        c.RA = 0x8010055Cu;
        SoTN.AddToInventory(c, m);
        c.A0 = 0u | 0x0070u;
        c.A1 = 0u + 0u;
        c.RA = 0x80100568u;
        SoTN.AddToInventory(c, m);
        c.A0 = 0u | 0x0071u;
        c.A1 = 0u + 0u;
        c.RA = 0x80100574u;
        SoTN.AddToInventory(c, m);
        c.A0 = 0u | 0x0062u;
        c.A1 = 0u + 0u;
        c.RA = 0x80100580u;
        SoTN.AddToInventory(c, m);
        c.A0 = 0u | 0x0080u;
        c.A1 = 0u + 0u;
        c.RA = 0x8010058Cu;
        SoTN.AddToInventory(c, m);
        c.A0 = 0u | 0x0064u;
        c.A1 = 0u + 0u;
        c.RA = 0x80100598u;
        SoTN.AddToInventory(c, m);
        c.A0 = 0u | 0x0006u;
        c.A1 = 0u + 0u;
        c.RA = 0x801005A4u;
        SoTN.AddToInventory(c, m);
        c.A0 = 0u | 0x0007u;
        c.A1 = 0u + 0u;
        c.RA = 0x801005B0u;
        SoTN.AddToInventory(c, m);
        c.A0 = 0u | 0x0012u;
        c.A1 = 0u + 0u;
        c.RA = 0x801005BCu;
        SoTN.AddToInventory(c, m);
        c.A0 = 0u | 0x0017u;
        c.A1 = 0u + 0u;
        c.RA = 0x801005C8u;
        SoTN.AddToInventory(c, m);
        c.A0 = 0u | 0x0055u;
        c.A1 = 0u + 0u;
        c.RA = 0x801005D4u;
        SoTN.AddToInventory(c, m);
        c.A0 = 0u | 0x0058u;
        c.A1 = 0u + 0u;
        c.RA = 0x801005E0u;
        SoTN.AddToInventory(c, m);
        c.A0 = 0u | 0x0001u;
        c.A1 = 0u | 0x0002u;
        c.RA = 0x801005ECu;
        SoTN.AddToInventory(c, m);
        c.A0 = 0u | 0x0003u;
        c.A1 = 0u | 0x0002u;
        c.RA = 0x801005F8u;
        SoTN.AddToInventory(c, m);
        c.A0 = 0u | 0x0004u;
        c.A1 = 0u | 0x0002u;
        c.RA = 0x80100604u;
        SoTN.AddToInventory(c, m);
        c.A0 = 0u | 0x0005u;
        c.A1 = 0u | 0x0002u;
        c.RA = 0x80100610u;
        SoTN.AddToInventory(c, m);
        c.A0 = 0u | 0x0006u;
        c.A1 = 0u | 0x0002u;
        c.RA = 0x8010061Cu;
        SoTN.AddToInventory(c, m);
        c.A0 = 0u | 0x0007u;
        c.A1 = 0u | 0x0002u;
        c.RA = 0x80100628u;
        SoTN.AddToInventory(c, m);
        c.A0 = 0u | 0x000Au;
        c.A1 = 0u | 0x0002u;
        c.RA = 0x80100634u;
        SoTN.AddToInventory(c, m);
        c.A0 = 0u | 0x000Du;
        c.A1 = 0u | 0x0002u;
        c.RA = 0x80100640u;
        SoTN.AddToInventory(c, m);
        c.A0 = 0u | 0x001Fu;
        c.A1 = 0u | 0x0001u;
        c.RA = 0x8010064Cu;
        SoTN.AddToInventory(c, m);
        c.A0 = 0u | 0x0021u;
        c.A1 = 0u | 0x0001u;
        c.RA = 0x80100658u;
        SoTN.AddToInventory(c, m);
        c.A0 = 0u | 0x0023u;
        c.A1 = 0u | 0x0001u;
        c.RA = 0x80100664u;
        SoTN.AddToInventory(c, m);
        c.A0 = 0u | 0x0031u;
        c.A1 = 0u | 0x0003u;
        c.RA = 0x80100670u;
        SoTN.AddToInventory(c, m);
        c.A0 = 0u | 0x0033u;
        c.A1 = 0u | 0x0003u;
        c.RA = 0x8010067Cu;
        SoTN.AddToInventory(c, m);
        c.A0 = 0u | 0x0035u;
        c.A1 = 0u | 0x0003u;
        c.RA = 0x80100688u;
        SoTN.AddToInventory(c, m);
        c.A0 = 0u | 0x0032u;
        c.A1 = 0u | 0x0003u;
        c.RA = 0x80100694u;
        SoTN.AddToInventory(c, m);
        c.A0 = 0u | 0x0052u;
        c.A1 = 0u | 0x0004u;
        c.RA = 0x801006A0u;
        SoTN.AddToInventory(c, m);
        c.A0 = 0u | 0x004Fu;
        c.A1 = 0u | 0x0004u;
        c.RA = 0x801006ACu;
        SoTN.AddToInventory(c, m);
        c.S0 = 0u + 0u;
        c.A0 = 0u | 0x009Fu;
    L801006B4:;
        c.A1 = 0u + 0u;
        c.RA = 0x801006BCu;
        SoTN.AddToInventory(c, m);
        c.S0 = c.S0 + 0x1u;
        c.V0 = (int)c.S0 < 80 ? 1u : 0u;
        if (c.V0 != 0u)
        {
            c.A0 = 0u | 0x009Fu;
            goto L801006B4;
        }
        c.A0 = 0u | 0x009Fu;
        c.S0 = 0u + 0u;
        c.A0 = 0u | 0x0019u;
    L801006D4:;
        c.A1 = 0u + 0u;
        c.RA = 0x801006DCu;
        SoTN.AddToInventory(c, m);
        c.A0 = 0u | 0x0045u;
        c.A1 = 0u + 0u;
        c.RA = 0x801006E8u;
        SoTN.AddToInventory(c, m);
        c.A0 = 0u | 0x0043u;
        c.A1 = 0u + 0u;
        c.RA = 0x801006F4u;
        SoTN.AddToInventory(c, m);
        c.A0 = 0u | 0x0090u;
        c.A1 = 0u + 0u;
        c.RA = 0x80100700u;
        SoTN.AddToInventory(c, m);
        c.A0 = 0u | 0x0051u;
        c.A1 = 0u + 0u;
        c.RA = 0x8010070Cu;
        SoTN.AddToInventory(c, m);
        c.A0 = 0u | 0x0052u;
        c.A1 = 0u + 0u;
        c.RA = 0x80100718u;
        SoTN.AddToInventory(c, m);
        c.A0 = 0u | 0x0049u;
        c.A1 = 0u + 0u;
        c.RA = 0x80100724u;
        SoTN.AddToInventory(c, m);
        c.S0 = c.S0 + 0x1u;
        c.V0 = (int)c.S0 < 10 ? 1u : 0u;
        if (c.V0 != 0u)
        {
            c.A0 = 0u | 0x0019u;
            goto L801006D4;
        }
        c.A0 = 0u | 0x0019u;
    L80100734:;
        c.RA = 0x8010073Cu;
        SoTN.func_800F53A4(c, m);
    L8010073C:;
        c.RA = m.ReadU32((c.SP + 0x14u));
        c.S0 = m.ReadU32((c.SP + 0x10u));
        c.SP = c.SP + 0x18u;
        return;
    }

    // Darkwing Bat
    public static void func_801AC7CC_rnz1(CpuContext c, IMemory m)
    {
        // Dark Wing Patch to allow Relic that shows up after he's dead to be changed.
        // We may also need to patch the "Blue Swirl" so that an Item can show up?
        c.SP = c.SP - 0x28u;
        m.WriteU32((c.SP + 0x1Cu), c.S3);
        c.S3 = c.A0 + 0u;
        m.WriteU32((c.SP + 0x20u), c.RA);
        m.WriteU32((c.SP + 0x18u), c.S2);
        m.WriteU32((c.SP + 0x14u), c.S1);
        m.WriteU32((c.SP + 0x10u), c.S0);
        c.V1 = m.ReadU16((c.S3 + 0x2Cu));
        c.V0 = c.V1 < 0x00000008u ? 1u : 0u;
        if (c.V0 == 0u)
        {
            c.V0 = c.V1 << 2;
            goto L801ACB8C;
        }
        c.V0 = c.V1 << 2;
        c.At = 0x801A0000u;
        c.At = c.At + c.V0;
        c.V0 = m.ReadU32((c.At + 0x6090u));
        switch (c.V0)
        {
            case 0x801AC814u: goto L801AC814;
            case 0x801AC8ECu: goto L801AC8EC;
            case 0x801AC940u: goto L801AC940;
            case 0x801AC9C8u: goto L801AC9C8;
            case 0x801ACA1Cu: goto L801ACA1C;
            case 0x801ACA68u: goto L801ACA68;
            case 0x801ACAC0u: goto L801ACAC0;
            case 0x801ACB40u: goto L801ACB40;
            default: Dispatcher.Call(c, m, c.V0); return;
        }
    L801AC814:;
        c.A0 = 0x80180000u;
        c.A0 = c.A0 + 0xB40u;
        c.RA = 0x801AC824u;
        SoTN.func_801B0FC8(c, m);
        c.A0 = 0u | 0x0014u;
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x37C0u));
        c.A1 = 0u + 0u;
        c.RA = 0x801AC83Cu;
        Dispatcher.Call(c, m, c.V0);
        if (c.V0 == 0u)
        {
            c.V0 = 0u | 0x000Bu;
            goto L801AC874;
        }
        // For Is Already Dead
        //c.V0 = 0u | 0x000Bu;
        c.V0 = m.ReadU16(0x801AC840);       // Entity Id
        m.WriteU16((c.S3 + 0x26u), (ushort)c.V0);
        //c.V0 = 0x801B0000u;
        c.V0 = m.ReadU16(0x801AC848);       // Update Func Pointer upper 16-bits
        c.V0 = c.V0 << 16;
        //c.V0 = c.V0 + 0x2F84u;            // Entity Func Pointer Lower 16-bits
        c.V0 = c.V0 + m.ReadU16(0x801AC84C);
        m.WriteU32((c.S3 + 0x28u), c.V0);
        c.V0 = 0u | 0x0010u;
        m.WriteU8((c.S3 + 0x6Du), (byte)c.V0);
        //c.V0 = 0u | 0x001Cu;	            // Ring of Vlad Relic
        c.V0 = 0u | m.ReadU16(0x801AC85C);  // Read updated Relic Id / Sub-type
        m.WriteU16((c.S3 + 0x52u), (ushort)0u);
        m.WriteU16((c.S3 + 0x50u), (ushort)0u);
        m.WriteU16((c.S3 + 0x30u), (ushort)c.V0);
        m.WriteU16((c.S3 + 0x2Cu), (ushort)0u);
        goto L801ACB8C;
    L801AC874:;
        c.S0 = 0x80070000u;
        c.S0 = c.S0 + 0x6DDCu;
        c.A0 = 0u | 0x002Fu;
        c.A1 = c.S0 + 0u;
        c.RA = 0x801AC888u;
        SoTN.func_801AF518(c, m);
        c.A1 = c.S0 + 0xBCu;
        c.S2 = 0x80070000u;
        c.S2 = c.S2 + 0x308Eu;
        c.V0 = m.ReadU16(c.S2);
        c.S1 = 0u | 0x0080u;
        c.V0 = c.S1 - c.V0;
        c.At = 0x80070000u;
        m.WriteU16((c.At + 0x6DDEu), (ushort)c.V0);
        c.V0 = 0x80070000u;
        c.V0 = m.ReadU16((c.V0 + 0x3092u));
        c.S0 = 0u | 0x0078u;
        c.V0 = c.S0 - c.V0;
        c.At = 0x80070000u;
        m.WriteU16((c.At + 0x6DE2u), (ushort)c.V0);
        c.A0 = 0u | 0x002Eu;
        c.RA = 0x801AC8C8u;
        SoTN.func_801AF518(c, m);
        c.V0 = m.ReadU16(c.S2);
        c.V1 = 0x80070000u;
        c.V1 = m.ReadU16((c.V1 + 0x3092u));
        c.S1 = c.S1 - c.V0;
        c.S0 = c.S0 - c.V1;
        c.At = 0x80070000u;
        m.WriteU16((c.At + 0x6E9Au), (ushort)c.S1);
        c.At = 0x80070000u;
        m.WriteU16((c.At + 0x6E9Eu), (ushort)c.S0);
    L801AC8EC:;
        c.V0 = 0x80070000u;
        c.V0 = (uint)(short)m.ReadU16((c.V0 + 0x33DAu));
        c.V1 = 0x80070000u;
        c.V1 = (uint)(short)m.ReadU16((c.V1 + 0x308Eu));
        c.V0 = c.V0 + c.V1;
        c.V0 = c.V0 - 0x31u;
        c.V0 = c.V0 < 0x0000009Fu ? 1u : 0u;
        if (c.V0 == 0u)
        {
            c.A0 = 0u | 0x0014u;
            goto L801ACB8C;
        }
        c.A0 = 0u | 0x0014u;
        c.V0 = 0x80180000u;
        c.V0 = m.ReadU32((c.V0 + 0x1308u));
        c.V1 = 0x80040000u;
        c.V1 = m.ReadU32((c.V1 - 0x37C0u));
        c.V0 = c.V0 | 0x0001u;
        c.At = 0x80180000u;
        m.WriteU32((c.At + 0x1308u), c.V0);
        c.A1 = 0u | 0x0002u;
        c.RA = 0x801AC938u;
        Dispatcher.Call(c, m, c.V1);
        goto L801ACB7C;
    L801AC940:;
        c.A0 = 0u | 0x0034u;
        c.A1 = c.S3 + 0xBCu;
        c.RA = 0x801AC94Cu;
        SoTN.func_801AF518(c, m);
        c.A0 = 0u | 0x0034u;
        c.S1 = 0x80070000u;
        c.S1 = c.S1 + 0x308Eu;
        c.V0 = 0xFFFFFFF8u;
        c.V1 = m.ReadU16(c.S1);
        c.A1 = c.S3 + 0x178u;
        c.V0 = c.V0 - c.V1;
        m.WriteU16((c.S3 + 0xBEu), (ushort)c.V0);
        c.V0 = 0x80070000u;
        c.V0 = m.ReadU16((c.V0 + 0x3092u));
        c.S0 = 0u | 0x0080u;
        m.WriteU16((c.S3 + 0xECu), (ushort)0u);
        c.V0 = c.S0 - c.V0;
        m.WriteU16((c.S3 + 0xC2u), (ushort)c.V0);
        c.RA = 0x801AC988u;
        SoTN.func_801AF518(c, m);
        c.V0 = 0u | 0x0001u;
        c.At = 0x80180000u;
        m.WriteU32((c.At + 0x1304u), c.V0);
        c.V0 = 0u | 0x0108u;
        c.A0 = m.ReadU16((c.S3 + 0x2Cu));
        c.V1 = m.ReadU16(c.S1);
        c.A0 = c.A0 + 0x1u;
        c.V0 = c.V0 - c.V1;
        m.WriteU16((c.S3 + 0x17Au), (ushort)c.V0);
        c.V1 = 0x80070000u;
        c.V1 = m.ReadU16((c.V1 + 0x3092u));
        c.V0 = 0u | 0x0001u;
        m.WriteU16((c.S3 + 0x1A8u), (ushort)c.V0);
        m.WriteU16((c.S3 + 0x2Cu), (ushort)c.A0);
        c.S0 = c.S0 - c.V1;
        m.WriteU16((c.S3 + 0x17Eu), (ushort)c.S0);
    L801AC9C8:;
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x3808u));
        c.RA = 0x801AC9DCu;
        Dispatcher.Call(c, m, c.V0);
        if (c.V0 == 0u)
        {
            c.V0 = 0u | 0x0001u;
            goto L801AC9FC;
        }
        c.V0 = 0u | 0x0001u;
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x3824u));
        c.A0 = 0u | 0x0090u;
        c.RA = 0x801AC9F8u;
        Dispatcher.Call(c, m, c.V0);
        c.V0 = 0u | 0x0001u;
    L801AC9FC:;
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7928u), c.V0);
        c.V0 = m.ReadU16((c.S3 + 0x2Cu));
        //c.V1 = 0u | 0x031Du;
        c.V1 = 0u | m.ReadU16(0x801ACA08);
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7910u), c.V1);
        c.V0 = c.V0 + 0x1u;
        goto L801ACB88;
    L801ACA1C:;
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x3808u));
        c.RA = 0x801ACA30u;
        Dispatcher.Call(c, m, c.V0);
        if (c.V0 != 0u)
        {
            goto L801ACA68;
        }
        c.A0 = 0x80090000u;
        c.A0 = m.ReadU32((c.A0 + 0x7910u));
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x3824u));
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7928u), 0u);
        c.RA = 0x801ACA58u;
        Dispatcher.Call(c, m, c.V0);
        c.V0 = m.ReadU16((c.S3 + 0x2Cu));
        c.V0 = c.V0 + 0x1u;
        m.WriteU16((c.S3 + 0x2Cu), (ushort)c.V0);
    L801ACA68:;
        c.V0 = 0x80180000u;
        c.V0 = m.ReadU32((c.V0 + 0x1308u));
        c.V0 = c.V0 & 0x0002u;
        if (c.V0 == 0u)
        {
            c.A0 = 0u | 0x0014u;
            goto L801ACB8C;
        }
        c.A0 = 0u | 0x0014u;
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x37C0u));
        c.A1 = 0u | 0x0001u;
        c.RA = 0x801ACA94u;
        Dispatcher.Call(c, m, c.V0);
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x3824u));
        c.A0 = 0u | 0x0090u;
        c.RA = 0x801ACAA8u;
        Dispatcher.Call(c, m, c.V0);
        c.V0 = m.ReadU16((c.S3 + 0x2Cu));
        //c.V1 = 0u | 0x0338u;
        c.V1 = 0u | m.ReadU16(0x801ACAAC);
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7910u), c.V1);
        c.V0 = c.V0 + 0x1u;
        goto L801ACB88;
    L801ACAC0:;
        c.V0 = 0x80180000u;
        c.V0 = m.ReadU32((c.V0 + 0x1308u));
        c.V0 = c.V0 & 0x0004u;
        if (c.V0 == 0u)
        {
            goto L801ACB8C;
        }
        c.A0 = 0x80080000u;
        c.A0 = c.A0 - 0x56A8u;
        c.A1 = c.A0 + 0x1780u;
        c.RA = 0x801ACAE8u;
        SoTN.func_801B0B28(c, m);
        c.S0 = c.V0 + 0u;
        if (c.S0 == 0u)
        {
            c.A0 = 0u | 0x0035u;
            goto L801ACB8C;
        }
        c.A0 = 0u | 0x0035u;
        c.A1 = c.S3 + 0u;
        c.A2 = c.S0 + 0u;
        c.RA = 0x801ACB00u;
        SoTN.func_801AF58C(c, m);
        c.V0 = 0u | 0x0080u;
        m.WriteU16((c.S0 + 0x2u), (ushort)c.V0);
        m.WriteU16((c.S0 + 0x6u), (ushort)c.V0);
        // c.V0 = 0u | 0x0014u;
        c.V0 = 0u | m.ReadU16(0x801ACB0C);
        m.WriteU16((c.S0 + 0x30u), (ushort)c.V0);
        c.V0 = 0u | 0x0001u;
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7928u), c.V0);
        c.V0 = m.ReadU16((c.S3 + 0x2Cu));
        //c.V1 = 0u | 0x0338u;
        c.V1 = 0u | m.ReadU16(0x801ACB24);
        c.At = 0x80180000u;
        m.WriteU32((c.At + 0x1304u), 0u);
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7910u), c.V1);
        c.V0 = c.V0 + 0x1u;
        goto L801ACB88;
    L801ACB40:;
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x3808u));
        c.RA = 0x801ACB54u;
        Dispatcher.Call(c, m, c.V0);
        if (c.V0 != 0u)
        {
            goto L801ACB8C;
        }
        c.A0 = 0x80090000u;
        c.A0 = m.ReadU32((c.A0 + 0x7910u));
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x3824u));
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7928u), 0u);
        c.RA = 0x801ACB7Cu;
        Dispatcher.Call(c, m, c.V0);
    L801ACB7C:;
        c.V0 = m.ReadU16((c.S3 + 0x2Cu));
        c.V0 = c.V0 + 0x1u;
    L801ACB88:;
        m.WriteU16((c.S3 + 0x2Cu), (ushort)c.V0);
    L801ACB8C:;
        c.RA = m.ReadU32((c.SP + 0x20u));
        c.S3 = m.ReadU32((c.SP + 0x1Cu));
        c.S2 = m.ReadU32((c.SP + 0x18u));
        c.S1 = m.ReadU32((c.SP + 0x14u));
        c.S0 = m.ReadU32((c.SP + 0x10u));
        c.SP = c.SP + 0x28u;
        return;
    }

    // Dark Wing Item Spawn
    public static void func_801BE578(CpuContext c, IMemory m)
    {
        c.SP = c.SP - 0xE8u;
        m.WriteU32((c.SP + 0xC8u), c.S2);
        c.S2 = c.A0 + 0u;
        m.WriteU32((c.SP + 0xE4u), c.RA);
        m.WriteU32((c.SP + 0xE0u), c.FP);
        m.WriteU32((c.SP + 0xDCu), c.S7);
        m.WriteU32((c.SP + 0xD8u), c.S6);
        m.WriteU32((c.SP + 0xD4u), c.S5);
        m.WriteU32((c.SP + 0xD0u), c.S4);
        m.WriteU32((c.SP + 0xCCu), c.S3);
        m.WriteU32((c.SP + 0xC4u), c.S1);
        m.WriteU32((c.SP + 0xC0u), c.S0);
        c.V1 = m.ReadU16((c.S2 + 0x2Cu));
        c.V0 = c.V1 < 0x00000007u ? 1u : 0u;
        if (c.V0 == 0u)
        {
            c.V0 = c.V1 << 2;
            goto L801BEDF8;
        }
        c.V0 = c.V1 << 2;
        c.At = 0x801A0000u;
        c.At = c.At + c.V0;
        c.V0 = m.ReadU32((c.At + 0x62B0u));
        switch (c.V0)
        {
            case 0x801BE5D4u: goto L801BE5D4;
            case 0x801BE83Cu: goto L801BE83C;
            case 0x801BE888u: goto L801BE888;
            case 0x801BEC28u: goto L801BEC28;
            case 0x801BEBA0u: goto L801BEBA0;
            case 0x801BECB8u: goto L801BECB8;
            case 0x801BECE8u: goto L801BECE8;
            default: Dispatcher.Call(c, m, c.V0); return;
        }
    L801BE5D4:;
        c.A0 = 0x80180000u;
        c.A0 = c.A0 + 0xB40u;
        c.RA = 0x801BE5E4u;
        SoTN.func_801B0FC8(c, m);
        c.A0 = 0u | 0x0004u;
        c.V0 = 0u | 0x0002u;
        m.WriteU16((c.S2 + 0x54u), (ushort)c.V0);
        m.WriteU16((c.S2 + 0x56u), (ushort)0u);
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x3820u));
        c.A1 = 0u | 0x0181u;
        c.RA = 0x801BE608u;
        Dispatcher.Call(c, m, c.V0);
        c.V0 = c.V0 << 16;
        c.A0 = (uint)((int)c.V0 >> 16);
        c.V0 = 0xFFFFFFFFu;
        if (c.A0 != c.V0)
        {
            c.V0 = c.A0 << 1;
            goto L801BE628;
        }
        c.V0 = c.A0 << 1;
        c.V0 = 0u | 0x0006u;
        m.WriteU16((c.S2 + 0x2Cu), (ushort)c.V0);
        goto L801BEDF8;
    L801BE628:;
        c.V0 = c.V0 + c.A0;
        c.V0 = c.V0 << 2;
        c.V0 = c.V0 + c.A0;
        c.V0 = c.V0 << 2;
        c.V1 = 0x80080000u;
        c.V1 = c.V1 + 0x6FECu;
        c.S1 = c.V0 + c.V1;
        c.V0 = m.ReadU32((c.S2 + 0x34u));
        c.V1 = 0x00800000u;
        m.WriteU32((c.S2 + 0x64u), c.A0);
        m.WriteU32((c.S2 + 0x80u), c.S1);
        c.V0 = c.V0 | c.V1;
        m.WriteU32((c.S2 + 0x34u), c.V0);
        c.V0 = 0u | 0x001Au;
        m.WriteU16((c.S1 + 0x1Au), (ushort)c.V0);
        c.V0 = 0u | 0x019Fu;
        m.WriteU16((c.S1 + 0xEu), (ushort)c.V0);
        c.V0 = 0u | 0x003Fu;
        m.WriteU8((c.S1 + 0x30u), (byte)c.V0);
        m.WriteU8((c.S1 + 0x18u), (byte)c.V0);
        c.V0 = 0u | 0x00C0u;
        m.WriteU8((c.S1 + 0x19u), (byte)c.V0);
        m.WriteU8((c.S1 + 0xDu), (byte)c.V0);
        c.V0 = 0u | 0x00FFu;
        m.WriteU8((c.S1 + 0x24u), (byte)0u);
        m.WriteU8((c.S1 + 0xCu), (byte)0u);
        m.WriteU8((c.S1 + 0x31u), (byte)c.V0);
        m.WriteU8((c.S1 + 0x25u), (byte)c.V0);
        c.V0 = m.ReadU16((c.S2 + 0x2u));
        c.S0 = 0u + 0u;
        m.WriteU16((c.S1 + 0x2Cu), (ushort)c.V0);
        m.WriteU16((c.S1 + 0x20u), (ushort)c.V0);
        m.WriteU16((c.S1 + 0x14u), (ushort)c.V0);
        m.WriteU16((c.S1 + 0x8u), (ushort)c.V0);
        c.V1 = m.ReadU16((c.S2 + 0x2u));
        c.V0 = 0u | 0x00C0u;
        m.WriteU16((c.S1 + 0x26u), (ushort)c.V0);
        c.V0 = 0u | 0x0033u;
        m.WriteU16((c.S1 + 0x32u), (ushort)c.V0);
        m.WriteU16((c.S1 + 0x2Eu), (ushort)c.V1);
        m.WriteU16((c.S1 + 0x22u), (ushort)c.V1);
        m.WriteU16((c.S1 + 0x16u), (ushort)c.V1);
        m.WriteU16((c.S1 + 0xAu), (ushort)c.V1);
        c.S1 = m.ReadU32(c.S1);
        c.S3 = 0u | 0x0020u;
        m.WriteU32((c.S2 + 0x7Cu), c.S1);
        c.S6 = 0u + 0u;
    L801BE6E4:;
        c.S2 = 0u + 0u;
    L801BE6E8:;
        c.S5 = 0u + 0u;
        c.S4 = c.S2 + 0u;
    L801BE6F0:;
        c.A0 = c.S1 + 0u;
        c.RA = 0x801BE6F8u;
        SoTN.func_801BE494(c, m);
        c.V1 = m.ReadU32(c.S1);
        c.V0 = 0u | 0x001Au;
        m.WriteU16((c.S1 + 0x1Au), (ushort)c.V0);
        c.V0 = 0u | 0x0194u;
        m.WriteU16((c.S1 + 0xEu), (ushort)c.V0);
        c.V0 = 0u | 0x0010u;
        m.WriteU8((c.S1 + 0x30u), (byte)c.V0);
        m.WriteU8((c.S1 + 0x18u), (byte)c.V0);
        c.V0 = 0u | 0x0050u;
        m.WriteU8((c.S1 + 0x19u), (byte)c.V0);
        m.WriteU8((c.S1 + 0xDu), (byte)c.V0);
        c.V0 = 0u | 0x0060u;
        m.WriteU8((c.S1 + 0x31u), (byte)c.V0);
        m.WriteU8((c.S1 + 0x25u), (byte)c.V0);
        c.V0 = 0u | 0x1000u;
        m.WriteU8((c.S1 + 0x24u), (byte)0u);
        m.WriteU8((c.S1 + 0xCu), (byte)0u);
        m.WriteU8((c.S1 + 0x28u), (byte)c.S3);
        m.WriteU8((c.S1 + 0x1Cu), (byte)c.S3);
        m.WriteU8((c.S1 + 0x10u), (byte)c.S3);
        m.WriteU8((c.S1 + 0x4u), (byte)c.S3);
        m.WriteU8((c.S1 + 0x29u), (byte)0u);
        m.WriteU8((c.S1 + 0x1Du), (byte)0u);
        m.WriteU8((c.S1 + 0x11u), (byte)0u);
        m.WriteU8((c.S1 + 0x5u), (byte)0u);
        m.WriteU8((c.S1 + 0x2Au), (byte)0u);
        m.WriteU8((c.S1 + 0x1Eu), (byte)0u);
        m.WriteU8((c.S1 + 0x12u), (byte)0u);
        m.WriteU8((c.S1 + 0x6u), (byte)0u);
        m.WriteU16((c.V1 + 0x22u), (ushort)c.V0);
        m.WriteU16((c.V1 + 0x20u), (ushort)c.V0);
        c.V1 = m.ReadU32(c.S1);
        c.V0 = c.S5 << 9;
        m.WriteU16((c.V1 + 0x1Au), (ushort)c.V0);
        c.V0 = m.ReadU32(c.S1);
        m.WriteU16((c.V0 + 0x2Cu), (ushort)0u);
        c.V0 = m.ReadU32(c.S1);
        m.WriteU16((c.V0 + 0x2Eu), (ushort)c.S4);
        c.V1 = m.ReadU32(c.S1);
        c.V0 = 0xFFFB0000u;
        m.WriteU32((c.V1 + 0xCu), c.V0);
        c.V0 = m.ReadU32(c.S1);
        m.WriteU32((c.V0 + 0x10u), 0u);
        c.V1 = m.ReadU32(c.S1);
        c.V0 = 0u | 0x0080u;
        m.WriteU16((c.V1 + 0x14u), (ushort)c.V0);
        c.V0 = m.ReadU32(c.S1);
        m.WriteU16((c.V0 + 0xAu), (ushort)0u);
        c.V0 = 0u | 0x00C0u;
        m.WriteU16((c.S1 + 0x26u), (ushort)c.V0);
        c.V0 = 0u | 0x0073u;
        m.WriteU16((c.S1 + 0x32u), (ushort)c.V0);
        c.S1 = m.ReadU32(c.S1);
        c.V0 = m.ReadU16((c.S1 + 0x32u));
        c.S5 = c.S5 + 0x1u;
        c.V0 = c.V0 & 0xFFFDu;
        m.WriteU16((c.S1 + 0x32u), (ushort)c.V0);
        c.V0 = (int)c.S5 < 8 ? 1u : 0u;
        c.S1 = m.ReadU32(c.S1);
        if (c.V0 != 0u)
        {
            goto L801BE6F0;
        }
        c.S6 = c.S6 + 0x1u;
        c.V0 = (int)c.S6 < 3 ? 1u : 0u;
        if (c.V0 != 0u)
        {
            c.S2 = c.S2 + 0x540u;
            goto L801BE6E8;
        }
        c.S2 = c.S2 + 0x540u;
        c.S0 = c.S0 + 0x1u;
        c.V0 = (int)c.S0 < 8 ? 1u : 0u;
        if (c.V0 != 0u)
        {
            c.S6 = 0u + 0u;
            goto L801BE6E4;
        }
        c.S6 = 0u + 0u;
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x3824u));
        c.A0 = 0u | 0x07D2u;
        c.RA = 0x801BE834u;
        Dispatcher.Call(c, m, c.V0);
        goto L801BEDF8;
    L801BE83C:;
        c.V0 = (uint)(short)m.ReadU16((c.S2 + 0x86u));
        if (c.V0 != 0u)
        {
            c.V1 = 0u | 0x0002u;
            goto L801BE85C;
        }
        c.V1 = 0u | 0x0002u;
        c.V0 = m.ReadU16((c.S2 + 0x88u));
        m.WriteU16((c.S2 + 0x86u), (ushort)c.V1);
        c.V0 = c.V0 + 0x1u;
        m.WriteU16((c.S2 + 0x88u), (ushort)c.V0);
    L801BE85C:;
        c.V0 = m.ReadU16((c.S2 + 0x86u));
        c.V1 = (uint)(short)m.ReadU16((c.S2 + 0x88u));
        c.V0 = c.V0 - 0x1u;
        c.V1 = (int)c.V1 < 8 ? 1u : 0u;
        if (c.V1 != 0u)
        {
            m.WriteU16((c.S2 + 0x86u), (ushort)c.V0);
            goto L801BE888;
        }
        m.WriteU16((c.S2 + 0x86u), (ushort)c.V0);
        c.V0 = m.ReadU16((c.S2 + 0x2Cu));
        c.V1 = 0u | 0x0007u;
        m.WriteU16((c.S2 + 0x88u), (ushort)c.V1);
        c.V0 = c.V0 + 0x1u;
        m.WriteU16((c.S2 + 0x2Cu), (ushort)c.V0);
    L801BE888:;
        c.A0 = 0u | 0x0200u;
        c.RA = 0x801BE890u;
        SoTN.SetGeomScreen(c, m);
        c.A0 = (uint)(short)m.ReadU16((c.S2 + 0x2u));
        c.A1 = (uint)(short)m.ReadU16((c.S2 + 0x6u));
        c.S6 = 0u + 0u;
        m.WriteU32((c.SP + 0xA0u), 0u);
        c.RA = 0x801BE8A4u;
        SoTN.SetGeomOffset(c, m);
        c.V0 = (uint)(short)m.ReadU16((c.S2 + 0x88u));
        c.S1 = m.ReadU32((c.S2 + 0x7Cu));
        c.V0 = c.V0 + 0x1u;
        if ((int)c.V0 <= 0)
        {
            c.T0 = c.SP + 0x60u;
            goto L801BEB68;
        }
        c.T0 = c.SP + 0x60u;
        c.S3 = c.SP + 0x70u;
        m.WriteU32((c.SP + 0xA8u), c.T0);
    L801BE8C0:;
        c.V1 = m.ReadU32(c.S1);
        c.V0 = (uint)(short)m.ReadU16((c.V1 + 0x14u));
        c.A0 = (uint)(short)m.ReadU16((c.V1 + 0x16u));
        c.A1 = m.ReadU32((c.V1 + 0xCu));
        c.V0 = c.V0 << 16;
        c.S4 = c.A0 + c.V0;
        c.S4 = c.S4 + c.A1;
        m.WriteU16((c.V1 + 0x16u), (ushort)c.S4);
        c.V0 = m.ReadU32(c.S1);
        c.V1 = (uint)((int)c.S4 >> 16);
        m.WriteU16((c.V0 + 0x14u), (ushort)c.V1);
        c.A1 = m.ReadU32(c.S1);
        c.A0 = m.ReadU32((c.A1 + 0xCu));
        c.V0 = (int)c.A0 < -16384 ? 1u : 0u;
        if (c.V0 == 0u)
        {
            c.S4 = c.V1 + 0u;
            goto L801BE914;
        }
        c.S4 = c.V1 + 0u;
        c.V0 = c.A0 + 0x3800u;
        m.WriteU32((c.A1 + 0xCu), c.V0);
    L801BE914:;
        c.V0 = m.ReadU32(c.S1);
        c.V0 = (uint)(short)m.ReadU16((c.V0 + 0x14u));
        c.V0 = (int)c.V0 < 8 ? 1u : 0u;
        if (c.V0 == 0u)
        {
            c.V1 = 0u | 0x0008u;
            goto L801BE968;
        }
        c.V1 = 0u | 0x0008u;
        c.T0 = m.ReadU32((c.SP + 0xA0u));
        c.T0 = c.T0 + 0x1u;
        m.WriteU32((c.SP + 0xA0u), c.T0);
        c.V0 = m.ReadU16((c.S2 + 0x84u));
        c.S0 = 0u | 0x002Fu;
        c.V0 = c.V0 + 0x4u;
        m.WriteU16((c.S2 + 0x84u), (ushort)c.V0);
    L801BE950:;
        m.WriteU16((c.S1 + 0x32u), (ushort)c.V1);
        c.S0 = c.S0 - 0x1u;
        if ((int)c.S0 >= 0)
        {
            c.S1 = c.S1 + 0x34u;
            goto L801BE950;
        }
        c.S1 = c.S1 + 0x34u;
        goto L801BEB50;
    L801BE968:;
        c.S5 = 0u + 0u;
        c.FP = c.SP + 0x98u;
        c.S7 = c.SP + 0x9Cu;
    L801BE974:;
        c.V0 = m.ReadU32(c.S1);
        c.V0 = m.ReadU16((c.V0 + 0x2Cu));
        m.WriteU16((c.SP + 0x58u), (ushort)c.V0);
        c.V0 = m.ReadU32(c.S1);
        c.V0 = m.ReadU16((c.V0 + 0x2Eu));
        m.WriteU16((c.SP + 0x5Au), (ushort)c.V0);
        c.V0 = m.ReadU32(c.S1);
        c.A0 = c.SP + 0x58u;
        c.V0 = m.ReadU16((c.V0 + 0x1Au));
        c.A1 = c.S3 + 0u;
        m.WriteU16((c.SP + 0x5Cu), (ushort)c.V0);
        c.RA = 0x801BE9B4u;
        SoTN.RotMatrix(c, m);
        c.A0 = c.S3 + 0u;
        c.A1 = m.ReadU32((c.SP + 0xA8u));
        c.V0 = 0u | 0x0200u;
        m.WriteU32((c.SP + 0x60u), 0u);
        m.WriteU32((c.SP + 0x64u), 0u);
        m.WriteU32((c.SP + 0x68u), c.V0);
        c.RA = 0x801BE9D0u;
        SoTN.TransMatrix(c, m);
        c.A0 = c.S3 + 0u;
        c.RA = 0x801BE9D8u;
        SoTN.SetRotMatrix(c, m);
        c.A0 = c.S3 + 0u;
        c.RA = 0x801BE9E0u;
        SoTN.SetTransMatrix(c, m);
        c.A0 = c.SP + 0x90u;
        c.A1 = c.SP + 0x50u;
        c.A2 = c.FP + 0u;
        c.A3 = c.S7 + 0u;
        m.WriteU16((c.SP + 0x90u), (ushort)c.S4);
        m.WriteU16((c.SP + 0x92u), (ushort)0u);
        m.WriteU16((c.SP + 0x94u), (ushort)0u);
        c.RA = 0x801BEA00u;
        SoTN.RotTransPers(c, m);
        c.A0 = 0x80180000u;
        c.A0 = c.A0 + 0x259Cu;
        c.A1 = c.S3 + 0u;
        c.S0 = c.V0 + 0u;
        c.RA = 0x801BEA14u;
        SoTN.RotMatrix(c, m);
        c.A0 = c.S3 + 0u;
        c.S0 = c.S0 << 16;
        c.V0 = (uint)(short)m.ReadU16((c.SP + 0x50u));
        c.V1 = (uint)(short)m.ReadU16((c.S2 + 0x2u));
        c.A1 = m.ReadU32((c.SP + 0xA8u));
        c.V0 = c.V0 - c.V1;
        m.WriteU32((c.SP + 0x60u), c.V0);
        c.V0 = (uint)(short)m.ReadU16((c.SP + 0x52u));
        c.V1 = (uint)(short)m.ReadU16((c.S2 + 0x6u));
        c.S0 = (uint)((int)c.S0 >> 14);
        m.WriteU32((c.SP + 0x68u), c.S0);
        c.V0 = c.V0 - c.V1;
        m.WriteU32((c.SP + 0x64u), c.V0);
        c.RA = 0x801BEA4Cu;
        SoTN.TransMatrix(c, m);
        c.V0 = m.ReadU32(c.S1);
        c.V0 = (uint)(short)m.ReadU16((c.V0 + 0x20u));
        c.A0 = c.S3 + 0u;
        m.WriteU32((c.SP + 0x60u), c.V0);
        c.V0 = m.ReadU32(c.S1);
        c.A1 = m.ReadU32((c.SP + 0xA8u));
        c.V1 = (uint)(short)m.ReadU16((c.V0 + 0x22u));
        c.V0 = 0u | 0x1000u;
        m.WriteU32((c.SP + 0x68u), c.V0);
        m.WriteU32((c.SP + 0x64u), c.V1);
        c.RA = 0x801BEA7Cu;
        SoTN.ScaleMatrix(c, m);
        c.A0 = c.S3 + 0u;
        c.RA = 0x801BEA84u;
        SoTN.SetRotMatrix(c, m);
        c.A0 = c.S3 + 0u;
        c.RA = 0x801BEA8Cu;
        SoTN.SetTransMatrix(c, m);
        c.A0 = 0x80180000u;
        c.A0 = c.A0 + 0x2550u;
        c.A1 = 0x80180000u;
        c.A1 = c.A1 + 0x2558u;
        c.A2 = 0x80180000u;
        c.A2 = c.A2 + 0x2560u;
        c.A3 = 0x80180000u;
        c.A3 = c.A3 + 0x2568u;
        c.V0 = c.S1 + 0x8u;
        m.WriteU32((c.SP + 0x10u), c.V0);
        c.V0 = c.S1 + 0x14u;
        m.WriteU32((c.SP + 0x14u), c.V0);
        c.V0 = c.S1 + 0x20u;
        m.WriteU32((c.SP + 0x18u), c.V0);
        c.V0 = c.S1 + 0x2Cu;
        m.WriteU32((c.SP + 0x1Cu), c.V0);
        m.WriteU32((c.SP + 0x20u), c.FP);
        m.WriteU32((c.SP + 0x24u), c.S7);
        c.RA = 0x801BEAD8u;
        SoTN.RotTransPers4(c, m);
        c.V1 = m.ReadU32(c.S1);
        c.V0 = m.ReadU16((c.V1 + 0x22u));
        c.V0 = c.V0 - 0x10u;
        m.WriteU16((c.V1 + 0x22u), (ushort)c.V0);
        m.WriteU16((c.V1 + 0x20u), (ushort)c.V0);
        c.V1 = m.ReadU32(c.S1);
        c.V0 = m.ReadU16((c.V1 + 0x1Au));
        c.V0 = c.V0 + 0x8u;
        m.WriteU16((c.V1 + 0x1Au), (ushort)c.V0);
        c.V1 = m.ReadU32(c.S1);
        c.V0 = m.ReadU16((c.V1 + 0x2Cu));
        c.V0 = c.V0 + 0x10u;
        m.WriteU16((c.V1 + 0x2Cu), (ushort)c.V0);
        c.V1 = m.ReadU32(c.S1);
        c.V0 = m.ReadU16((c.V1 + 0x2Eu));
        c.S5 = c.S5 + 0x1u;
        c.V0 = c.V0 + 0x20u;
        m.WriteU16((c.V1 + 0x2Eu), (ushort)c.V0);
        c.S1 = m.ReadU32(c.S1);
        c.V0 = (int)c.S5 < 24 ? 1u : 0u;
        c.S1 = m.ReadU32(c.S1);
        if (c.V0 != 0u)
        {
            goto L801BE974;
        }
    L801BEB50:;
        c.V0 = (uint)(short)m.ReadU16((c.S2 + 0x88u));
        c.S6 = c.S6 + 0x1u;
        c.V0 = c.V0 + 0x1u;
        c.V0 = (int)c.S6 < (int)c.V0 ? 1u : 0u;
        if (c.V0 != 0u)
        {
            goto L801BE8C0;
        }
    L801BEB68:;
        c.T0 = m.ReadU32((c.SP + 0xA0u));
        c.V0 = 0u | 0x0008u;
        if (c.T0 != c.V0)
        {
            goto L801BEB88;
        }
        c.V0 = m.ReadU16((c.S2 + 0x2Cu));
        c.V0 = c.V0 + 0x1u;
        m.WriteU16((c.S2 + 0x2Cu), (ushort)c.V0);
    L801BEB88:;
        c.V0 = (uint)(short)m.ReadU16((c.S2 + 0x84u));
        c.S1 = m.ReadU32((c.S2 + 0x80u));
        c.S4 = (uint)(short)m.ReadU16((c.S2 + 0x2u));
        m.WriteU32((c.SP + 0x98u), c.V0);
        c.V0 = (int)c.V0 < 257 ? 1u : 0u;
        goto L801BEC68;
    L801BEBA0:;
        c.RA = 0x801BEBA8u;
        SoTN.func_801B066C(c, m);
        c.A2 = c.SP + 0x28u;
        c.A3 = 0u + 0u;
        c.A0 = (uint)(short)m.ReadU16((c.S2 + 0x2u));
        c.V0 = m.ReadU32((c.S2 + 0xCu));
        c.A1 = m.ReadU16((c.S2 + 0x6u));
        c.V0 = c.V0 + 0x2000u;
        c.A1 = c.A1 + 0x4u;
        c.A1 = c.A1 << 16;
        m.WriteU32((c.S2 + 0xCu), c.V0);
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x3844u));
        c.A1 = (uint)((int)c.A1 >> 16);
        c.RA = 0x801BEBE0u;
        Dispatcher.Call(c, m, c.V0);
        c.V0 = m.ReadU32((c.SP + 0x28u));
        c.V0 = c.V0 & 0x0001u;
        if (c.V0 == 0u)
        {
            goto L801BEC28;
        }
        c.V0 = m.ReadU16((c.S2 + 0x6u));
        m.WriteU32((c.S2 + 0xCu), 0u);
        c.A0 = m.ReadU16((c.SP + 0x40u));
        c.V1 = m.ReadU16((c.S2 + 0x84u));
        c.V0 = c.V0 + c.A0;
        c.V1 = c.V1 - 0x1u;
        m.WriteU16((c.S2 + 0x84u), (ushort)c.V1);
        c.V1 = c.V1 << 16;
        if (c.V1 != 0u)
        {
            m.WriteU16((c.S2 + 0x6u), (ushort)c.V0);
            goto L801BEC28;
        }
        m.WriteU16((c.S2 + 0x6u), (ushort)c.V0);
        c.V0 = 0u | 0x0005u;
        m.WriteU16((c.S2 + 0x2Cu), (ushort)c.V0);
        goto L801BEDF8;
    L801BEC28:;
        c.V0 = (uint)(short)m.ReadU16((c.S2 + 0x84u));
        if ((int)c.V0 <= 0)
        {
            c.V1 = c.V0 + 0u;
            goto L801BEC44;
        }
        c.V1 = c.V0 + 0u;
        c.V0 = c.V1 - 0x20u;
        m.WriteU16((c.S2 + 0x84u), (ushort)c.V0);
        goto L801BEC54;
    L801BEC44:;
        c.V0 = 0u | 0x0010u;
        m.WriteU16((c.S2 + 0x84u), (ushort)c.V0);
        c.V0 = 0u | 0x0005u;
        m.WriteU16((c.S2 + 0x2Cu), (ushort)c.V0);
    L801BEC54:;
        c.V0 = (uint)(short)m.ReadU16((c.S2 + 0x84u));
        c.S1 = m.ReadU32((c.S2 + 0x80u));
        c.S4 = (uint)(short)m.ReadU16((c.S2 + 0x2u));
        m.WriteU32((c.SP + 0x98u), c.V0);
        c.V0 = (int)c.V0 < 225 ? 1u : 0u;
    L801BEC68:;
        if (c.V0 != 0u)
        {
            c.V0 = 0u | 0x00E0u;
            goto L801BEC74;
        }
        c.V0 = 0u | 0x00E0u;
        m.WriteU32((c.SP + 0x98u), c.V0);
    L801BEC74:;
        c.V0 = m.ReadU16((c.SP + 0x98u));
        c.V1 = c.S4 - c.V0;
        c.A0 = c.V0 + c.S4;
        m.WriteU16((c.S1 + 0x20u), (ushort)c.V1);
        m.WriteU16((c.S1 + 0x8u), (ushort)c.V1);
        m.WriteU16((c.S1 + 0x2Cu), (ushort)c.A0);
        m.WriteU16((c.S1 + 0x14u), (ushort)c.A0);
        c.S4 = (uint)(short)m.ReadU16((c.S2 + 0x6u));
        c.V1 = c.S4 - c.V0;
        c.V0 = c.V0 + c.S4;
        m.WriteU16((c.S1 + 0x16u), (ushort)c.V1);
        m.WriteU16((c.S1 + 0xAu), (ushort)c.V1);
        m.WriteU16((c.S1 + 0x2Eu), (ushort)c.V0);
        m.WriteU16((c.S1 + 0x22u), (ushort)c.V0);
        goto L801BEDF8;
    L801BECB8:;
        c.A0 = m.ReadU32((c.S2 + 0x64u));
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x384Cu));
        c.RA = 0x801BECD0u;
        Dispatcher.Call(c, m, c.V0);
        c.V1 = m.ReadU16((c.S2 + 0x2Cu));
        c.V0 = m.ReadU16((c.S2 + 0x6u));
        c.V1 = c.V1 + 0x1u;
        c.V0 = c.V0 - 0x4u;
        m.WriteU16((c.S2 + 0x6u), (ushort)c.V0);
        m.WriteU16((c.S2 + 0x2Cu), (ushort)c.V1);
    L801BECE8:;
        c.V1 = m.ReadU16((c.S2 + 0x30u));
        c.V0 = c.V1 < 0x00000011u ? 1u : 0u;
        if (c.V0 != 0u)
        {
            c.V0 = c.V1 << 1;
            goto L801BED84;
        }
        c.V0 = c.V1 << 1;
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x3660u));
        if (c.V0 == 0u)
        {
            c.V1 = 0u | 0x0017u;
            goto L801BED40;
        }
        c.V1 = 0u | 0x0017u;
        m.WriteU16((c.S2 + 0x30u), (ushort)c.V1);
        m.WriteU16((c.S2 + 0x30u), (ushort)c.V1);
        c.V1 = m.ReadU16((c.S2 + 0x30u));
        c.V0 = 0u | 0x0003u;
        m.WriteU16((c.S2 + 0x26u), (ushort)c.V0);
        c.V0 = 0x801B0000u;
        c.V0 = c.V0 + 0x1CD8u;
        m.WriteU32((c.S2 + 0x28u), c.V0);
        c.V0 = 0u | 0x0010u;
        m.WriteU16((c.S2 + 0x52u), (ushort)0u);
        m.WriteU16((c.S2 + 0x50u), (ushort)0u);
        goto L801BEDE8;
    L801BED40:;
        c.V0 = 0u | 0x000Bu;
        m.WriteU16((c.S2 + 0x26u), (ushort)c.V0);
        c.V0 = 0x801B0000u;
        c.V0 = c.V0 + 0x2F84u;
        m.WriteU32((c.S2 + 0x28u), c.V0);
        c.V0 = m.ReadU16((c.S2 + 0x30u));
        c.V1 = 0u | 0x0010u;
        m.WriteU16((c.S2 + 0x52u), (ushort)0u);
        m.WriteU16((c.S2 + 0x50u), (ushort)0u);
        m.WriteU8((c.S2 + 0x6Du), (byte)c.V1);
        c.V0 = c.V0 << 1;
        c.At = 0x80180000u;
        c.At = c.At + c.V0;
        c.V0 = m.ReadU16((c.At + 0x2570u));
        m.WriteU16((c.S2 + 0x2Cu), (ushort)0u);
        m.WriteU16((c.S2 + 0x30u), (ushort)c.V0);
        goto L801BEDF8;
    L801BED84:;
        c.At = 0x80180000u;
        c.At = c.At + c.V0;
        c.V0 = m.ReadU16((c.At + 0x2570u));
        c.V1 = c.V0 & 0x0FFFu;
        m.WriteU16((c.S2 + 0x30u), (ushort)c.V0);
        c.V0 = (int)c.V1 < 128 ? 1u : 0u;
        if (c.V0 == 0u)
        {
            c.V0 = 0u | 0x0003u;
            goto L801BEDC4;
        }
        c.V0 = 0u | 0x0003u;
        m.WriteU16((c.S2 + 0x26u), (ushort)c.V0);
        c.V0 = 0x801B0000u;
        c.V0 = c.V0 + 0x1CD8u;
        m.WriteU32((c.S2 + 0x28u), c.V0);
        m.WriteU16((c.S2 + 0x52u), (ushort)0u);
        m.WriteU16((c.S2 + 0x50u), (ushort)0u);
        goto L801BEDDC;
    L801BEDC4:;
        c.V1 = c.V1 - 0x80u;
        c.V0 = 0u | 0x000Au;
        m.WriteU16((c.S2 + 0x26u), (ushort)c.V0);
        c.V0 = 0x801B0000u;
        c.V0 = c.V0 + 0x26ECu;
        if (m.ReadU32(0x801AC84C) == 0x24423A54) // detecting Item patch
        {
            c.V0 = 0x801B3A54u;     // Change to Entity 0C Func Ptr
        }
        m.WriteU32((c.S2 + 0x28u), c.V0);
    L801BEDDC:;
        m.WriteU16((c.S2 + 0x30u), (ushort)c.V1);
        c.V1 = m.ReadU16((c.S2 + 0x30u));
        c.V0 = 0u | 0x0010u;
    L801BEDE8:;
        m.WriteU8((c.S2 + 0x6Du), (byte)c.V0);
        m.WriteU16((c.S2 + 0x2Cu), (ushort)0u);
        c.V1 = c.V1 | 0x8000u;
        if (m.ReadU32(0x801AC84C) == 0x24423A54) // detecting Item patch
        {
            c.V1 = 4u;  // Seems to use Equipment List Entry 4...?
        }
        m.WriteU16((c.S2 + 0x30u), (ushort)c.V1);
    L801BEDF8:;
        c.RA = m.ReadU32((c.SP + 0xE4u));
        c.FP = m.ReadU32((c.SP + 0xE0u));
        c.S7 = m.ReadU32((c.SP + 0xDCu));
        c.S6 = m.ReadU32((c.SP + 0xD8u));
        c.S5 = m.ReadU32((c.SP + 0xD4u));
        c.S4 = m.ReadU32((c.SP + 0xD0u));
        c.S3 = m.ReadU32((c.SP + 0xCCu));
        c.S2 = m.ReadU32((c.SP + 0xC8u));
        c.S1 = m.ReadU32((c.SP + 0xC4u));
        c.S0 = m.ReadU32((c.SP + 0xC0u));
        c.SP = c.SP + 0xE8u;
        return;
    }


    // Holy Glasses Location
    public static void EntityPlatform(CpuContext c, IMemory m)
    {
        // This is for Patching Holy Glasses to be another Item or No Item
        c.SP = c.SP - 0x28u;
        m.WriteU32((c.SP + 0x10u), c.S0);
        c.S0 = c.A0 + 0u;
        c.A1 = 0u | 0x0020u;
        c.A2 = 0u | 0x0011u;
        m.WriteU32((c.SP + 0x20u), c.RA);
        m.WriteU32((c.SP + 0x1Cu), c.S3);
        m.WriteU32((c.SP + 0x18u), c.S2);
        m.WriteU32((c.SP + 0x14u), c.S1);
        c.V0 = m.ReadU16((c.S0 + 0x6u));
        c.A3 = 0u | 0x0004u;
        c.V0 = c.V0 - 0x8u;
        m.WriteU16((c.S0 + 0x6u), (ushort)c.V0);
        c.RA = 0x8018F994u;
        SoTN.GetPlayerCollisionWith_cen(c, m);
        c.S3 = 0x80070000u;
        c.S3 = c.S3 + 0x3084u;
        c.S2 = 0x80070000u;
        c.S2 = c.S2 + 0x33D8u;
        c.A1 = c.V0 + 0u;
        c.V1 = 0x80070000u;
        c.V1 = m.ReadU16((c.V1 + 0x33DAu));
        c.A0 = 0x80070000u;
        c.A0 = m.ReadU16((c.A0 + 0x308Eu));
        c.V0 = m.ReadU16((c.S0 + 0x6u));
        c.V1 = c.V1 + c.A0;
        c.S1 = c.V1 + 0u;
        c.A0 = 0x80070000u;
        c.A0 = m.ReadU16((c.A0 + 0x3092u));
        c.V1 = m.ReadU16((c.S0 + 0x2Cu));
        c.V0 = c.V0 + c.A0;
        c.A0 = c.V0 + 0u;
        c.V0 = c.V1 < 0x0000000Au ? 1u : 0u;
        if (c.V0 == 0u)
        {
            c.V0 = c.V1 << 2;
            goto L8018FFE8;
        }
        c.V0 = c.V1 << 2;
        c.At = 0x80190000u;
        c.At = c.At + c.V0;
        c.V0 = m.ReadU32((c.At - 0x2B50u));
        switch (c.V0)
        {
            case 0x8018F9FCu: goto L8018F9FC;
            case 0x8018FAF4u: goto L8018FAF4;
            case 0x8018FBF4u: goto L8018FBF4;
            case 0x8018FCBCu: goto L8018FCBC;
            case 0x8018FD9Cu: goto L8018FD9C;
            case 0x8018FE60u: goto L8018FE60;
            case 0x8018FEC8u: goto L8018FEC8;
            case 0x8018FF0Cu: goto L8018FF0C;
            case 0x8018FFC0u: goto L8018FFC0;
            case 0x8018FFE8u: goto L8018FFE8;
            default: Dispatcher.Call(c, m, c.V0); return;
        }
    L8018F9FC:;
        c.A0 = 0u | 0x0004u;
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x3848u));
        c.A1 = 0u | 0x0001u;
        c.RA = 0x8018FA14u;
        Dispatcher.Call(c, m, c.V0);
        c.V0 = c.V0 << 16;
        c.S1 = (uint)((int)c.V0 >> 16);
        c.V0 = 0xFFFFFFFFu;
        if (c.S1 == c.V0)
        {
            goto L8018FFE8;
        }
        c.A0 = 0x80180000u;
        c.A0 = c.A0 + 0x434u;
        c.RA = 0x8018FA38u;
        SoTN.InitializeEntity_cen(c, m);
        c.V0 = 0xFFFF8002u;
        c.V1 = 0u | 0x0009u;
        m.WriteU16((c.S0 + 0x54u), (ushort)c.V0);
        c.V0 = 0u | 0x0080u;
        m.WriteU16((c.S0 + 0x56u), (ushort)c.V1);
        m.WriteU16((c.S0 + 0x24u), (ushort)c.V0);
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU8((c.V0 - 0x413Cu));
        if (c.V0 == 0u)
        {
            goto L8018FA68;
        }
        m.WriteU16((c.S0 + 0x2Cu), (ushort)c.V1);
    L8018FA68:;
        c.A0 = 0u + 0u;
        c.RA = 0x8018FA70u;
        SoTN.func_8018F8EC(c, m);
        c.V0 = c.S1 << 1;
        c.V0 = c.V0 + c.S1;
        c.V0 = c.V0 << 2;
        c.V0 = c.V0 + c.S1;
        c.V0 = c.V0 << 2;
        c.V1 = 0x80080000u;
        c.V1 = c.V1 + 0x6FECu;
        c.A1 = c.V0 + c.V1;
        c.V0 = m.ReadU32((c.S0 + 0x34u));
        c.V1 = 0x00800000u;
        m.WriteU32((c.S0 + 0x64u), c.S1);
        c.V0 = c.V0 | c.V1;
        m.WriteU32((c.S0 + 0x34u), c.V0);
        c.V0 = 0u | 0x000Fu;
        c.V1 = 0u | 0x0002u;
        m.WriteU16((c.A1 + 0x1Au), (ushort)c.V0);
        c.V0 = 0u | 0x00A0u;
        m.WriteU8((c.A1 + 0x24u), (byte)c.V0);
        m.WriteU8((c.A1 + 0xCu), (byte)c.V0);
        c.V0 = 0u | 0x00B0u;
        m.WriteU8((c.A1 + 0x30u), (byte)c.V0);
        m.WriteU8((c.A1 + 0x18u), (byte)c.V0);
        c.V0 = 0u | 0x00A1u;
        m.WriteU8((c.A1 + 0x19u), (byte)c.V0);
        m.WriteU8((c.A1 + 0xDu), (byte)c.V0);
        c.V0 = 0u | 0x00A7u;
        m.WriteU8((c.A1 + 0x31u), (byte)c.V0);
        m.WriteU8((c.A1 + 0x25u), (byte)c.V0);
        c.V0 = 0u | 0x007Fu;
        m.WriteU16((c.A1 + 0xEu), (ushort)c.V1);
        m.WriteU16((c.A1 + 0x26u), (ushort)c.V0);
        m.WriteU16((c.A1 + 0x32u), (ushort)c.V1);
        goto L8018FFE8;
    L8018FAF4:;
        c.RA = 0x8018FAFCu;
        SoTN.GetDistanceToPlayerX_cen(c, m);
        c.V0 = (int)c.V0 < 32 ? 1u : 0u;
        if (c.V0 == 0u)
        {
            goto L8018FFE8;
        }
        c.V0 = (uint)(short)m.ReadU16((c.S0 + 0x6u));
        c.V1 = (uint)(short)m.ReadU16((c.S2 + 0x6u));
        c.V0 = c.V0 - c.V1;
        c.V0 = (int)c.V0 < 80 ? 1u : 0u;
        if (c.V0 == 0u)
        {
            c.V0 = 0u | 0x0001u;
            goto L8018FFE8;
        }
        c.V0 = 0u | 0x0001u;
        c.V1 = 0x80070000u;
        c.V1 = m.ReadU32((c.V1 + 0x2F2Cu));
        c.At = 0x80040000u;
        m.WriteU32((c.At - 0x3748u), 0u);
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7400u), c.V0);
        c.V0 = c.V1 & 0x0001u;
        if (c.V0 == 0u)
        {
            c.V0 = 0u | 0x0008u;
            goto L8018FB58;
        }
        c.V0 = 0u | 0x0008u;
        c.At = 0x80070000u;
        m.WriteU32((c.At + 0x2EF4u), c.V0);
        c.V0 = 0u | 0x0001u;
        goto L8018FBDC;
    L8018FB58:;
        c.V0 = c.V1 & 0x0002u;
        if (c.V0 == 0u)
        {
            c.V0 = 0u | 0x0004u;
            goto L8018FB74;
        }
        c.V0 = 0u | 0x0004u;
        c.At = 0x80070000u;
        m.WriteU32((c.At + 0x2EF4u), c.V0);
        c.V0 = 0u | 0x0001u;
        goto L8018FBDC;
    L8018FB74:;
        c.V0 = c.V1 & 0x0004u;
        if (c.V0 == 0u)
        {
            c.V0 = 0u | 0x0002u;
            goto L8018FB90;
        }
        c.V0 = 0u | 0x0002u;
        c.At = 0x80070000u;
        m.WriteU32((c.At + 0x2EF4u), c.V0);
        c.V0 = 0u | 0x0001u;
        goto L8018FBDC;
    L8018FB90:;
        c.V0 = c.S1 << 16;
        c.V1 = (uint)((int)c.V0 >> 16);
        c.V0 = (int)c.V1 < 385 ? 1u : 0u;
        if (c.V0 != 0u)
        {
            c.V0 = (int)c.V1 < 384 ? 1u : 0u;
            goto L8018FBB8;
        }
        c.V0 = (int)c.V1 < 384 ? 1u : 0u;
        c.V0 = 0u | 0x8000u;
        c.At = 0x80070000u;
        m.WriteU32((c.At + 0x2EF4u), c.V0);
        c.V0 = 0u | 0x0001u;
        goto L8018FBDC;
    L8018FBB8:;
        if (c.V0 == 0u)
        {
            c.V0 = 0u | 0x2000u;
            goto L8018FBD0;
        }
        c.V0 = 0u | 0x2000u;
        c.At = 0x80070000u;
        m.WriteU32((c.At + 0x2EF4u), c.V0);
        c.V0 = 0u | 0x0001u;
        goto L8018FBDC;
    L8018FBD0:;
        c.At = 0x80070000u;
        m.WriteU32((c.At + 0x2EF4u), 0u);
        c.V0 = 0u | 0x0001u;
    L8018FBDC:;
        c.At = 0x80070000u;
        m.WriteU8((c.At + 0x3510u), (byte)0u);
        c.At = 0x80070000u;
        m.WriteU32((c.At + 0x2EFCu), c.V0);
        goto L8018FFD8;
    L8018FBF4:;
        c.V1 = 0x80070000u;
        c.V1 = m.ReadU32((c.V1 + 0x2F2Cu));
        c.A0 = 0x80070000u;
        c.A0 = c.A0 + 0x2EF4u;
        c.V0 = c.V1 & 0x0007u;
        if (c.V0 == 0u)
        {
            m.WriteU32(c.A0, 0u);
            goto L8018FC60;
        }
        m.WriteU32(c.A0, 0u);
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x3668u));
        c.V0 = c.V0 & 0x0001u;
        if (c.V0 == 0u)
        {
            c.V0 = c.V1 & 0x0001u;
            goto L8018FD88;
        }
        c.V0 = c.V1 & 0x0001u;
        if (c.V0 == 0u)
        {
            c.V0 = 0u | 0x0008u;
            goto L8018FC38;
        }
        c.V0 = 0u | 0x0008u;
        m.WriteU32(c.A0, c.V0);
        goto L8018FD88;
    L8018FC38:;
        c.V0 = c.V1 & 0x0002u;
        if (c.V0 == 0u)
        {
            c.V0 = 0u | 0x0004u;
            goto L8018FC4C;
        }
        c.V0 = 0u | 0x0004u;
        m.WriteU32(c.A0, c.V0);
        goto L8018FD88;
    L8018FC4C:;
        c.V0 = c.V1 & 0x0004u;
        if (c.V0 == 0u)
        {
            c.V0 = 0u | 0x0002u;
            goto L8018FD88;
        }
        c.V0 = 0u | 0x0002u;
        m.WriteU32(c.A0, c.V0);
        goto L8018FD88;
    L8018FC60:;
        c.V0 = c.A1 & 0xFFFFu;
        if (c.V0 != 0u)
        {
            c.V0 = c.S1 << 16;
            goto L8018FC84;
        }
        c.V0 = c.S1 << 16;
        c.V0 = 0x80070000u;
        c.V0 = m.ReadU32((c.V0 + 0x2F20u));
        c.V0 = c.V0 & 0x0001u;
        if (c.V0 == 0u)
        {
            c.V0 = c.S1 << 16;
            goto L8018FD88;
        }
        c.V0 = c.S1 << 16;
    L8018FC84:;
        c.V1 = (uint)((int)c.V0 >> 16);
        c.V0 = (int)c.V1 < 385 ? 1u : 0u;
        if (c.V0 != 0u)
        {
            c.V0 = (int)c.V1 < 384 ? 1u : 0u;
            goto L8018FC9C;
        }
        c.V0 = (int)c.V1 < 384 ? 1u : 0u;
        c.V0 = 0u | 0x8000u;
        goto L8018FCA4;
    L8018FC9C:;
        if (c.V0 == 0u)
        {
            c.V0 = 0u | 0x2000u;
            goto L8018FCA8;
        }
        c.V0 = 0u | 0x2000u;
    L8018FCA4:;
        m.WriteU32(c.A0, c.V0);
    L8018FCA8:;
        c.V0 = m.ReadU16((c.S0 + 0x2Cu));
        c.V0 = c.V0 + 0x1u;
        m.WriteU16((c.S0 + 0x2Cu), (ushort)c.V0);
        goto L8018FD88;
    L8018FCBC:;
        c.A0 = 0x80070000u;
        c.A0 = c.A0 + 0x2EF4u;
        c.V1 = m.ReadU32(c.A0);
        c.V0 = 0u | 0x8000u;
        if (c.V1 != c.V0)
        {
            c.V0 = 0u | 0x2000u;
            goto L8018FCF0;
        }
        c.V0 = 0u | 0x2000u;
        c.V0 = c.S1 << 16;
        c.V0 = (uint)((int)c.V0 >> 16);
        c.V0 = (int)c.V0 < 385 ? 1u : 0u;
        if (c.V0 == 0u)
        {
            goto L8018FD0C;
        }
        m.WriteU32(c.A0, 0u);
        goto L8018FD0C;
    L8018FCF0:;
        if (c.V1 != c.V0)
        {
            c.V0 = c.S1 << 16;
            goto L8018FD0C;
        }
        c.V0 = c.S1 << 16;
        c.V0 = (uint)((int)c.V0 >> 16);
        c.V0 = (int)c.V0 < 384 ? 1u : 0u;
        if (c.V0 != 0u)
        {
            goto L8018FD0C;
        }
        m.WriteU32(c.A0, 0u);
    L8018FD0C:;
        c.V1 = 0x80070000u;
        c.V1 = c.V1 + 0x2EF4u;
        c.V0 = m.ReadU32(c.V1);
        if (c.V0 != 0u)
        {
            c.V0 = 0u | 0x0001u;
            goto L8018FD8C;
        }
        c.V0 = 0u | 0x0001u;
        c.V0 = 0u | 0x8000u;
        m.WriteU32(c.V1, c.V0);
        c.V1 = m.ReadU16((c.S3 + 0xAu));
        c.V0 = 0u | 0x0180u;
        c.V0 = c.V0 - c.V1;
        m.WriteU16((c.S2 + 0x2u), (ushort)c.V0);
        c.V0 = m.ReadU16((c.S0 + 0x2Cu));
        c.V0 = c.V0 + 0x1u;
        m.WriteU16((c.S0 + 0x2Cu), (ushort)c.V0);
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x3824u));
        c.A0 = 0u | 0x060Du;
        c.RA = 0x8018FD60u;
        Dispatcher.Call(c, m, c.V0);
        c.A0 = 0u + 0u;
        c.V0 = 0x801A0000u;
        c.V0 = m.ReadU32((c.V0 - 0x2BDCu));
        c.V1 = (uint)(short)m.ReadU16((c.S3 + 0xEu));
        c.V0 = c.V0 | 0x0001u;
        c.V1 = c.V1 + 0x100u;
        c.At = 0x801A0000u;
        m.WriteU32((c.At - 0x2BDCu), c.V0);
        m.WriteU32((c.S3 + 0x48u), c.V1);
        c.RA = 0x8018FD88u;
        SoTN.func_8018F8EC(c, m);
    L8018FD88:;
        c.V0 = 0u | 0x0001u;
    L8018FD8C:;
        c.At = 0x80070000u;
        m.WriteU32((c.At + 0x2EFCu), c.V0);
        goto L8018FFE8;
    L8018FD9C:;
        c.S1 = 0x80070000u;
        c.S1 = c.S1 + 0x2EF4u;
        c.V0 = 0u | 0x0001u;
        m.WriteU32(c.S1, 0u);
        c.At = 0x80070000u;
        m.WriteU32((c.At + 0x2EFCu), c.V0);
        c.V1 = m.ReadU16((c.S3 + 0xAu));
        c.V0 = 0u | 0x0180u;
        c.V0 = c.V0 - c.V1;
        m.WriteU16((c.S2 + 0x2u), (ushort)c.V0);
        c.V0 = c.A0 << 16;
        c.V0 = (uint)((int)c.V0 >> 16);
        c.V0 = (int)c.V0 < 497 ? 1u : 0u;
        if (c.V0 != 0u)
        {
            goto L8018FE08;
        }
        c.V0 = m.ReadU16((c.S0 + 0x6u));
        c.A0 = 0x80090000u;
        c.A0 = c.A0 + 0x748Eu;
        c.V0 = c.V0 - 0x1u;
        m.WriteU16((c.S0 + 0x6u), (ushort)c.V0);
        c.V0 = m.ReadU16((c.S2 + 0x6u));
        c.V1 = m.ReadU16(c.A0);
        c.V0 = c.V0 - 0x1u;
        c.V1 = c.V1 - 0x1u;
        m.WriteU16((c.S2 + 0x6u), (ushort)c.V0);
        m.WriteU16(c.A0, (ushort)c.V1);
        goto L8018FE50;
    L8018FE08:;
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x3824u));
        c.A0 = 0u | 0x064Fu;
        c.RA = 0x8018FE1Cu;
        Dispatcher.Call(c, m, c.V0);
        c.V0 = m.ReadU16((c.S2 + 0x14u));
        if (c.V0 != 0u)
        {
            c.V0 = 0u | 0x8000u;
            goto L8018FE30;
        }
        c.V0 = 0u | 0x8000u;
        m.WriteU32(c.S1, c.V0);
    L8018FE30:;
        c.V1 = m.ReadU16((c.S0 + 0x2Cu));
        c.V0 = 0x801A0000u;
        c.V0 = m.ReadU32((c.V0 - 0x2BDCu));
        c.V1 = c.V1 + 0x1u;
        c.V0 = c.V0 | 0x0004u;
        c.At = 0x801A0000u;
        m.WriteU32((c.At - 0x2BDCu), c.V0);
        m.WriteU16((c.S0 + 0x2Cu), (ushort)c.V1);
    L8018FE50:;
        c.A0 = 0u | 0x0200u;
        c.RA = 0x8018FE58u;
        SoTN.func_8018F890(c, m);
        goto L8018FFE8;
    L8018FE60:;
        c.A0 = 0u | 0x0200u;
        c.RA = 0x8018FE68u;
        SoTN.func_8018F890(c, m);
        c.V0 = 0x801A0000u;
        c.V0 = m.ReadU32((c.V0 - 0x2BDCu));
        c.V1 = 0u | 0x0001u;
        c.At = 0x80070000u;
        m.WriteU32((c.At + 0x2EF4u), 0u);
        c.At = 0x80070000u;
        m.WriteU32((c.At + 0x2EFCu), c.V1);
        c.V0 = c.V0 & 0x0008u;
        if (c.V0 == 0u)
        {
            goto L8018FFE8;
        }
        c.A1 = 0x80080000u;
        c.A1 = c.A1 - 0x3658u;
        c.A0 = 0u | 0x000Au;
        c.RA = 0x8018FEA0u;
        if (m.ReadU32(0x8018FE98) != 0x0C064D31) // added
        {
            goto L8018FFD8;
        }
        Dispatcher.Call(c, m, 0x801934C4u);
        //c.V0 = 0u | 0x00CBu;  // CB = Holy Glasses
        c.V0 = 0u | m.ReadU16(0x8018FEA0);  // Read Updated Item Id
        c.At = 0x80080000u;
        m.WriteU16((c.At - 0x3628u), (ushort)c.V0);
        c.V0 = 0u | 0x0005u;
        c.At = 0x80080000u;
        m.WriteU16((c.At - 0x362Cu), (ushort)c.V0);
        c.At = 0x80080000u;
        m.WriteU32((c.At - 0x3624u), 0u);
        goto L8018FFD8;
    L8018FEC8:;
        c.V0 = 0x801A0000u;
        c.V0 = m.ReadU32((c.V0 - 0x2BDCu));
        c.V0 = c.V0 & 0x0002u;
        if (c.V0 == 0u)
        {
            c.V0 = 0u | 0x0001u;
            goto L8018FFA8;
        }
        c.V0 = 0u | 0x0001u;
        c.V0 = m.ReadU16((c.S0 + 0x2Cu));
        c.V0 = c.V0 + 0x1u;
        m.WriteU16((c.S0 + 0x2Cu), (ushort)c.V0);
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x3824u));
        c.A0 = 0u | 0x060Du;
        c.RA = 0x8018FF04u;
        Dispatcher.Call(c, m, c.V0);
        c.V0 = 0u | 0x0001u;
        goto L8018FFA8;
    L8018FF0C:;
        c.V0 = c.A0 << 16;
        c.V0 = (uint)((int)c.V0 >> 16);
        c.V0 = (int)c.V0 < 592 ? 1u : 0u;
        if (c.V0 == 0u)
        {
            c.V0 = 0u | 0x0001u;
            goto L8018FF50;
        }
        c.V0 = 0u | 0x0001u;
        c.V0 = m.ReadU16((c.S0 + 0x6u));
        c.A0 = 0x80090000u;
        c.A0 = c.A0 + 0x748Au;
        c.V0 = c.V0 + 0x1u;
        m.WriteU16((c.S0 + 0x6u), (ushort)c.V0);
        c.V0 = m.ReadU16((c.S2 + 0x6u));
        c.V1 = m.ReadU16(c.A0);
        c.V0 = c.V0 + 0x1u;
        c.V1 = c.V1 + 0x1u;
        m.WriteU16((c.S2 + 0x6u), (ushort)c.V0);
        m.WriteU16(c.A0, (ushort)c.V1);
        goto L8018FF9C;
    L8018FF50:;
        c.A0 = 0x80090000u;
        c.A0 = c.A0 + 0x7400u;
        c.V1 = m.ReadU32(c.A0);
        c.At = 0x80040000u;
        m.WriteU32((c.At - 0x3748u), c.V0);
        if (c.V1 == 0u)
        {
            c.V0 = 0u | 0x0001u;
            goto L8018FF70;
        }
        c.V0 = 0u | 0x0001u;
        m.WriteU32(c.A0, 0u);
    L8018FF70:;
        c.At = 0x80070000u;
        m.WriteU8((c.At + 0x3510u), (byte)c.V0);
        c.V0 = m.ReadU16((c.S0 + 0x2Cu));
        c.V0 = c.V0 + 0x1u;
        m.WriteU16((c.S0 + 0x2Cu), (ushort)c.V0);
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x3824u));
        c.A0 = 0u | 0x064Fu;
        c.RA = 0x8018FF9Cu;
        Dispatcher.Call(c, m, c.V0);
    L8018FF9C:;
        c.A0 = 0u | 0x0300u;
        c.RA = 0x8018FFA4u;
        SoTN.func_8018F890(c, m);
        c.V0 = 0u | 0x0001u;
    L8018FFA8:;
        c.At = 0x80070000u;
        m.WriteU32((c.At + 0x2EF4u), 0u);
        c.At = 0x80070000u;
        m.WriteU32((c.At + 0x2EFCu), c.V0);
        goto L8018FFE8;
    L8018FFC0:;
        c.A0 = 0u | 0x0300u;
        c.RA = 0x8018FFC8u;
        SoTN.func_8018F890(c, m);
        c.V1 = m.ReadU32((c.S3 + 0x48u));
        c.V0 = 0u | 0x0300u;
        if (c.V1 != c.V0)
        {
            goto L8018FFE8;
        }
    L8018FFD8:;
        c.V0 = m.ReadU16((c.S0 + 0x2Cu));
        c.V0 = c.V0 + 0x1u;
        m.WriteU16((c.S0 + 0x2Cu), (ushort)c.V0);
    L8018FFE8:;
        c.V1 = m.ReadU32((c.S0 + 0x64u));
        c.A0 = 0x80080000u;
        c.A0 = c.A0 + 0x6FECu;
        c.V0 = c.V1 << 1;
        c.V0 = c.V0 + c.V1;
        c.V0 = c.V0 << 2;
        c.V0 = c.V0 + c.V1;
        c.V0 = c.V0 << 2;
        c.A1 = c.V0 + c.A0;
        c.V1 = m.ReadU16((c.S0 + 0x2u));
        c.V0 = m.ReadU16((c.S0 + 0x6u));
        c.A0 = c.V1 - 0x8u;
        c.V1 = c.V1 + 0x8u;
        c.V0 = c.V0 + 0x8u;
        m.WriteU16((c.S0 + 0x6u), (ushort)c.V0);
        m.WriteU16((c.A1 + 0x20u), (ushort)c.A0);
        m.WriteU16((c.A1 + 0x8u), (ushort)c.A0);
        m.WriteU16((c.A1 + 0x2Cu), (ushort)c.V1);
        m.WriteU16((c.A1 + 0x14u), (ushort)c.V1);
        c.V0 = m.ReadU16((c.S0 + 0x6u));
        c.V0 = c.V0 + 0xFu;
        m.WriteU16((c.A1 + 0x16u), (ushort)c.V0);
        m.WriteU16((c.A1 + 0xAu), (ushort)c.V0);
        c.V1 = m.ReadU16((c.S3 + 0xEu));
        c.V0 = 0u | 0x0268u;
        c.V0 = c.V0 - c.V1;
        m.WriteU16((c.A1 + 0x2Eu), (ushort)c.V0);
        m.WriteU16((c.A1 + 0x22u), (ushort)c.V0);
        c.RA = m.ReadU32((c.SP + 0x20u));
        c.S3 = m.ReadU32((c.SP + 0x1Cu));
        c.S2 = m.ReadU32((c.SP + 0x18u));
        c.S1 = m.ReadU32((c.SP + 0x14u));
        c.S0 = m.ReadU32((c.SP + 0x10u));
        c.SP = c.SP + 0x28u;
        return;
    }

    // Gold Ring Location
    public static void func_us_801C8248(CpuContext c, IMemory m)
    {
        // Gold Ring, Checks if it was changed to a Relic
        bool GR_is_Relic = false;
        if (m.ReadU32(0x801CC590) == 0x08077AED)
        {
            GR_is_Relic = true;
        }
        c.SP = c.SP - 0x20u;
        m.WriteU32((c.SP + 0x18u), c.S2);
        c.S2 = c.A0 + 0u;
        c.A0 = 0u | 0x0009u;
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x37C0u));
        c.A1 = 0u + 0u;
        m.WriteU32((c.SP + 0x1Cu), c.RA);
        m.WriteU32((c.SP + 0x14u), c.S1);
        m.WriteU32((c.SP + 0x10u), c.S0);
        c.RA = 0x801C8274u;
        Dispatcher.Call(c, m, c.V0);
        if (c.V0 == 0u)
        {
            c.A0 = 0u | 0x000Cu;
            goto L801C829C;
        }
        c.A0 = 0u | 0x000Cu;
        if (GR_is_Relic)
        {
            c.A0 = 0x000Bu;
        }
        c.S0 = m.ReadU32(c.S2);
        c.S1 = m.ReadU32((c.S2 + 0x4u));
        c.A1 = c.S2 + 0u;
        c.RA = 0x801C828Cu;
        SoTN.CreateEntityFromCurrentEntity_no4(c, m);
        c.V0 = 0u | 0x000Au;
        if (GR_is_Relic)
        {
            c.V0 = m.ReadU8(0x80184278);
        }
        m.WriteU16((c.S2 + 0x30u), (ushort)c.V0);
        m.WriteU32(c.S2, c.S0);
        m.WriteU32((c.S2 + 0x4u), c.S1);
    L801C829C:;
        c.RA = m.ReadU32((c.SP + 0x1Cu));
        c.S2 = m.ReadU32((c.SP + 0x18u));
        c.S1 = m.ReadU32((c.SP + 0x14u));
        c.S0 = m.ReadU32((c.SP + 0x10u));
        c.SP = c.SP + 0x20u;
        return;
    }

    // Pot Roast drop in Entrance
    // 0x801BA7CC in 0x41
    // 0x801B506C in 0x07
    public static void EntityMermanRockLeftSide_no3(CpuContext c, IMemory m)
    {
        c.SP = c.SP - 0x28u;
        m.WriteU32((c.SP + 0x18u), c.S2);
        c.S2 = c.A0 + 0u;
        m.WriteU32((c.SP + 0x24u), c.RA);
        m.WriteU32((c.SP + 0x20u), c.S4);
        m.WriteU32((c.SP + 0x1Cu), c.S3);
        m.WriteU32((c.SP + 0x14u), c.S1);
        m.WriteU32((c.SP + 0x10u), c.S0);
        c.V1 = m.ReadU16((c.S2 + 0x2Cu));
        c.V0 = 0u | 0x0001u;
        if (c.V1 == c.V0)
        {
            c.V0 = (int)c.V1 < 2 ? 1u : 0u;
            goto L801BA608;
        }
        c.V0 = (int)c.V1 < 2 ? 1u : 0u;
        if (c.V0 == 0u)
        {
            goto L801BA508;
        }
        if (c.V1 == 0u)
        {
            goto L801BA51C;
        }
        goto L801BA844;
    L801BA508:;
        c.V0 = 0u | 0x0002u;
        if (c.V1 == c.V0)
        {
            goto L801BA804;
        }
        goto L801BA844;
    L801BA51C:;
        c.A0 = 0x80180000u;
        c.A0 = c.A0 + 0xADCu;
        c.S1 = 0u + 0u;
        c.RA = 0x801BA52Cu;
        SoTN.InitializeEntity_no3(c, m);
        c.A1 = 0x80180000u;
        c.A1 = c.A1 + 0x127Cu;
        c.A2 = 0u | 0x01F1u;
        c.V0 = 0u | 0x0002u;
        m.WriteU16((c.S2 + 0x3Cu), (ushort)c.V0);
        c.V0 = 0u | 0x0010u;
        m.WriteU8((c.S2 + 0x46u), (byte)c.V0);
        c.V0 = 0u | 0x0018u;
        m.WriteU8((c.S2 + 0x47u), (byte)c.V0);
    L801BA550:;
        c.A0 = c.A2 << 1;
        c.A2 = c.A2 + 0x30u;
        c.S1 = c.S1 + 0x1u;
        c.V0 = 0x80070000u;
        c.V0 = m.ReadU32((c.V0 + 0x30D8u));
        c.V1 = m.ReadU16(c.A1);
        c.V0 = c.A0 + c.V0;
        m.WriteU16(c.V0, (ushort)c.V1);
        c.V0 = 0x80070000u;
        c.V0 = m.ReadU32((c.V0 + 0x30D8u));
        c.V1 = m.ReadU16((c.A1 + 0x6u));
        c.A1 = c.A1 + 0x2u;
        c.A0 = c.A0 + c.V0;
        c.V0 = (int)c.S1 < 3 ? 1u : 0u;
        if (c.V0 != 0u)
        {
            m.WriteU16((c.A0 + 0x2u), (ushort)c.V1);
            goto L801BA550;
        }
        m.WriteU16((c.A0 + 0x2u), (ushort)c.V1);
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU8((c.V0 - 0x41E1u));
        c.V0 = c.V0 & 0x0001u;
        if (c.V0 == 0u)
        {
            c.A2 = 0u | 0x01F1u;
            goto L801BA844;
        }
        c.A2 = 0u | 0x01F1u;
        c.A1 = 0x80180000u;
        c.A1 = c.A1 + 0x1264u;
        c.S1 = 0u + 0u;
    L801BA5B4:;
        c.A0 = c.A2 << 1;
        c.A2 = c.A2 + 0x30u;
        c.S1 = c.S1 + 0x1u;
        c.V0 = 0x80070000u;
        c.V0 = m.ReadU32((c.V0 + 0x3084u));
        c.V1 = m.ReadU16(c.A1);
        c.V0 = c.A0 + c.V0;
        m.WriteU16(c.V0, (ushort)c.V1);
        c.V0 = 0x80070000u;
        c.V0 = m.ReadU32((c.V0 + 0x3084u));
        c.V1 = m.ReadU16((c.A1 + 0x6u));
        c.A1 = c.A1 + 0x2u;
        c.A0 = c.A0 + c.V0;
        c.V0 = (int)c.S1 < 3 ? 1u : 0u;
        if (c.V0 != 0u)
        {
            m.WriteU16((c.A0 + 0x2u), (ushort)c.V1);
            goto L801BA5B4;
        }
        m.WriteU16((c.A0 + 0x2u), (ushort)c.V1);
        c.V0 = 0u | 0x0001u;
        m.WriteU16((c.S2 + 0x3Cu), (ushort)c.V0);
        c.V0 = 0u | 0x0002u;
        m.WriteU16((c.S2 + 0x2Cu), (ushort)c.V0);
        goto L801BA844;
    L801BA608:;
        c.V0 = m.ReadU8((c.S2 + 0x48u));
        if (c.V0 == 0u)
        {
            c.A2 = 0u | 0x01F1u;
            goto L801BA790;
        }
        c.A2 = 0u | 0x01F1u;
        c.V0 = (uint)(short)m.ReadU16((c.S2 + 0x84u));
        c.S1 = 0u + 0u;
        c.V1 = c.V0 << 1;
        c.V1 = c.V1 + c.V0;
        c.V1 = c.V1 << 2;
        c.V0 = 0x80180000u;
        c.V0 = c.V0 + 0x1258u;
        c.A1 = c.V1 + c.V0;
    L801BA638:;
        c.A0 = c.A2 << 1;
        c.A2 = c.A2 + 0x30u;
        c.S1 = c.S1 + 0x1u;
        c.V0 = 0x80070000u;
        c.V0 = m.ReadU32((c.V0 + 0x3084u));
        c.V1 = m.ReadU16(c.A1);
        c.V0 = c.A0 + c.V0;
        m.WriteU16(c.V0, (ushort)c.V1);
        c.V0 = 0x80070000u;
        c.V0 = m.ReadU32((c.V0 + 0x3084u));
        c.V1 = m.ReadU16((c.A1 + 0x6u));
        c.A1 = c.A1 + 0x2u;
        c.A0 = c.A0 + c.V0;
        c.V0 = (int)c.S1 < 3 ? 1u : 0u;
        if (c.V0 != 0u)
        {
            m.WriteU16((c.A0 + 0x2u), (ushort)c.V1);
            goto L801BA638;
        }
        m.WriteU16((c.A0 + 0x2u), (ushort)c.V1);
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x3824u));
        c.A0 = 0u | 0x0644u;
        c.RA = 0x801BA68Cu;
        Dispatcher.Call(c, m, c.V0);
        c.S3 = 0x80080000u;
        c.S3 = c.S3 - 0x27A8u;
        c.A0 = c.S3 + 0u;
        c.A1 = c.S3 + 0x1780u;
        c.RA = 0x801BA6A0u;
        Dispatcher.Call(c, m, 0x801C54D4u);
        c.S0 = c.V0 + 0u;
        if (c.S0 == 0u)
        {
            c.S1 = 0u + 0u;
            goto L801BA6EC;
        }
        c.S1 = 0u + 0u;
        c.A0 = 0u | 0x0002u;
        c.A1 = c.S2 + 0u;
        c.A2 = c.S0 + 0u;
        c.RA = 0x801BA6BCu;
        SoTN.CreateEntityFromEntity_no3(c, m);
        c.V0 = 0u | 0x0013u;
        m.WriteU16((c.S0 + 0x30u), (ushort)c.V0);
        c.V0 = 0u | 0x00A9u;
        m.WriteU16((c.S0 + 0x24u), (ushort)c.V0);
        c.V1 = (uint)(short)m.ReadU16((c.S2 + 0x84u));
        c.V0 = m.ReadU16((c.S0 + 0x2u));
        c.A0 = m.ReadU16((c.S0 + 0x6u));
        c.V1 = c.V1 << 4;
        c.V0 = c.V0 + c.V1;
        c.A0 = c.A0 + 0x10u;
        m.WriteU16((c.S0 + 0x2u), (ushort)c.V0);
        m.WriteU16((c.S0 + 0x6u), (ushort)c.A0);
    L801BA6EC:;
        c.V0 = (uint)(short)m.ReadU16((c.S2 + 0x84u));
        c.S4 = c.S3 + 0u;
        c.V1 = c.V0 << 1;
        c.V1 = c.V1 + c.V0;
        c.V0 = 0x80180000u;
        c.V0 = c.V0 + 0x1344u;
        c.S3 = c.V1 + c.V0;
        c.A0 = c.S4 + 0u;
    L801BA70C:;
        c.A1 = c.S4 + 0x1780u;
        c.RA = 0x801BA714u;
        Dispatcher.Call(c, m, 0x801C54D4u);
        c.S0 = c.V0 + 0u;
        if (c.S0 == 0u)
        {
            c.A0 = 0u | 0x0027u;
            goto L801BA770;
        }
        c.A0 = 0u | 0x0027u;
        c.A1 = c.S2 + 0u;
        c.A2 = c.S0 + 0u;
        c.RA = 0x801BA72Cu;
        SoTN.CreateEntityFromEntity_no3(c, m);
        c.V0 = m.ReadU8(c.S3);
        c.S3 = c.S3 + 0x1u;
        m.WriteU16((c.S0 + 0x30u), (ushort)c.V0);
        c.RA = 0x801BA73Cu;
        SoTN.Random_no3(c, m);
        c.V0 = c.V0 << 8;
        c.V1 = 0xFFFF8000u;
        c.V1 = c.V1 - c.V0;
        m.WriteU32((c.S0 + 0x8u), c.V1);
        c.RA = 0x801BA750u;
        SoTN.Random_no3(c, m);
        c.V0 = 0u - c.V0;
        c.V1 = m.ReadU16((c.S0 + 0x6u));
        c.V0 = c.V0 << 8;
        m.WriteU32((c.S0 + 0xCu), c.V0);
        c.V0 = c.S1 << 4;
        c.V1 = c.V1 - 0x10u;
        c.V1 = c.V1 + c.V0;
        m.WriteU16((c.S0 + 0x6u), (ushort)c.V1);
    L801BA770:;
        c.S1 = c.S1 + 0x1u;
        c.V0 = (int)c.S1 < 3 ? 1u : 0u;
        if (c.V0 != 0u)
        {
            c.A0 = c.S4 + 0u;
            goto L801BA70C;
        }
        c.A0 = c.S4 + 0u;
        c.V0 = m.ReadU16((c.S2 + 0x84u));
        c.V0 = c.V0 + 0x1u;
        m.WriteU16((c.S2 + 0x84u), (ushort)c.V0);
    L801BA790:;
        c.V0 = (uint)(short)m.ReadU16((c.S2 + 0x84u));
        c.V0 = (int)c.V0 < 2 ? 1u : 0u;
        if (c.V0 != 0u)
        {
            goto L801BA844;
        }
        c.A0 = 0x80080000u;
        c.A0 = c.A0 - 0x56A8u;
        c.A1 = c.A0 + 0x1780u;
        c.RA = 0x801BA7B4u;
        Dispatcher.Call(c, m, 0x801C54D4u);
        c.S0 = c.V0 + 0u;
        if (c.S0 == 0u)
        {
            c.A0 = 0u | 0x000Au;
            goto L801BA7D4;
        }
        c.A0 = 0u | 0x000Au;
        c.A1 = c.S2 + 0u;
        c.A2 = c.S0 + 0u;
        c.RA = 0x801BA7CCu;
        SoTN.CreateEntityFromEntity_no3(c, m);
        //c.V0 = 0u | 0x0043u;
        c.V0 = 0u | m.ReadU16(0x801BA7CC);      // Read Pot Roast Replacement ID
        m.WriteU16((c.S0 + 0x30u), (ushort)c.V0);
    L801BA7D4:;
        c.V1 = 0x80040000u;
        c.V1 = c.V1 - 0x41E1u;
        c.V0 = m.ReadU8(c.V1);
        c.V0 = c.V0 | 0x0001u;
        m.WriteU8(c.V1, (byte)c.V0);
        c.V0 = m.ReadU16((c.S2 + 0x2Cu));
        c.V1 = 0u | 0x0001u;
        m.WriteU16((c.S2 + 0x3Cu), (ushort)c.V1);
        c.V0 = c.V0 + 0x1u;
        m.WriteU16((c.S2 + 0x2Cu), (ushort)c.V0);
        goto L801BA844;
    L801BA804:;
        c.V0 = m.ReadU8((c.S2 + 0x48u));
        if (c.V0 == 0u)
        {
            goto L801BA844;
        }
        c.V0 = 0x80070000u;
        c.V0 = m.ReadU32((c.V0 + 0x2F2Cu));
        c.V0 = c.V0 & 0x0004u;
        if (c.V0 == 0u)
        {
            goto L801BA844;
        }
        c.V1 = 0x80040000u;
        c.V1 = c.V1 - 0x41E1u;
        c.V0 = m.ReadU8(c.V1);
        c.V0 = c.V0 | 0x0004u;
        m.WriteU8(c.V1, (byte)c.V0);
    L801BA844:;
        c.RA = m.ReadU32((c.SP + 0x24u));
        c.S4 = m.ReadU32((c.SP + 0x20u));
        c.S3 = m.ReadU32((c.SP + 0x1Cu));
        c.S2 = m.ReadU32((c.SP + 0x18u));
        c.S1 = m.ReadU32((c.SP + 0x14u));
        c.S0 = m.ReadU32((c.SP + 0x10u));
        c.SP = c.SP + 0x28u;
        return;
    }

    // Pot Roast in NP3 Entrance
    public static void EntityMermanRockLeftSide_np3(CpuContext c, IMemory m)
    {
        c.SP = c.SP - 0x28u;
        m.WriteU32((c.SP + 0x18u), c.S2);
        c.S2 = c.A0 + 0u;
        m.WriteU32((c.SP + 0x24u), c.RA);
        m.WriteU32((c.SP + 0x20u), c.S4);
        m.WriteU32((c.SP + 0x1Cu), c.S3);
        m.WriteU32((c.SP + 0x14u), c.S1);
        m.WriteU32((c.SP + 0x10u), c.S0);
        c.V1 = m.ReadU16((c.S2 + 0x2Cu));
        c.V0 = 0u | 0x0001u;
        if (c.V1 == c.V0)
        {
            c.V0 = (int)c.V1 < 2 ? 1u : 0u;
            goto L801B4EA8;
        }
        c.V0 = (int)c.V1 < 2 ? 1u : 0u;
        if (c.V0 == 0u)
        {
            goto L801B4DA8;
        }
        if (c.V1 == 0u)
        {
            goto L801B4DBC;
        }
        goto L801B50E4;
    L801B4DA8:;
        c.V0 = 0u | 0x0002u;
        if (c.V1 == c.V0)
        {
            goto L801B50A4;
        }
        goto L801B50E4;
    L801B4DBC:;
        c.A0 = 0x80180000u;
        c.A0 = c.A0 + 0xA6Cu;
        c.S1 = 0u + 0u;
        c.RA = 0x801B4DCCu;
        SoTN.InitializeEntity_np3(c, m);
        c.A1 = 0x80180000u;
        c.A1 = c.A1 + 0x1144u;
        c.A2 = 0u | 0x01F1u;
        c.V0 = 0u | 0x0002u;
        m.WriteU16((c.S2 + 0x3Cu), (ushort)c.V0);
        c.V0 = 0u | 0x0010u;
        m.WriteU8((c.S2 + 0x46u), (byte)c.V0);
        c.V0 = 0u | 0x0018u;
        m.WriteU8((c.S2 + 0x47u), (byte)c.V0);
    L801B4DF0:;
        c.A0 = c.A2 << 1;
        c.A2 = c.A2 + 0x30u;
        c.S1 = c.S1 + 0x1u;
        c.V0 = 0x80070000u;
        c.V0 = m.ReadU32((c.V0 + 0x30D8u));
        c.V1 = m.ReadU16(c.A1);
        c.V0 = c.A0 + c.V0;
        m.WriteU16(c.V0, (ushort)c.V1);
        c.V0 = 0x80070000u;
        c.V0 = m.ReadU32((c.V0 + 0x30D8u));
        c.V1 = m.ReadU16((c.A1 + 0x6u));
        c.A1 = c.A1 + 0x2u;
        c.A0 = c.A0 + c.V0;
        c.V0 = (int)c.S1 < 3 ? 1u : 0u;
        if (c.V0 != 0u)
        {
            m.WriteU16((c.A0 + 0x2u), (ushort)c.V1);
            goto L801B4DF0;
        }
        m.WriteU16((c.A0 + 0x2u), (ushort)c.V1);
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU8((c.V0 - 0x41E1u));
        c.V0 = c.V0 & 0x0001u;
        if (c.V0 == 0u)
        {
            c.A2 = 0u | 0x01F1u;
            goto L801B50E4;
        }
        c.A2 = 0u | 0x01F1u;
        c.A1 = 0x80180000u;
        c.A1 = c.A1 + 0x112Cu;
        c.S1 = 0u + 0u;
    L801B4E54:;
        c.A0 = c.A2 << 1;
        c.A2 = c.A2 + 0x30u;
        c.S1 = c.S1 + 0x1u;
        c.V0 = 0x80070000u;
        c.V0 = m.ReadU32((c.V0 + 0x3084u));
        c.V1 = m.ReadU16(c.A1);
        c.V0 = c.A0 + c.V0;
        m.WriteU16(c.V0, (ushort)c.V1);
        c.V0 = 0x80070000u;
        c.V0 = m.ReadU32((c.V0 + 0x3084u));
        c.V1 = m.ReadU16((c.A1 + 0x6u));
        c.A1 = c.A1 + 0x2u;
        c.A0 = c.A0 + c.V0;
        c.V0 = (int)c.S1 < 3 ? 1u : 0u;
        if (c.V0 != 0u)
        {
            m.WriteU16((c.A0 + 0x2u), (ushort)c.V1);
            goto L801B4E54;
        }
        m.WriteU16((c.A0 + 0x2u), (ushort)c.V1);
        c.V0 = 0u | 0x0001u;
        m.WriteU16((c.S2 + 0x3Cu), (ushort)c.V0);
        c.V0 = 0u | 0x0002u;
        m.WriteU16((c.S2 + 0x2Cu), (ushort)c.V0);
        goto L801B50E4;
    L801B4EA8:;
        c.V0 = m.ReadU8((c.S2 + 0x48u));
        if (c.V0 == 0u)
        {
            c.A2 = 0u | 0x01F1u;
            goto L801B5030;
        }
        c.A2 = 0u | 0x01F1u;
        c.V0 = (uint)(short)m.ReadU16((c.S2 + 0x84u));
        c.S1 = 0u + 0u;
        c.V1 = c.V0 << 1;
        c.V1 = c.V1 + c.V0;
        c.V1 = c.V1 << 2;
        c.V0 = 0x80180000u;
        c.V0 = c.V0 + 0x1120u;
        c.A1 = c.V1 + c.V0;
    L801B4ED8:;
        c.A0 = c.A2 << 1;
        c.A2 = c.A2 + 0x30u;
        c.S1 = c.S1 + 0x1u;
        c.V0 = 0x80070000u;
        c.V0 = m.ReadU32((c.V0 + 0x3084u));
        c.V1 = m.ReadU16(c.A1);
        c.V0 = c.A0 + c.V0;
        m.WriteU16(c.V0, (ushort)c.V1);
        c.V0 = 0x80070000u;
        c.V0 = m.ReadU32((c.V0 + 0x3084u));
        c.V1 = m.ReadU16((c.A1 + 0x6u));
        c.A1 = c.A1 + 0x2u;
        c.A0 = c.A0 + c.V0;
        c.V0 = (int)c.S1 < 3 ? 1u : 0u;
        if (c.V0 != 0u)
        {
            m.WriteU16((c.A0 + 0x2u), (ushort)c.V1);
            goto L801B4ED8;
        }
        m.WriteU16((c.A0 + 0x2u), (ushort)c.V1);
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x3824u));
        c.A0 = 0u | 0x0644u;
        c.RA = 0x801B4F2Cu;
        Dispatcher.Call(c, m, c.V0);
        c.S3 = 0x80080000u;
        c.S3 = c.S3 - 0x27A8u;
        c.A0 = c.S3 + 0u;
        c.A1 = c.S3 + 0x1780u;
        c.RA = 0x801B4F40u;
        SoTN.AllocEntity_np3(c, m);
        c.S0 = c.V0 + 0u;
        if (c.S0 == 0u)
        {
            c.S1 = 0u + 0u;
            goto L801B4F8C;
        }
        c.S1 = 0u + 0u;
        c.A0 = 0u | 0x0002u;
        c.A1 = c.S2 + 0u;
        c.A2 = c.S0 + 0u;
        c.RA = 0x801B4F5Cu;
        SoTN.CreateEntityFromEntity_np3(c, m);
        c.V0 = 0u | 0x0013u;
        m.WriteU16((c.S0 + 0x30u), (ushort)c.V0);
        c.V0 = 0u | 0x00A9u;
        m.WriteU16((c.S0 + 0x24u), (ushort)c.V0);
        c.V1 = (uint)(short)m.ReadU16((c.S2 + 0x84u));
        c.V0 = m.ReadU16((c.S0 + 0x2u));
        c.A0 = m.ReadU16((c.S0 + 0x6u));
        c.V1 = c.V1 << 4;
        c.V0 = c.V0 + c.V1;
        c.A0 = c.A0 + 0x10u;
        m.WriteU16((c.S0 + 0x2u), (ushort)c.V0);
        m.WriteU16((c.S0 + 0x6u), (ushort)c.A0);
    L801B4F8C:;
        c.V0 = (uint)(short)m.ReadU16((c.S2 + 0x84u));
        c.S4 = c.S3 + 0u;
        c.V1 = c.V0 << 1;
        c.V1 = c.V1 + c.V0;
        c.V0 = 0x80180000u;
        c.V0 = c.V0 + 0x120Cu;
        c.S3 = c.V1 + c.V0;
        c.A0 = c.S4 + 0u;
    L801B4FAC:;
        c.A1 = c.S4 + 0x1780u;
        c.RA = 0x801B4FB4u;
        SoTN.AllocEntity_np3(c, m);
        c.S0 = c.V0 + 0u;
        if (c.S0 == 0u)
        {
            c.A0 = 0u | 0x0027u;
            goto L801B5010;
        }
        c.A0 = 0u | 0x0027u;
        c.A1 = c.S2 + 0u;
        c.A2 = c.S0 + 0u;
        c.RA = 0x801B4FCCu;
        SoTN.CreateEntityFromEntity_np3(c, m);
        c.V0 = m.ReadU8(c.S3);
        c.S3 = c.S3 + 0x1u;
        m.WriteU16((c.S0 + 0x30u), (ushort)c.V0);
        c.RA = 0x801B4FDCu;
        Dispatcher.Call(c, m, 0x801B90BCu);
        c.V0 = c.V0 << 8;
        c.V1 = 0xFFFF8000u;
        c.V1 = c.V1 - c.V0;
        m.WriteU32((c.S0 + 0x8u), c.V1);
        c.RA = 0x801B4FF0u;
        Dispatcher.Call(c, m, 0x801B90BCu);
        c.V0 = 0u - c.V0;
        c.V1 = m.ReadU16((c.S0 + 0x6u));
        c.V0 = c.V0 << 8;
        m.WriteU32((c.S0 + 0xCu), c.V0);
        c.V0 = c.S1 << 4;
        c.V1 = c.V1 - 0x10u;
        c.V1 = c.V1 + c.V0;
        m.WriteU16((c.S0 + 0x6u), (ushort)c.V1);
    L801B5010:;
        c.S1 = c.S1 + 0x1u;
        c.V0 = (int)c.S1 < 3 ? 1u : 0u;
        if (c.V0 != 0u)
        {
            c.A0 = c.S4 + 0u;
            goto L801B4FAC;
        }
        c.A0 = c.S4 + 0u;
        c.V0 = m.ReadU16((c.S2 + 0x84u));
        c.V0 = c.V0 + 0x1u;
        m.WriteU16((c.S2 + 0x84u), (ushort)c.V0);
    L801B5030:;
        c.V0 = (uint)(short)m.ReadU16((c.S2 + 0x84u));
        c.V0 = (int)c.V0 < 2 ? 1u : 0u;
        if (c.V0 != 0u)
        {
            goto L801B50E4;
        }
        c.A0 = 0x80080000u;
        c.A0 = c.A0 - 0x56A8u;
        c.A1 = c.A0 + 0x1780u;
        c.RA = 0x801B5054u;
        SoTN.AllocEntity_np3(c, m);
        c.S0 = c.V0 + 0u;
        if (c.S0 == 0u)
        {
            c.A0 = 0u | 0x000Au;
            goto L801B5074;
        }
        c.A0 = 0u | 0x000Au;
        c.A1 = c.S2 + 0u;
        c.A2 = c.S0 + 0u;
        c.RA = 0x801B506Cu;
        SoTN.CreateEntityFromEntity_np3(c, m);
        //c.V0 = 0u | 0x0043u;
        c.V0 = 0u | m.ReadU16(0x801B506C);
        m.WriteU16((c.S0 + 0x30u), (ushort)c.V0);
    L801B5074:;
        c.V1 = 0x80040000u;
        c.V1 = c.V1 - 0x41E1u;
        c.V0 = m.ReadU8(c.V1);
        c.V0 = c.V0 | 0x0001u;
        m.WriteU8(c.V1, (byte)c.V0);
        c.V0 = m.ReadU16((c.S2 + 0x2Cu));
        c.V1 = 0u | 0x0001u;
        m.WriteU16((c.S2 + 0x3Cu), (ushort)c.V1);
        c.V0 = c.V0 + 0x1u;
        m.WriteU16((c.S2 + 0x2Cu), (ushort)c.V0);
        goto L801B50E4;
    L801B50A4:;
        c.V0 = m.ReadU8((c.S2 + 0x48u));
        if (c.V0 == 0u)
        {
            goto L801B50E4;
        }
        c.V0 = 0x80070000u;
        c.V0 = m.ReadU32((c.V0 + 0x2F2Cu));
        c.V0 = c.V0 & 0x0004u;
        if (c.V0 == 0u)
        {
            goto L801B50E4;
        }
        c.V1 = 0x80040000u;
        c.V1 = c.V1 - 0x41E1u;
        c.V0 = m.ReadU8(c.V1);
        c.V0 = c.V0 | 0x0004u;
        m.WriteU8(c.V1, (byte)c.V0);
    L801B50E4:;
        c.RA = m.ReadU32((c.SP + 0x24u));
        c.S4 = m.ReadU32((c.SP + 0x20u));
        c.S3 = m.ReadU32((c.SP + 0x1Cu));
        c.S2 = m.ReadU32((c.SP + 0x18u));
        c.S1 = m.ReadU32((c.SP + 0x14u));
        c.S0 = m.ReadU32((c.SP + 0x10u));
        c.SP = c.SP + 0x28u;
        return;
    }

    // Turkey Drop in Entrance
    public static void EntityStairwayPiece_no3(CpuContext c, IMemory m)
    {
        c.SP = c.SP - 0x58u;
        m.WriteU32((c.SP + 0x44u), c.S1);
        c.S1 = c.A0 + 0u;
        m.WriteU32((c.SP + 0x54u), c.RA);
        m.WriteU32((c.SP + 0x50u), c.S4);
        m.WriteU32((c.SP + 0x4Cu), c.S3);
        m.WriteU32((c.SP + 0x48u), c.S2);
        m.WriteU32((c.SP + 0x40u), c.S0);
        c.V1 = m.ReadU16((c.S1 + 0x2Cu));
        c.V0 = c.V1 < 0x00000005u ? 1u : 0u;
        if (c.V0 == 0u)
        {
            c.V0 = c.V1 << 2;
            goto L801BB398;
        }
        c.V0 = c.V1 << 2;
        c.At = 0x801B0000u;
        c.At = c.At + c.V0;
        c.V0 = m.ReadU32((c.At + 0x7400u));
        switch (c.V0)
        {
            case 0x801BAF3Cu: goto L801BAF3C;
            case 0x801BAFF8u: goto L801BAFF8;
            case 0x801BB03Cu: goto L801BB03C;
            case 0x801BB240u: goto L801BB240;
            case 0x801BB2D8u: goto L801BB2D8;
            default: Dispatcher.Call(c, m, c.V0); return;
        }
    L801BAF3C:;
        c.A0 = 0x80180000u;
        c.A0 = c.A0 + 0xADCu;
        c.RA = 0x801BAF4Cu;
        SoTN.InitializeEntity_no3(c, m);
        c.V0 = 0u | 0x0008u;
        m.WriteU8((c.S1 + 0x46u), (byte)c.V0);
        m.WriteU8((c.S1 + 0x47u), (byte)c.V0);
        c.V1 = 0x80070000u;
        c.V1 = m.ReadU16((c.V1 + 0x308Eu));
        c.V0 = 0u | 0x0598u;
        c.V0 = c.V0 - c.V1;
        m.WriteU16((c.S1 + 0x2u), (ushort)c.V0);
        c.V1 = 0x80070000u;
        c.V1 = m.ReadU16((c.V1 + 0x3092u));
        c.V0 = 0u | 0x0010u;
        m.WriteU16((c.S1 + 0x3Eu), (ushort)c.V0);
        c.V0 = 0u | 0x00C8u;
        c.V0 = c.V0 - c.V1;
        m.WriteU16((c.S1 + 0x6u), (ushort)c.V0);
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU8((c.V0 - 0x41DCu));
        if (c.V0 == 0u)
        {
            c.V0 = 0u | 0x03EEu;
            goto L801BAFCC;
        }
        c.V0 = 0u | 0x03EEu;
        m.WriteU16((c.S1 + 0x3Cu), (ushort)0u);
        c.V1 = 0x80070000u;
        c.V1 = m.ReadU32((c.V1 + 0x3084u));
        m.WriteU16((c.V1 + 0x9B2u), (ushort)c.V0);
        c.V1 = 0x80070000u;
        c.V1 = m.ReadU32((c.V1 + 0x3084u));
        c.V0 = 0u | 0x03D2u;
        m.WriteU16((c.V1 + 0xA72u), (ushort)c.V0);
        c.V0 = 0u | 0x0020u;
        m.WriteU16((c.S1 + 0x2Cu), (ushort)c.V0);
        goto L801BB398;
    L801BAFCC:;
        c.V0 = 0u | 0x0002u;
        m.WriteU16((c.S1 + 0x3Cu), (ushort)c.V0);
        c.V1 = 0x80070000u;
        c.V1 = m.ReadU32((c.V1 + 0x3084u));
        c.V0 = 0u | 0x0408u;
        m.WriteU16((c.V1 + 0x9B2u), (ushort)c.V0);
        c.V1 = 0x80070000u;
        c.V1 = m.ReadU32((c.V1 + 0x3084u));
        c.V0 = 0u | 0x040Du;
        m.WriteU16((c.V1 + 0xA72u), (ushort)c.V0);
        goto L801BB398;
    L801BAFF8:;
        c.V0 = m.ReadU8((c.S1 + 0x48u));
        if (c.V0 == 0u)
        {
            goto L801BB01C;
        }
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x3824u));
        c.A0 = 0u | 0x064Bu;
        c.RA = 0x801BB01Cu;
        Dispatcher.Call(c, m, c.V0);
    L801BB01C:;
        c.V0 = m.ReadU32((c.S1 + 0x34u));
        c.V0 = c.V0 & 0x0100u;
        if (c.V0 == 0u)
        {
            goto L801BB398;
        }
        c.V0 = m.ReadU16((c.S1 + 0x2Cu));
        c.V0 = c.V0 + 0x1u;
        goto L801BB2D0;
    L801BB03C:;
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x3824u));
        c.A0 = 0u | 0x0644u;
        c.RA = 0x801BB050u;
        Dispatcher.Call(c, m, c.V0);
        c.S2 = 0x80080000u;
        c.S2 = c.S2 - 0x56A8u;
        c.A0 = c.S2 + 0u;
        c.V1 = 0x80070000u;
        c.V1 = m.ReadU32((c.V1 + 0x3084u));
        c.V0 = 0u | 0x03EEu;
        m.WriteU16((c.V1 + 0x9B2u), (ushort)c.V0);
        c.V1 = 0x80070000u;
        c.V1 = m.ReadU32((c.V1 + 0x3084u));
        c.V0 = 0u | 0x03D2u;
        m.WriteU16((c.V1 + 0xA72u), (ushort)c.V0);
        c.V0 = 0u | 0x0001u;
        c.At = 0x80040000u;
        m.WriteU8((c.At - 0x41DCu), (byte)c.V0);
        c.A1 = c.S2 + 0x1780u;
        c.RA = 0x801BB090u;
        Dispatcher.Call(c, m, 0x801C54D4u);
        c.S0 = c.V0 + 0u;
        if (c.S0 == 0u)
        {
            c.A0 = 0u | 0x000Au;
            goto L801BB0B0;
        }
        c.A0 = 0u | 0x000Au;
        c.A1 = c.S1 + 0u;
        c.A2 = c.S0 + 0u;
        c.RA = 0x801BB0A8u;
        SoTN.CreateEntityFromEntity_no3(c, m);
        //c.V0 = 0u | 0x0045u;
        c.V0 = 0u | m.ReadU16(0x801BB0A8);
        m.WriteU16((c.S0 + 0x30u), (ushort)c.V0);
    L801BB0B0:;
        c.A0 = c.S2 + 0x2F00u;
        c.A1 = c.S2 + 0x4680u;
        c.RA = 0x801BB0BCu;
        Dispatcher.Call(c, m, 0x801C54D4u);
        c.S0 = c.V0 + 0u;
        if (c.S0 == 0u)
        {
            c.A0 = 0u | 0x0006u;
            goto L801BB100;
        }
        c.A0 = 0u | 0x0006u;
        c.A1 = c.S1 + 0u;
        c.A2 = c.S0 + 0u;
        c.RA = 0x801BB0D4u;
        SoTN.CreateEntityFromEntity_no3(c, m);
        c.V0 = 0u | 0x0010u;
        m.WriteU16((c.S0 + 0x30u), (ushort)c.V0);
        c.V0 = m.ReadU16((c.S1 + 0x24u));
        c.V1 = m.ReadU16((c.S0 + 0x6u));
        c.V0 = c.V0 + 0x1u;
        m.WriteU16((c.S0 + 0x24u), (ushort)c.V0);
        c.V0 = m.ReadU16((c.S0 + 0x2u));
        c.V1 = c.V1 + 0x8u;
        m.WriteU16((c.S0 + 0x6u), (ushort)c.V1);
        c.V0 = c.V0 + 0x8u;
        m.WriteU16((c.S0 + 0x2u), (ushort)c.V0);
    L801BB100:;
        c.A0 = 0u | 0x0004u;
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x3848u));
        c.A1 = 0u | 0x0010u;
        c.RA = 0x801BB118u;
        Dispatcher.Call(c, m, c.V0);
        c.V0 = c.V0 << 16;
        c.A1 = (uint)((int)c.V0 >> 16);
        c.V0 = 0xFFFFFFFFu;
        if (c.A1 == c.V0)
        {
            c.V0 = c.A1 << 1;
            goto L801BB390;
        }
        c.V0 = c.A1 << 1;
        c.V0 = c.V0 + c.A1;
        c.V0 = c.V0 << 2;
        c.V0 = c.V0 + c.A1;
        c.V0 = c.V0 << 2;
        c.V1 = 0x80080000u;
        c.V1 = c.V1 + 0x6FECu;
        c.S0 = c.V0 + c.V1;
        c.A0 = c.S0 + 0u;
        c.V0 = m.ReadU32((c.S1 + 0x34u));
        c.V1 = 0x00800000u;
        m.WriteU32((c.S1 + 0x64u), c.A1);
        m.WriteU32((c.S1 + 0x7Cu), c.S0);
        c.V0 = c.V0 | c.V1;
        m.WriteU32((c.S1 + 0x34u), c.V0);
        c.RA = 0x801BB168u;
        SoTN.UnkPolyFunc2_no3(c, m);
        c.A0 = 0x80070000u;
        c.A0 = m.ReadU32((c.A0 + 0x3088u));
        c.V0 = m.ReadU32((c.A0 + 0x4u));
        c.V1 = m.ReadU8((c.V0 + 0x409u));
        c.V0 = m.ReadU32(c.A0);
        c.A0 = m.ReadU32((c.A0 + 0x8u));
        c.A1 = c.V1 << 4;
        c.A3 = c.A1 | 0x000Fu;
        c.V1 = c.V1 & 0x00F0u;
        c.A2 = c.V1 | 0x000Fu;
        c.V0 = m.ReadU8((c.V0 + 0x409u));
        c.A0 = m.ReadU8((c.A0 + 0x409u));
        m.WriteU8((c.S0 + 0x19u), (byte)c.V1);
        m.WriteU8((c.S0 + 0xDu), (byte)c.V1);
        c.V1 = m.ReadU32(c.S0);
        m.WriteU8((c.S0 + 0x24u), (byte)c.A1);
        m.WriteU8((c.S0 + 0xCu), (byte)c.A1);
        m.WriteU8((c.S0 + 0x30u), (byte)c.A3);
        m.WriteU8((c.S0 + 0x18u), (byte)c.A3);
        m.WriteU8((c.S0 + 0x31u), (byte)c.A2);
        m.WriteU8((c.S0 + 0x25u), (byte)c.A2);
        c.V0 = c.V0 + 0x8u;
        m.WriteU16((c.S0 + 0xEu), (ushort)c.A0);
        m.WriteU16((c.S0 + 0x1Au), (ushort)c.V0);
        c.V0 = m.ReadU16((c.S1 + 0x2u));
        m.WriteU16((c.V1 + 0x14u), (ushort)c.V0);
        c.V1 = m.ReadU32(c.S0);
        c.V0 = m.ReadU16((c.S1 + 0x6u));
        m.WriteU16((c.V1 + 0xAu), (ushort)c.V0);
        c.V0 = m.ReadU32(c.S0);
        c.V1 = 0xFFFF0000u;
        m.WriteU32((c.V0 + 0xCu), c.V1);
        c.V0 = m.ReadU32(c.S0);
        m.WriteU32((c.V0 + 0x10u), c.V1);
        c.V0 = m.ReadU32(c.S0);
        c.V1 = 0u | 0x0010u;
        m.WriteU16((c.V0 + 0x1Cu), (ushort)c.V1);
        c.V0 = m.ReadU32(c.S0);
        m.WriteU16((c.V0 + 0x1Eu), (ushort)c.V1);
        c.V0 = m.ReadU16((c.S1 + 0x24u));
        m.WriteU16((c.S0 + 0x26u), (ushort)c.V0);
        c.V0 = 0u | 0x0002u;
        m.WriteU16((c.S0 + 0x32u), (ushort)c.V0);
        c.V0 = m.ReadU16((c.S1 + 0x2Cu));
        c.V0 = c.V0 + 0x1u;
        m.WriteU16((c.S1 + 0x2Cu), (ushort)c.V0);
    L801BB240:;
        c.S0 = m.ReadU32((c.S1 + 0x7Cu));
        c.V1 = m.ReadU32(c.S0);
        c.V0 = m.ReadU16((c.V1 + 0x1Au));
        c.V0 = c.V0 - 0x20u;
        m.WriteU16((c.V1 + 0x1Au), (ushort)c.V0);
        c.V1 = m.ReadU32(c.S0);
        c.V0 = m.ReadU32((c.V1 + 0x10u));
        c.A0 = c.S0 + 0u;
        c.V0 = c.V0 + 0x2000u;
        m.WriteU32((c.V1 + 0x10u), c.V0);
        c.RA = 0x801BB27Cu;
        SoTN.UnkPrimHelper_no3(c, m);
        c.A2 = c.SP + 0x10u;
        c.V0 = m.ReadU32(c.S0);
        c.A3 = 0u + 0u;
        c.A0 = (uint)(short)m.ReadU16((c.V0 + 0x14u));
        c.S2 = m.ReadU16((c.V0 + 0xAu));
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x3844u));
        c.A1 = c.S2 + 0x8u;
        c.A1 = c.A1 << 16;
        c.A1 = (uint)((int)c.A1 >> 16);
        c.S0 = c.A0 + 0u;
        c.RA = 0x801BB2ACu;
        Dispatcher.Call(c, m, c.V0);
        c.V0 = m.ReadU32((c.SP + 0x10u));
        c.V0 = c.V0 & 0x0001u;
        if (c.V0 == 0u)
        {
            c.V1 = c.S2 - 0x4u;
            goto L801BB398;
        }
        c.V1 = c.S2 - 0x4u;
        c.V0 = m.ReadU16((c.S1 + 0x2Cu));
        m.WriteU16((c.S1 + 0x2u), (ushort)c.S0);
        m.WriteU16((c.S1 + 0x6u), (ushort)c.V1);
        c.V0 = c.V0 + 0x1u;
    L801BB2D0:;
        m.WriteU16((c.S1 + 0x2Cu), (ushort)c.V0);
        goto L801BB398;
    L801BB2D8:;
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x3824u));
        c.A0 = 0u | 0x0644u;
        c.RA = 0x801BB2ECu;
        Dispatcher.Call(c, m, c.V0);
        c.S3 = 0x80080000u;
        c.S3 = c.S3 - 0x27A8u;
        c.A0 = c.S3 + 0u;
        c.A1 = c.S3 + 0x1780u;
        c.RA = 0x801BB300u;
        Dispatcher.Call(c, m, 0x801C54D4u);
        c.S0 = c.V0 + 0u;
        if (c.S0 == 0u)
        {
            c.S2 = 0u + 0u;
            goto L801BB334;
        }
        c.S2 = 0u + 0u;
        c.A0 = 0u | 0x0002u;
        c.A1 = c.S1 + 0u;
        c.A2 = c.S0 + 0u;
        c.RA = 0x801BB31Cu;
        SoTN.CreateEntityFromEntity_no3(c, m);
        c.V0 = 0u | 0x0011u;
        m.WriteU16((c.S0 + 0x30u), (ushort)c.V0);
        c.V0 = m.ReadU16((c.S1 + 0x24u));
        c.V0 = c.V0 + 0x1u;
        m.WriteU16((c.S0 + 0x24u), (ushort)c.V0);
    L801BB334:;
        c.S4 = 0u | 0x0003u;
        c.A0 = c.S3 + 0u;
    L801BB33C:;
        c.A1 = c.S3 + 0x1780u;
        c.RA = 0x801BB344u;
        Dispatcher.Call(c, m, 0x801C54D4u);
        c.S0 = c.V0 + 0u;
        if (c.S0 == 0u)
        {
            c.S2 = c.S2 + 0x1u;
            goto L801BB384;
        }
        c.S2 = c.S2 + 0x1u;
        c.A0 = 0u | 0x005Du;
        c.A1 = c.S1 + 0u;
        c.A2 = c.S0 + 0u;
        c.RA = 0x801BB360u;
        SoTN.CreateEntityFromEntity_no3(c, m);
        c.RA = 0x801BB368u;
        SoTN.Random_no3(c, m);
        c.V0 = c.V0 & 0x0003u;
        m.WriteU16((c.S0 + 0x30u), (ushort)c.V0);
        c.V0 = m.ReadU16((c.S0 + 0x30u));
        if (c.V0 != c.S4)
        {
            c.V0 = (int)c.S2 < 6 ? 1u : 0u;
            goto L801BB388;
        }
        c.V0 = (int)c.S2 < 6 ? 1u : 0u;
        m.WriteU16((c.S0 + 0x30u), (ushort)0u);
    L801BB384:;
        c.V0 = (int)c.S2 < 6 ? 1u : 0u;
    L801BB388:;
        if (c.V0 != 0u)
        {
            c.A0 = c.S3 + 0u;
            goto L801BB33C;
        }
        c.A0 = c.S3 + 0u;
    L801BB390:;
        c.A0 = c.S1 + 0u;
        c.RA = 0x801BB398u;
        SoTN.DestroyEntity_no3(c, m);
    L801BB398:;
        c.RA = m.ReadU32((c.SP + 0x54u));
        c.S4 = m.ReadU32((c.SP + 0x50u));
        c.S3 = m.ReadU32((c.SP + 0x4Cu));
        c.S2 = m.ReadU32((c.SP + 0x48u));
        c.S1 = m.ReadU32((c.SP + 0x44u));
        c.S0 = m.ReadU32((c.SP + 0x40u));
        c.SP = c.SP + 0x58u;
        return;
    }

    // Turkey Drop
    public static void EntityStairwayPiece_np3(CpuContext c, IMemory m)
    {
        c.SP = c.SP - 0x58u;
        m.WriteU32((c.SP + 0x44u), c.S1);
        c.S1 = c.A0 + 0u;
        m.WriteU32((c.SP + 0x54u), c.RA);
        m.WriteU32((c.SP + 0x50u), c.S4);
        m.WriteU32((c.SP + 0x4Cu), c.S3);
        m.WriteU32((c.SP + 0x48u), c.S2);
        m.WriteU32((c.SP + 0x40u), c.S0);
        c.V1 = m.ReadU16((c.S1 + 0x2Cu));
        c.V0 = c.V1 < 0x00000005u ? 1u : 0u;
        if (c.V0 == 0u)
        {
            c.V0 = c.V1 << 2;
            goto L801B5C38;
        }
        c.V0 = c.V1 << 2;
        c.At = 0x801B0000u;
        c.At = c.At + c.V0;
        c.V0 = m.ReadU32((c.At + 0x1EA8u));
        switch (c.V0)
        {
            case 0x801B57DCu: goto L801B57DC;
            case 0x801B5898u: goto L801B5898;
            case 0x801B58DCu: goto L801B58DC;
            case 0x801B5AE0u: goto L801B5AE0;
            case 0x801B5B78u: goto L801B5B78;
            default: Dispatcher.Call(c, m, c.V0); return;
        }
    L801B57DC:;
        c.A0 = 0x80180000u;
        c.A0 = c.A0 + 0xA6Cu;
        c.RA = 0x801B57ECu;
        SoTN.InitializeEntity_np3(c, m);
        c.V0 = 0u | 0x0008u;
        m.WriteU8((c.S1 + 0x46u), (byte)c.V0);
        m.WriteU8((c.S1 + 0x47u), (byte)c.V0);
        c.V1 = 0x80070000u;
        c.V1 = m.ReadU16((c.V1 + 0x308Eu));
        c.V0 = 0u | 0x0598u;
        c.V0 = c.V0 - c.V1;
        m.WriteU16((c.S1 + 0x2u), (ushort)c.V0);
        c.V1 = 0x80070000u;
        c.V1 = m.ReadU16((c.V1 + 0x3092u));
        c.V0 = 0u | 0x0010u;
        m.WriteU16((c.S1 + 0x3Eu), (ushort)c.V0);
        c.V0 = 0u | 0x00C8u;
        c.V0 = c.V0 - c.V1;
        m.WriteU16((c.S1 + 0x6u), (ushort)c.V0);
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU8((c.V0 - 0x41DCu));
        if (c.V0 == 0u)
        {
            c.V0 = 0u | 0x03EEu;
            goto L801B586C;
        }
        c.V0 = 0u | 0x03EEu;
        m.WriteU16((c.S1 + 0x3Cu), (ushort)0u);
        c.V1 = 0x80070000u;
        c.V1 = m.ReadU32((c.V1 + 0x3084u));
        m.WriteU16((c.V1 + 0x9B2u), (ushort)c.V0);
        c.V1 = 0x80070000u;
        c.V1 = m.ReadU32((c.V1 + 0x3084u));
        c.V0 = 0u | 0x03D2u;
        m.WriteU16((c.V1 + 0xA72u), (ushort)c.V0);
        c.V0 = 0u | 0x0020u;
        m.WriteU16((c.S1 + 0x2Cu), (ushort)c.V0);
        goto L801B5C38;
    L801B586C:;
        c.V0 = 0u | 0x0002u;
        m.WriteU16((c.S1 + 0x3Cu), (ushort)c.V0);
        c.V1 = 0x80070000u;
        c.V1 = m.ReadU32((c.V1 + 0x3084u));
        c.V0 = 0u | 0x0408u;
        m.WriteU16((c.V1 + 0x9B2u), (ushort)c.V0);
        c.V1 = 0x80070000u;
        c.V1 = m.ReadU32((c.V1 + 0x3084u));
        c.V0 = 0u | 0x040Du;
        m.WriteU16((c.V1 + 0xA72u), (ushort)c.V0);
        goto L801B5C38;
    L801B5898:;
        c.V0 = m.ReadU8((c.S1 + 0x48u));
        if (c.V0 == 0u)
        {
            goto L801B58BC;
        }
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x3824u));
        c.A0 = 0u | 0x064Bu;
        c.RA = 0x801B58BCu;
        Dispatcher.Call(c, m, c.V0);
    L801B58BC:;
        c.V0 = m.ReadU32((c.S1 + 0x34u));
        c.V0 = c.V0 & 0x0100u;
        if (c.V0 == 0u)
        {
            goto L801B5C38;
        }
        c.V0 = m.ReadU16((c.S1 + 0x2Cu));
        c.V0 = c.V0 + 0x1u;
        goto L801B5B70;
    L801B58DC:;
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x3824u));
        c.A0 = 0u | 0x0644u;
        c.RA = 0x801B58F0u;
        Dispatcher.Call(c, m, c.V0);
        c.S2 = 0x80080000u;
        c.S2 = c.S2 - 0x56A8u;
        c.A0 = c.S2 + 0u;
        c.V1 = 0x80070000u;
        c.V1 = m.ReadU32((c.V1 + 0x3084u));
        c.V0 = 0u | 0x03EEu;
        m.WriteU16((c.V1 + 0x9B2u), (ushort)c.V0);
        c.V1 = 0x80070000u;
        c.V1 = m.ReadU32((c.V1 + 0x3084u));
        c.V0 = 0u | 0x03D2u;
        m.WriteU16((c.V1 + 0xA72u), (ushort)c.V0);
        c.V0 = 0u | 0x0001u;
        c.At = 0x80040000u;
        m.WriteU8((c.At - 0x41DCu), (byte)c.V0);
        c.A1 = c.S2 + 0x1780u;
        c.RA = 0x801B5930u;
        SoTN.AllocEntity_np3(c, m);
        c.S0 = c.V0 + 0u;
        if (c.S0 == 0u)
        {
            c.A0 = 0u | 0x000Au;
            goto L801B5950;
        }
        c.A0 = 0u | 0x000Au;
        c.A1 = c.S1 + 0u;
        c.A2 = c.S0 + 0u;
        c.RA = 0x801B5948u;
        SoTN.CreateEntityFromEntity_np3(c, m);
        //c.V0 = 0u | 0x0045u;
        c.V0 = 0u | m.ReadU16(0x801B5948);
        m.WriteU16((c.S0 + 0x30u), (ushort)c.V0);
    L801B5950:;
        c.A0 = c.S2 + 0x2F00u;
        c.A1 = c.S2 + 0x4680u;
        c.RA = 0x801B595Cu;
        SoTN.AllocEntity_np3(c, m);
        c.S0 = c.V0 + 0u;
        if (c.S0 == 0u)
        {
            c.A0 = 0u | 0x0006u;
            goto L801B59A0;
        }
        c.A0 = 0u | 0x0006u;
        c.A1 = c.S1 + 0u;
        c.A2 = c.S0 + 0u;
        c.RA = 0x801B5974u;
        SoTN.CreateEntityFromEntity_np3(c, m);
        c.V0 = 0u | 0x0010u;
        m.WriteU16((c.S0 + 0x30u), (ushort)c.V0);
        c.V0 = m.ReadU16((c.S1 + 0x24u));
        c.V1 = m.ReadU16((c.S0 + 0x6u));
        c.V0 = c.V0 + 0x1u;
        m.WriteU16((c.S0 + 0x24u), (ushort)c.V0);
        c.V0 = m.ReadU16((c.S0 + 0x2u));
        c.V1 = c.V1 + 0x8u;
        m.WriteU16((c.S0 + 0x6u), (ushort)c.V1);
        c.V0 = c.V0 + 0x8u;
        m.WriteU16((c.S0 + 0x2u), (ushort)c.V0);
    L801B59A0:;
        c.A0 = 0u | 0x0004u;
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x3848u));
        c.A1 = 0u | 0x0010u;
        c.RA = 0x801B59B8u;
        Dispatcher.Call(c, m, c.V0);
        c.V0 = c.V0 << 16;
        c.A1 = (uint)((int)c.V0 >> 16);
        c.V0 = 0xFFFFFFFFu;
        if (c.A1 == c.V0)
        {
            c.V0 = c.A1 << 1;
            goto L801B5C30;
        }
        c.V0 = c.A1 << 1;
        c.V0 = c.V0 + c.A1;
        c.V0 = c.V0 << 2;
        c.V0 = c.V0 + c.A1;
        c.V0 = c.V0 << 2;
        c.V1 = 0x80080000u;
        c.V1 = c.V1 + 0x6FECu;
        c.S0 = c.V0 + c.V1;
        c.A0 = c.S0 + 0u;
        c.V0 = m.ReadU32((c.S1 + 0x34u));
        c.V1 = 0x00800000u;
        m.WriteU32((c.S1 + 0x64u), c.A1);
        m.WriteU32((c.S1 + 0x7Cu), c.S0);
        c.V0 = c.V0 | c.V1;
        m.WriteU32((c.S1 + 0x34u), c.V0);
        c.RA = 0x801B5A08u;
        SoTN.UnkPolyFunc2_np3(c, m);
        c.A0 = 0x80070000u;
        c.A0 = m.ReadU32((c.A0 + 0x3088u));
        c.V0 = m.ReadU32((c.A0 + 0x4u));
        c.V1 = m.ReadU8((c.V0 + 0x409u));
        c.V0 = m.ReadU32(c.A0);
        c.A0 = m.ReadU32((c.A0 + 0x8u));
        c.A1 = c.V1 << 4;
        c.A3 = c.A1 | 0x000Fu;
        c.V1 = c.V1 & 0x00F0u;
        c.A2 = c.V1 | 0x000Fu;
        c.V0 = m.ReadU8((c.V0 + 0x409u));
        c.A0 = m.ReadU8((c.A0 + 0x409u));
        m.WriteU8((c.S0 + 0x19u), (byte)c.V1);
        m.WriteU8((c.S0 + 0xDu), (byte)c.V1);
        c.V1 = m.ReadU32(c.S0);
        m.WriteU8((c.S0 + 0x24u), (byte)c.A1);
        m.WriteU8((c.S0 + 0xCu), (byte)c.A1);
        m.WriteU8((c.S0 + 0x30u), (byte)c.A3);
        m.WriteU8((c.S0 + 0x18u), (byte)c.A3);
        m.WriteU8((c.S0 + 0x31u), (byte)c.A2);
        m.WriteU8((c.S0 + 0x25u), (byte)c.A2);
        c.V0 = c.V0 + 0x8u;
        m.WriteU16((c.S0 + 0xEu), (ushort)c.A0);
        m.WriteU16((c.S0 + 0x1Au), (ushort)c.V0);
        c.V0 = m.ReadU16((c.S1 + 0x2u));
        m.WriteU16((c.V1 + 0x14u), (ushort)c.V0);
        c.V1 = m.ReadU32(c.S0);
        c.V0 = m.ReadU16((c.S1 + 0x6u));
        m.WriteU16((c.V1 + 0xAu), (ushort)c.V0);
        c.V0 = m.ReadU32(c.S0);
        c.V1 = 0xFFFF0000u;
        m.WriteU32((c.V0 + 0xCu), c.V1);
        c.V0 = m.ReadU32(c.S0);
        m.WriteU32((c.V0 + 0x10u), c.V1);
        c.V0 = m.ReadU32(c.S0);
        c.V1 = 0u | 0x0010u;
        m.WriteU16((c.V0 + 0x1Cu), (ushort)c.V1);
        c.V0 = m.ReadU32(c.S0);
        m.WriteU16((c.V0 + 0x1Eu), (ushort)c.V1);
        c.V0 = m.ReadU16((c.S1 + 0x24u));
        m.WriteU16((c.S0 + 0x26u), (ushort)c.V0);
        c.V0 = 0u | 0x0002u;
        m.WriteU16((c.S0 + 0x32u), (ushort)c.V0);
        c.V0 = m.ReadU16((c.S1 + 0x2Cu));
        c.V0 = c.V0 + 0x1u;
        m.WriteU16((c.S1 + 0x2Cu), (ushort)c.V0);
    L801B5AE0:;
        c.S0 = m.ReadU32((c.S1 + 0x7Cu));
        c.V1 = m.ReadU32(c.S0);
        c.V0 = m.ReadU16((c.V1 + 0x1Au));
        c.V0 = c.V0 - 0x20u;
        m.WriteU16((c.V1 + 0x1Au), (ushort)c.V0);
        c.V1 = m.ReadU32(c.S0);
        c.V0 = m.ReadU32((c.V1 + 0x10u));
        c.A0 = c.S0 + 0u;
        c.V0 = c.V0 + 0x2000u;
        m.WriteU32((c.V1 + 0x10u), c.V0);
        c.RA = 0x801B5B1Cu;
        SoTN.UnkPrimHelper_np3(c, m);
        c.A2 = c.SP + 0x10u;
        c.V0 = m.ReadU32(c.S0);
        c.A3 = 0u + 0u;
        c.A0 = (uint)(short)m.ReadU16((c.V0 + 0x14u));
        c.S2 = m.ReadU16((c.V0 + 0xAu));
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x3844u));
        c.A1 = c.S2 + 0x8u;
        c.A1 = c.A1 << 16;
        c.A1 = (uint)((int)c.A1 >> 16);
        c.S0 = c.A0 + 0u;
        c.RA = 0x801B5B4Cu;
        Dispatcher.Call(c, m, c.V0);
        c.V0 = m.ReadU32((c.SP + 0x10u));
        c.V0 = c.V0 & 0x0001u;
        if (c.V0 == 0u)
        {
            c.V1 = c.S2 - 0x4u;
            goto L801B5C38;
        }
        c.V1 = c.S2 - 0x4u;
        c.V0 = m.ReadU16((c.S1 + 0x2Cu));
        m.WriteU16((c.S1 + 0x2u), (ushort)c.S0);
        m.WriteU16((c.S1 + 0x6u), (ushort)c.V1);
        c.V0 = c.V0 + 0x1u;
    L801B5B70:;
        m.WriteU16((c.S1 + 0x2Cu), (ushort)c.V0);
        goto L801B5C38;
    L801B5B78:;
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x3824u));
        c.A0 = 0u | 0x0644u;
        c.RA = 0x801B5B8Cu;
        Dispatcher.Call(c, m, c.V0);
        c.S3 = 0x80080000u;
        c.S3 = c.S3 - 0x27A8u;
        c.A0 = c.S3 + 0u;
        c.A1 = c.S3 + 0x1780u;
        c.RA = 0x801B5BA0u;
        SoTN.AllocEntity_np3(c, m);
        c.S0 = c.V0 + 0u;
        if (c.S0 == 0u)
        {
            c.S2 = 0u + 0u;
            goto L801B5BD4;
        }
        c.S2 = 0u + 0u;
        c.A0 = 0u | 0x0002u;
        c.A1 = c.S1 + 0u;
        c.A2 = c.S0 + 0u;
        c.RA = 0x801B5BBCu;
        SoTN.CreateEntityFromEntity_np3(c, m);
        c.V0 = 0u | 0x0011u;
        m.WriteU16((c.S0 + 0x30u), (ushort)c.V0);
        c.V0 = m.ReadU16((c.S1 + 0x24u));
        c.V0 = c.V0 + 0x1u;
        m.WriteU16((c.S0 + 0x24u), (ushort)c.V0);
    L801B5BD4:;
        c.S4 = 0u | 0x0003u;
        c.A0 = c.S3 + 0u;
    L801B5BDC:;
        c.A1 = c.S3 + 0x1780u;
        c.RA = 0x801B5BE4u;
        SoTN.AllocEntity_np3(c, m);
        c.S0 = c.V0 + 0u;
        if (c.S0 == 0u)
        {
            c.S2 = c.S2 + 0x1u;
            goto L801B5C24;
        }
        c.S2 = c.S2 + 0x1u;
        c.A0 = 0u | 0x004Cu;
        c.A1 = c.S1 + 0u;
        c.A2 = c.S0 + 0u;
        c.RA = 0x801B5C00u;
        SoTN.CreateEntityFromEntity_np3(c, m);
        c.RA = 0x801B5C08u;
        Dispatcher.Call(c, m, 0x801B90BCu);
        c.V0 = c.V0 & 0x0003u;
        m.WriteU16((c.S0 + 0x30u), (ushort)c.V0);
        c.V0 = m.ReadU16((c.S0 + 0x30u));
        if (c.V0 != c.S4)
        {
            c.V0 = (int)c.S2 < 6 ? 1u : 0u;
            goto L801B5C28;
        }
        c.V0 = (int)c.S2 < 6 ? 1u : 0u;
        m.WriteU16((c.S0 + 0x30u), (ushort)0u);
    L801B5C24:;
        c.V0 = (int)c.S2 < 6 ? 1u : 0u;
    L801B5C28:;
        if (c.V0 != 0u)
        {
            c.A0 = c.S3 + 0u;
            goto L801B5BDC;
        }
        c.A0 = c.S3 + 0u;
    L801B5C30:;
        c.A0 = c.S1 + 0u;
        c.RA = 0x801B5C38u;
        SoTN.DestroyEntity_np3(c, m);
    L801B5C38:;
        c.RA = m.ReadU32((c.SP + 0x54u));
        c.S4 = m.ReadU32((c.SP + 0x50u));
        c.S3 = m.ReadU32((c.SP + 0x4Cu));
        c.S2 = m.ReadU32((c.SP + 0x48u));
        c.S1 = m.ReadU32((c.SP + 0x44u));
        c.S0 = m.ReadU32((c.SP + 0x40u));
        c.SP = c.SP + 0x58u;
        return;
    }

    // Bone Scimitar Drops
    public static void EntityBoneScimitar_no3(CpuContext c, IMemory m)
    {
        c.SP = c.SP - 0x28u;
        m.WriteU32((c.SP + 0x14u), c.S1);
        c.S1 = c.A0 + 0u;
        m.WriteU32((c.SP + 0x24u), c.RA);
        m.WriteU32((c.SP + 0x20u), c.S4);
        m.WriteU32((c.SP + 0x1Cu), c.S3);
        m.WriteU32((c.SP + 0x18u), c.S2);
        m.WriteU32((c.SP + 0x10u), c.S0);
        c.V0 = m.ReadU32((c.S1 + 0x34u));
        c.V0 = c.V0 & 0x0100u;
        if (c.V0 == 0u)
        {
            c.V0 = 0u | 0x0007u;
            goto L801D5AE4;
        }
        c.V0 = 0u | 0x0007u;
        m.WriteU16((c.S1 + 0x2Cu), (ushort)c.V0);
    L801D5AE4:;
        c.V1 = m.ReadU16((c.S1 + 0x2Cu));
        c.V0 = c.V1 < 0x00000008u ? 1u : 0u;
        if (c.V0 == 0u)
        {
            c.V0 = c.V1 << 2;
            goto L801D6138;
        }
        c.V0 = c.V1 << 2;
        c.At = 0x801B0000u;
        c.At = c.At + c.V0;
        c.V0 = m.ReadU32((c.At + 0x7784u));
        switch (c.V0)
        {
            case 0x801D5B10u: goto L801D5B10;
            case 0x801D5B8Cu: goto L801D5B8C;
            case 0x801D5BB8u: goto L801D5BB8;
            case 0x801D5C20u: goto L801D5C20;
            case 0x801D5C98u: goto L801D5C98;
            case 0x801D5D74u: goto L801D5D74;
            case 0x801D5E84u: goto L801D5E84;
            case 0x801D5FD0u: goto L801D5FD0;
            default: Dispatcher.Call(c, m, c.V0); return;
        }
    L801D5B10:;
        c.A0 = 0x80180000u;
        c.A0 = c.A0 + 0xB78u;
        c.RA = 0x801D5B20u;
        SoTN.InitializeEntity_no3(c, m);
        c.V0 = m.ReadU16((c.S1 + 0x30u));
        if (c.V0 == 0u)
        {
            c.A1 = 0x3FFF0000u;
            goto L801D5B78;
        }
        c.A1 = 0x3FFF0000u;
        c.A1 = c.A1 | 0xF3FFu;
        c.V0 = m.ReadU16((c.S1 + 0x16u));
        c.A0 = m.ReadU16((c.S1 + 0x30u));
        c.V1 = m.ReadU32((c.S1 + 0x34u));
        c.V0 = c.V0 + c.A0;
        c.V1 = c.V1 & c.A1;
        m.WriteU16((c.S1 + 0x16u), (ushort)c.V0);
        m.WriteU32((c.S1 + 0x34u), c.V1);
        c.V0 = 0x80070000u;
        c.V0 = (uint)(short)m.ReadU16((c.V0 + 0x308Eu));
        c.A0 = (uint)(short)m.ReadU16((c.S1 + 0x2u));
        c.V1 = m.ReadU16((c.S1 + 0x30u));
        c.A1 = 0x80180000u;
        c.A1 = m.ReadU32((c.A1 + 0x3B50u));
        c.V0 = c.V0 + c.A0;
        c.V1 = c.V1 & c.A1;
        if (c.V1 != 0u)
        {
            m.WriteU32((c.S1 + 0x9Cu), c.V0);
            goto L801D6130;
        }
        m.WriteU32((c.S1 + 0x9Cu), c.V0);
    L801D5B78:;
        c.V0 = 0u | 0x0050u;
        m.WriteU8((c.S1 + 0x7Cu), (byte)c.V0);
        m.WriteU8((c.S1 + 0x80u), (byte)0u);
        m.WriteU8((c.S1 + 0x84u), (byte)0u);
        goto L801D6138;
    L801D5B8C:;
        c.A0 = 0x80180000u;
        c.A0 = c.A0 + 0x3C20u;
        c.RA = 0x801D5B9Cu;
        Dispatcher.Call(c, m, 0x801C5074u);
        if (c.V0 == 0u)
        {
            goto L801D6138;
        }
        c.V0 = m.ReadU16((c.S1 + 0x2Cu));
        c.V1 = m.ReadU16((c.S1 + 0x30u));
        c.V0 = c.V0 + 0x1u;
        m.WriteU16((c.S1 + 0x2Cu), (ushort)c.V0);
        goto L801D5D5C;
    L801D5BB8:;
        c.A0 = 0x80180000u;
        c.A0 = c.A0 + 0x3B54u;
        c.A1 = c.S1 + 0u;
        c.RA = 0x801D5BC8u;
        Dispatcher.Call(c, m, 0x801C4D94u);
        if (c.V0 != 0u)
        {
            goto L801D5BE4;
        }
        c.RA = 0x801D5BD8u;
        Dispatcher.Call(c, m, 0x801C4FD4u);
        c.V0 = c.V0 & 0x0001u;
        c.V0 = c.V0 ^ 0x0001u;
        m.WriteU16((c.S1 + 0x14u), (ushort)c.V0);
    L801D5BE4:;
        c.V0 = m.ReadU8((c.S1 + 0x14u));
        m.WriteU8((c.S1 + 0x80u), (byte)c.V0);
        c.V0 = m.ReadU8((c.S1 + 0x80u));
        if (c.V0 != 0u)
        {
            c.V0 = 0u | 0x8000u;
            goto L801D5C04;
        }
        c.V0 = 0u | 0x8000u;
        c.V0 = 0xFFFF8000u;
    L801D5C04:;
        m.WriteU32((c.S1 + 0x8u), c.V0);
        c.RA = 0x801D5C0Cu;
        Dispatcher.Call(c, m, 0x801C4F64u);
        c.V0 = (int)c.V0 < 76 ? 1u : 0u;
        if (c.V0 == 0u)
        {
            c.V0 = 0u | 0x0003u;
            goto L801D5C88;
        }
        c.V0 = 0u | 0x0003u;
        m.WriteU16((c.S1 + 0x2Cu), (ushort)c.V0);
        goto L801D5C88;
    L801D5C20:;
        c.A0 = 0x80180000u;
        c.A0 = c.A0 + 0x3B64u;
        c.A1 = c.S1 + 0u;
        c.RA = 0x801D5C30u;
        Dispatcher.Call(c, m, 0x801C4D94u);
        if (c.V0 != 0u)
        {
            goto L801D5C4C;
        }
        c.RA = 0x801D5C40u;
        Dispatcher.Call(c, m, 0x801C4FD4u);
        c.V0 = c.V0 & 0x0001u;
        c.V0 = c.V0 ^ 0x0001u;
        m.WriteU16((c.S1 + 0x14u), (ushort)c.V0);
    L801D5C4C:;
        c.V0 = m.ReadU8((c.S1 + 0x14u));
        c.V0 = c.V0 ^ 0x0001u;
        m.WriteU8((c.S1 + 0x80u), (byte)c.V0);
        c.V0 = m.ReadU8((c.S1 + 0x80u));
        if (c.V0 != 0u)
        {
            c.V0 = 0u | 0x8000u;
            goto L801D5C70;
        }
        c.V0 = 0u | 0x8000u;
        c.V0 = 0xFFFF8000u;
    L801D5C70:;
        m.WriteU32((c.S1 + 0x8u), c.V0);
        c.RA = 0x801D5C78u;
        Dispatcher.Call(c, m, 0x801C4F64u);
        c.V0 = (int)c.V0 < 93 ? 1u : 0u;
        if (c.V0 != 0u)
        {
            c.V0 = 0u | 0x0002u;
            goto L801D5C88;
        }
        c.V0 = 0u | 0x0002u;
        m.WriteU16((c.S1 + 0x2Cu), (ushort)c.V0);
    L801D5C88:;
        c.RA = 0x801D5C90u;
        SoTN.BoneScimitarAttackCheck_no3(c, m);
        goto L801D6138;
    L801D5C98:;
        c.A0 = 0x80180000u;
        c.A0 = c.A0 + 0x3B74u;
        c.A1 = c.S1 + 0u;
        c.RA = 0x801D5CA8u;
        Dispatcher.Call(c, m, 0x801C4D94u);
        c.S0 = c.V0 + 0u;
        c.V1 = (uint)(short)m.ReadU16((c.S1 + 0x56u));
        c.V0 = 0u | 0x000Cu;
        if (c.V1 != c.V0)
        {
            c.V0 = 0u | 0x0008u;
            goto L801D5CE0;
        }
        c.V0 = 0u | 0x0008u;
        c.V0 = 0u | 0x0014u;
        m.WriteU8((c.S1 + 0x46u), (byte)c.V0);
        c.V0 = 0u | 0x0011u;
        m.WriteU8((c.S1 + 0x47u), (byte)c.V0);
        c.V0 = 0xFFFFFFF5u;
        m.WriteU16((c.S1 + 0x10u), (ushort)c.V0);
        c.V0 = 0xFFFFFFF2u;
        m.WriteU16((c.S1 + 0x12u), (ushort)c.V0);
        goto L801D5CF8;
    L801D5CE0:;
        m.WriteU8((c.S1 + 0x46u), (byte)c.V0);
        c.V0 = 0u | 0x0012u;
        m.WriteU8((c.S1 + 0x47u), (byte)c.V0);
        c.V0 = 0xFFFFFFFFu;
        m.WriteU16((c.S1 + 0x10u), (ushort)c.V0);
        m.WriteU16((c.S1 + 0x12u), (ushort)0u);
    L801D5CF8:;
        c.V1 = m.ReadU32((c.S1 + 0x50u));
        c.V0 = 0u | 0x0007u;
        if (c.V1 != c.V0)
        {
            c.V0 = c.S0 & 0x00FFu;
            goto L801D5D14;
        }
        c.V0 = c.S0 & 0x00FFu;
        c.A0 = 0u | 0x066Du;
        c.RA = 0x801D5D10u;
        SoTN.PlaySfxPositional_no3(c, m);
        c.V0 = c.S0 & 0x00FFu;
    L801D5D14:;
        if (c.V0 != 0u)
        {
            goto L801D6138;
        }
        c.A0 = 0u | 0x0003u;
        c.RA = 0x801D5D24u;
        SoTN.SetStep_no3(c, m);
        c.A0 = 0x80180000u;
        c.A0 = c.A0 + 0x3C18u;
        c.V1 = m.ReadU8((c.S1 + 0x84u));
        c.V0 = m.ReadU16((c.S1 + 0x30u));
        c.V1 = c.V1 + 0x1u;
        c.V0 = c.V0 & 0x0001u;
        c.V0 = c.V0 << 2;
        c.V0 = c.V0 + c.A0;
        m.WriteU8((c.S1 + 0x84u), (byte)c.V1);
        c.V1 = c.V1 & 0x0003u;
        c.V0 = c.V0 + c.V1;
        c.V0 = m.ReadU8(c.V0);
        c.V1 = m.ReadU16((c.S1 + 0x30u));
        m.WriteU8((c.S1 + 0x7Cu), (byte)c.V0);
    L801D5D5C:;
        if (c.V1 == 0u)
        {
            goto L801D6138;
        }
        c.A0 = 0u | 0x0006u;
        c.RA = 0x801D5D6Cu;
        SoTN.SetStep_no3(c, m);
        goto L801D6138;
    L801D5D74:;
        c.V1 = m.ReadU16((c.S1 + 0x2Eu));
        c.V0 = 0u | 0x0001u;
        if (c.V1 == c.V0)
        {
            c.V0 = (int)c.V1 < 2 ? 1u : 0u;
            goto L801D5E1C;
        }
        c.V0 = (int)c.V1 < 2 ? 1u : 0u;
        if (c.V0 == 0u)
        {
            goto L801D5D9C;
        }
        if (c.V1 == 0u)
        {
            goto L801D5DB0;
        }
        goto L801D6138;
    L801D5D9C:;
        c.V0 = 0u | 0x0002u;
        if (c.V1 == c.V0)
        {
            goto L801D5E5C;
        }
        goto L801D6138;
    L801D5DB0:;
        c.A0 = 0x80180000u;
        c.A0 = c.A0 + 0x3B90u;
        c.A1 = c.S1 + 0u;
        c.RA = 0x801D5DC0u;
        Dispatcher.Call(c, m, 0x801C4D94u);
        c.V0 = c.V0 & 0x0001u;
        if (c.V0 != 0u)
        {
            goto L801D6138;
        }
        c.S0 = m.ReadU8((c.S1 + 0x80u));
        c.RA = 0x801D5DD8u;
        SoTN.Random_no3(c, m);
        c.V0 = c.V0 & 0x0003u;
        if (c.V0 != 0u)
        {
            c.V0 = c.S0 & 0x00FFu;
            goto L801D5DEC;
        }
        c.V0 = c.S0 & 0x00FFu;
        c.S0 = c.S0 ^ 0x0001u;
        c.V0 = c.S0 & 0x00FFu;
    L801D5DEC:;
        if (c.V0 != 0u)
        {
            c.V0 = 0x00020000u;
            goto L801D5DF8;
        }
        c.V0 = 0x00020000u;
        c.V0 = 0xFFFE0000u;
    L801D5DF8:;
        m.WriteU32((c.S1 + 0x8u), c.V0);
        c.V1 = m.ReadU16((c.S1 + 0x2Eu));
        c.V0 = 0xFFFD0000u;
        m.WriteU32((c.S1 + 0xCu), c.V0);
        m.WriteU16((c.S1 + 0x50u), (ushort)0u);
        m.WriteU16((c.S1 + 0x52u), (ushort)0u);
        c.V1 = c.V1 + 0x1u;
        m.WriteU16((c.S1 + 0x2Eu), (ushort)c.V1);
        goto L801D6138;
    L801D5E1C:;
        c.A0 = 0x80180000u;
        c.A0 = c.A0 + 0x3C20u;
        c.RA = 0x801D5E2Cu;
        Dispatcher.Call(c, m, 0x801C5074u);
        if (c.V0 == 0u)
        {
            goto L801D5E44;
        }
        c.V0 = m.ReadU16((c.S1 + 0x2Eu));
        c.V0 = c.V0 + 0x1u;
        m.WriteU16((c.S1 + 0x2Eu), (ushort)c.V0);
    L801D5E44:;
        c.A0 = 0x80180000u;
        c.A0 = c.A0 + 0x3C38u;
        c.A1 = 0u | 0x0002u;
        c.RA = 0x801D5E54u;
        Dispatcher.Call(c, m, 0x801C5BC0u);
        goto L801D6138;
    L801D5E5C:;
        c.A0 = 0x80180000u;
        c.A0 = c.A0 + 0x3B9Cu;
        c.A1 = c.S1 + 0u;
        c.RA = 0x801D5E6Cu;
        Dispatcher.Call(c, m, 0x801C4D94u);
        if (c.V0 != 0u)
        {
            goto L801D6138;
        }
        c.A0 = 0u | 0x0003u;
        c.RA = 0x801D5E7Cu;
        SoTN.SetStep_no3(c, m);
        goto L801D6138;
    L801D5E84:;
        c.RA = 0x801D5E8Cu;
        Dispatcher.Call(c, m, 0x801C4FD4u);
        c.A0 = 0x80180000u;
        c.A0 = c.A0 + 0x3C30u;
        c.V0 = c.V0 & 0x0001u;
        c.V0 = c.V0 ^ 0x0001u;
        m.WriteU16((c.S1 + 0x14u), (ushort)c.V0);
        c.RA = 0x801D5EA4u;
        SoTN.UnkCollisionFunc2_no3(c, m);
        c.V0 = m.ReadU32((c.S1 + 0x8u));
        c.V1 = m.ReadU16((c.S1 + 0x14u));
        c.V0 = c.V0 >> 31;
        c.V0 = c.V0 ^ c.V1;
        c.A0 = 0x80180000u;
        c.A0 = c.A0 + 0x3B64u;
        if (c.V0 == 0u)
        {
            goto L801D5ECC;
        }
        c.A0 = 0x80180000u;
        c.A0 = c.A0 + 0x3B54u;
    L801D5ECC:;
        c.A1 = c.S1 + 0u;
        c.RA = 0x801D5ED4u;
        Dispatcher.Call(c, m, 0x801C4D94u);
        c.V1 = m.ReadU16((c.S1 + 0x2Eu));
        if (c.V1 == 0u)
        {
            c.V0 = 0u | 0x0001u;
            goto L801D5EF4;
        }
        c.V0 = 0u | 0x0001u;
        if (c.V1 == c.V0)
        {
            c.V0 = 0xFFFF8000u;
            goto L801D5F34;
        }
        c.V0 = 0xFFFF8000u;
        goto L801D5F74;
    L801D5EF4:;
        c.V0 = 0u | 0x8000u;
        m.WriteU32((c.S1 + 0x8u), c.V0);
        c.V0 = 0x80070000u;
        c.V0 = m.ReadU16((c.V0 + 0x308Eu));
        c.V1 = m.ReadU16((c.S1 + 0x2u));
        c.A0 = m.ReadU16((c.S1 + 0x9Cu));
        c.V0 = c.V0 + c.V1;
        c.V0 = c.V0 - c.A0;
        c.V0 = c.V0 << 16;
        c.V0 = (uint)((int)c.V0 >> 16);
        c.V0 = (int)c.V0 < 33 ? 1u : 0u;
        if (c.V0 != 0u)
        {
            goto L801D5F74;
        }
        c.V0 = m.ReadU16((c.S1 + 0x2Eu));
        c.V0 = c.V0 + 0x1u;
        goto L801D5F70;
    L801D5F34:;
        m.WriteU32((c.S1 + 0x8u), c.V0);
        c.V0 = 0x80070000u;
        c.V0 = m.ReadU16((c.V0 + 0x308Eu));
        c.V1 = m.ReadU16((c.S1 + 0x2u));
        c.A0 = m.ReadU16((c.S1 + 0x9Cu));
        c.V0 = c.V0 + c.V1;
        c.V0 = c.V0 - c.A0;
        c.V0 = c.V0 << 16;
        c.V0 = (uint)((int)c.V0 >> 16);
        c.V0 = (int)c.V0 < -32 ? 1u : 0u;
        if (c.V0 == 0u)
        {
            goto L801D5F74;
        }
        c.V0 = m.ReadU16((c.S1 + 0x2Eu));
        c.V0 = c.V0 - 0x1u;
    L801D5F70:;
        m.WriteU16((c.S1 + 0x2Eu), (ushort)c.V0);
    L801D5F74:;
        c.V0 = m.ReadU8((c.S1 + 0x7Cu));
        if (c.V0 == 0u)
        {
            goto L801D5F98;
        }
        c.V0 = m.ReadU8((c.S1 + 0x7Cu));
        c.V0 = c.V0 - 0x1u;
        m.WriteU8((c.S1 + 0x7Cu), (byte)c.V0);
        goto L801D6138;
    L801D5F98:;
        c.RA = 0x801D5FA0u;
        Dispatcher.Call(c, m, 0x801C4F64u);
        c.V0 = (int)c.V0 < 48 ? 1u : 0u;
        if (c.V0 == 0u)
        {
            goto L801D6138;
        }
        c.RA = 0x801D5FB4u;
        SoTN.GetDistanceToPlayerY_no3(c, m);
        c.V0 = (int)c.V0 < 32 ? 1u : 0u;
        if (c.V0 == 0u)
        {
            goto L801D6138;
        }
        c.A0 = 0u | 0x0004u;
        c.RA = 0x801D5FC8u;
        SoTN.SetStep_no3(c, m);
        goto L801D6138;
    L801D5FD0:;
        c.A0 = 0u | 0x062Bu;
        c.S2 = 0u + 0u;
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x3824u));
        c.S3 = 0x80180000u;
        c.S3 = c.S3 + 0x3BF8u;
        c.S4 = 0u + 0u;
        c.RA = 0x801D5FF0u;
        Dispatcher.Call(c, m, c.V0);
    L801D5FF0:;
        c.A0 = 0x80080000u;
        c.A0 = c.A0 - 0x27A8u;
        c.A1 = c.A0 + 0x1780u;
        c.RA = 0x801D6000u;
        Dispatcher.Call(c, m, 0x801C54D4u);
        c.S0 = c.V0 + 0u;
        if (c.S0 == 0u)
        {
            c.A0 = 0u | 0x0047u;
            goto L801D60CC;
        }
        c.A0 = 0u | 0x0047u;
        c.A1 = c.S0 + 0u;
        c.RA = 0x801D6014u;
        SoTN.CreateEntityFromCurrentEntity_no3(c, m);
        c.V0 = m.ReadU16((c.S1 + 0x14u));
        m.WriteU16((c.S0 + 0x30u), (ushort)c.S2);
        m.WriteU16((c.S0 + 0x14u), (ushort)c.V0);
        c.At = 0x80180000u;
        c.At = c.At + c.S2;
        c.V0 = m.ReadU8((c.At + 0x3BB8u));
        m.WriteU8((c.S0 + 0x88u), (byte)c.V0);
        c.V0 = m.ReadU16((c.S1 + 0x14u));
        if (c.V0 == 0u)
        {
            goto L801D6054;
        }
        c.V0 = m.ReadU16((c.S0 + 0x2u));
        c.V1 = m.ReadU16(c.S3);
        c.V0 = c.V0 - c.V1;
        goto L801D6064;
    L801D6054:;
        c.V0 = m.ReadU16((c.S0 + 0x2u));
        c.V1 = m.ReadU16(c.S3);
        c.V0 = c.V0 + c.V1;
    L801D6064:;
        m.WriteU16((c.S0 + 0x2u), (ushort)c.V0);
        c.V0 = m.ReadU16((c.S0 + 0x6u));
        c.At = 0x80180000u;
        c.At = c.At + c.S4;
        c.V1 = m.ReadU16((c.At + 0x3C08u));
        c.S4 = c.S4 + 0x2u;
        c.V0 = c.V0 + c.V1;
        c.V1 = c.S2 << 2;
        m.WriteU16((c.S0 + 0x6u), (ushort)c.V0);
        c.At = 0x80180000u;
        c.At = c.At + c.V1;
        c.V0 = m.ReadU32((c.At + 0x3BC0u));
        c.S3 = c.S3 + 0x2u;
        m.WriteU32((c.S0 + 0x8u), c.V0);
        c.At = 0x80180000u;
        c.At = c.At + c.V1;
        c.V0 = m.ReadU32((c.At + 0x3BDCu));
        c.S2 = c.S2 + 0x1u;
        m.WriteU32((c.S0 + 0xCu), c.V0);
        c.V0 = m.ReadU16((c.S1 + 0x30u));
        c.V1 = m.ReadU16((c.S0 + 0x30u));
        c.V0 = c.V0 << 8;
        c.V1 = c.V1 | c.V0;
        c.V0 = (int)c.S2 < 7 ? 1u : 0u;
        if (c.V0 != 0u)
        {
            m.WriteU16((c.S0 + 0x30u), (ushort)c.V1);
            goto L801D5FF0;
        }
        m.WriteU16((c.S0 + 0x30u), (ushort)c.V1);
    L801D60CC:;
        c.V0 = m.ReadU16((c.S1 + 0x30u));
        if (c.V0 == 0u)
        {
            c.S0 = c.S1 + 0xBCu;
            goto L801D6130;
        }
        c.S0 = c.S1 + 0xBCu;
        c.A0 = 0u | 0x000Au;
        c.A1 = c.S1 + 0u;
        c.A2 = c.S0 + 0u;
        c.RA = 0x801D60ECu;
        SoTN.CreateEntityFromEntity_no3(c, m);
        c.V0 = m.ReadU16((c.S1 + 0x30u));
        c.V0 = c.V0 & 0x0001u;
        if (c.V0 != 0u)
        {
            //c.V0 = 0u | 0x0013u;
            c.V0 = 0u | m.ReadU16(0x800A9984);
            c.V0 -= 0x80;   // added adjustment
            goto L801D6104;
        }
        c.V0 = 0u | 0x0013u;
        //c.V0 = 0u | 0x001Au;
        c.V0 = 0u | m.ReadU16(0x800A9982);
        c.V0 -= 0x80;   // added adjustment
    L801D6104:;
        m.WriteU16((c.S1 + 0xECu), (ushort)c.V0);
        c.V0 = m.ReadU16((c.S0 + 0x30u));
        c.V1 = 0x80180000u;
        c.V1 = m.ReadU32((c.V1 + 0x3B50u));
        c.V0 = c.V0 | 0x8000u;
        m.WriteU16((c.S0 + 0x30u), (ushort)c.V0);
        c.V0 = m.ReadU16((c.S1 + 0x30u));
        c.V0 = c.V0 | c.V1;
        c.At = 0x80180000u;
        m.WriteU32((c.At + 0x3B50u), c.V0);
    L801D6130:;
        c.A0 = c.S1 + 0u;
        c.RA = 0x801D6138u;
        SoTN.DestroyEntity_no3(c, m);
    L801D6138:;
        c.RA = m.ReadU32((c.SP + 0x24u));
        c.S4 = m.ReadU32((c.SP + 0x20u));
        c.S3 = m.ReadU32((c.SP + 0x1Cu));
        c.S2 = m.ReadU32((c.SP + 0x18u));
        c.S1 = m.ReadU32((c.SP + 0x14u));
        c.S0 = m.ReadU32((c.SP + 0x10u));
        c.SP = c.SP + 0x28u;
        return;
    }

    // Trio Relic/Item Drop
    public static void RBO0_EntityBoss(CpuContext c, IMemory m)
    {
        c.SP = c.SP - 0x28u;
        m.WriteU32((c.SP + 0x1Cu), c.S3);
        c.S3 = c.A0 + 0u;
        m.WriteU32((c.SP + 0x20u), c.RA);
        m.WriteU32((c.SP + 0x18u), c.S2);
        m.WriteU32((c.SP + 0x14u), c.S1);
        m.WriteU32((c.SP + 0x10u), c.S0);
        c.V1 = m.ReadU16((c.S3 + 0x2Cu));
        c.V0 = c.V1 < 0x00000008u ? 1u : 0u;
        if (c.V0 == 0u)
        {
            c.V0 = c.V1 << 2;
            goto L801946D4;
        }
        c.V0 = c.V1 << 2;
        c.At = 0x80190000u;
        c.At = c.At + c.V0;
        c.V0 = m.ReadU32((c.At + 0x35B0u));
        switch (c.V0)
        {
            case 0x801943DCu: goto L801943DC;
            case 0x801944A0u: goto L801944A0;
            case 0x80194530u: goto L80194530;
            case 0x80194570u: goto L80194570;
            case 0x801945E0u: goto L801945E0;
            case 0x80194604u: goto L80194604;
            case 0x80194688u: goto L80194688;
            case 0x801946D4u: goto L801946D4;
            default: Dispatcher.Call(c, m, c.V0); return;
        }
    L801943DC:;
        c.A0 = 0x80180000u;
        c.A0 = c.A0 + 0x458u;
        c.S0 = 0u | 0x00C4u;
        c.RA = 0x801943ECu;
        SoTN.InitializeEntity_rbo0(c, m);
        c.S2 = 0x80070000u;
        c.S2 = c.S2 + 0x6E98u;
        c.A0 = 0u | 0x001Bu;
        c.A1 = c.S2 + 0u;
        c.RA = 0x80194400u;
        SoTN.CreateEntityFromCurrentEntity_rbo0(c, m);
        c.S2 = c.S2 + 0x5E0u;
        c.A0 = 0u | 0x001Cu;
        c.S1 = 0x80070000u;
        c.S1 = c.S1 + 0x308Eu;
        c.V1 = m.ReadU16(c.S1);
        c.V0 = 0u | 0x0100u;
        c.V0 = c.V0 - c.V1;
        c.At = 0x80070000u;
        m.WriteU16((c.At + 0x6E9Au), (ushort)c.V0);
        c.V0 = 0x80070000u;
        c.V0 = m.ReadU16((c.V0 + 0x3092u));
        c.V0 = c.S0 - c.V0;
        c.At = 0x80070000u;
        m.WriteU16((c.At + 0x6E9Eu), (ushort)c.V0);
        c.A1 = c.S2 + 0u;
        c.RA = 0x80194444u;
        SoTN.CreateEntityFromCurrentEntity_rbo0(c, m);
        c.A0 = 0u | 0x001Du;
        c.V0 = 0u | 0x00B8u;
        c.A2 = m.ReadU16(c.S1);
        c.V1 = 0x80070000u;
        c.V1 = m.ReadU16((c.V1 + 0x3092u));
        c.V0 = c.V0 - c.A2;
        c.V1 = c.S0 - c.V1;
        c.At = 0x80070000u;
        m.WriteU16((c.At + 0x747Au), (ushort)c.V0);
        c.At = 0x80070000u;
        m.WriteU16((c.At + 0x747Eu), (ushort)c.V1);
        c.A1 = c.S2 + 0x5E0u;
        c.RA = 0x80194478u;
        SoTN.CreateEntityFromCurrentEntity_rbo0(c, m);
        c.V0 = 0u | 0x0148u;
        c.V1 = m.ReadU16(c.S1);
        c.A0 = 0x80070000u;
        c.A0 = m.ReadU16((c.A0 + 0x3092u));
        c.V0 = c.V0 - c.V1;
        c.S0 = c.S0 - c.A0;
        c.At = 0x80070000u;
        m.WriteU16((c.At + 0x7A5Au), (ushort)c.V0);
        c.At = 0x80070000u;
        m.WriteU16((c.At + 0x7A5Eu), (ushort)c.S0);
    L801944A0:;
        c.V0 = 0x80070000u;
        c.V0 = (uint)(short)m.ReadU16((c.V0 + 0x33DAu));
        c.V1 = 0x80070000u;
        c.V1 = (uint)(short)m.ReadU16((c.V1 + 0x308Eu));
        c.V0 = c.V0 + c.V1;
        c.V0 = c.V0 - 0xE1u;
        c.V0 = c.V0 < 0x0000005Fu ? 1u : 0u;
        if (c.V0 == 0u)
        {
            c.A0 = 0u | 0x000Bu;
            goto L801946D4;
        }
        c.A0 = 0u | 0x000Bu;
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x37C0u));
        c.A1 = 0u | 0x0002u;
        c.RA = 0x801944DCu;
        Dispatcher.Call(c, m, c.V0);
        c.V0 = 0u | 0x0001u;
        c.At = 0x80180000u;
        m.WriteU32((c.At + 0x6ACu), c.V0);
        c.V0 = 0u | 0x0140u;
        m.WriteU16((c.S3 + 0x80u), (ushort)c.V0);
        c.V0 = 0u | 0x031Du;
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7910u), c.V0);
        c.V0 = 0x80180000u;
        c.V0 = m.ReadU32((c.V0 + 0x6B0u));
        c.V1 = 0x80040000u;
        c.V1 = m.ReadU32((c.V1 - 0x3824u));
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7928u), 0u);
        c.V0 = c.V0 | 0x0001u;
        c.At = 0x80180000u;
        m.WriteU32((c.At + 0x6B0u), c.V0);
        c.A0 = 0u | 0x031Du;
        c.RA = 0x80194528u;
        Dispatcher.Call(c, m, c.V1);
        goto L801946C4;
    L80194530:;
        c.V0 = m.ReadU16((c.S3 + 0x80u));
        c.V0 = c.V0 - 0x1u;
        m.WriteU16((c.S3 + 0x80u), (ushort)c.V0);
        c.V0 = c.V0 << 16;
        if (c.V0 != 0u)
        {
            goto L801946D4;
        }
        c.V1 = m.ReadU16((c.S3 + 0x2Cu));
        c.V0 = 0x80180000u;
        c.V0 = m.ReadU32((c.V0 + 0x6B0u));
        c.V1 = c.V1 + 0x1u;
        c.V0 = c.V0 | 0x0002u;
        c.At = 0x80180000u;
        m.WriteU32((c.At + 0x6B0u), c.V0);
        m.WriteU16((c.S3 + 0x2Cu), (ushort)c.V1);
        goto L801946D4;
    L80194570:;
        c.V0 = 0x80180000u;
        c.V0 = m.ReadU32((c.V0 + 0x6B4u));
        c.V0 = (int)c.V0 < 3 ? 1u : 0u;
        if (c.V0 != 0u)
        {
            c.A0 = 0u | 0x000Bu;
            goto L801946D4;
        }
        c.A0 = 0u | 0x000Bu;
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x37C0u));
        c.A1 = 0u | 0x0001u;
        c.RA = 0x8019459Cu;
        Dispatcher.Call(c, m, c.V0);
        c.V0 = 0u | 0x0080u;
        m.WriteU16((c.S3 + 0x80u), (ushort)c.V0);
        c.V0 = 0x80180000u;
        c.V0 = m.ReadU32((c.V0 + 0x6B0u));
        c.V1 = 0x80040000u;
        c.V1 = m.ReadU32((c.V1 - 0x3824u));
        c.V0 = c.V0 | 0x0004u;
        c.At = 0x80180000u;
        m.WriteU32((c.At + 0x6B0u), c.V0);
        c.A0 = 0u | 0x0090u;
        c.RA = 0x801945C8u;
        Dispatcher.Call(c, m, c.V1);
        c.V0 = m.ReadU16((c.S3 + 0x2Cu));
        //c.V1 = 0u | 0x0315u;
        c.V1 = 0u | m.ReadU16(0x801945CC);
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7910u), c.V1);
        c.V0 = c.V0 + 0x1u;
        goto L801946D0;
    L801945E0:;
        c.V0 = m.ReadU16((c.S3 + 0x80u));
        c.V0 = c.V0 - 0x1u;
        m.WriteU16((c.S3 + 0x80u), (ushort)c.V0);
        c.V0 = c.V0 << 16;
        if (c.V0 == 0u)
        {
            goto L801946C4;
        }
        goto L801946D4;
    L80194604:;
        c.A0 = 0x80080000u;
        c.A0 = c.A0 - 0x56A8u;
        c.A1 = c.A0 + 0x1780u;
        c.RA = 0x80194614u;
        SoTN.AllocEntity_rbo0(c, m);
        c.S2 = c.V0 + 0u;
        if (c.S2 == 0u)
        {
            c.A0 = 0u | 0x0018u;
            goto L801946D4;
        }
        c.A0 = 0u | 0x0018u;
        c.A1 = c.S3 + 0u;
        c.A2 = c.S2 + 0u;
        c.RA = 0x8019462Cu;
        SoTN.CreateEntityFromEntity_rbo0(c, m);
        c.V1 = 0x80070000u;
        c.V1 = m.ReadU16((c.V1 + 0x308Eu));
        c.V0 = 0u | 0x0001u;
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7928u), c.V0);
        c.V0 = 0u | 0x0100u;
        c.At = 0x80180000u;
        m.WriteU32((c.At + 0x6ACu), 0u);
        c.V0 = c.V0 - c.V1;
        m.WriteU16((c.S2 + 0x2u), (ushort)c.V0);
        c.V1 = 0x80070000u;
        c.V1 = m.ReadU16((c.V1 + 0x3092u));
        //c.V0 = 0u | 0x0002u;
        c.V0 = 0u | m.ReadU16(0x8019465c);      // Allow Changing Reward Index
        if (m.ReadU32(0x801a6088) == 0x34020000)    // If this change has been made then it (sotn.io rando) is trying to force an item into a relic, so lets just move the id instead and then select the right index.
        {
            m.WriteU8(0x801819AA, m.ReadU8(0x8018198C));
            c.V0 = 0x11;
        }
        m.WriteU16((c.S2 + 0x30u), (ushort)c.V0);
        c.V0 = 0u | 0x0080u;
        c.V0 = c.V0 - c.V1;
        m.WriteU16((c.S2 + 0x6u), (ushort)c.V0);
        c.V0 = m.ReadU16((c.S3 + 0x2Cu));
        //c.V1 = 0u | 0x0315u;
        c.V1 = 0u | m.ReadU16(0x80194674);
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7910u), c.V1);
        c.V0 = c.V0 + 0x1u;
        goto L801946D0;
    L80194688:;
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x3808u));
        c.RA = 0x8019469Cu;
        Dispatcher.Call(c, m, c.V0);
        if (c.V0 != 0u)
        {
            goto L801946D4;
        }
        c.A0 = 0x80090000u;
        c.A0 = m.ReadU32((c.A0 + 0x7910u));
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x3824u));
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7928u), 0u);
        c.RA = 0x801946C4u;
        Dispatcher.Call(c, m, c.V0);
    L801946C4:;
        c.V0 = m.ReadU16((c.S3 + 0x2Cu));
        c.V0 = c.V0 + 0x1u;
    L801946D0:;
        m.WriteU16((c.S3 + 0x2Cu), (ushort)c.V0);
    L801946D4:;
        c.RA = m.ReadU32((c.SP + 0x20u));
        c.S3 = m.ReadU32((c.SP + 0x1Cu));
        c.S2 = m.ReadU32((c.SP + 0x18u));
        c.S1 = m.ReadU32((c.SP + 0x14u));
        c.S0 = m.ReadU32((c.SP + 0x10u));
        c.SP = c.SP + 0x28u;
        return;
    }

    // Below should be no longer needed. Saved currently for reference.
    /*
    // Trio Life Max Up Spawn
    public static void EntityLifeUpSpawn_rbo0(CpuContext c, IMemory m)
    {
        c.SP = c.SP - 0xE8u;
        m.WriteU32((c.SP + 0xC8u), c.S2);
        c.S2 = c.A0 + 0u;
        m.WriteU32((c.SP + 0xE4u), c.RA);
        m.WriteU32((c.SP + 0xE0u), c.FP);
        m.WriteU32((c.SP + 0xDCu), c.S7);
        m.WriteU32((c.SP + 0xD8u), c.S6);
        m.WriteU32((c.SP + 0xD4u), c.S5);
        m.WriteU32((c.SP + 0xD0u), c.S4);
        m.WriteU32((c.SP + 0xCCu), c.S3);
        m.WriteU32((c.SP + 0xC4u), c.S1);
        m.WriteU32((c.SP + 0xC0u), c.S0);
        c.V1 = m.ReadU16((c.S2 + 0x2Cu));
        c.V0 = c.V1 < 0x00000007u ? 1u : 0u;
        if (c.V0 == 0u)
        {
            c.V0 = c.V1 << 2;
            goto L801A6190;
        }
        c.V0 = c.V1 << 2;
        c.At = 0x80190000u;
        c.At = c.At + c.V0;
        c.V0 = m.ReadU32((c.At + 0x37E0u));
        switch (c.V0)
        {
            case 0x801A596Cu: goto L801A596C;
            case 0x801A5BD4u: goto L801A5BD4;
            case 0x801A5C20u: goto L801A5C20;
            case 0x801A5FC0u: goto L801A5FC0;
            case 0x801A5F38u: goto L801A5F38;
            case 0x801A6050u: goto L801A6050;
            case 0x801A6080u: goto L801A6080;
            default: Dispatcher.Call(c, m, c.V0); return;
        }
    L801A596C:;
        c.A0 = 0x80180000u;
        c.A0 = c.A0 + 0x458u;
        c.RA = 0x801A597Cu;
        SoTN.InitializeEntity_rbo0(c, m);
        c.A0 = 0u | 0x0004u;
        c.V0 = 0u | 0x0002u;
        m.WriteU16((c.S2 + 0x54u), (ushort)c.V0);
        m.WriteU16((c.S2 + 0x56u), (ushort)0u);
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x3820u));
        c.A1 = 0u | 0x0181u;
        c.RA = 0x801A59A0u;
        Dispatcher.Call(c, m, c.V0);
        c.V0 = c.V0 << 16;
        c.A0 = (uint)((int)c.V0 >> 16);
        c.V0 = 0xFFFFFFFFu;
        if (c.A0 != c.V0)
        {
            c.V0 = c.A0 << 1;
            goto L801A59C0;
        }
        c.V0 = c.A0 << 1;
        c.V0 = 0u | 0x0006u;
        m.WriteU16((c.S2 + 0x2Cu), (ushort)c.V0);
        goto L801A6190;
    L801A59C0:;
        c.V0 = c.V0 + c.A0;
        c.V0 = c.V0 << 2;
        c.V0 = c.V0 + c.A0;
        c.V0 = c.V0 << 2;
        c.V1 = 0x80080000u;
        c.V1 = c.V1 + 0x6FECu;
        c.S1 = c.V0 + c.V1;
        c.V0 = m.ReadU32((c.S2 + 0x34u));
        c.V1 = 0x00800000u;
        m.WriteU32((c.S2 + 0x64u), c.A0);
        m.WriteU32((c.S2 + 0x80u), c.S1);
        c.V0 = c.V0 | c.V1;
        m.WriteU32((c.S2 + 0x34u), c.V0);
        c.V0 = 0u | 0x001Au;
        m.WriteU16((c.S1 + 0x1Au), (ushort)c.V0);
        c.V0 = 0u | 0x019Fu;
        m.WriteU16((c.S1 + 0xEu), (ushort)c.V0);
        c.V0 = 0u | 0x003Fu;
        m.WriteU8((c.S1 + 0x30u), (byte)c.V0);
        m.WriteU8((c.S1 + 0x18u), (byte)c.V0);
        c.V0 = 0u | 0x00C0u;
        m.WriteU8((c.S1 + 0x19u), (byte)c.V0);
        m.WriteU8((c.S1 + 0xDu), (byte)c.V0);
        c.V0 = 0u | 0x00FFu;
        m.WriteU8((c.S1 + 0x24u), (byte)0u);
        m.WriteU8((c.S1 + 0xCu), (byte)0u);
        m.WriteU8((c.S1 + 0x31u), (byte)c.V0);
        m.WriteU8((c.S1 + 0x25u), (byte)c.V0);
        c.V0 = m.ReadU16((c.S2 + 0x2u));
        c.S0 = 0u + 0u;
        m.WriteU16((c.S1 + 0x2Cu), (ushort)c.V0);
        m.WriteU16((c.S1 + 0x20u), (ushort)c.V0);
        m.WriteU16((c.S1 + 0x14u), (ushort)c.V0);
        m.WriteU16((c.S1 + 0x8u), (ushort)c.V0);
        c.V1 = m.ReadU16((c.S2 + 0x2u));
        c.V0 = 0u | 0x00C0u;
        m.WriteU16((c.S1 + 0x26u), (ushort)c.V0);
        c.V0 = 0u | 0x0033u;
        m.WriteU16((c.S1 + 0x32u), (ushort)c.V0);
        m.WriteU16((c.S1 + 0x2Eu), (ushort)c.V1);
        m.WriteU16((c.S1 + 0x22u), (ushort)c.V1);
        m.WriteU16((c.S1 + 0x16u), (ushort)c.V1);
        m.WriteU16((c.S1 + 0xAu), (ushort)c.V1);
        c.S1 = m.ReadU32(c.S1);
        c.S3 = 0u | 0x0020u;
        m.WriteU32((c.S2 + 0x7Cu), c.S1);
        c.S6 = 0u + 0u;
    L801A5A7C:;
        c.S2 = 0u + 0u;
    L801A5A80:;
        c.S5 = 0u + 0u;
        c.S4 = c.S2 + 0u;
    L801A5A88:;
        c.A0 = c.S1 + 0u;
        c.RA = 0x801A5A90u;
        SoTN.UnkPolyFunc2_rbo0(c, m);
        c.V1 = m.ReadU32(c.S1);
        c.V0 = 0u | 0x001Au;
        m.WriteU16((c.S1 + 0x1Au), (ushort)c.V0);
        c.V0 = 0u | 0x0194u;
        m.WriteU16((c.S1 + 0xEu), (ushort)c.V0);
        c.V0 = 0u | 0x0010u;
        m.WriteU8((c.S1 + 0x30u), (byte)c.V0);
        m.WriteU8((c.S1 + 0x18u), (byte)c.V0);
        c.V0 = 0u | 0x0050u;
        m.WriteU8((c.S1 + 0x19u), (byte)c.V0);
        m.WriteU8((c.S1 + 0xDu), (byte)c.V0);
        c.V0 = 0u | 0x0060u;
        m.WriteU8((c.S1 + 0x31u), (byte)c.V0);
        m.WriteU8((c.S1 + 0x25u), (byte)c.V0);
        c.V0 = 0u | 0x1000u;
        m.WriteU8((c.S1 + 0x24u), (byte)0u);
        m.WriteU8((c.S1 + 0xCu), (byte)0u);
        m.WriteU8((c.S1 + 0x28u), (byte)c.S3);
        m.WriteU8((c.S1 + 0x1Cu), (byte)c.S3);
        m.WriteU8((c.S1 + 0x10u), (byte)c.S3);
        m.WriteU8((c.S1 + 0x4u), (byte)c.S3);
        m.WriteU8((c.S1 + 0x29u), (byte)0u);
        m.WriteU8((c.S1 + 0x1Du), (byte)0u);
        m.WriteU8((c.S1 + 0x11u), (byte)0u);
        m.WriteU8((c.S1 + 0x5u), (byte)0u);
        m.WriteU8((c.S1 + 0x2Au), (byte)0u);
        m.WriteU8((c.S1 + 0x1Eu), (byte)0u);
        m.WriteU8((c.S1 + 0x12u), (byte)0u);
        m.WriteU8((c.S1 + 0x6u), (byte)0u);
        m.WriteU16((c.V1 + 0x22u), (ushort)c.V0);
        m.WriteU16((c.V1 + 0x20u), (ushort)c.V0);
        c.V1 = m.ReadU32(c.S1);
        c.V0 = c.S5 << 9;
        m.WriteU16((c.V1 + 0x1Au), (ushort)c.V0);
        c.V0 = m.ReadU32(c.S1);
        m.WriteU16((c.V0 + 0x2Cu), (ushort)0u);
        c.V0 = m.ReadU32(c.S1);
        m.WriteU16((c.V0 + 0x2Eu), (ushort)c.S4);
        c.V1 = m.ReadU32(c.S1);
        c.V0 = 0xFFFB0000u;
        m.WriteU32((c.V1 + 0xCu), c.V0);
        c.V0 = m.ReadU32(c.S1);
        m.WriteU32((c.V0 + 0x10u), 0u);
        c.V1 = m.ReadU32(c.S1);
        c.V0 = 0u | 0x0080u;
        m.WriteU16((c.V1 + 0x14u), (ushort)c.V0);
        c.V0 = m.ReadU32(c.S1);
        m.WriteU16((c.V0 + 0xAu), (ushort)0u);
        c.V0 = 0u | 0x00C0u;
        m.WriteU16((c.S1 + 0x26u), (ushort)c.V0);
        c.V0 = 0u | 0x0073u;
        m.WriteU16((c.S1 + 0x32u), (ushort)c.V0);
        c.S1 = m.ReadU32(c.S1);
        c.V0 = m.ReadU16((c.S1 + 0x32u));
        c.S5 = c.S5 + 0x1u;
        c.V0 = c.V0 & 0xFFFDu;
        m.WriteU16((c.S1 + 0x32u), (ushort)c.V0);
        c.V0 = (int)c.S5 < 8 ? 1u : 0u;
        c.S1 = m.ReadU32(c.S1);
        if (c.V0 != 0u)
        {
            goto L801A5A88;
        }
        c.S6 = c.S6 + 0x1u;
        c.V0 = (int)c.S6 < 3 ? 1u : 0u;
        if (c.V0 != 0u)
        {
            c.S2 = c.S2 + 0x540u;
            goto L801A5A80;
        }
        c.S2 = c.S2 + 0x540u;
        c.S0 = c.S0 + 0x1u;
        c.V0 = (int)c.S0 < 8 ? 1u : 0u;
        if (c.V0 != 0u)
        {
            c.S6 = 0u + 0u;
            goto L801A5A7C;
        }
        c.S6 = 0u + 0u;
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x3824u));
        c.A0 = 0u | 0x07D2u;
        c.RA = 0x801A5BCCu;
        Dispatcher.Call(c, m, c.V0);
        goto L801A6190;
    L801A5BD4:;
        c.V0 = (uint)(short)m.ReadU16((c.S2 + 0x86u));
        if (c.V0 != 0u)
        {
            c.V1 = 0u | 0x0002u;
            goto L801A5BF4;
        }
        c.V1 = 0u | 0x0002u;
        c.V0 = m.ReadU16((c.S2 + 0x88u));
        m.WriteU16((c.S2 + 0x86u), (ushort)c.V1);
        c.V0 = c.V0 + 0x1u;
        m.WriteU16((c.S2 + 0x88u), (ushort)c.V0);
    L801A5BF4:;
        c.V0 = m.ReadU16((c.S2 + 0x86u));
        c.V1 = (uint)(short)m.ReadU16((c.S2 + 0x88u));
        c.V0 = c.V0 - 0x1u;
        c.V1 = (int)c.V1 < 8 ? 1u : 0u;
        if (c.V1 != 0u)
        {
            m.WriteU16((c.S2 + 0x86u), (ushort)c.V0);
            goto L801A5C20;
        }
        m.WriteU16((c.S2 + 0x86u), (ushort)c.V0);
        c.V0 = m.ReadU16((c.S2 + 0x2Cu));
        c.V1 = 0u | 0x0007u;
        m.WriteU16((c.S2 + 0x88u), (ushort)c.V1);
        c.V0 = c.V0 + 0x1u;
        m.WriteU16((c.S2 + 0x2Cu), (ushort)c.V0);
    L801A5C20:;
        c.A0 = 0u | 0x0200u;
        c.RA = 0x801A5C28u;
        SoTN.SetGeomScreen(c, m);
        c.A0 = (uint)(short)m.ReadU16((c.S2 + 0x2u));
        c.A1 = (uint)(short)m.ReadU16((c.S2 + 0x6u));
        c.S6 = 0u + 0u;
        m.WriteU32((c.SP + 0xA0u), 0u);
        c.RA = 0x801A5C3Cu;
        SoTN.SetGeomOffset(c, m);
        c.V0 = (uint)(short)m.ReadU16((c.S2 + 0x88u));
        c.S1 = m.ReadU32((c.S2 + 0x7Cu));
        c.V0 = c.V0 + 0x1u;
        if ((int)c.V0 <= 0)
        {
            c.T0 = c.SP + 0x60u;
            goto L801A5F00;
        }
        c.T0 = c.SP + 0x60u;
        c.S3 = c.SP + 0x70u;
        m.WriteU32((c.SP + 0xA8u), c.T0);
    L801A5C58:;
        c.V1 = m.ReadU32(c.S1);
        c.V0 = (uint)(short)m.ReadU16((c.V1 + 0x14u));
        c.A0 = (uint)(short)m.ReadU16((c.V1 + 0x16u));
        c.A1 = m.ReadU32((c.V1 + 0xCu));
        c.V0 = c.V0 << 16;
        c.S4 = c.A0 + c.V0;
        c.S4 = c.S4 + c.A1;
        m.WriteU16((c.V1 + 0x16u), (ushort)c.S4);
        c.V0 = m.ReadU32(c.S1);
        c.V1 = (uint)((int)c.S4 >> 16);
        m.WriteU16((c.V0 + 0x14u), (ushort)c.V1);
        c.A1 = m.ReadU32(c.S1);
        c.A0 = m.ReadU32((c.A1 + 0xCu));
        c.V0 = (int)c.A0 < -16384 ? 1u : 0u;
        if (c.V0 == 0u)
        {
            c.S4 = c.V1 + 0u;
            goto L801A5CAC;
        }
        c.S4 = c.V1 + 0u;
        c.V0 = c.A0 + 0x3800u;
        m.WriteU32((c.A1 + 0xCu), c.V0);
    L801A5CAC:;
        c.V0 = m.ReadU32(c.S1);
        c.V0 = (uint)(short)m.ReadU16((c.V0 + 0x14u));
        c.V0 = (int)c.V0 < 8 ? 1u : 0u;
        if (c.V0 == 0u)
        {
            c.V1 = 0u | 0x0008u;
            goto L801A5D00;
        }
        c.V1 = 0u | 0x0008u;
        c.T0 = m.ReadU32((c.SP + 0xA0u));
        c.T0 = c.T0 + 0x1u;
        m.WriteU32((c.SP + 0xA0u), c.T0);
        c.V0 = m.ReadU16((c.S2 + 0x84u));
        c.S0 = 0u | 0x002Fu;
        c.V0 = c.V0 + 0x4u;
        m.WriteU16((c.S2 + 0x84u), (ushort)c.V0);
    L801A5CE8:;
        m.WriteU16((c.S1 + 0x32u), (ushort)c.V1);
        c.S0 = c.S0 - 0x1u;
        if ((int)c.S0 >= 0)
        {
            c.S1 = c.S1 + 0x34u;
            goto L801A5CE8;
        }
        c.S1 = c.S1 + 0x34u;
        goto L801A5EE8;
    L801A5D00:;
        c.S5 = 0u + 0u;
        c.FP = c.SP + 0x98u;
        c.S7 = c.SP + 0x9Cu;
    L801A5D0C:;
        c.V0 = m.ReadU32(c.S1);
        c.V0 = m.ReadU16((c.V0 + 0x2Cu));
        m.WriteU16((c.SP + 0x58u), (ushort)c.V0);
        c.V0 = m.ReadU32(c.S1);
        c.V0 = m.ReadU16((c.V0 + 0x2Eu));
        m.WriteU16((c.SP + 0x5Au), (ushort)c.V0);
        c.V0 = m.ReadU32(c.S1);
        c.A0 = c.SP + 0x58u;
        c.V0 = m.ReadU16((c.V0 + 0x1Au));
        c.A1 = c.S3 + 0u;
        m.WriteU16((c.SP + 0x5Cu), (ushort)c.V0);
        c.RA = 0x801A5D4Cu;
        SoTN.RotMatrix(c, m);
        c.A0 = c.S3 + 0u;
        c.A1 = m.ReadU32((c.SP + 0xA8u));
        c.V0 = 0u | 0x0200u;
        m.WriteU32((c.SP + 0x60u), 0u);
        m.WriteU32((c.SP + 0x64u), 0u);
        m.WriteU32((c.SP + 0x68u), c.V0);
        c.RA = 0x801A5D68u;
        SoTN.TransMatrix(c, m);
        c.A0 = c.S3 + 0u;
        c.RA = 0x801A5D70u;
        SoTN.SetRotMatrix(c, m);
        c.A0 = c.S3 + 0u;
        c.RA = 0x801A5D78u;
        SoTN.SetTransMatrix(c, m);
        c.A0 = c.SP + 0x90u;
        c.A1 = c.SP + 0x50u;
        c.A2 = c.FP + 0u;
        c.A3 = c.S7 + 0u;
        m.WriteU16((c.SP + 0x90u), (ushort)c.S4);
        m.WriteU16((c.SP + 0x92u), (ushort)0u);
        m.WriteU16((c.SP + 0x94u), (ushort)0u);
        c.RA = 0x801A5D98u;
        SoTN.RotTransPers(c, m);
        c.A0 = 0x80180000u;
        c.A0 = c.A0 + 0x19B4u;
        c.A1 = c.S3 + 0u;
        c.S0 = c.V0 + 0u;
        c.RA = 0x801A5DACu;
        SoTN.RotMatrix(c, m);
        c.A0 = c.S3 + 0u;
        c.S0 = c.S0 << 16;
        c.V0 = (uint)(short)m.ReadU16((c.SP + 0x50u));
        c.V1 = (uint)(short)m.ReadU16((c.S2 + 0x2u));
        c.A1 = m.ReadU32((c.SP + 0xA8u));
        c.V0 = c.V0 - c.V1;
        m.WriteU32((c.SP + 0x60u), c.V0);
        c.V0 = (uint)(short)m.ReadU16((c.SP + 0x52u));
        c.V1 = (uint)(short)m.ReadU16((c.S2 + 0x6u));
        c.S0 = (uint)((int)c.S0 >> 14);
        m.WriteU32((c.SP + 0x68u), c.S0);
        c.V0 = c.V0 - c.V1;
        m.WriteU32((c.SP + 0x64u), c.V0);
        c.RA = 0x801A5DE4u;
        SoTN.TransMatrix(c, m);
        c.V0 = m.ReadU32(c.S1);
        c.V0 = (uint)(short)m.ReadU16((c.V0 + 0x20u));
        c.A0 = c.S3 + 0u;
        m.WriteU32((c.SP + 0x60u), c.V0);
        c.V0 = m.ReadU32(c.S1);
        c.A1 = m.ReadU32((c.SP + 0xA8u));
        c.V1 = (uint)(short)m.ReadU16((c.V0 + 0x22u));
        c.V0 = 0u | 0x1000u;
        m.WriteU32((c.SP + 0x68u), c.V0);
        m.WriteU32((c.SP + 0x64u), c.V1);
        c.RA = 0x801A5E14u;
        SoTN.ScaleMatrix(c, m);
        c.A0 = c.S3 + 0u;
        c.RA = 0x801A5E1Cu;
        SoTN.SetRotMatrix(c, m);
        c.A0 = c.S3 + 0u;
        c.RA = 0x801A5E24u;
        SoTN.SetTransMatrix(c, m);
        c.A0 = 0x80180000u;
        c.A0 = c.A0 + 0x1968u;
        c.A1 = 0x80180000u;
        c.A1 = c.A1 + 0x1970u;
        c.A2 = 0x80180000u;
        c.A2 = c.A2 + 0x1978u;
        c.A3 = 0x80180000u;
        c.A3 = c.A3 + 0x1980u;
        c.V0 = c.S1 + 0x8u;
        m.WriteU32((c.SP + 0x10u), c.V0);
        c.V0 = c.S1 + 0x14u;
        m.WriteU32((c.SP + 0x14u), c.V0);
        c.V0 = c.S1 + 0x20u;
        m.WriteU32((c.SP + 0x18u), c.V0);
        c.V0 = c.S1 + 0x2Cu;
        m.WriteU32((c.SP + 0x1Cu), c.V0);
        m.WriteU32((c.SP + 0x20u), c.FP);
        m.WriteU32((c.SP + 0x24u), c.S7);
        c.RA = 0x801A5E70u;
        SoTN.RotTransPers4(c, m);
        c.V1 = m.ReadU32(c.S1);
        c.V0 = m.ReadU16((c.V1 + 0x22u));
        c.V0 = c.V0 - 0x10u;
        m.WriteU16((c.V1 + 0x22u), (ushort)c.V0);
        m.WriteU16((c.V1 + 0x20u), (ushort)c.V0);
        c.V1 = m.ReadU32(c.S1);
        c.V0 = m.ReadU16((c.V1 + 0x1Au));
        c.V0 = c.V0 + 0x8u;
        m.WriteU16((c.V1 + 0x1Au), (ushort)c.V0);
        c.V1 = m.ReadU32(c.S1);
        c.V0 = m.ReadU16((c.V1 + 0x2Cu));
        c.V0 = c.V0 + 0x10u;
        m.WriteU16((c.V1 + 0x2Cu), (ushort)c.V0);
        c.V1 = m.ReadU32(c.S1);
        c.V0 = m.ReadU16((c.V1 + 0x2Eu));
        c.S5 = c.S5 + 0x1u;
        c.V0 = c.V0 + 0x20u;
        m.WriteU16((c.V1 + 0x2Eu), (ushort)c.V0);
        c.S1 = m.ReadU32(c.S1);
        c.V0 = (int)c.S5 < 24 ? 1u : 0u;
        c.S1 = m.ReadU32(c.S1);
        if (c.V0 != 0u)
        {
            goto L801A5D0C;
        }
    L801A5EE8:;
        c.V0 = (uint)(short)m.ReadU16((c.S2 + 0x88u));
        c.S6 = c.S6 + 0x1u;
        c.V0 = c.V0 + 0x1u;
        c.V0 = (int)c.S6 < (int)c.V0 ? 1u : 0u;
        if (c.V0 != 0u)
        {
            goto L801A5C58;
        }
    L801A5F00:;
        c.T0 = m.ReadU32((c.SP + 0xA0u));
        c.V0 = 0u | 0x0008u;
        if (c.T0 != c.V0)
        {
            goto L801A5F20;
        }
        c.V0 = m.ReadU16((c.S2 + 0x2Cu));
        c.V0 = c.V0 + 0x1u;
        m.WriteU16((c.S2 + 0x2Cu), (ushort)c.V0);
    L801A5F20:;
        c.V0 = (uint)(short)m.ReadU16((c.S2 + 0x84u));
        c.S1 = m.ReadU32((c.S2 + 0x80u));
        c.S4 = (uint)(short)m.ReadU16((c.S2 + 0x2u));
        m.WriteU32((c.SP + 0x98u), c.V0);
        c.V0 = (int)c.V0 < 257 ? 1u : 0u;
        goto L801A6000;
    L801A5F38:;
        c.RA = 0x801A5F40u;
        SoTN.MoveEntity_rbo0(c, m);
        c.A2 = c.SP + 0x28u;
        c.A3 = 0u + 0u;
        c.A0 = (uint)(short)m.ReadU16((c.S2 + 0x2u));
        c.V0 = m.ReadU32((c.S2 + 0xCu));
        c.A1 = m.ReadU16((c.S2 + 0x6u));
        c.V0 = c.V0 + 0x2000u;
        c.A1 = c.A1 + 0x4u;
        c.A1 = c.A1 << 16;
        m.WriteU32((c.S2 + 0xCu), c.V0);
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x3844u));
        c.A1 = (uint)((int)c.A1 >> 16);
        c.RA = 0x801A5F78u;
        Dispatcher.Call(c, m, c.V0);
        c.V0 = m.ReadU32((c.SP + 0x28u));
        c.V0 = c.V0 & 0x0001u;
        if (c.V0 == 0u)
        {
            goto L801A5FC0;
        }
        c.V0 = m.ReadU16((c.S2 + 0x6u));
        m.WriteU32((c.S2 + 0xCu), 0u);
        c.A0 = m.ReadU16((c.SP + 0x40u));
        c.V1 = m.ReadU16((c.S2 + 0x84u));
        c.V0 = c.V0 + c.A0;
        c.V1 = c.V1 - 0x1u;
        m.WriteU16((c.S2 + 0x84u), (ushort)c.V1);
        c.V1 = c.V1 << 16;
        if (c.V1 != 0u)
        {
            m.WriteU16((c.S2 + 0x6u), (ushort)c.V0);
            goto L801A5FC0;
        }
        m.WriteU16((c.S2 + 0x6u), (ushort)c.V0);
        c.V0 = 0u | 0x0005u;
        m.WriteU16((c.S2 + 0x2Cu), (ushort)c.V0);
        goto L801A6190;
    L801A5FC0:;
        c.V0 = (uint)(short)m.ReadU16((c.S2 + 0x84u));
        if ((int)c.V0 <= 0)
        {
            c.V1 = c.V0 + 0u;
            goto L801A5FDC;
        }
        c.V1 = c.V0 + 0u;
        c.V0 = c.V1 - 0x20u;
        m.WriteU16((c.S2 + 0x84u), (ushort)c.V0);
        goto L801A5FEC;
    L801A5FDC:;
        c.V0 = 0u | 0x0010u;
        m.WriteU16((c.S2 + 0x84u), (ushort)c.V0);
        c.V0 = 0u | 0x0005u;
        m.WriteU16((c.S2 + 0x2Cu), (ushort)c.V0);
    L801A5FEC:;
        c.V0 = (uint)(short)m.ReadU16((c.S2 + 0x84u));
        c.S1 = m.ReadU32((c.S2 + 0x80u));
        c.S4 = (uint)(short)m.ReadU16((c.S2 + 0x2u));
        m.WriteU32((c.SP + 0x98u), c.V0);
        c.V0 = (int)c.V0 < 225 ? 1u : 0u;
    L801A6000:;
        if (c.V0 != 0u)
        {
            c.V0 = 0u | 0x00E0u;
            goto L801A600C;
        }
        c.V0 = 0u | 0x00E0u;
        m.WriteU32((c.SP + 0x98u), c.V0);
    L801A600C:;
        c.V0 = m.ReadU16((c.SP + 0x98u));
        c.V1 = c.S4 - c.V0;
        c.A0 = c.V0 + c.S4;
        m.WriteU16((c.S1 + 0x20u), (ushort)c.V1);
        m.WriteU16((c.S1 + 0x8u), (ushort)c.V1);
        m.WriteU16((c.S1 + 0x2Cu), (ushort)c.A0);
        m.WriteU16((c.S1 + 0x14u), (ushort)c.A0);
        c.S4 = (uint)(short)m.ReadU16((c.S2 + 0x6u));
        c.V1 = c.S4 - c.V0;
        c.V0 = c.V0 + c.S4;
        m.WriteU16((c.S1 + 0x16u), (ushort)c.V1);
        m.WriteU16((c.S1 + 0xAu), (ushort)c.V1);
        m.WriteU16((c.S1 + 0x2Eu), (ushort)c.V0);
        m.WriteU16((c.S1 + 0x22u), (ushort)c.V0);
        goto L801A6190;
    L801A6050:;
        c.A0 = m.ReadU32((c.S2 + 0x64u));
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x384Cu));
        c.RA = 0x801A6068u;
        Dispatcher.Call(c, m, c.V0);
        c.V1 = m.ReadU16((c.S2 + 0x2Cu));
        c.V0 = m.ReadU16((c.S2 + 0x6u));
        c.V1 = c.V1 + 0x1u;
        c.V0 = c.V0 - 0x4u;
        m.WriteU16((c.S2 + 0x6u), (ushort)c.V0);
        m.WriteU16((c.S2 + 0x2Cu), (ushort)c.V1);
    L801A6080:;
        c.V1 = m.ReadU16((c.S2 + 0x30u));
        c.V0 = c.V1 < 0x00000011u ? 1u : 0u;
        // Added for Modified Instruction to happen if present.
        if (m.ReadU32(0x801a6088) == 0x34020000)
        {
            c.V0 = 0;
        }
        if (c.V0 != 0u)
        {
            c.V0 = c.V1 << 1;
            goto L801A611C;
        }
        c.V0 = c.V1 << 1;
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x3660u));
        if (c.V0 == 0u)
        {
            c.V1 = 0u | 0x0017u;
            goto L801A60D8;
        }
        c.V1 = 0u | 0x0017u;
        m.WriteU16((c.S2 + 0x30u), (ushort)c.V1);
        m.WriteU16((c.S2 + 0x30u), (ushort)c.V1);
        c.V1 = m.ReadU16((c.S2 + 0x30u));
        c.V0 = 0u | 0x0003u;
        m.WriteU16((c.S2 + 0x26u), (ushort)c.V0);
        c.V0 = 0x801A0000u;
        c.V0 = c.V0 - 0x1164u;
        m.WriteU32((c.S2 + 0x28u), c.V0);
        c.V0 = 0u | 0x0010u;
        m.WriteU16((c.S2 + 0x52u), (ushort)0u);
        m.WriteU16((c.S2 + 0x50u), (ushort)0u);
        goto L801A6180;
    L801A60D8:;
        c.V0 = 0u | 0x000Bu;
        m.WriteU16((c.S2 + 0x26u), (ushort)c.V0);
        c.V0 = 0x801A0000u;
        c.V0 = c.V0 + 0x148u;
        m.WriteU32((c.S2 + 0x28u), c.V0);
        c.V0 = m.ReadU16((c.S2 + 0x30u));
        c.V1 = 0u | 0x0010u;
        m.WriteU16((c.S2 + 0x52u), (ushort)0u);
        m.WriteU16((c.S2 + 0x50u), (ushort)0u);
        m.WriteU8((c.S2 + 0x6Du), (byte)c.V1);
        c.V0 = c.V0 << 1;
        c.At = 0x80180000u;
        c.At = c.At + c.V0;
        c.V0 = m.ReadU16((c.At + 0x1988u));
        m.WriteU16((c.S2 + 0x2Cu), (ushort)0u);
        m.WriteU16((c.S2 + 0x30u), (ushort)c.V0);
        goto L801A6190;
    L801A611C:;
        c.At = 0x80180000u;
        c.At = c.At + c.V0;
        c.V0 = m.ReadU16((c.At + 0x1988u));
        c.V1 = c.V0 & 0x0FFFu;
        m.WriteU16((c.S2 + 0x30u), (ushort)c.V0);
        c.V0 = (int)c.V1 < 128 ? 1u : 0u;
        if (c.V0 == 0u)
        {
            c.V0 = 0u | 0x0003u;
            goto L801A615C;
        }
        c.V0 = 0u | 0x0003u;
        m.WriteU16((c.S2 + 0x26u), (ushort)c.V0);
        c.V0 = 0x801A0000u;
        c.V0 = c.V0 - 0x1164u;
        if (m.ReadU16(0x801A6148) == 0x0148)     // Added for integrated to change to relic
            c.V0 = 0x801A0148u;
        m.WriteU32((c.S2 + 0x28u), c.V0);
        m.WriteU16((c.S2 + 0x52u), (ushort)0u);
        m.WriteU16((c.S2 + 0x50u), (ushort)0u);
        goto L801A6174;
    L801A615C:;
        c.V1 = c.V1 - 0x80u;
        c.V0 = 0u | 0x000Au;
        m.WriteU16((c.S2 + 0x26u), (ushort)c.V0);
        c.V0 = 0x801A0000u;
        c.V0 = c.V0 - 0x750u;
        m.WriteU32((c.S2 + 0x28u), c.V0);
    L801A6174:;
        m.WriteU16((c.S2 + 0x30u), (ushort)c.V1);
        c.V1 = m.ReadU16((c.S2 + 0x30u));
        c.V0 = 0u | 0x0010u;
    L801A6180:;
        m.WriteU8((c.S2 + 0x6Du), (byte)c.V0);
        m.WriteU16((c.S2 + 0x2Cu), (ushort)0u);
        c.V1 = c.V1 | 0x8000u;
        m.WriteU16((c.S2 + 0x30u), (ushort)c.V1);
    L801A6190:;
        c.RA = m.ReadU32((c.SP + 0xE4u));
        c.FP = m.ReadU32((c.SP + 0xE0u));
        c.S7 = m.ReadU32((c.SP + 0xDCu));
        c.S6 = m.ReadU32((c.SP + 0xD8u));
        c.S5 = m.ReadU32((c.SP + 0xD4u));
        c.S4 = m.ReadU32((c.SP + 0xD0u));
        c.S3 = m.ReadU32((c.SP + 0xCCu));
        c.S2 = m.ReadU32((c.SP + 0xC8u));
        c.S1 = m.ReadU32((c.SP + 0xC4u));
        c.S0 = m.ReadU32((c.SP + 0xC0u));
        c.SP = c.SP + 0xE8u;
        return;
    }
    */

    // AntiFreeze
    // Rather than patching the whole function, this is a post function hook to get the same effect.
    public static void AntiFreeze(CpuContext c, IMemory m)
    {
        if (m.ReadU8(0x80121B74) == 0x00 && m.ReadU8(0x80097420) == 0x03)
        {
            m.WriteU8(0x80097420, 0);
        }
    }

    // Fast Warps
    // Rather than patching the whole function, this is a post function hook to get the same effect.
    public static void FastWarps(CpuContext c, IMemory m)
    {
        if (m.ReadU8(0x800974A0) == 0x0E && m.ReadU8(0x801878B8) == 0x02 && m.ReadU8(0x80076EC4) == 0x03)   // If StageId == WRP, AntiFreeze Byte Change Detected, and Warp Step == 3
        {
            m.WriteU8(0x80076EC4, 0x4); // Warp Step = 4
        }
        if (m.ReadU8(0x800974A0) == 0x2E && m.ReadU8(0x8018972C) == 0x02 && m.ReadU8(0x80076EC4) == 0x04)   // If StageId == WRP, AntiFreeze Byte Change Detected, and Warp Step == 4
        {
            m.WriteU8(0x80076EC4, 0x5); // Warp Step = 5
        }
    }

    // Clock Statue Always Open NO0
    public static void EntityClockRoomController_no0(CpuContext c, IMemory m)
    {
        c.SP = c.SP - 0x28u;
        m.WriteU32((c.SP + 0x18u), c.S2);
        c.S2 = c.A0 + 0u;
        m.WriteU32((c.SP + 0x20u), c.RA);
        m.WriteU32((c.SP + 0x1Cu), c.S3);
        m.WriteU32((c.SP + 0x14u), c.S1);
        m.WriteU32((c.SP + 0x10u), c.S0);
        c.V0 = m.ReadU16((c.S2 + 0x84u));
        c.S3 = 0x80090000u;
        c.S3 = c.S3 + 0x7964u;
        if (c.V0 == 0u)
        {
            goto L801CCCFC;
        }
        c.V0 = m.ReadU16((c.S2 + 0x86u));
        if (c.V0 != 0u)
        {
            goto L801CCCEC;
        }
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x3824u));
        c.A0 = 0u | 0x07A6u;
        c.RA = 0x801CCCC8u;
        Dispatcher.Call(c, m, c.V0);
        c.V0 = m.ReadU16((c.S2 + 0x84u));
        c.V0 = c.V0 - 0x1u;
        m.WriteU16((c.S2 + 0x84u), (ushort)c.V0);
        c.V0 = c.V0 & 0xFFFFu;
        if (c.V0 == 0u)
        {
            c.V0 = 0u | 0x0040u;
            goto L801CCCFC;
        }
        c.V0 = 0u | 0x0040u;
        m.WriteU16((c.S2 + 0x86u), (ushort)c.V0);
        goto L801CCCFC;
    L801CCCEC:;
        c.V0 = m.ReadU16((c.S2 + 0x86u));
        c.V0 = c.V0 - 0x1u;
        m.WriteU16((c.S2 + 0x86u), (ushort)c.V0);
    L801CCCFC:;
        c.V0 = 0x80090000u;
        c.V0 = m.ReadU32((c.V0 + 0x73FCu));
        c.S1 = 0x80070000u;
        c.S1 = c.S1 + 0x33D8u;
        if (c.V0 != 0u)
        {
            goto L801CCD3C;
        }
        c.V0 = 0x80070000u;
        c.V0 = (uint)(short)m.ReadU16((c.V0 + 0x33DEu));
        c.V0 = (int)c.V0 < 129 ? 1u : 0u;
        if (c.V0 != 0u)
        {
            goto L801CCD54;
        }
        c.At = 0x801E0000u;
        m.WriteU16((c.At - 0xAB8u), (ushort)0u);
        goto L801CCD54;
    L801CCD3C:;
        c.V0 = m.ReadU16((c.S2 + 0x8Au));
        if (c.V0 != 0u)
        {
            c.V0 = 0u | 0x0001u;
            goto L801CCD54;
        }
        c.V0 = 0u | 0x0001u;
        c.At = 0x801E0000u;
        m.WriteU16((c.At - 0xAB8u), (ushort)c.V0);
    L801CCD54:;
        c.V0 = 0x80090000u;
        c.V0 = m.ReadU16((c.V0 + 0x73FCu));
        m.WriteU16((c.S2 + 0x8Au), (ushort)c.V0);
        c.V0 = m.ReadU32((c.S3 + 0x2D0u));
        if (m.ReadU32(0x801CCD64) != 0x8E6202D0)     // Clock Statue always Open
        {
            c.V0 = 0;
        }
        c.V0 = c.V0 & 0x0001u;
        if (c.V0 == 0u)
        {
            c.V0 = 0u | 0x0001u;
            goto L801CCD9C;
        }
        c.V0 = 0u | 0x0001u;
        c.V0 = (uint)(short)m.ReadU16((c.S1 + 0x6u));
        c.V0 = (int)c.V0 < 129 ? 1u : 0u;
        if (c.V0 != 0u)
        {
            goto L801CCDA4;
        }
        c.At = 0x801E0000u;
        m.WriteU16((c.At - 0xAB6u), (ushort)0u);
        goto L801CCDA4;
    L801CCD9C:;
        c.At = 0x801E0000u;
        m.WriteU16((c.At - 0xAB6u), (ushort)c.V0);
    L801CCDA4:;
        c.V1 = m.ReadU16((c.S2 + 0x2Cu));
        c.S1 = 0u | 0x0001u;
        if (c.V1 == c.S1)
        {
            c.V0 = (int)c.V1 < 2 ? 1u : 0u;
            goto L801CD0C4;
        }
        c.V0 = (int)c.V1 < 2 ? 1u : 0u;
        if (c.V0 == 0u)
        {
            goto L801CCDCC;
        }
        if (c.V1 == 0u)
        {
            c.V0 = 0x88880000u;
            goto L801CCDE8;
        }
        c.V0 = 0x88880000u;
        goto L801CD730;
    L801CCDCC:;
        c.V0 = 0u | 0x0002u;
        if (c.V1 == c.V0)
        {
            c.V0 = 0u | 0x0003u;
            goto L801CD1F4;
        }
        c.V0 = 0u | 0x0003u;
        if (c.V1 == c.V0)
        {
            goto L801CD640;
        }
        goto L801CD730;
    L801CCDE8:;
        c.A0 = 0x80040000u;
        c.A0 = m.ReadU32((c.A0 - 0x3668u));
        c.V0 = c.V0 | 0x8889u;
        { var _r = (ulong)c.A0 * c.V0; c.LO = (uint)_r; c.HI = (uint)(_r >> 32); }
        c.V1 = c.HI;
        c.V1 = c.V1 >> 5;
        c.V0 = c.V1 << 4;
        c.V0 = c.V0 - c.V1;
        c.V0 = c.V0 << 2;
        if (c.A0 != c.V0)
        {
            c.A0 = 0u | 0x0003u;
            goto L801CCE2C;
        }
        c.A0 = 0u | 0x0003u;
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x3824u));
        c.A0 = 0u | 0x07A9u;
        c.RA = 0x801CCE28u;
        Dispatcher.Call(c, m, c.V0);
        c.A0 = 0u | 0x0003u;
    L801CCE2C:;
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x3848u));
        c.A1 = 0u | 0x0001u;
        c.RA = 0x801CCE40u;
        Dispatcher.Call(c, m, c.V0);
        c.V0 = c.V0 << 16;
        c.S0 = (uint)((int)c.V0 >> 16);
        c.V0 = 0xFFFFFFFFu;
        if (c.S0 == c.V0)
        {
            goto L801CD730;
        }
        c.A0 = 0x80180000u;
        c.A0 = c.A0 + 0xAE8u;
        c.RA = 0x801CCE64u;
        SoTN.InitializeEntity_no0(c, m);
        c.V0 = c.S0 << 1;
        c.V0 = c.V0 + c.S0;
        c.V0 = c.V0 << 2;
        c.V0 = c.V0 + c.S0;
        c.V0 = c.V0 << 2;
        c.V1 = 0x80080000u;
        c.V1 = c.V1 + 0x6FECu;
        c.A3 = c.V0 + c.V1;
        c.V0 = m.ReadU32((c.S2 + 0x34u));
        c.V1 = 0x00800000u;
        m.WriteU32((c.S2 + 0x64u), c.S0);
        c.V0 = c.V0 | c.V1;
        m.WriteU32((c.S2 + 0x34u), c.V0);
        m.WriteU8((c.A3 + 0x6u), (byte)0u);
        m.WriteU8((c.A3 + 0x5u), (byte)0u);
        m.WriteU8((c.A3 + 0x4u), (byte)0u);
        c.V1 = m.ReadU32((c.A3 + 0x4u));
        c.A0 = m.ReadU32((c.A3 + 0x4u));
        c.A1 = m.ReadU32((c.A3 + 0x4u));
        c.V0 = 0u | 0x0100u;
        m.WriteU16((c.A3 + 0x2Eu), (ushort)c.V0);
        m.WriteU16((c.A3 + 0x22u), (ushort)c.V0);
        m.WriteU16((c.A3 + 0x2Cu), (ushort)c.V0);
        m.WriteU16((c.A3 + 0x14u), (ushort)c.V0);
        c.V0 = 0u | 0x01F0u;
        m.WriteU16((c.A3 + 0x26u), (ushort)c.V0);
        c.V0 = 0u | 0x0008u;
        m.WriteU16((c.A3 + 0x16u), (ushort)0u);
        m.WriteU16((c.A3 + 0xAu), (ushort)0u);
        m.WriteU16((c.A3 + 0x20u), (ushort)0u);
        m.WriteU16((c.A3 + 0x8u), (ushort)0u);
        m.WriteU16((c.A3 + 0x32u), (ushort)c.V0);
        m.WriteU32((c.A3 + 0x10u), c.V1);
        m.WriteU32((c.A3 + 0x1Cu), c.A0);
        m.WriteU32((c.A3 + 0x28u), c.A1);
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x3824u));
        c.A0 = 0u | 0x000Au;
        c.RA = 0x801CCF04u;
        Dispatcher.Call(c, m, c.V0);
        c.V0 = 0x80070000u;
        c.V0 = (uint)(short)m.ReadU16((c.V0 + 0x33DEu));
        c.A0 = 0x801E0000u;
        c.A0 = c.A0 - 0xAB8u;
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7928u), c.S1);
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7910u), 0u);
        c.V0 = (int)c.V0 < 64 ? 1u : 0u;
        if (c.V0 == 0u)
        {
            m.WriteU16(c.A0, (ushort)0u);
            goto L801CCF68;
        }
        m.WriteU16(c.A0, (ushort)0u);
        c.V1 = 0x80070000u;
        c.V1 = (uint)(short)m.ReadU16((c.V1 + 0x33DAu));
        c.V0 = (int)c.V1 < 64 ? 1u : 0u;
        if (c.V0 == 0u)
        {
            c.V0 = 0u | 0x0001u;
            goto L801CCF58;
        }
        c.V0 = 0u | 0x0001u;
        c.At = 0x801E0000u;
        m.WriteU16((c.At - 0xAB6u), (ushort)c.V0);
        c.S1 = c.S2 + 0x3ACu;
        goto L801CCF6C;
    L801CCF58:;
        c.V0 = (int)c.V1 < 193 ? 1u : 0u;
        if (c.V0 != 0u)
        {
            c.V0 = 0u | 0x0001u;
            goto L801CCF68;
        }
        c.V0 = 0u | 0x0001u;
        m.WriteU16(c.A0, (ushort)c.V0);
    L801CCF68:;
        c.S1 = c.S2 + 0x3ACu;
    L801CCF6C:;
        c.S0 = 0u + 0u;
        c.V0 = 0xFFFF8001u;
        m.WriteU16((c.S2 + 0x54u), (ushort)c.V0);
        c.V0 = 0u | 0x0017u;
        m.WriteU16((c.S2 + 0x56u), (ushort)c.V0);
        c.V0 = 0u | 0x0040u;
        m.WriteU16((c.S2 + 0x24u), (ushort)c.V0);
    L801CCF88:;
        c.A0 = 0u | 0x001Au;
        c.A1 = c.S1 + 0u;
        c.RA = 0x801CCF94u;
        SoTN.CreateEntityFromCurrentEntity_no0(c, m);
        m.WriteU16((c.S1 + 0x30u), (ushort)c.S0);
        c.S0 = c.S0 + 0x1u;
        c.V0 = c.S0 & 0xFFFFu;
        c.V0 = c.V0 < 0x00000002u ? 1u : 0u;
        if (c.V0 != 0u)
        {
            c.S1 = c.S1 + 0xBCu;
            goto L801CCF88;
        }
        c.S1 = c.S1 + 0xBCu;
        c.A0 = c.S2 + 0u;
        c.A1 = c.S3 + 0u;
        c.RA = 0x801CCFB8u;
        SoTN.UpdateClockHands(c, m);
        c.S1 = c.S2 + 0x524u;
        c.S0 = 0u + 0u;
    L801CCFC0:;
        c.A0 = 0u | 0x001Bu;
        c.A1 = c.S1 + 0u;
        c.RA = 0x801CCFCCu;
        SoTN.CreateEntityFromCurrentEntity_no0(c, m);
        m.WriteU16((c.S1 + 0x30u), (ushort)c.S0);
        c.S0 = c.S0 + 0x1u;
        c.V0 = c.S0 & 0xFFFFu;
        c.V0 = c.V0 < 0x00000002u ? 1u : 0u;
        if (c.V0 != 0u)
        {
            c.S1 = c.S1 + 0xBCu;
            goto L801CCFC0;
        }
        c.S1 = c.S1 + 0xBCu;
        c.A0 = c.S2 + 0u;
        c.S1 = c.S2 + 0xBCu;
        c.A1 = m.ReadU32((c.S3 + 0x2D0u));
        c.S0 = 0u + 0u;
        c.RA = 0x801CCFF8u;
        SoTN.UpdateBirdcages(c, m);
        c.A0 = 0u | 0x0020u;
        c.A1 = c.S2 + 0x69Cu;
        c.RA = 0x801CD004u;
        SoTN.CreateEntityFromCurrentEntity_no0(c, m);
        c.V0 = 0xFFFF8001u;
        m.WriteU16((c.S2 + 0x6F0u), (ushort)c.V0);
        c.V0 = 0u | 0x0017u;
        m.WriteU16((c.S2 + 0x6F2u), (ushort)c.V0);
        c.V0 = 0u | 0x0040u;
        m.WriteU16((c.S2 + 0x6C0u), (ushort)c.V0);
        c.V0 = 0u | 0x804Bu;
        m.WriteU16((c.S2 + 0x6B2u), (ushort)c.V0);
        c.V0 = 0u | 0x0008u;
        m.WriteU8((c.S2 + 0x6B5u), (byte)c.V0);
        c.V0 = m.ReadU16((c.S2 + 0x6A2u));
        c.V1 = 0u | 0x0010u;
        m.WriteU8((c.S2 + 0x6B4u), (byte)c.V1);
        c.V0 = c.V0 + 0x4u;
        m.WriteU16((c.S2 + 0x6A2u), (ushort)c.V0);
    L801CD040:;
        c.A0 = 0u | 0x001Cu;
        c.A1 = c.S1 + 0u;
        c.RA = 0x801CD04Cu;
        SoTN.CreateEntityFromCurrentEntity_no0(c, m);
        m.WriteU16((c.S1 + 0x30u), (ushort)c.S0);
        c.S0 = c.S0 + 0x1u;
        c.V0 = c.S0 & 0xFFFFu;
        c.V0 = c.V0 < 0x00000002u ? 1u : 0u;
        if (c.V0 != 0u)
        {
            c.S1 = c.S1 + 0xBCu;
            goto L801CD040;
        }
        c.S1 = c.S1 + 0xBCu;
        c.S1 = c.S2 + 0x8D0u;
        c.S0 = 0u + 0u;
    L801CD06C:;
        c.A0 = 0u | 0x001Du;
        c.A1 = c.S1 + 0u;
        c.RA = 0x801CD078u;
        SoTN.CreateEntityFromCurrentEntity_no0(c, m);
        m.WriteU16((c.S1 + 0x30u), (ushort)c.S0);
        c.S0 = c.S0 + 0x1u;
        c.V0 = c.S0 & 0xFFFFu;
        c.V0 = c.V0 < 0x00000002u ? 1u : 0u;
        if (c.V0 != 0u)
        {
            c.S1 = c.S1 + 0xBCu;
            goto L801CD06C;
        }
        c.S1 = c.S1 + 0xBCu;
        c.S1 = c.S2 + 0xA48u;
        c.S0 = 0u + 0u;
    L801CD098:;
        c.A0 = 0u | 0x001Eu;
        c.A1 = c.S1 + 0u;
        c.RA = 0x801CD0A4u;
        SoTN.CreateEntityFromCurrentEntity_no0(c, m);
        m.WriteU16((c.S1 + 0x30u), (ushort)c.S0);
        c.S0 = c.S0 + 0x1u;
        c.V0 = c.S0 & 0xFFFFu;
        c.V0 = c.V0 < 0x00000002u ? 1u : 0u;
        if (c.V0 != 0u)
        {
            c.S1 = c.S1 + 0xBCu;
            goto L801CD098;
        }
        c.S1 = c.S1 + 0xBCu;
        goto L801CD730;
    L801CD0C4:;
        c.V0 = m.ReadU32((c.S3 + 0x2D8u));
        if (c.V0 != 0u)
        {
            c.A0 = c.S2 + 0u;
            goto L801CD0EC;
        }
        c.A0 = c.S2 + 0u;
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x3824u));
        c.A0 = 0u | 0x07A9u;
        c.RA = 0x801CD0E8u;
        Dispatcher.Call(c, m, c.V0);
        c.A0 = c.S2 + 0u;
    L801CD0EC:;
        c.A1 = c.S3 + 0u;
        c.RA = 0x801CD0F4u;
        SoTN.UpdateClockHands(c, m);
        c.V0 = m.ReadU32((c.S3 + 0x2D4u));
        if (c.V0 != 0u)
        {
            goto L801CD170;
        }
        c.V0 = m.ReadU32((c.S3 + 0x2D8u));
        if (c.V0 != 0u)
        {
            goto L801CD170;
        }
        c.V0 = m.ReadU32((c.S3 + 0x2D0u));
        if (c.V0 != 0u)
        {
            c.V0 = 0x2AAA0000u;
            goto L801CD170;
        }
        c.V0 = 0x2AAA0000u;
        c.A0 = m.ReadU32((c.S3 + 0x2CCu));
        c.V0 = c.V0 | 0xAAABu;
        c.A0 = c.A0 + 0xBu;
        { var _r = (long)(int)c.A0 * (int)c.V0; c.LO = (uint)_r; c.HI = (uint)(_r >> 32); }
        c.V0 = (uint)((int)c.A0 >> 31);
        c.V1 = c.HI;
        c.V1 = (uint)((int)c.V1 >> 1);
        c.V1 = c.V1 - c.V0;
        c.V0 = c.V1 << 1;
        c.V0 = c.V0 + c.V1;
        c.V0 = c.V0 << 2;
        c.A0 = c.A0 - c.V0;
        c.A0 = c.A0 + 0x1u;
        m.WriteU16((c.S2 + 0x84u), (ushort)c.A0);
        c.V0 = m.ReadU16((c.S2 + 0x84u));
        if (c.V0 != 0u)
        {
            c.V0 = 0u | 0x000Cu;
            goto L801CD170;
        }
        c.V0 = 0u | 0x000Cu;
        m.WriteU16((c.S2 + 0x84u), (ushort)c.V0);
    L801CD170:;
        c.A1 = m.ReadU32((c.S3 + 0x2D0u));
        c.A0 = c.S2 + 0u;
        c.RA = 0x801CD17Cu;
        SoTN.UpdateBirdcages(c, m);
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU8((c.V0 - 0x4214u));
        if (c.V0 != 0u)
        {
            goto L801CD730;
        }
        c.V0 = 0x80070000u;
        c.V0 = m.ReadU16((c.V0 + 0x33DAu));
        c.V0 = c.V0 - 0x30u;
        c.V0 = c.V0 < 0x000000A1u ? 1u : 0u;
        if (c.V0 == 0u)
        {
            c.V1 = 0u | 0xFFB8u;
            goto L801CD730;
        }
        c.V1 = 0u | 0xFFB8u;
        c.S0 = 0x80090000u;
        c.S0 = m.ReadU16((c.S0 + 0x7C14u));
        c.V0 = c.S0 + c.V1;
        c.V0 = c.V0 & 0xFFFFu;
        c.V0 = c.V0 < 0x00000002u ? 1u : 0u;
        if (c.V0 == 0u)
        {
            goto L801CD730;
        }
        c.S0 = 0x80090000u;
        c.S0 = m.ReadU16((c.S0 + 0x7C18u));
        c.V0 = c.S0 + c.V1;
        c.V0 = c.V0 & 0xFFFFu;
        c.V0 = c.V0 < 0x00000002u ? 1u : 0u;
        if (c.V0 == 0u)
        {
            c.A0 = 0u | 0x0002u;
            goto L801CD730;
        }
        c.A0 = 0u | 0x0002u;
        // Added for Integrated to required gold + silver and not gold+gold and silver+silver
        if (m.ReadU32(0x80097C14) == m.ReadU32(0x80097C18) && m.ReadU8(0x8000C000) == (byte)PresetId.Integrated)
        {
            goto L801CD730;
        }
        goto L801CD728;
    L801CD1F4:;
        c.At = 0x80070000u;
        m.WriteU32((c.At + 0x2EFCu), c.S1);
        c.S1 = 0x80070000u;
        c.S1 = c.S1 + 0x33D8u;
        c.At = 0x801E0000u;
        m.WriteU16((c.At - 0xAB8u), (ushort)0u);
        c.At = 0x801E0000u;
        m.WriteU16((c.At - 0xAB6u), (ushort)0u);
        c.At = 0x80070000u;
        m.WriteU32((c.At + 0x2EF4u), 0u);
        c.A0 = m.ReadU16((c.S2 + 0x2Eu));
        c.V1 = 0x80070000u;
        c.V1 = m.ReadU16((c.V1 + 0x33DAu));
        c.V0 = c.A0 < 0x0000000Au ? 1u : 0u;
        if (c.V0 == 0u)
        {
            c.V0 = c.A0 << 2;
            goto L801CD730;
        }
        c.V0 = c.A0 << 2;
        c.At = 0x801C0000u;
        c.At = c.At + c.V0;
        c.V0 = m.ReadU32((c.At + 0x1468u));
        switch (c.V0)
        {
            case 0x801CD24Cu: goto L801CD24C;
            case 0x801CD2D8u: goto L801CD2D8;
            case 0x801CD324u: goto L801CD324;
            case 0x801CD388u: goto L801CD388;
            case 0x801CD3D8u: goto L801CD3D8;
            case 0x801CD450u: goto L801CD450;
            case 0x801CD4C8u: goto L801CD4C8;
            case 0x801CD4E8u: goto L801CD4E8;
            case 0x801CD5C0u: goto L801CD5C0;
            case 0x801CD5F8u: goto L801CD5F8;
            default: Dispatcher.Call(c, m, c.V0); return;
        }
    L801CD24C:;
        c.S0 = 0x80070000u;
        c.S0 = m.ReadU32((c.S0 + 0x2F2Cu));
        c.V0 = c.S0 & 0x0007u;
        if (c.V0 == 0u)
        {
            goto L801CD2CC;
        }
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x3668u));
        c.V0 = c.V0 & 0x0001u;
        if (c.V0 == 0u)
        {
            c.V0 = c.S0 & 0x0001u;
            goto L801CD730;
        }
        c.V0 = c.S0 & 0x0001u;
        if (c.V0 == 0u)
        {
            c.V0 = 0u | 0x0008u;
            goto L801CD294;
        }
        c.V0 = 0u | 0x0008u;
        c.At = 0x80070000u;
        m.WriteU32((c.At + 0x2EF4u), c.V0);
        goto L801CD730;
    L801CD294:;
        c.V0 = c.S0 & 0x0002u;
        if (c.V0 == 0u)
        {
            c.V0 = 0u | 0x0004u;
            goto L801CD2B0;
        }
        c.V0 = 0u | 0x0004u;
        c.At = 0x80070000u;
        m.WriteU32((c.At + 0x2EF4u), c.V0);
        goto L801CD730;
    L801CD2B0:;
        c.V0 = c.S0 & 0x0004u;
        if (c.V0 == 0u)
        {
            c.V0 = 0u | 0x0002u;
            goto L801CD730;
        }
        c.V0 = 0u | 0x0002u;
        c.At = 0x80070000u;
        m.WriteU32((c.At + 0x2EF4u), c.V0);
        goto L801CD730;
    L801CD2CC:;
        c.V0 = m.ReadU16((c.S2 + 0x2Eu));
        m.WriteU16((c.S2 + 0x88u), (ushort)0u);
        goto L801CD5EC;
    L801CD2D8:;
        c.V0 = 0x80070000u;
        c.V0 = m.ReadU32((c.V0 + 0x2F20u));
        c.V0 = c.V0 & 0x0001u;
        if (c.V0 == 0u)
        {
            goto L801CD730;
        }
        c.A0 = m.ReadU16((c.S2 + 0x2Eu));
        c.V0 = c.A0 + 0x1u;
        m.WriteU16((c.S2 + 0x2Eu), (ushort)c.V0);
        c.V0 = c.V1 << 16;
        c.V1 = (uint)((int)c.V0 >> 16);
        c.V0 = (int)c.V1 < 73 ? 1u : 0u;
        if (c.V0 == 0u)
        {
            c.V0 = (int)c.V1 < 184 ? 1u : 0u;
            goto L801CD730;
        }
        c.V0 = (int)c.V1 < 184 ? 1u : 0u;
        if (c.V0 != 0u)
        {
            c.V0 = c.A0 + 0x2u;
            goto L801CD730;
        }
        c.V0 = c.A0 + 0x2u;
        m.WriteU16((c.S2 + 0x2Eu), (ushort)c.V0);
        goto L801CD730;
    L801CD324:;
        c.V0 = c.V1 - 0x41u;
        c.V0 = c.V0 < 0x0000003Fu ? 1u : 0u;
        if (c.V0 == 0u)
        {
            c.V0 = 0u | 0x8040u;
            goto L801CD344;
        }
        c.V0 = 0u | 0x8040u;
        c.At = 0x80070000u;
        m.WriteU32((c.At + 0x2EF4u), c.V0);
        goto L801CD730;
    L801CD344:;
        c.V0 = c.V1 - 0x80u;
        c.V0 = c.V0 < 0x00000040u ? 1u : 0u;
        if (c.V0 == 0u)
        {
            c.V0 = 0u | 0x2040u;
            goto L801CD364;
        }
        c.V0 = 0u | 0x2040u;
        c.At = 0x80070000u;
        m.WriteU32((c.At + 0x2EF4u), c.V0);
        goto L801CD730;
    L801CD364:;
        c.V0 = 0x80070000u;
        c.V0 = m.ReadU32((c.V0 + 0x2F20u));
        c.V0 = c.V0 & 0x0001u;
        if (c.V0 == 0u)
        {
            goto L801CD730;
        }
        c.V0 = m.ReadU16((c.S2 + 0x2Eu));
        c.V0 = c.V0 + 0x1u;
        goto L801CD5F0;
    L801CD388:;
        c.V0 = c.V1 << 16;
        c.V0 = (uint)((int)c.V0 >> 16);
        c.V0 = (int)c.V0 < 73 ? 1u : 0u;
        if (c.V0 == 0u)
        {
            goto L801CD3B4;
        }
        c.V0 = m.ReadU16((c.S1 + 0x14u));
        if (c.V0 == 0u)
        {
            c.V0 = 0u | 0x2000u;
            goto L801CD3CC;
        }
        c.V0 = 0u | 0x2000u;
        goto L801CD3C4;
    L801CD3B4:;
        c.V0 = m.ReadU16((c.S1 + 0x14u));
        if (c.V0 != 0u)
        {
            c.V0 = 0u | 0x8000u;
            goto L801CD3CC;
        }
        c.V0 = 0u | 0x8000u;
    L801CD3C4:;
        c.At = 0x80070000u;
        m.WriteU32((c.At + 0x2EF4u), c.V0);
    L801CD3CC:;
        c.V0 = m.ReadU16((c.S2 + 0x2Eu));
        c.V0 = c.V0 + 0x1u;
        goto L801CD5F0;
    L801CD3D8:;
        c.V1 = m.ReadU32((c.S2 + 0x64u));
        c.V0 = c.V1 << 1;
        c.V0 = c.V0 + c.V1;
        c.V0 = c.V0 << 2;
        c.V0 = c.V0 + c.V1;
        c.V0 = c.V0 << 2;
        c.V1 = 0x80080000u;
        c.V1 = c.V1 + 0x6FECu;
        c.A3 = c.V0 + c.V1;
        c.V0 = m.ReadU8((c.A3 + 0x6u));
        c.V0 = c.V0 + 0x10u;
        m.WriteU8((c.A3 + 0x6u), (byte)c.V0);
        m.WriteU8((c.A3 + 0x5u), (byte)c.V0);
        m.WriteU8((c.A3 + 0x4u), (byte)c.V0);
        c.A0 = m.ReadU32((c.A3 + 0x4u));
        c.A1 = m.ReadU32((c.A3 + 0x4u));
        c.A2 = m.ReadU32((c.A3 + 0x4u));
        c.V1 = m.ReadU8((c.A3 + 0x4u));
        c.V0 = 0u | 0x0031u;
        m.WriteU16((c.A3 + 0x32u), (ushort)c.V0);
        c.V1 = c.V1 < 0x000000C1u ? 1u : 0u;
        m.WriteU32((c.A3 + 0x10u), c.A0);
        m.WriteU32((c.A3 + 0x1Cu), c.A1);
        if (c.V1 != 0u)
        {
            m.WriteU32((c.A3 + 0x28u), c.A2);
            goto L801CD730;
        }
        m.WriteU32((c.A3 + 0x28u), c.A2);
        c.V0 = m.ReadU16((c.S2 + 0x2Eu));
        c.V0 = c.V0 + 0x1u;
        goto L801CD5F0;
    L801CD450:;
        c.V1 = m.ReadU32((c.S2 + 0x64u));
        c.V0 = c.V1 << 1;
        c.V0 = c.V0 + c.V1;
        c.V0 = c.V0 << 2;
        c.V0 = c.V0 + c.V1;
        c.V0 = c.V0 << 2;
        c.V1 = 0x80080000u;
        c.V1 = c.V1 + 0x6FECu;
        c.A3 = c.V0 + c.V1;
        c.V0 = m.ReadU8((c.A3 + 0x6u));
        c.V0 = c.V0 - 0x4u;
        m.WriteU8((c.A3 + 0x6u), (byte)c.V0);
        m.WriteU8((c.A3 + 0x5u), (byte)c.V0);
        m.WriteU8((c.A3 + 0x4u), (byte)c.V0);
        c.V1 = m.ReadU32((c.A3 + 0x4u));
        c.A0 = m.ReadU32((c.A3 + 0x4u));
        c.V0 = m.ReadU8((c.A3 + 0x4u));
        c.A1 = m.ReadU32((c.A3 + 0x4u));
        c.V0 = c.V0 < 0x00000008u ? 1u : 0u;
        m.WriteU32((c.A3 + 0x10u), c.V1);
        m.WriteU32((c.A3 + 0x1Cu), c.A0);
        if (c.V0 == 0u)
        {
            m.WriteU32((c.A3 + 0x28u), c.A1);
            goto L801CD730;
        }
        m.WriteU32((c.A3 + 0x28u), c.A1);
        c.V0 = 0u | 0x0008u;
        m.WriteU16((c.A3 + 0x32u), (ushort)c.V0);
        c.V0 = m.ReadU16((c.S2 + 0x2Eu));
        c.V0 = c.V0 + 0x1u;
        goto L801CD5F0;
    L801CD4C8:;
        c.V1 = m.ReadU16((c.S2 + 0x2Eu));
        c.V0 = 0u | 0x0001u;
        m.WriteU16((c.S2 + 0x5A4u), (ushort)c.V0);
        m.WriteU16((c.S2 + 0x660u), (ushort)c.V0);
        m.WriteU16((c.S2 + 0x88u), (ushort)c.V0);
        c.V1 = c.V1 + 0x1u;
        m.WriteU16((c.S2 + 0x2Eu), (ushort)c.V1);
        goto L801CD730;
    L801CD4E8:;
        c.V0 = m.ReadU16((c.S2 + 0x88u));
        c.V0 = c.V0 - 0x1u;
        m.WriteU16((c.S2 + 0x88u), (ushort)c.V0);
        c.V0 = c.V0 & 0xFFFFu;
        if (c.V0 != 0u)
        {
            c.T0 = 0x91A20000u;
            goto L801CD730;
        }
        c.T0 = 0x91A20000u;
        c.T0 = c.T0 | 0xB3C5u;
        c.V1 = m.ReadU32((c.S2 + 0x428u));
        c.V0 = m.ReadU16((c.S2 + 0x2Eu));
        c.A3 = c.V1 << 16;
        c.A1 = (uint)((int)c.A3 >> 16);
        { var _r = (long)(int)c.A1 * (int)c.T0; c.LO = (uint)_r; c.HI = (uint)(_r >> 32); }
        c.A2 = m.ReadU32((c.S2 + 0x4E4u));
        m.WriteU16((c.S2 + 0x88u), (ushort)0u);
        c.V0 = c.V0 + 0x1u;
        m.WriteU32((c.S2 + 0x4E8u), c.A2);
        c.A2 = c.A2 << 16;
        c.A0 = (uint)((int)c.A2 >> 16);
        c.A3 = (uint)((int)c.A3 >> 31);
        c.A2 = (uint)((int)c.A2 >> 31);
        m.WriteU32((c.S2 + 0x42Cu), c.V1);
        m.WriteU16((c.S2 + 0x2Eu), (ushort)c.V0);
        c.V1 = c.HI;
        c.V1 = c.V1 + c.A1;
        c.V1 = (uint)((int)c.V1 >> 11);
        c.V1 = c.V1 - c.A3;
        { var _r = (long)(int)c.A0 * (int)c.T0; c.LO = (uint)_r; c.HI = (uint)(_r >> 32); }
        c.V0 = c.V1 << 3;
        c.V0 = c.V0 - c.V1;
        c.V0 = c.V0 << 5;
        c.V0 = c.V0 + c.V1;
        c.V0 = c.V0 << 4;
        c.A1 = c.A1 - c.V0;
        c.A1 = c.A1 << 16;
        c.A1 = (uint)((int)c.A1 >> 16);
        c.V0 = 0u | 0x1518u;
        c.V0 = c.V0 - c.A1;
        m.WriteU32((c.S2 + 0x430u), c.V0);
        c.V1 = c.HI;
        c.V1 = c.V1 + c.A0;
        c.V1 = (uint)((int)c.V1 >> 11);
        c.V1 = c.V1 - c.A2;
        c.V0 = c.V1 << 3;
        c.V0 = c.V0 - c.V1;
        c.V0 = c.V0 << 5;
        c.V0 = c.V0 + c.V1;
        c.V0 = c.V0 << 4;
        c.A0 = c.A0 - c.V0;
        c.A0 = c.A0 << 16;
        c.A0 = (uint)((int)c.A0 >> 16);
        c.A0 = c.A0 + 0x708u;
        m.WriteU32((c.S2 + 0x4ECu), c.A0);
        goto L801CD730;
    L801CD5C0:;
        c.A0 = c.S2 + 0u;
        c.RA = 0x801CD5C8u;
        SoTN.func_us_801CCAAC(c, m);
        c.V0 = m.ReadU16((c.S2 + 0x88u));
        c.V0 = c.V0 < 0x00000200u ? 1u : 0u;
        if (c.V0 != 0u)
        {
            c.V0 = 0u | 0x000Du;
            goto L801CD730;
        }
        c.V0 = 0u | 0x000Du;
        m.WriteU16((c.S2 + 0x84u), (ushort)c.V0);
        c.V0 = m.ReadU16((c.S2 + 0x2Eu));
        c.V1 = 0u | 0x0380u;
        m.WriteU16((c.S2 + 0x88u), (ushort)c.V1);
    L801CD5EC:;
        c.V0 = c.V0 + 0x1u;
    L801CD5F0:;
        m.WriteU16((c.S2 + 0x2Eu), (ushort)c.V0);
        goto L801CD730;
    L801CD5F8:;
        c.V0 = m.ReadU16((c.S2 + 0x88u));
        c.V0 = c.V0 - 0x1u;
        m.WriteU16((c.S2 + 0x88u), (ushort)c.V0);
        c.V0 = c.V0 & 0xFFFFu;
        if (c.V0 != 0u)
        {
            c.V1 = 0u | 0x0001u;
            goto L801CD730;
        }
        c.V1 = 0u | 0x0001u;
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x3794u));
        c.At = 0x80040000u;
        m.WriteU8((c.At - 0x4214u), (byte)c.V1);
        c.A0 = 0u + 0u;
        c.RA = 0x801CD62Cu;
        Dispatcher.Call(c, m, c.V0);
        c.A0 = 0u | 0x0003u;
        c.RA = 0x801CD634u;
        SoTN.SetStep_no0(c, m);
        c.V0 = 0u | 0x0140u;
        m.WriteU16((c.S2 + 0x88u), (ushort)c.V0);
        goto L801CD730;
    L801CD640:;
        c.At = 0x801E0000u;
        m.WriteU16((c.At - 0xAB8u), (ushort)0u);
        c.At = 0x801E0000u;
        m.WriteU16((c.At - 0xAB6u), (ushort)0u);
        c.V0 = m.ReadU16((c.S2 + 0x2Eu));
        if (c.V0 == 0u)
        {
            goto L801CD670;
        }
        if (c.V0 == c.S1)
        {
            goto L801CD70C;
        }
        goto L801CD730;
    L801CD670:;
        c.V0 = m.ReadU16((c.S2 + 0x88u));
        c.V0 = c.V0 - 0x1u;
        m.WriteU16((c.S2 + 0x88u), (ushort)c.V0);
        c.V0 = c.V0 & 0xFFFFu;
        if (c.V0 != 0u)
        {
            goto L801CD730;
        }
        c.V0 = m.ReadU32((c.S2 + 0x428u));
        m.WriteU32((c.S2 + 0x42Cu), c.V0);
        c.V0 = m.ReadU32((c.S2 + 0x4E4u));
        c.V1 = m.ReadU32((c.S3 + 0x2D0u));
        m.WriteU32((c.S2 + 0x4E8u), c.V0);
        c.V0 = c.V1 << 4;
        c.V0 = c.V0 - c.V1;
        c.V0 = c.V0 << 18;
        c.V0 = (uint)((int)c.V0 >> 16);
        c.V0 = c.V0 + 0x708u;
        m.WriteU32((c.S2 + 0x430u), c.V0);
        c.A0 = m.ReadU32((c.S3 + 0x2CCu));
        c.V0 = m.ReadU16((c.S2 + 0x2Eu));
        c.A1 = m.ReadU32((c.S3 + 0x2D0u));
        m.WriteU16((c.S2 + 0x88u), (ushort)0u);
        c.V0 = c.V0 + 0x1u;
        c.V1 = c.A0 << 2;
        c.V1 = c.V1 + c.A0;
        m.WriteU16((c.S2 + 0x2Eu), (ushort)c.V0);
        c.V0 = c.V1 << 4;
        c.V0 = c.V0 - c.V1;
        c.V0 = c.V0 << 2;
        c.V1 = c.A1 << 2;
        c.V1 = c.V1 + c.A1;
        c.V0 = c.V0 + c.V1;
        c.V0 = c.V0 << 16;
        c.V0 = (uint)((int)c.V0 >> 16);
        c.V1 = 0u | 0x1518u;
        c.V1 = c.V1 - c.V0;
        m.WriteU32((c.S2 + 0x4ECu), c.V1);
        goto L801CD730;
    L801CD70C:;
        c.A0 = c.S2 + 0u;
        c.RA = 0x801CD714u;
        SoTN.func_us_801CCAAC(c, m);
        c.V0 = m.ReadU16((c.S2 + 0x88u));
        c.V0 = c.V0 < 0x00000200u ? 1u : 0u;
        if (c.V0 != 0u)
        {
            c.A0 = 0u | 0x0001u;
            goto L801CD730;
        }
        c.A0 = 0u | 0x0001u;
    L801CD728:;
        c.RA = 0x801CD730u;
        SoTN.SetStep_no0(c, m);
    L801CD730:;
        c.RA = m.ReadU32((c.SP + 0x20u));
        c.S3 = m.ReadU32((c.SP + 0x1Cu));
        c.S2 = m.ReadU32((c.SP + 0x18u));
        c.S1 = m.ReadU32((c.SP + 0x14u));
        c.S0 = m.ReadU32((c.SP + 0x10u));
        c.SP = c.SP + 0x28u;
        return;
    }

    // Reverse Clock Statue
    public static void func_801C0E1C(CpuContext c, IMemory m)
    {
        c.SP = c.SP - 0x28u;
        m.WriteU32((c.SP + 0x18u), c.S2);
        c.S2 = c.A0 + 0u;
        m.WriteU32((c.SP + 0x20u), c.RA);
        m.WriteU32((c.SP + 0x1Cu), c.S3);
        m.WriteU32((c.SP + 0x14u), c.S1);
        m.WriteU32((c.SP + 0x10u), c.S0);
        c.V0 = m.ReadU16((c.S2 + 0x84u));
        c.S3 = 0x80090000u;
        c.S3 = c.S3 + 0x7964u;
        if (c.V0 == 0u)
        {
            goto L801C0EA4;
        }
        c.V0 = m.ReadU16((c.S2 + 0x86u));
        if (c.V0 != 0u)
        {
            goto L801C0E94;
        }
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x3824u));
        c.A0 = 0u | 0x07A6u;
        c.RA = 0x801C0E70u;
        Dispatcher.Call(c, m, c.V0);
        c.V0 = m.ReadU16((c.S2 + 0x84u));
        c.V0 = c.V0 - 0x1u;
        m.WriteU16((c.S2 + 0x84u), (ushort)c.V0);
        c.V0 = c.V0 & 0xFFFFu;
        if (c.V0 == 0u)
        {
            c.V0 = 0u | 0x0040u;
            goto L801C0EA4;
        }
        c.V0 = 0u | 0x0040u;
        m.WriteU16((c.S2 + 0x86u), (ushort)c.V0);
        goto L801C0EA4;
    L801C0E94:;
        c.V0 = m.ReadU16((c.S2 + 0x86u));
        c.V0 = c.V0 - 0x1u;
        m.WriteU16((c.S2 + 0x86u), (ushort)c.V0);
    L801C0EA4:;
        c.V0 = 0x80090000u;
        c.V0 = m.ReadU32((c.V0 + 0x73FCu));
        c.S1 = 0x80070000u;
        c.S1 = c.S1 + 0x33D8u;
        if (c.V0 != 0u)
        {
            goto L801C0EE4;
        }
        c.V0 = 0x80070000u;
        c.V0 = (uint)(short)m.ReadU16((c.V0 + 0x33DEu));
        c.V0 = (int)c.V0 < 144 ? 1u : 0u;
        if (c.V0 == 0u)
        {
            goto L801C0EFC;
        }
        c.At = 0x801D0000u;
        m.WriteU16((c.At + 0x4B48u), (ushort)0u);
        goto L801C0EFC;
    L801C0EE4:;
        c.V0 = m.ReadU16((c.S2 + 0x8Au));
        if (c.V0 != 0u)
        {
            c.V0 = 0u | 0x0001u;
            goto L801C0EFC;
        }
        c.V0 = 0u | 0x0001u;
        c.At = 0x801D0000u;
        m.WriteU16((c.At + 0x4B48u), (ushort)c.V0);
    L801C0EFC:;
        c.V0 = 0x80090000u;
        c.V0 = m.ReadU16((c.V0 + 0x73FCu));
        m.WriteU16((c.S2 + 0x8Au), (ushort)c.V0);
        c.V0 = m.ReadU32((c.S3 + 0x2D0u));
        if (m.ReadU32(0x801C0F0C) != 0x8E6202D0)     // Clock Statue always Open
        {
            c.V0 = 0;
        }
        c.V0 = c.V0 & 0x0001u;
        if (c.V0 == 0u)
        {
            c.V0 = 0u | 0x0001u;
            goto L801C0F44;
        }
        c.V0 = 0u | 0x0001u;
        c.V0 = (uint)(short)m.ReadU16((c.S1 + 0x6u));
        c.V0 = (int)c.V0 < 144 ? 1u : 0u;
        if (c.V0 == 0u)
        {
            goto L801C0F4C;
        }
        c.At = 0x801D0000u;
        m.WriteU16((c.At + 0x4B4Au), (ushort)0u);
        goto L801C0F4C;
    L801C0F44:;
        c.At = 0x801D0000u;
        m.WriteU16((c.At + 0x4B4Au), (ushort)c.V0);
    L801C0F4C:;
        c.V1 = m.ReadU16((c.S2 + 0x2Cu));
        c.S1 = 0u | 0x0001u;
        if (c.V1 == c.S1)
        {
            c.V0 = (int)c.V1 < 2 ? 1u : 0u;
            goto L801C1268;
        }
        c.V0 = (int)c.V1 < 2 ? 1u : 0u;
        if (c.V0 == 0u)
        {
            goto L801C0F74;
        }
        if (c.V1 == 0u)
        {
            c.V0 = 0x88880000u;
            goto L801C0F90;
        }
        c.V0 = 0x88880000u;
        goto L801C1790;
    L801C0F74:;
        c.V0 = 0u | 0x0002u;
        if (c.V1 == c.V0)
        {
            c.V0 = 0u | 0x0003u;
            goto L801C139C;
        }
        c.V0 = 0u | 0x0003u;
        if (c.V1 == c.V0)
        {
            goto L801C16A0;
        }
        goto L801C1790;
    L801C0F90:;
        c.A0 = 0x80040000u;
        c.A0 = m.ReadU32((c.A0 - 0x3668u));
        c.V0 = c.V0 | 0x8889u;
        { var _r = (ulong)c.A0 * c.V0; c.LO = (uint)_r; c.HI = (uint)(_r >> 32); }
        c.V1 = c.HI;
        c.V1 = c.V1 >> 5;
        c.V0 = c.V1 << 4;
        c.V0 = c.V0 - c.V1;
        c.V0 = c.V0 << 2;
        if (c.A0 != c.V0)
        {
            c.A0 = 0u | 0x0003u;
            goto L801C0FD4;
        }
        c.A0 = 0u | 0x0003u;
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x3824u));
        c.A0 = 0u | 0x07A9u;
        c.RA = 0x801C0FD0u;
        Dispatcher.Call(c, m, c.V0);
        c.A0 = 0u | 0x0003u;
    L801C0FD4:;
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x3848u));
        c.A1 = 0u | 0x0001u;
        c.RA = 0x801C0FE8u;
        Dispatcher.Call(c, m, c.V0);
        c.V0 = c.V0 << 16;
        c.S0 = (uint)((int)c.V0 >> 16);
        c.V0 = 0xFFFFFFFFu;
        if (c.S0 == c.V0)
        {
            goto L801C1790;
        }
        c.A0 = 0x80180000u;
        c.A0 = c.A0 + 0xAB0u;
        c.RA = 0x801C100Cu;
        SoTN.func_801BB44C(c, m);
        c.V0 = c.S0 << 1;
        c.V0 = c.V0 + c.S0;
        c.V0 = c.V0 << 2;
        c.V0 = c.V0 + c.S0;
        c.V0 = c.V0 << 2;
        c.V1 = 0x80080000u;
        c.V1 = c.V1 + 0x6FECu;
        c.A3 = c.V0 + c.V1;
        c.V0 = m.ReadU32((c.S2 + 0x34u));
        c.V1 = 0x00800000u;
        m.WriteU32((c.S2 + 0x64u), c.S0);
        c.V0 = c.V0 | c.V1;
        m.WriteU32((c.S2 + 0x34u), c.V0);
        m.WriteU8((c.A3 + 0x6u), (byte)0u);
        m.WriteU8((c.A3 + 0x5u), (byte)0u);
        m.WriteU8((c.A3 + 0x4u), (byte)0u);
        c.V1 = m.ReadU32((c.A3 + 0x4u));
        c.A0 = m.ReadU32((c.A3 + 0x4u));
        c.A1 = m.ReadU32((c.A3 + 0x4u));
        c.V0 = 0u | 0x0100u;
        m.WriteU16((c.A3 + 0x2Eu), (ushort)c.V0);
        m.WriteU16((c.A3 + 0x22u), (ushort)c.V0);
        m.WriteU16((c.A3 + 0x2Cu), (ushort)c.V0);
        m.WriteU16((c.A3 + 0x14u), (ushort)c.V0);
        c.V0 = 0u | 0x01F0u;
        m.WriteU16((c.A3 + 0x26u), (ushort)c.V0);
        c.V0 = 0u | 0x0008u;
        m.WriteU16((c.A3 + 0x16u), (ushort)0u);
        m.WriteU16((c.A3 + 0xAu), (ushort)0u);
        m.WriteU16((c.A3 + 0x20u), (ushort)0u);
        m.WriteU16((c.A3 + 0x8u), (ushort)0u);
        m.WriteU16((c.A3 + 0x32u), (ushort)c.V0);
        m.WriteU32((c.A3 + 0x10u), c.V1);
        m.WriteU32((c.A3 + 0x1Cu), c.A0);
        m.WriteU32((c.A3 + 0x28u), c.A1);
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x3824u));
        c.A0 = 0u | 0x000Au;
        c.RA = 0x801C10ACu;
        Dispatcher.Call(c, m, c.V0);
        c.V0 = 0x80070000u;
        c.V0 = (uint)(short)m.ReadU16((c.V0 + 0x33DEu));
        c.A0 = 0x801D0000u;
        c.A0 = c.A0 + 0x4B48u;
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7928u), c.S1);
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7910u), 0u);
        c.V0 = (int)c.V0 < 193 ? 1u : 0u;
        if (c.V0 != 0u)
        {
            m.WriteU16(c.A0, (ushort)0u);
            goto L801C110C;
        }
        m.WriteU16(c.A0, (ushort)0u);
        c.V1 = 0x80070000u;
        c.V1 = (uint)(short)m.ReadU16((c.V1 + 0x33DAu));
        c.V0 = (int)c.V1 < 64 ? 1u : 0u;
        if (c.V0 == 0u)
        {
            c.V0 = 0u | 0x0001u;
            goto L801C10F8;
        }
        c.V0 = 0u | 0x0001u;
        m.WriteU16(c.A0, (ushort)c.V0);
        goto L801C110C;
    L801C10F8:;
        c.V0 = (int)c.V1 < 193 ? 1u : 0u;
        if (c.V0 != 0u)
        {
            c.V0 = 0u | 0x0001u;
            goto L801C110C;
        }
        c.V0 = 0u | 0x0001u;
        c.At = 0x801D0000u;
        m.WriteU16((c.At + 0x4B4Au), (ushort)c.V0);
    L801C110C:;
        c.S1 = c.S2 + 0x3ACu;
        c.S0 = 0u + 0u;
        c.V0 = 0xFFFF8002u;
        m.WriteU16((c.S2 + 0x54u), (ushort)c.V0);
        c.V0 = 0u | 0x0017u;
        m.WriteU16((c.S2 + 0x56u), (ushort)c.V0);
        c.V0 = 0u | 0x0040u;
        m.WriteU16((c.S2 + 0x24u), (ushort)c.V0);
    L801C112C:;
        c.A0 = 0u | 0x001Au;
        c.A1 = c.S1 + 0u;
        c.RA = 0x801C1138u;
        SoTN.func_801B999C(c, m);
        m.WriteU16((c.S1 + 0x30u), (ushort)c.S0);
        c.S0 = c.S0 + 0x1u;
        c.V0 = c.S0 & 0xFFFFu;
        c.V0 = c.V0 < 0x00000002u ? 1u : 0u;
        if (c.V0 != 0u)
        {
            c.S1 = c.S1 + 0xBCu;
            goto L801C112C;
        }
        c.S1 = c.S1 + 0xBCu;
        c.A0 = c.S2 + 0u;
        c.A1 = c.S3 + 0u;
        c.RA = 0x801C115Cu;
        SoTN.func_801C0DD4(c, m);
        c.S1 = c.S2 + 0x524u;
        c.S0 = 0u + 0u;
    L801C1164:;
        c.A0 = 0u | 0x001Bu;
        c.A1 = c.S1 + 0u;
        c.RA = 0x801C1170u;
        SoTN.func_801B999C(c, m);
        m.WriteU16((c.S1 + 0x30u), (ushort)c.S0);
        c.S0 = c.S0 + 0x1u;
        c.V0 = c.S0 & 0xFFFFu;
        c.V0 = c.V0 < 0x00000002u ? 1u : 0u;
        if (c.V0 != 0u)
        {
            c.S1 = c.S1 + 0xBCu;
            goto L801C1164;
        }
        c.S1 = c.S1 + 0xBCu;
        c.A0 = c.S2 + 0u;
        c.S1 = c.S2 + 0xBCu;
        c.A1 = m.ReadU32((c.S3 + 0x2D0u));
        c.S0 = 0u + 0u;
        c.RA = 0x801C119Cu;
        SoTN.func_801C0D8C(c, m);
        c.A0 = 0u | 0x0020u;
        c.A1 = c.S2 + 0x69Cu;
        c.RA = 0x801C11A8u;
        SoTN.func_801B999C(c, m);
        c.V0 = 0xFFFF8002u;
        m.WriteU16((c.S2 + 0x6F0u), (ushort)c.V0);
        c.V0 = 0u | 0x0017u;
        m.WriteU16((c.S2 + 0x6F2u), (ushort)c.V0);
        c.V0 = 0u | 0x0040u;
        m.WriteU16((c.S2 + 0x6C0u), (ushort)c.V0);
        c.V0 = 0u | 0x804Bu;
        m.WriteU16((c.S2 + 0x6B2u), (ushort)c.V0);
        c.V0 = 0u | 0x0008u;
        m.WriteU8((c.S2 + 0x6B5u), (byte)c.V0);
        c.V0 = m.ReadU16((c.S2 + 0x6A2u));
        c.V1 = 0u | 0x0010u;
        m.WriteU8((c.S2 + 0x6B4u), (byte)c.V1);
        c.V0 = c.V0 + 0x4u;
        m.WriteU16((c.S2 + 0x6A2u), (ushort)c.V0);
    L801C11E4:;
        c.A0 = 0u | 0x001Cu;
        c.A1 = c.S1 + 0u;
        c.RA = 0x801C11F0u;
        SoTN.func_801B999C(c, m);
        m.WriteU16((c.S1 + 0x30u), (ushort)c.S0);
        c.S0 = c.S0 + 0x1u;
        c.V0 = c.S0 & 0xFFFFu;
        c.V0 = c.V0 < 0x00000002u ? 1u : 0u;
        if (c.V0 != 0u)
        {
            c.S1 = c.S1 + 0xBCu;
            goto L801C11E4;
        }
        c.S1 = c.S1 + 0xBCu;
        c.S1 = c.S2 + 0x8D0u;
        c.S0 = 0u + 0u;
    L801C1210:;
        c.A0 = 0u | 0x001Du;
        c.A1 = c.S1 + 0u;
        c.RA = 0x801C121Cu;
        SoTN.func_801B999C(c, m);
        m.WriteU16((c.S1 + 0x30u), (ushort)c.S0);
        c.S0 = c.S0 + 0x1u;
        c.V0 = c.S0 & 0xFFFFu;
        c.V0 = c.V0 < 0x00000002u ? 1u : 0u;
        if (c.V0 != 0u)
        {
            c.S1 = c.S1 + 0xBCu;
            goto L801C1210;
        }
        c.S1 = c.S1 + 0xBCu;
        c.S1 = c.S2 + 0xA48u;
        c.S0 = 0u + 0u;
    L801C123C:;
        c.A0 = 0u | 0x001Eu;
        c.A1 = c.S1 + 0u;
        c.RA = 0x801C1248u;
        SoTN.func_801B999C(c, m);
        m.WriteU16((c.S1 + 0x30u), (ushort)c.S0);
        c.S0 = c.S0 + 0x1u;
        c.V0 = c.S0 & 0xFFFFu;
        c.V0 = c.V0 < 0x00000002u ? 1u : 0u;
        if (c.V0 != 0u)
        {
            c.S1 = c.S1 + 0xBCu;
            goto L801C123C;
        }
        c.S1 = c.S1 + 0xBCu;
        goto L801C1790;
    L801C1268:;
        c.V0 = m.ReadU32((c.S3 + 0x2D8u));
        if (c.V0 != 0u)
        {
            c.A0 = c.S2 + 0u;
            goto L801C1290;
        }
        c.A0 = c.S2 + 0u;
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x3824u));
        c.A0 = 0u | 0x07A9u;
        c.RA = 0x801C128Cu;
        Dispatcher.Call(c, m, c.V0);
        c.A0 = c.S2 + 0u;
    L801C1290:;
        c.A1 = c.S3 + 0u;
        c.RA = 0x801C1298u;
        SoTN.func_801C0DD4(c, m);
        c.V0 = m.ReadU32((c.S3 + 0x2D4u));
        if (c.V0 != 0u)
        {
            goto L801C1314;
        }
        c.V0 = m.ReadU32((c.S3 + 0x2D8u));
        if (c.V0 != 0u)
        {
            goto L801C1314;
        }
        c.V0 = m.ReadU32((c.S3 + 0x2D0u));
        if (c.V0 != 0u)
        {
            c.V0 = 0x2AAA0000u;
            goto L801C1314;
        }
        c.V0 = 0x2AAA0000u;
        c.A0 = m.ReadU32((c.S3 + 0x2CCu));
        c.V0 = c.V0 | 0xAAABu;
        c.A0 = c.A0 + 0xBu;
        { var _r = (long)(int)c.A0 * (int)c.V0; c.LO = (uint)_r; c.HI = (uint)(_r >> 32); }
        c.V0 = (uint)((int)c.A0 >> 31);
        c.V1 = c.HI;
        c.V1 = (uint)((int)c.V1 >> 1);
        c.V1 = c.V1 - c.V0;
        c.V0 = c.V1 << 1;
        c.V0 = c.V0 + c.V1;
        c.V0 = c.V0 << 2;
        c.A0 = c.A0 - c.V0;
        c.A0 = c.A0 + 0x1u;
        m.WriteU16((c.S2 + 0x84u), (ushort)c.A0);
        c.V0 = m.ReadU16((c.S2 + 0x84u));
        if (c.V0 != 0u)
        {
            c.V0 = 0u | 0x000Cu;
            goto L801C1314;
        }
        c.V0 = 0u | 0x000Cu;
        m.WriteU16((c.S2 + 0x84u), (ushort)c.V0);
    L801C1314:;
        c.A1 = m.ReadU32((c.S3 + 0x2D0u));
        c.A0 = c.S2 + 0u;
        c.RA = 0x801C1320u;
        SoTN.func_801C0D8C(c, m);
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU8((c.V0 - 0x4130u));
        if (c.V0 != 0u)
        {
            goto L801C1790;
        }
        c.V0 = 0x80070000u;
        c.V0 = m.ReadU16((c.V0 + 0x33DAu));
        c.V0 = c.V0 - 0x60u;
        c.V0 = c.V0 < 0x00000041u ? 1u : 0u;
        if (c.V0 == 0u)
        {
            c.V1 = 0u + 0u;
            goto L801C1790;
        }
        c.V1 = 0u + 0u;
        c.S0 = 0u | 0x0019u;
        c.V0 = c.S0 & 0xFFFFu;
    L801C1358:;
        c.At = 0x80090000u;
        c.At = c.At + c.V0;
        c.V0 = m.ReadU8((c.At + 0x7964u));
        c.V0 = c.V0 & 0x0001u;
        if (c.V0 != 0u)
        {
            c.S0 = c.S0 + 0x1u;
            goto L801C1378;
        }
        c.S0 = c.S0 + 0x1u;
        c.V1 = c.V1 + 0x1u;
    L801C1378:;
        c.V0 = c.S0 & 0xFFFFu;
        c.V0 = c.V0 < 0x0000001Eu ? 1u : 0u;
        if (c.V0 != 0u)
        {
            c.V0 = c.S0 & 0xFFFFu;
            goto L801C1358;
        }
        c.V0 = c.S0 & 0xFFFFu;
        c.V0 = c.V1 << 16;
        if (c.V0 != 0u)
        {
            c.A0 = 0u | 0x0002u;
            goto L801C1790;
        }
        c.A0 = 0u | 0x0002u;
        goto L801C1788;
    L801C139C:;
        c.V0 = 0x80070000u;
        c.V0 = (uint)(short)m.ReadU16((c.V0 + 0x33DAu));
        c.At = 0x801D0000u;
        m.WriteU16((c.At + 0x4B48u), (ushort)0u);
        c.At = 0x801D0000u;
        m.WriteU16((c.At + 0x4B4Au), (ushort)0u);
        c.At = 0x80070000u;
        m.WriteU32((c.At + 0x2EF4u), 0u);
        c.At = 0x80070000u;
        m.WriteU32((c.At + 0x2EFCu), c.S1);
        c.V0 = (int)c.V0 < 129 ? 1u : 0u;
        if (c.V0 != 0u)
        {
            c.V0 = 0u | 0x0060u;
            goto L801C13D4;
        }
        c.V0 = 0u | 0x0060u;
        c.V0 = 0u | 0x00A0u;
    L801C13D4:;
        c.At = 0x80070000u;
        m.WriteU16((c.At + 0x33DAu), (ushort)c.V0);
        c.V1 = m.ReadU16((c.S2 + 0x2Eu));
        c.V0 = c.V1 < 0x0000000Au ? 1u : 0u;
        if (c.V0 == 0u)
        {
            c.V0 = c.V1 << 2;
            goto L801C1790;
        }
        c.V0 = c.V1 << 2;
        c.At = 0x801B0000u;
        c.At = c.At + c.V0;
        c.V0 = m.ReadU32((c.At + 0x5BECu));
        switch (c.V0)
        {
            case 0x801C1408u: goto L801C1408;
            case 0x801C1414u: goto L801C1414;
            case 0x801C1420u: goto L801C1420;
            case 0x801C142Cu: goto L801C142C;
            case 0x801C1438u: goto L801C1438;
            case 0x801C14B0u: goto L801C14B0;
            case 0x801C1528u: goto L801C1528;
            case 0x801C1548u: goto L801C1548;
            case 0x801C1620u: goto L801C1620;
            case 0x801C1658u: goto L801C1658;
            default: Dispatcher.Call(c, m, c.V0); return;
        }
    L801C1408:;
        c.V0 = m.ReadU16((c.S2 + 0x2Eu));
        m.WriteU16((c.S2 + 0x88u), (ushort)0u);
        goto L801C164C;
    L801C1414:;
        c.V0 = m.ReadU16((c.S2 + 0x2Eu));
        c.V0 = c.V0 + 0x1u;
        goto L801C1650;
    L801C1420:;
        c.V0 = m.ReadU16((c.S2 + 0x2Eu));
        c.V0 = c.V0 + 0x1u;
        goto L801C1650;
    L801C142C:;
        c.V0 = m.ReadU16((c.S2 + 0x2Eu));
        c.V0 = c.V0 + 0x1u;
        goto L801C1650;
    L801C1438:;
        c.V1 = m.ReadU32((c.S2 + 0x64u));
        c.V0 = c.V1 << 1;
        c.V0 = c.V0 + c.V1;
        c.V0 = c.V0 << 2;
        c.V0 = c.V0 + c.V1;
        c.V0 = c.V0 << 2;
        c.V1 = 0x80080000u;
        c.V1 = c.V1 + 0x6FECu;
        c.A3 = c.V0 + c.V1;
        c.V0 = m.ReadU8((c.A3 + 0x6u));
        c.V0 = c.V0 + 0x10u;
        m.WriteU8((c.A3 + 0x6u), (byte)c.V0);
        m.WriteU8((c.A3 + 0x5u), (byte)c.V0);
        m.WriteU8((c.A3 + 0x4u), (byte)c.V0);
        c.A0 = m.ReadU32((c.A3 + 0x4u));
        c.A1 = m.ReadU32((c.A3 + 0x4u));
        c.A2 = m.ReadU32((c.A3 + 0x4u));
        c.V1 = m.ReadU8((c.A3 + 0x4u));
        c.V0 = 0u | 0x0031u;
        m.WriteU16((c.A3 + 0x32u), (ushort)c.V0);
        c.V1 = c.V1 < 0x000000C1u ? 1u : 0u;
        m.WriteU32((c.A3 + 0x10u), c.A0);
        m.WriteU32((c.A3 + 0x1Cu), c.A1);
        if (c.V1 != 0u)
        {
            m.WriteU32((c.A3 + 0x28u), c.A2);
            goto L801C1790;
        }
        m.WriteU32((c.A3 + 0x28u), c.A2);
        c.V0 = m.ReadU16((c.S2 + 0x2Eu));
        c.V0 = c.V0 + 0x1u;
        goto L801C1650;
    L801C14B0:;
        c.V1 = m.ReadU32((c.S2 + 0x64u));
        c.V0 = c.V1 << 1;
        c.V0 = c.V0 + c.V1;
        c.V0 = c.V0 << 2;
        c.V0 = c.V0 + c.V1;
        c.V0 = c.V0 << 2;
        c.V1 = 0x80080000u;
        c.V1 = c.V1 + 0x6FECu;
        c.A3 = c.V0 + c.V1;
        c.V0 = m.ReadU8((c.A3 + 0x6u));
        c.V0 = c.V0 - 0x4u;
        m.WriteU8((c.A3 + 0x6u), (byte)c.V0);
        m.WriteU8((c.A3 + 0x5u), (byte)c.V0);
        m.WriteU8((c.A3 + 0x4u), (byte)c.V0);
        c.V1 = m.ReadU32((c.A3 + 0x4u));
        c.A0 = m.ReadU32((c.A3 + 0x4u));
        c.V0 = m.ReadU8((c.A3 + 0x4u));
        c.A1 = m.ReadU32((c.A3 + 0x4u));
        c.V0 = c.V0 < 0x00000008u ? 1u : 0u;
        m.WriteU32((c.A3 + 0x10u), c.V1);
        m.WriteU32((c.A3 + 0x1Cu), c.A0);
        if (c.V0 == 0u)
        {
            m.WriteU32((c.A3 + 0x28u), c.A1);
            goto L801C1790;
        }
        m.WriteU32((c.A3 + 0x28u), c.A1);
        c.V0 = 0u | 0x0008u;
        m.WriteU16((c.A3 + 0x32u), (ushort)c.V0);
        c.V0 = m.ReadU16((c.S2 + 0x2Eu));
        c.V0 = c.V0 + 0x1u;
        goto L801C1650;
    L801C1528:;
        c.V1 = m.ReadU16((c.S2 + 0x2Eu));
        c.V0 = 0u | 0x0001u;
        m.WriteU16((c.S2 + 0x5A4u), (ushort)c.V0);
        m.WriteU16((c.S2 + 0x660u), (ushort)c.V0);
        m.WriteU16((c.S2 + 0x88u), (ushort)c.V0);
        c.V1 = c.V1 + 0x1u;
        m.WriteU16((c.S2 + 0x2Eu), (ushort)c.V1);
        goto L801C1790;
    L801C1548:;
        c.V0 = m.ReadU16((c.S2 + 0x88u));
        c.V0 = c.V0 - 0x1u;
        m.WriteU16((c.S2 + 0x88u), (ushort)c.V0);
        c.V0 = c.V0 & 0xFFFFu;
        if (c.V0 != 0u)
        {
            c.T0 = 0x91A20000u;
            goto L801C1790;
        }
        c.T0 = 0x91A20000u;
        c.T0 = c.T0 | 0xB3C5u;
        c.V1 = m.ReadU32((c.S2 + 0x428u));
        c.V0 = m.ReadU16((c.S2 + 0x2Eu));
        c.A3 = c.V1 << 16;
        c.A1 = (uint)((int)c.A3 >> 16);
        { var _r = (long)(int)c.A1 * (int)c.T0; c.LO = (uint)_r; c.HI = (uint)(_r >> 32); }
        c.A2 = m.ReadU32((c.S2 + 0x4E4u));
        m.WriteU16((c.S2 + 0x88u), (ushort)0u);
        c.V0 = c.V0 + 0x1u;
        m.WriteU32((c.S2 + 0x4E8u), c.A2);
        c.A2 = c.A2 << 16;
        c.A0 = (uint)((int)c.A2 >> 16);
        c.A3 = (uint)((int)c.A3 >> 31);
        c.A2 = (uint)((int)c.A2 >> 31);
        m.WriteU32((c.S2 + 0x42Cu), c.V1);
        m.WriteU16((c.S2 + 0x2Eu), (ushort)c.V0);
        c.V1 = c.HI;
        c.V1 = c.V1 + c.A1;
        c.V1 = (uint)((int)c.V1 >> 11);
        c.V1 = c.V1 - c.A3;
        { var _r = (long)(int)c.A0 * (int)c.T0; c.LO = (uint)_r; c.HI = (uint)(_r >> 32); }
        c.V0 = c.V1 << 3;
        c.V0 = c.V0 - c.V1;
        c.V0 = c.V0 << 5;
        c.V0 = c.V0 + c.V1;
        c.V0 = c.V0 << 4;
        c.A1 = c.A1 - c.V0;
        c.A1 = c.A1 << 16;
        c.A1 = (uint)((int)c.A1 >> 16);
        c.V0 = 0u | 0x1518u;
        c.V0 = c.V0 - c.A1;
        m.WriteU32((c.S2 + 0x430u), c.V0);
        c.V1 = c.HI;
        c.V1 = c.V1 + c.A0;
        c.V1 = (uint)((int)c.V1 >> 11);
        c.V1 = c.V1 - c.A2;
        c.V0 = c.V1 << 3;
        c.V0 = c.V0 - c.V1;
        c.V0 = c.V0 << 5;
        c.V0 = c.V0 + c.V1;
        c.V0 = c.V0 << 4;
        c.A0 = c.A0 - c.V0;
        c.A0 = c.A0 << 16;
        c.A0 = (uint)((int)c.A0 >> 16);
        c.A0 = c.A0 + 0x708u;
        m.WriteU32((c.S2 + 0x4ECu), c.A0);
        goto L801C1790;
    L801C1620:;
        c.A0 = c.S2 + 0u;
        c.RA = 0x801C1628u;
        SoTN.func_801C0C54(c, m);
        c.V0 = m.ReadU16((c.S2 + 0x88u));
        c.V0 = c.V0 < 0x00000200u ? 1u : 0u;
        if (c.V0 != 0u)
        {
            c.V0 = 0u | 0x000Du;
            goto L801C1790;
        }
        c.V0 = 0u | 0x000Du;
        m.WriteU16((c.S2 + 0x84u), (ushort)c.V0);
        c.V0 = m.ReadU16((c.S2 + 0x2Eu));
        c.V1 = 0u | 0x0380u;
        m.WriteU16((c.S2 + 0x88u), (ushort)c.V1);
    L801C164C:;
        c.V0 = c.V0 + 0x1u;
    L801C1650:;
        m.WriteU16((c.S2 + 0x2Eu), (ushort)c.V0);
        goto L801C1790;
    L801C1658:;
        c.V0 = m.ReadU16((c.S2 + 0x88u));
        c.V0 = c.V0 - 0x1u;
        m.WriteU16((c.S2 + 0x88u), (ushort)c.V0);
        c.V0 = c.V0 & 0xFFFFu;
        if (c.V0 != 0u)
        {
            c.V1 = 0u | 0x0001u;
            goto L801C1790;
        }
        c.V1 = 0u | 0x0001u;
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x3794u));
        c.At = 0x80040000u;
        m.WriteU8((c.At - 0x4130u), (byte)c.V1);
        c.A0 = 0u | 0x00E4u;
        c.RA = 0x801C168Cu;
        Dispatcher.Call(c, m, c.V0);
        c.A0 = 0u | 0x0003u;
        c.RA = 0x801C1694u;
        SoTN.func_801BB37C(c, m);
        c.V0 = 0u | 0x0140u;
        m.WriteU16((c.S2 + 0x88u), (ushort)c.V0);
        goto L801C1790;
    L801C16A0:;
        c.At = 0x801D0000u;
        m.WriteU16((c.At + 0x4B48u), (ushort)0u);
        c.At = 0x801D0000u;
        m.WriteU16((c.At + 0x4B4Au), (ushort)0u);
        c.V0 = m.ReadU16((c.S2 + 0x2Eu));
        if (c.V0 == 0u)
        {
            goto L801C16D0;
        }
        if (c.V0 == c.S1)
        {
            goto L801C176C;
        }
        goto L801C1790;
    L801C16D0:;
        c.V0 = m.ReadU16((c.S2 + 0x88u));
        c.V0 = c.V0 - 0x1u;
        m.WriteU16((c.S2 + 0x88u), (ushort)c.V0);
        c.V0 = c.V0 & 0xFFFFu;
        if (c.V0 != 0u)
        {
            goto L801C1790;
        }
        c.V0 = m.ReadU32((c.S2 + 0x428u));
        m.WriteU32((c.S2 + 0x42Cu), c.V0);
        c.V0 = m.ReadU32((c.S2 + 0x4E4u));
        c.V1 = m.ReadU32((c.S3 + 0x2D0u));
        m.WriteU32((c.S2 + 0x4E8u), c.V0);
        c.V0 = c.V1 << 4;
        c.V0 = c.V0 - c.V1;
        c.V0 = c.V0 << 18;
        c.V0 = (uint)((int)c.V0 >> 16);
        c.V0 = c.V0 + 0x708u;
        m.WriteU32((c.S2 + 0x430u), c.V0);
        c.A0 = m.ReadU32((c.S3 + 0x2CCu));
        c.V0 = m.ReadU16((c.S2 + 0x2Eu));
        c.A1 = m.ReadU32((c.S3 + 0x2D0u));
        m.WriteU16((c.S2 + 0x88u), (ushort)0u);
        c.V0 = c.V0 + 0x1u;
        c.V1 = c.A0 << 2;
        c.V1 = c.V1 + c.A0;
        m.WriteU16((c.S2 + 0x2Eu), (ushort)c.V0);
        c.V0 = c.V1 << 4;
        c.V0 = c.V0 - c.V1;
        c.V0 = c.V0 << 2;
        c.V1 = c.A1 << 2;
        c.V1 = c.V1 + c.A1;
        c.V0 = c.V0 + c.V1;
        c.V0 = c.V0 << 16;
        c.V0 = (uint)((int)c.V0 >> 16);
        c.V1 = 0u | 0x1518u;
        c.V1 = c.V1 - c.V0;
        m.WriteU32((c.S2 + 0x4ECu), c.V1);
        goto L801C1790;
    L801C176C:;
        c.A0 = c.S2 + 0u;
        c.RA = 0x801C1774u;
        SoTN.func_801C0C54(c, m);
        c.V0 = m.ReadU16((c.S2 + 0x88u));
        c.V0 = c.V0 < 0x00000200u ? 1u : 0u;
        if (c.V0 != 0u)
        {
            c.A0 = 0u | 0x0001u;
            goto L801C1790;
        }
        c.A0 = 0u | 0x0001u;
    L801C1788:;
        c.RA = 0x801C1790u;
        SoTN.func_801BB37C(c, m);
    L801C1790:;
        c.RA = m.ReadU32((c.SP + 0x20u));
        c.S3 = m.ReadU32((c.SP + 0x1Cu));
        c.S2 = m.ReadU32((c.SP + 0x18u));
        c.S1 = m.ReadU32((c.SP + 0x14u));
        c.S0 = m.ReadU32((c.SP + 0x10u));
        c.SP = c.SP + 0x28u;
        return;
    }

    // Heart of Vlad Boss Room Controller
    public static void func_us_80192B38(CpuContext c, IMemory m)
    {
        c.SP = c.SP - 0x28u;
        m.WriteU32((c.SP + 0x18u), c.S2);
        c.S2 = c.A0 + 0u;
        m.WriteU32((c.SP + 0x20u), c.RA);
        m.WriteU32((c.SP + 0x1Cu), c.S3);
        m.WriteU32((c.SP + 0x14u), c.S1);
        m.WriteU32((c.SP + 0x10u), c.S0);
        c.V1 = m.ReadU16((c.S2 + 0x2Cu));
        c.V0 = c.V1 < 0x00000007u ? 1u : 0u;
        if (c.V0 == 0u)
        {
            c.V0 = c.V1 << 2;
            goto L80192D44;
        }
        c.V0 = c.V1 << 2;
        c.At = 0x80190000u;
        c.At = c.At + c.V0;
        c.V0 = m.ReadU32((c.At + 0xEF0u));
        switch (c.V0)
        {
            case 0x80192B80u: goto L80192B80;
            case 0x80192B90u: goto L80192B90;
            case 0x80192CF8u: goto L80192CF8;
            case 0x80192BFCu: goto L80192BFC;
            case 0x80192C54u: goto L80192C54;
            case 0x80192C74u: goto L80192C74;
            default: Dispatcher.Call(c, m, c.V0); return;
        }
    L80192B80:;
        c.A0 = 0x80180000u;
        c.A0 = c.A0 + 0x444u;
        c.RA = 0x80192B90u;
        SoTN.InitializeEntity_rbo3(c, m);
    L80192B90:;
        c.V1 = 0x80070000u;
        c.V1 = (uint)(short)m.ReadU16((c.V1 + 0x33DAu));
        c.V0 = 0x80070000u;
        c.V0 = (uint)(short)m.ReadU16((c.V0 + 0x308Eu));
        c.S1 = c.V1 + c.V0;
        c.V0 = c.S1 - 0x81u;
        c.V0 = c.V0 < 0x000000FFu ? 1u : 0u;
        if (c.V0 == 0u)
        {
            c.A0 = 0u | 0x000Fu;
            goto L80192D44;
        }
        c.A0 = 0u | 0x000Fu;
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x37C0u));
        c.S0 = 0u | 0x0001u;
        c.At = 0x80180000u;
        m.WriteU32((c.At + 0x72Cu), c.S0);
        c.At = 0x80180000u;
        m.WriteU32((c.At + 0x728u), c.S0);
        c.A1 = 0u | 0x0002u;
        c.RA = 0x80192BDCu;
        Dispatcher.Call(c, m, c.V0);
        c.V0 = m.ReadU16((c.S2 + 0x2Cu));
        c.V1 = 0u | 0x0330u;
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7928u), c.S0);
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7910u), c.V1);
        c.V0 = c.V0 + 0x1u;
        goto L80192D40;
    L80192BFC:;
        c.V0 = 0x80180000u;
        c.V0 = m.ReadU32((c.V0 + 0x728u));
        c.V0 = c.V0 & 0x0002u;
        if (c.V0 == 0u)
        {
            c.A0 = 0u | 0x000Fu;
            goto L80192D44;
        }
        c.A0 = 0u | 0x000Fu;
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x37C0u));
        c.A1 = 0u | 0x0001u;
        c.RA = 0x80192C28u;
        Dispatcher.Call(c, m, c.V0);
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x3824u));
        c.A0 = 0u | 0x0090u;
        c.RA = 0x80192C3Cu;
        Dispatcher.Call(c, m, c.V0);
        c.V0 = m.ReadU16((c.S2 + 0x2Cu));
        c.V1 = 0u | 0x0301u;
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7910u), c.V1);
        c.V0 = c.V0 + 0x1u;
        goto L80192D40;
    L80192C54:;
        c.V0 = 0x80180000u;
        c.V0 = m.ReadU32((c.V0 + 0x728u));
        c.V0 = c.V0 & 0x0004u;
        if (c.V0 == 0u)
        {
            goto L80192D44;
        }
        goto L80192D34;
    L80192C74:;
        c.A0 = 0x80080000u;
        c.A0 = c.A0 - 0x56A8u;
        c.A1 = c.A0 + 0x1780u;
        c.V1 = 0x80070000u;
        c.V1 = (uint)(short)m.ReadU16((c.V1 + 0x308Eu));
        c.V0 = 0u | 0x0100u;
        c.S1 = c.V0 - c.V1;
        c.V1 = 0x80070000u;
        c.V1 = (uint)(short)m.ReadU16((c.V1 + 0x3092u));
        c.V0 = 0u | 0x0080u;
        c.S3 = c.V0 - c.V1;
        c.RA = 0x80192CA4u;
        SoTN.AllocEntity_rbo3(c, m);
        c.S0 = c.V0 + 0u;
        if (c.S0 == 0u)
        {
            c.A0 = 0u | 0x001Eu;
            goto L80192D44;
        }
        c.A0 = 0u | 0x001Eu;
        c.A1 = c.S2 + 0u;
        c.A2 = c.S0 + 0u;
        c.RA = 0x80192CBCu;
        SoTN.CreateEntityFromEntity_rbo3(c, m);
        //c.V0 = 0u | 0x0011u;
        c.V0 = 0u | m.ReadU16(0x80192CBC);  // Allow Changing reward Index
        m.WriteU16((c.S0 + 0x30u), (ushort)c.V0);
        c.V0 = 0u | 0x0001u;
        m.WriteU16((c.S0 + 0x2u), (ushort)c.S1);
        m.WriteU16((c.S0 + 0x6u), (ushort)c.S3);
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7928u), c.V0);
        c.V0 = m.ReadU16((c.S2 + 0x2Cu));
        c.V1 = 0u | 0x0301u;
        c.At = 0x80180000u;
        m.WriteU32((c.At + 0x72Cu), 0u);
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7910u), c.V1);
        c.V0 = c.V0 + 0x1u;
        goto L80192D40;
    L80192CF8:;
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x3808u));
        c.RA = 0x80192D0Cu;
        Dispatcher.Call(c, m, c.V0);
        if (c.V0 != 0u)
        {
            goto L80192D44;
        }
        c.A0 = 0x80090000u;
        c.A0 = m.ReadU32((c.A0 + 0x7910u));
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x3824u));
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7928u), 0u);
        c.RA = 0x80192D34u;
        Dispatcher.Call(c, m, c.V0);
    L80192D34:;
        c.V0 = m.ReadU16((c.S2 + 0x2Cu));
        c.V0 = c.V0 + 0x1u;
    L80192D40:;
        m.WriteU16((c.S2 + 0x2Cu), (ushort)c.V0);
    L80192D44:;
        c.RA = m.ReadU32((c.SP + 0x20u));
        c.S3 = m.ReadU32((c.SP + 0x1Cu));
        c.S2 = m.ReadU32((c.SP + 0x18u));
        c.S1 = m.ReadU32((c.SP + 0x14u));
        c.S0 = m.ReadU32((c.SP + 0x10u));
        c.SP = c.SP + 0x28u;
        return;
    }

    // Tooth of Vlad Boss Controller
    public static void func_8019879C(CpuContext c, IMemory m)
    {
        c.SP = c.SP - 0x28u;
        m.WriteU32((c.SP + 0x18u), c.S2);
        c.S2 = c.A0 + 0u;
        m.WriteU32((c.SP + 0x20u), c.RA);
        m.WriteU32((c.SP + 0x1Cu), c.S3);
        m.WriteU32((c.SP + 0x14u), c.S1);
        m.WriteU32((c.SP + 0x10u), c.S0);
        c.V1 = m.ReadU16((c.S2 + 0x2Cu));
        c.V0 = c.V1 < 0x00000007u ? 1u : 0u;
        if (c.V0 == 0u)
        {
            c.V0 = c.V1 << 2;
            goto L801989F8;
        }
        c.V0 = c.V1 << 2;
        c.At = 0x80190000u;
        c.At = c.At + c.V0;
        c.V0 = m.ReadU32((c.At + 0x7400u));
        switch (c.V0)
        {
            case 0x801987E4u: goto L801987E4;
            case 0x80198844u: goto L80198844;
            case 0x801989ACu: goto L801989AC;
            case 0x801988B0u: goto L801988B0;
            case 0x80198908u: goto L80198908;
            case 0x80198928u: goto L80198928;
            default: Dispatcher.Call(c, m, c.V0); return;
        }
    L801987E4:;
        c.A0 = 0x80180000u;
        c.A0 = c.A0 + 0x490u;
        c.S0 = c.S2 + 0xBCu;
        c.RA = 0x801987F4u;
        SoTN.func_8019CF34(c, m);
        c.S1 = 0u | 0x0001u;
        m.WriteU16((c.S2 + 0x54u), (ushort)0u);
        m.WriteU16((c.S2 + 0x56u), (ushort)0u);
    L80198800:;
        c.A0 = 0u | 0x0019u;
        c.A1 = c.S2 + 0u;
        c.A2 = c.S0 + 0u;
        c.RA = 0x80198810u;
        SoTN.func_8019B4F8(c, m);
        c.V0 = c.S1 + 0x100u;
        m.WriteU16((c.S0 + 0x30u), (ushort)c.V0);
        c.S0 = c.S0 + 0xBCu;
        c.A0 = 0u | 0x0019u;
        c.A1 = c.S2 + 0u;
        c.A2 = c.S0 + 0u;
        c.RA = 0x8019882Cu;
        SoTN.func_8019B4F8(c, m);
        c.V0 = c.S1 + 0u;
        m.WriteU16((c.S0 + 0x30u), (ushort)c.V0);
        c.S1 = c.S1 + 0x1u;
        c.V0 = (int)c.S1 < 2 ? 1u : 0u;
        if (c.V0 != 0u)
        {
            c.S0 = c.S0 + 0xBCu;
            goto L80198800;
        }
        c.S0 = c.S0 + 0xBCu;
    L80198844:;
        c.V1 = 0x80070000u;
        c.V1 = (uint)(short)m.ReadU16((c.V1 + 0x33DAu));
        c.V0 = 0x80070000u;
        c.V0 = (uint)(short)m.ReadU16((c.V0 + 0x308Eu));
        c.S1 = c.V1 + c.V0;
        c.V0 = c.S1 - 0x61u;
        c.V0 = c.V0 < 0x0000013Fu ? 1u : 0u;
        if (c.V0 == 0u)
        {
            c.A0 = 0u | 0x0010u;
            goto L801989F8;
        }
        c.A0 = 0u | 0x0010u;
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x37C0u));
        c.S0 = 0u | 0x0001u;
        c.At = 0x80180000u;
        m.WriteU32((c.At + 0x810u), c.S0);
        c.At = 0x80180000u;
        m.WriteU32((c.At + 0x7ECu), c.S0);
        c.A1 = 0u | 0x0002u;
        c.RA = 0x80198890u;
        Dispatcher.Call(c, m, c.V0);
        c.V0 = m.ReadU16((c.S2 + 0x2Cu));
        c.V1 = 0u | 0x031Du;
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7928u), c.S0);
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7910u), c.V1);
        c.V0 = c.V0 + 0x1u;
        goto L801989F4;
    L801988B0:;
        c.V0 = 0x80180000u;
        c.V0 = m.ReadU32((c.V0 + 0x7ECu));
        c.V0 = c.V0 & 0x0002u;
        if (c.V0 == 0u)
        {
            c.A0 = 0u | 0x0010u;
            goto L801989F8;
        }
        c.A0 = 0u | 0x0010u;
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x37C0u));
        c.A1 = 0u | 0x0001u;
        c.RA = 0x801988DCu;
        Dispatcher.Call(c, m, c.V0);
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x3824u));
        c.A0 = 0u | 0x0090u;
        c.RA = 0x801988F0u;
        Dispatcher.Call(c, m, c.V0);
        c.V0 = m.ReadU16((c.S2 + 0x2Cu));
        c.V1 = 0u | 0x0338u;
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7910u), c.V1);
        c.V0 = c.V0 + 0x1u;
        goto L801989F4;
    L80198908:;
        c.V0 = 0x80180000u;
        c.V0 = m.ReadU32((c.V0 + 0x7ECu));
        c.V0 = c.V0 & 0x0004u;
        if (c.V0 == 0u)
        {
            goto L801989F8;
        }
        goto L801989E8;
    L80198928:;
        c.A0 = 0x80080000u;
        c.A0 = c.A0 - 0x56A8u;
        c.A1 = c.A0 + 0x1780u;
        c.V1 = 0x80070000u;
        c.V1 = (uint)(short)m.ReadU16((c.V1 + 0x308Eu));
        c.V0 = 0u | 0x0100u;
        c.S1 = c.V0 - c.V1;
        c.V1 = 0x80070000u;
        c.V1 = (uint)(short)m.ReadU16((c.V1 + 0x3092u));
        c.V0 = 0u | 0x0080u;
        c.S3 = c.V0 - c.V1;
        c.RA = 0x80198958u;
        SoTN.func_8019CA94(c, m);
        c.S0 = c.V0 + 0u;
        if (c.S0 == 0u)
        {
            c.A0 = 0u | 0x001Cu;
            goto L801989F8;
        }
        c.A0 = 0u | 0x001Cu;
        c.A1 = c.S2 + 0u;
        c.A2 = c.S0 + 0u;
        c.RA = 0x80198970u;
        SoTN.func_8019B4F8(c, m);
        //c.V0 = 0u | 0x0012u;
        c.V0 = 0u | m.ReadU16(0x80198970);      // Allow Reward Index Change
        m.WriteU16((c.S0 + 0x30u), (ushort)c.V0);
        c.V0 = 0u | 0x0001u;
        m.WriteU16((c.S0 + 0x2u), (ushort)c.S1);
        m.WriteU16((c.S0 + 0x6u), (ushort)c.S3);
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7928u), c.V0);
        c.V0 = m.ReadU16((c.S2 + 0x2Cu));
        c.V1 = 0u | 0x0338u;
        c.At = 0x80180000u;
        m.WriteU32((c.At + 0x810u), 0u);
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7910u), c.V1);
        c.V0 = c.V0 + 0x1u;
        goto L801989F4;
    L801989AC:;
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x3808u));
        c.RA = 0x801989C0u;
        Dispatcher.Call(c, m, c.V0);
        if (c.V0 != 0u)
        {
            goto L801989F8;
        }
        c.A0 = 0x80090000u;
        c.A0 = m.ReadU32((c.A0 + 0x7910u));
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x3824u));
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7928u), 0u);
        c.RA = 0x801989E8u;
        Dispatcher.Call(c, m, c.V0);
    L801989E8:;
        c.V0 = m.ReadU16((c.S2 + 0x2Cu));
        c.V0 = c.V0 + 0x1u;
    L801989F4:;
        m.WriteU16((c.S2 + 0x2Cu), (ushort)c.V0);
    L801989F8:;
        c.RA = m.ReadU32((c.SP + 0x20u));
        c.S3 = m.ReadU32((c.SP + 0x1Cu));
        c.S2 = m.ReadU32((c.SP + 0x18u));
        c.S1 = m.ReadU32((c.SP + 0x14u));
        c.S0 = m.ReadU32((c.SP + 0x10u));
        c.SP = c.SP + 0x28u;
        return;
    }

    // Rib of Vlad Boss Controller
    public static void func_80193E88(CpuContext c, IMemory m)
    {
        c.SP = c.SP - 0x28u;
        m.WriteU32((c.SP + 0x18u), c.S2);
        c.S2 = c.A0 + 0u;
        m.WriteU32((c.SP + 0x20u), c.RA);
        m.WriteU32((c.SP + 0x1Cu), c.S3);
        m.WriteU32((c.SP + 0x14u), c.S1);
        m.WriteU32((c.SP + 0x10u), c.S0);
        c.V1 = m.ReadU16((c.S2 + 0x2Cu));
        c.V0 = c.V1 < 0x00000007u ? 1u : 0u;
        if (c.V0 == 0u)
        {
            c.V0 = c.V1 << 2;
            goto L80194094;
        }
        c.V0 = c.V1 << 2;
        c.At = 0x80190000u;
        c.At = c.At + c.V0;
        c.V0 = m.ReadU32((c.At + 0x342Cu));
        switch (c.V0)
        {
            case 0x80193ED0u: goto L80193ED0;
            case 0x80193EE0u: goto L80193EE0;
            case 0x80194048u: goto L80194048;
            case 0x80193F4Cu: goto L80193F4C;
            case 0x80193FA4u: goto L80193FA4;
            case 0x80193FC4u: goto L80193FC4;
            default: Dispatcher.Call(c, m, c.V0); return;
        }
    L80193ED0:;
        c.A0 = 0x80180000u;
        c.A0 = c.A0 + 0x3E4u;
        c.RA = 0x80193EE0u;
        SoTN.func_80199FFC(c, m);
    L80193EE0:;
        c.V1 = 0x80070000u;
        c.V1 = (uint)(short)m.ReadU16((c.V1 + 0x33DAu));
        c.V0 = 0x80070000u;
        c.V0 = (uint)(short)m.ReadU16((c.V0 + 0x308Eu));
        c.S1 = c.V1 + c.V0;
        c.V0 = c.S1 - 0x81u;
        c.V0 = c.V0 < 0x000000FFu ? 1u : 0u;
        if (c.V0 == 0u)
        {
            c.A0 = 0u | 0x0013u;
            goto L80194094;
        }
        c.A0 = 0u | 0x0013u;
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x37C0u));
        c.S0 = 0u | 0x0001u;
        c.At = 0x80180000u;
        m.WriteU32((c.At + 0x564u), c.S0);
        c.At = 0x80180000u;
        m.WriteU32((c.At + 0x5C8u), c.S0);
        c.A1 = 0u | 0x0002u;
        c.RA = 0x80193F2Cu;
        Dispatcher.Call(c, m, c.V0);
        c.V0 = m.ReadU16((c.S2 + 0x2Cu));
        c.V1 = 0u | 0x031Du;
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7928u), c.S0);
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7910u), c.V1);
        c.V0 = c.V0 + 0x1u;
        goto L80194090;
    L80193F4C:;
        c.V0 = 0x80180000u;
        c.V0 = m.ReadU32((c.V0 + 0x5C8u));
        c.V0 = c.V0 & 0x0002u;
        if (c.V0 == 0u)
        {
            c.A0 = 0u | 0x0013u;
            goto L80194094;
        }
        c.A0 = 0u | 0x0013u;
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x37C0u));
        c.A1 = 0u | 0x0001u;
        c.RA = 0x80193F78u;
        Dispatcher.Call(c, m, c.V0);
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x3824u));
        c.A0 = 0u | 0x0090u;
        c.RA = 0x80193F8Cu;
        Dispatcher.Call(c, m, c.V0);
        c.V0 = m.ReadU16((c.S2 + 0x2Cu));
        c.V1 = 0u | 0x0338u;
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7910u), c.V1);
        c.V0 = c.V0 + 0x1u;
        goto L80194090;
    L80193FA4:;
        c.V0 = 0x80180000u;
        c.V0 = m.ReadU32((c.V0 + 0x5C8u));
        c.V0 = c.V0 & 0x0004u;
        if (c.V0 == 0u)
        {
            goto L80194094;
        }
        goto L80194084;
    L80193FC4:;
        c.A0 = 0x80080000u;
        c.A0 = c.A0 - 0x56A8u;
        c.A1 = c.A0 + 0x1780u;
        c.V1 = 0x80070000u;
        c.V1 = (uint)(short)m.ReadU16((c.V1 + 0x308Eu));
        c.V0 = 0u | 0x0100u;
        c.S1 = c.V0 - c.V1;
        c.V1 = 0x80070000u;
        c.V1 = (uint)(short)m.ReadU16((c.V1 + 0x3092u));
        c.V0 = 0u | 0x0180u;
        c.S3 = c.V0 - c.V1;
        c.RA = 0x80193FF4u;
        SoTN.func_80199B5C(c, m);
        c.S0 = c.V0 + 0u;
        if (c.S0 == 0u)
        {
            c.A0 = 0u | 0x0018u;
            goto L80194094;
        }
        c.A0 = 0u | 0x0018u;
        c.A1 = c.S2 + 0u;
        c.A2 = c.S0 + 0u;
        c.RA = 0x8019400Cu;
        SoTN.func_801985C0(c, m);
        //c.V0 = 0u | 0x0013u;
        c.V0 = 0u | m.ReadU16(0x8019400C);  // allow reward id change
        m.WriteU16((c.S0 + 0x30u), (ushort)c.V0);
        c.V0 = 0u | 0x0001u;
        m.WriteU16((c.S0 + 0x2u), (ushort)c.S1);
        m.WriteU16((c.S0 + 0x6u), (ushort)c.S3);
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7928u), c.V0);
        c.V0 = m.ReadU16((c.S2 + 0x2Cu));
        c.V1 = 0u | 0x0338u;
        c.At = 0x80180000u;
        m.WriteU32((c.At + 0x564u), 0u);
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7910u), c.V1);
        c.V0 = c.V0 + 0x1u;
        goto L80194090;
    L80194048:;
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x3808u));
        c.RA = 0x8019405Cu;
        Dispatcher.Call(c, m, c.V0);
        if (c.V0 != 0u)
        {
            goto L80194094;
        }
        c.A0 = 0x80090000u;
        c.A0 = m.ReadU32((c.A0 + 0x7910u));
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x3824u));
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7928u), 0u);
        c.RA = 0x80194084u;
        Dispatcher.Call(c, m, c.V0);
    L80194084:;
        c.V0 = m.ReadU16((c.S2 + 0x2Cu));
        c.V0 = c.V0 + 0x1u;
    L80194090:;
        m.WriteU16((c.S2 + 0x2Cu), (ushort)c.V0);
    L80194094:;
        c.RA = m.ReadU32((c.SP + 0x20u));
        c.S3 = m.ReadU32((c.SP + 0x1Cu));
        c.S2 = m.ReadU32((c.SP + 0x18u));
        c.S1 = m.ReadU32((c.SP + 0x14u));
        c.S0 = m.ReadU32((c.SP + 0x10u));
        c.SP = c.SP + 0x28u;
        return;
    }

    // Eye of Vlad Boss Controller
    public static void func_8019F4AC(CpuContext c, IMemory m)
    {
        c.SP = c.SP - 0x28u;
        m.WriteU32((c.SP + 0x18u), c.S2);
        c.S2 = c.A0 + 0u;
        m.WriteU32((c.SP + 0x20u), c.RA);
        m.WriteU32((c.SP + 0x1Cu), c.S3);
        m.WriteU32((c.SP + 0x14u), c.S1);
        m.WriteU32((c.SP + 0x10u), c.S0);
        c.V1 = m.ReadU16((c.S2 + 0x2Cu));
        c.V0 = c.V1 < 0x00000008u ? 1u : 0u;
        if (c.V0 == 0u)
        {
            c.V0 = c.V1 << 2;
            goto L8019F7C0;
        }
        c.V0 = c.V1 << 2;
        c.At = 0x801A0000u;
        c.At = c.At + c.V0;
        c.V0 = m.ReadU32((c.At - 0x5238u));
        switch (c.V0)
        {
            case 0x8019F4F4u: goto L8019F4F4;
            case 0x8019F514u: goto L8019F514;
            case 0x8019F554u: goto L8019F554;
            case 0x8019F61Cu: goto L8019F61C;
            case 0x8019F668u: goto L8019F668;
            case 0x8019F6C0u: goto L8019F6C0;
            case 0x8019F6E0u: goto L8019F6E0;
            case 0x8019F774u: goto L8019F774;
            default: Dispatcher.Call(c, m, c.V0); return;
        }
    L8019F4F4:;
        c.A0 = 0x80180000u;
        c.A0 = c.A0 + 0x458u;
        c.RA = 0x8019F504u;
        SoTN.func_801A5BA8(c, m);
        c.V0 = m.ReadU32((c.S2 + 0x34u));
        c.V1 = 0x00010000u;
        c.V0 = c.V0 | c.V1;
        m.WriteU32((c.S2 + 0x34u), c.V0);
    L8019F514:;
        c.V1 = 0x80070000u;
        c.V1 = (uint)(short)m.ReadU16((c.V1 + 0x33DAu));
        c.V0 = 0x80070000u;
        c.V0 = (uint)(short)m.ReadU16((c.V0 + 0x308Eu));
        c.S1 = c.V1 + c.V0;
        c.V0 = c.S1 - 0x41u;
        c.V0 = c.V0 < 0x0000017Fu ? 1u : 0u;
        if (c.V0 == 0u)
        {
            goto L8019F7C0;
        }
        c.V1 = m.ReadU16((c.S2 + 0x2Cu));
        c.V0 = 0x80180000u;
        c.V0 = m.ReadU32((c.V0 + 0xB5Cu));
        c.V1 = c.V1 + 0x1u;
        c.V0 = c.V0 | 0x0001u;
        goto L8019F764;
    L8019F554:;
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU8((c.V0 - 0x4138u));
        if (c.V0 != 0u)
        {
            goto L8019F590;
        }
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x3660u));
        if (c.V0 != 0u)
        {
            goto L8019F590;
        }
        c.V0 = 0x80090000u;
        c.V0 = m.ReadU32((c.V0 + 0x7914u));
        if (c.V0 == 0u)
        {
            goto L8019F5C0;
        }
    L8019F590:;
        c.V1 = 0x80070000u;
        c.V1 = (uint)(short)m.ReadU16((c.V1 + 0x33DAu));
        c.V0 = 0x80070000u;
        c.V0 = (uint)(short)m.ReadU16((c.V0 + 0x308Eu));
        c.S1 = c.V1 + c.V0;
        c.V0 = c.S1 - 0x81u;
        c.V0 = c.V0 < 0x000000FFu ? 1u : 0u;
        if (c.V0 == 0u)
        {
            c.A0 = 0u | 0x000Cu;
            goto L8019F7C0;
        }
        c.A0 = 0u | 0x000Cu;
        goto L8019F5D8;
    L8019F5C0:;
        c.V0 = 0x801B0000u;
        c.V0 = m.ReadU32((c.V0 - 0x1700u));
        c.V0 = c.V0 & 0x0002u;
        if (c.V0 == 0u)
        {
            c.A0 = 0u | 0x000Cu;
            goto L8019F7C0;
        }
        c.A0 = 0u | 0x000Cu;
    L8019F5D8:;
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x37C0u));
        c.A1 = 0u | 0x0002u;
        c.RA = 0x8019F5ECu;
        Dispatcher.Call(c, m, c.V0);
        c.V1 = m.ReadU16((c.S2 + 0x2Cu));
        c.V0 = 0u | 0x0001u;
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7928u), c.V0);
        c.V0 = 0u | 0x0334u;
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7910u), c.V0);
        c.V0 = 0x80180000u;
        c.V0 = m.ReadU32((c.V0 + 0xB5Cu));
        c.V1 = c.V1 + 0x1u;
        c.V0 = c.V0 | 0x0002u;
        goto L8019F764;
    L8019F61C:;
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x3808u));
        c.RA = 0x8019F630u;
        Dispatcher.Call(c, m, c.V0);
        if (c.V0 != 0u)
        {
            goto L8019F668;
        }
        c.A0 = 0x80090000u;
        c.A0 = m.ReadU32((c.A0 + 0x7910u));
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x3824u));
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7928u), 0u);
        c.RA = 0x8019F658u;
        Dispatcher.Call(c, m, c.V0);
        c.V0 = m.ReadU16((c.S2 + 0x2Cu));
        c.V0 = c.V0 + 0x1u;
        m.WriteU16((c.S2 + 0x2Cu), (ushort)c.V0);
    L8019F668:;
        c.V0 = 0x80180000u;
        c.V0 = m.ReadU32((c.V0 + 0xB5Cu));
        c.V0 = c.V0 & 0x0010u;
        if (c.V0 == 0u)
        {
            c.A0 = 0u | 0x000Cu;
            goto L8019F7C0;
        }
        c.A0 = 0u | 0x000Cu;
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x37C0u));
        c.A1 = 0u | 0x0001u;
        c.RA = 0x8019F694u;
        Dispatcher.Call(c, m, c.V0);
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x3824u));
        c.A0 = 0u | 0x0090u;
        c.RA = 0x8019F6A8u;
        Dispatcher.Call(c, m, c.V0);
        c.V0 = m.ReadU16((c.S2 + 0x2Cu));
        c.V1 = 0u | 0x0319u;
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7910u), c.V1);
        c.V0 = c.V0 + 0x1u;
        goto L8019F7BC;
    L8019F6C0:;
        c.V0 = 0x80180000u;
        c.V0 = m.ReadU32((c.V0 + 0xB5Cu));
        c.V0 = c.V0 & 0x0040u;
        if (c.V0 == 0u)
        {
            goto L8019F7C0;
        }
        goto L8019F7B0;
    L8019F6E0:;
        c.A0 = 0x80080000u;
        c.A0 = c.A0 - 0x56A8u;
        c.A1 = c.A0 + 0x1780u;
        c.V1 = 0x80070000u;
        c.V1 = (uint)(short)m.ReadU16((c.V1 + 0x308Eu));
        c.V0 = 0u | 0x0100u;
        c.S1 = c.V0 - c.V1;
        c.V1 = 0x80070000u;
        c.V1 = (uint)(short)m.ReadU16((c.V1 + 0x3092u));
        c.V0 = 0u | 0x0080u;
        c.S3 = c.V0 - c.V1;
        c.RA = 0x8019F710u;
        SoTN.func_801A5708(c, m);
        c.S0 = c.V0 + 0u;
        if (c.S0 == 0u)
        {
            c.A0 = 0u | 0x0022u;
            goto L8019F7C0;
        }
        c.A0 = 0u | 0x0022u;
        c.A1 = c.S2 + 0u;
        c.A2 = c.S0 + 0u;
        c.RA = 0x8019F728u;
        SoTN.func_801A416C(c, m);
        //c.V0 = 0u | 0x0015u;
        c.V0 = 0u | m.ReadU16(0x8019F728);  // Allow Reward Index Change
        m.WriteU16((c.S0 + 0x2u), (ushort)c.S1);
        m.WriteU16((c.S0 + 0x6u), (ushort)c.S3);
        m.WriteU16((c.S0 + 0x30u), (ushort)c.V0);
        c.V1 = m.ReadU16((c.S2 + 0x2Cu));
        c.V0 = 0u | 0x0001u;
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7928u), c.V0);
        c.V0 = 0u | 0x0319u;
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7910u), c.V0);
        c.V0 = 0x80180000u;
        c.V0 = m.ReadU32((c.V0 + 0xB5Cu));
        c.V1 = c.V1 + 0x1u;
        c.V0 = c.V0 | 0x0080u;
    L8019F764:;
        c.At = 0x80180000u;
        m.WriteU32((c.At + 0xB5Cu), c.V0);
        m.WriteU16((c.S2 + 0x2Cu), (ushort)c.V1);
        goto L8019F7C0;
    L8019F774:;
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x3808u));
        c.RA = 0x8019F788u;
        Dispatcher.Call(c, m, c.V0);
        if (c.V0 != 0u)
        {
            goto L8019F7C0;
        }
        c.A0 = 0x80090000u;
        c.A0 = m.ReadU32((c.A0 + 0x7910u));
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x3824u));
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7928u), 0u);
        c.RA = 0x8019F7B0u;
        Dispatcher.Call(c, m, c.V0);
    L8019F7B0:;
        c.V0 = m.ReadU16((c.S2 + 0x2Cu));
        c.V0 = c.V0 + 0x1u;
    L8019F7BC:;
        m.WriteU16((c.S2 + 0x2Cu), (ushort)c.V0);
    L8019F7C0:;
        c.A1 = m.ReadU16((c.S2 + 0x2Cu));
        c.A0 = 0x801A0000u;
        c.A0 = c.A0 - 0x5258u;
        c.RA = 0x8019F7D4u;
        SoTN.FntPrint(c, m);
        c.A1 = 0x80180000u;
        c.A1 = m.ReadU32((c.A1 + 0xB5Cu));
        c.A0 = 0x801A0000u;
        c.A0 = c.A0 - 0x5248u;
        c.RA = 0x8019F7ECu;
        SoTN.FntPrint(c, m);
        c.RA = m.ReadU32((c.SP + 0x20u));
        c.S3 = m.ReadU32((c.SP + 0x1Cu));
        c.S2 = m.ReadU32((c.SP + 0x18u));
        c.S1 = m.ReadU32((c.SP + 0x14u));
        c.S0 = m.ReadU32((c.SP + 0x10u));
        c.SP = c.SP + 0x28u;
        return;
    }

    // Jewel of Open Purchased, Handle Out of Stock
    public static void func_us_801B29C4(CpuContext c, IMemory m)
    {
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU8((c.V0 - 0x41B2u));
        c.SP = c.SP - 0x28u;
        m.WriteU32((c.SP + 0x18u), c.S2);
        c.S2 = 0x801D0000u;
        c.S2 = c.S2 + 0x4364u;
        m.WriteU32((c.SP + 0x14u), c.S1);
        c.S1 = 0x80180000u;
        c.S1 = c.S1 + 0x134Cu;
        m.WriteU32((c.SP + 0x20u), c.RA);
        m.WriteU32((c.SP + 0x1Cu), c.S3);
        m.WriteU32((c.SP + 0x10u), c.S0);
        c.V1 = 0u < c.V0 ? 1u : 0u;
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU8((c.V0 - 0x4160u));
        if (c.V0 == 0u)
        {
            c.S0 = c.V1 + 0u;
            goto L801B2A10;
        }
        c.S0 = c.V1 + 0u;
        c.S0 = c.V1 + 0x1u;
    L801B2A10:;
        c.S3 = 0x80090000u;
        c.S3 = c.S3 + 0x7964u;
        c.V0 = m.ReadU8(c.S3);
        c.V0 = c.V0 & 0x0001u;
        if (c.V0 == 0u)
        {
            goto L801B2A30;
        }
        c.S0 = c.S0 + 0x1u;
    L801B2A30:;
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU8((c.V0 - 0x413Cu));
        if (c.V0 == 0u)
        {
            goto L801B2A48;
        }
        c.S0 = c.S0 + 0x1u;
    L801B2A48:;
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU8((c.V0 - 0x417Eu));
        if (c.V0 == 0u)
        {
            goto L801B2A60;
        }
        c.S0 = c.S0 + 0x1u;
    L801B2A60:;
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU8((c.V0 - 0x4138u));
        if (c.V0 == 0u)
        {
            c.A0 = 0u | 0x0015u;
            goto L801B2A78;
        }
        c.A0 = 0u | 0x0015u;
        c.S0 = c.S0 + 0x1u;
    L801B2A78:;
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x37C0u));
        c.A1 = 0u + 0u;
        c.RA = 0x801B2A8Cu;
        Dispatcher.Call(c, m, c.V0);
        if (c.V0 == 0u)
        {
            goto L801B2A98;
        }
        c.S0 = c.S0 + 0x1u;
    L801B2A98:;
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x4220u));
        if (c.V0 == 0u)
        {
            c.T1 = 0u + 0u;
            goto L801B2AB0;
        }
        c.T1 = 0u + 0u;
        c.S0 = 0u | 0x0008u;
    L801B2AB0:;
        c.T0 = 0u + 0u;
        c.T3 = 0x80180000u;
        c.T3 = c.T3 + 0x1510u;
        c.T2 = c.S3 + 0x238u;
        c.A2 = c.S1 + 0x4u;
        c.A3 = c.S2 + 0x4u;
    L801B2AC8:;
        c.V1 = m.ReadU8((c.A2 - 0x3u));
        c.A0 = c.V1 & 0x00FFu;
        c.V0 = (int)c.A0 < 134 ? 1u : 0u;
        if (c.V0 == 0u)
        {
            c.V0 = (int)c.A0 < 129 ? 1u : 0u;
            goto L801B2AF8;
        }
        c.V0 = (int)c.A0 < 129 ? 1u : 0u;
        if (c.V0 == 0u)
        {
            c.V0 = 0u | 0x0080u;
            goto L801B2B30;
        }
        c.V0 = 0u | 0x0080u;
        if (c.A0 == c.V0)
        {
            goto L801B2B14;
        }
        c.V1 = c.V1 & 0x00FFu;
        goto L801B2B70;
    L801B2AF8:;
        c.V0 = 0u | 0x00FFu;
        if (c.A0 != c.V0)
        {
            goto L801B2B68;
        }
        c.V0 = 0x80090000u;
        //c.V0 = m.ReadU8((c.V0 + 0x7974u));
        c.V0 = m.ReadU8((c.V0 + m.ReadU16(0x801B2B08)));     // Read updated 16-bit offset for changing relic/item
        c.V0 = c.V0 & 0x0001u;
        goto L801B2B1C;
    L801B2B14:;
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU8((c.V0 - 0x41A1u));
    L801B2B1C:;
        if (c.V0 != 0u)
        {
            c.V1 = c.V1 & 0x00FFu;
            goto L801B2B70;
        }
        c.V1 = c.V1 & 0x00FFu;
        c.V1 = 0u + 0u;
        goto L801B2B68;
    L801B2B30:;
        c.V1 = c.V1 + 0x7Fu;
        c.V0 = c.V1 & 0x00FFu;
        c.A1 = c.V0 << 1;
        c.V1 = c.A1 + c.T3;
        c.V0 = 0u | 0x0001u;
        c.V1 = m.ReadU8(c.V1);
        c.A0 = m.ReadU32(c.T2);
        c.V0 = c.V0 << (int)(c.V1 & 31u);
        c.V0 = c.V0 & c.A0;
        if (c.V0 != 0u)
        {
            c.V1 = 0u | 0x00FFu;
            goto L801B2B68;
        }
        c.V1 = 0u | 0x00FFu;
        c.At = 0x80180000u;
        c.At = c.At + c.A1;
        c.V1 = m.ReadU8((c.At + 0x151Cu));
    L801B2B68:;
        c.V1 = c.V1 & 0x00FFu;
    L801B2B70:;
        c.V0 = c.S0 & 0xFFFFu;
        c.V0 = c.V0 < c.V1 ? 1u : 0u;
        if (c.V0 != 0u)
        {
            c.T0 = c.T0 + 0x1u;
            goto L801B2BA8;
        }
        c.T0 = c.T0 + 0x1u;
        c.V0 = m.ReadU8(c.S1);
        m.WriteU16(c.S2, (ushort)c.V0);
        c.V0 = m.ReadU16((c.A2 - 0x2u));
        c.T1 = c.T1 + 0x1u;
        m.WriteU16((c.A3 - 0x2u), (ushort)c.V0);
        c.V0 = m.ReadU32(c.A2);
        c.S2 = c.S2 + 0x8u;
        m.WriteU32(c.A3, c.V0);
        c.A3 = c.A3 + 0x8u;
    L801B2BA8:;
        c.A2 = c.A2 + 0x8u;
        c.V0 = (int)c.T0 < 48 ? 1u : 0u;
        if (c.V0 != 0u)
        {
            c.S1 = c.S1 + 0x8u;
            goto L801B2AC8;
        }
        c.S1 = c.S1 + 0x8u;
        c.V0 = c.T1 & 0x00FFu;
        c.V0 = c.V0 - 0x7u;
        c.V0 = c.V0 & 0xFFFFu;
        c.RA = m.ReadU32((c.SP + 0x20u));
        c.S3 = m.ReadU32((c.SP + 0x1Cu));
        c.S2 = m.ReadU32((c.SP + 0x18u));
        c.S1 = m.ReadU32((c.SP + 0x14u));
        c.S0 = m.ReadU32((c.SP + 0x10u));
        c.SP = c.SP + 0x28u;
        return;
    }

    // JP Familiar Support in Menu Part 1
    public static void DrawRelicsMenu(CpuContext c, IMemory m)
    {
        c.SP = c.SP - 0x58u;
        m.WriteU32((c.SP + 0x48u), c.S6);
        c.S6 = c.A0 + 0u;
        m.WriteU32((c.SP + 0x40u), c.S4);
        c.S4 = 0x80090000u;
        c.S4 = c.S4 + 0x7964u;
        m.WriteU32((c.SP + 0x34u), c.S1);
        c.S1 = 0u + 0u;
        m.WriteU32((c.SP + 0x3Cu), c.S3);
        c.S3 = 0u + 0u;
        m.WriteU32((c.SP + 0x50u), c.RA);
        m.WriteU32((c.SP + 0x4Cu), c.S7);
        m.WriteU32((c.SP + 0x44u), c.S5);
        m.WriteU32((c.SP + 0x38u), c.S2);
        m.WriteU32((c.SP + 0x30u), c.S0);
        c.S7 = (uint)(short)m.ReadU16((c.S6 + 0x12u));
    L800F5F30:;
        c.V0 = 0u | 0x0017u;
        if (HasJPCard(m))
        {
            c.V0 = 0u | 0x0040u;    // Drawing Slots
            m.WriteU8(0x800A2E32, 0xC3);
            m.WriteU8(0x800A2E42, 0xAC);
        }
        else
        {
            m.WriteU8(0x800A2E32, 0xC1);
            m.WriteU8(0x800A2E42, 0xAA);
        }
        if (c.S1 != c.V0)
        {
            c.V1 = c.S3 >> 31;
            goto L800F5F44;
        }
        c.V1 = c.S3 >> 31;
        c.S1 = 0u | 0x0019u;
        c.S4 = c.S4 + 0x2u;
    L800F5F44:;
        c.V1 = c.S3 + c.V1;
        c.V1 = (uint)((int)c.V1 >> 1);
        c.V0 = c.V1 << 2;
        c.V0 = c.V0 + c.V1;
        c.V0 = c.V0 << 2;
        c.V0 = c.V0 - c.V1;
        c.V1 = c.S7 + 0x22u;
        c.S2 = c.V0 + c.V1;
        c.S0 = c.S1 & 0x0001u;
        c.V0 = c.S0 << 1;
        c.V0 = c.V0 + c.S0;
        c.V0 = c.V0 << 2;
        c.V0 = c.V0 - c.S0;
        c.V1 = m.ReadU8(c.S4);
        c.V1 = c.V1 & 0x0001u;
        if (c.V1 == 0u)
        {
            c.S5 = c.V0 << 4;
            goto L800F606C;
        }
        c.S5 = c.V0 << 4;
        if ((int)c.S2 < 0)
        {
            c.V0 = (int)c.S2 < 193 ? 1u : 0u;
            goto L800F617C;
        }
        c.V0 = (int)c.S2 < 193 ? 1u : 0u;
        if (c.V0 == 0u)
        {
            c.V0 = c.S1 & 0x0002u;
            goto L800F617C;
        }
        c.V0 = c.S1 & 0x0002u;
        if (c.V0 != 0u)
        {
            goto L800F5FFC;
        }
        if ((int)c.S1 >= 0)
        {
            c.A0 = c.S1 + 0u;
            goto L800F5FB0;
        }
        c.A0 = c.S1 + 0u;
        c.A0 = c.S1 + 0x3u;
    L800F5FB0:;
        c.A0 = (uint)((int)c.A0 >> 2);
        c.A0 = c.A0 - 0x80u;
        c.A0 = c.A0 & 0x00FFu;
        c.RA = 0x800F5FC0u;
        SoTN.func_800F548C(c, m);
        c.A0 = c.S6 + 0u;
        c.A1 = c.S5 + 0x38u;
        c.A2 = c.S2 + 0u;
        c.A3 = 0u | 0x0078u;
        c.V1 = 0u | 0x0010u;
        m.WriteU32((c.SP + 0x10u), c.V1);
        c.V1 = c.S0 << 4;
        c.V1 = c.V1 - c.S0;
        c.V1 = c.V1 << 3;
        c.V0 = c.V0 & 0x00FFu;
        m.WriteU32((c.SP + 0x18u), c.V0);
        c.V0 = 0u | 0x01A1u;
        m.WriteU32((c.SP + 0x1Cu), c.V0);
        c.V0 = 0u | 0x0006u;
        goto L800F6050;
    L800F5FFC:;
        if ((int)c.S1 >= 0)
        {
            c.A0 = c.S1 + 0u;
            goto L800F6008;
        }
        c.A0 = c.S1 + 0u;
        c.A0 = c.S1 + 0x3u;
    L800F6008:;
        c.A0 = (uint)((int)c.A0 >> 2);
        c.A0 = c.A0 + 0x3u;
        c.A0 = c.A0 & 0x00FFu;
        c.RA = 0x800F6018u;
        SoTN.func_800F548C(c, m);
        c.A0 = c.S6 + 0u;
        c.A1 = c.S5 + 0x38u;
        c.A2 = c.S2 + 0u;
        c.A3 = 0u | 0x0078u;
        c.V1 = 0u | 0x0010u;
        m.WriteU32((c.SP + 0x10u), c.V1);
        c.V1 = c.S0 << 4;
        c.V1 = c.V1 - c.S0;
        c.V1 = c.V1 << 3;
        c.V0 = c.V0 & 0x00FFu;
        m.WriteU32((c.SP + 0x18u), c.V0);
        c.V0 = 0u | 0x01A1u;
        m.WriteU32((c.SP + 0x1Cu), c.V0);
        c.V0 = 0u | 0x0007u;
    L800F6050:;
        m.WriteU32((c.SP + 0x20u), c.V0);
        c.V0 = 0u | 0x0001u;
        m.WriteU32((c.SP + 0x14u), c.V1);
        m.WriteU32((c.SP + 0x24u), c.V0);
        m.WriteU32((c.SP + 0x28u), 0u);
        m.WriteU32((c.SP + 0x2Cu), 0u);
        c.RA = 0x800F606Cu;
        SoTN.MenuDrawSprite(c, m);
    L800F606C:;
        c.T0 = 0u + 0u;
        c.V0 = m.ReadU8(c.S4);
        c.V1 = 0x80040000u;
        c.V1 = m.ReadU32((c.V1 - 0x3654u));
        c.V0 = c.V0 & 0x0002u;
        c.V0 = c.V0 < 0x00000001u ? 1u : 0u;
        c.V0 = 0u - c.V0;
        if (c.S3 != c.V1)
        {
            c.T1 = c.V0 & 0x0030u;
            goto L800F6124;
        }
        c.T1 = c.V0 & 0x0030u;
        c.V0 = 0x80130000u;
        c.V0 = m.ReadU16((c.V0 + 0x7850u));
        c.V0 = c.V0 + 0x1u;
        c.At = 0x80130000u;
        m.WriteU16((c.At + 0x7850u), (ushort)c.V0);
        c.V0 = c.V0 << 16;
        c.V0 = (uint)((int)c.V0 >> 16);
        c.V0 = (int)c.V0 < 72 ? 1u : 0u;
        if (c.V0 != 0u)
        {
            goto L800F60C4;
        }
        c.At = 0x80130000u;
        m.WriteU16((c.At + 0x7850u), (ushort)0u);
    L800F60C4:;
        c.V0 = 0x80130000u;
        c.V0 = m.ReadU16((c.V0 + 0x7850u));
        c.A0 = c.V0 << 16;
        c.V1 = (uint)((int)c.A0 >> 16);
        c.V0 = (int)c.V1 < 36 ? 1u : 0u;
        if (c.V0 == 0u)
        {
            c.V0 = 0x2AAA0000u;
            goto L800F6104;
        }
        c.V0 = 0x2AAA0000u;
        c.V0 = c.V0 | 0xAAABu;
        { var _r = (long)(int)c.V1 * (int)c.V0; c.LO = (uint)_r; c.HI = (uint)(_r >> 32); }
        c.V1 = (uint)((int)c.A0 >> 31);
        c.V0 = c.HI;
        c.V0 = c.V0 - c.V1;
        c.V0 = c.V0 << 16;
        c.T0 = (uint)((int)c.V0 >> 16);
        goto L800F6124;
    L800F6104:;
        c.V0 = c.V0 | 0xAAABu;
        c.V1 = c.V1 - 0x24u;
        { var _r = (long)(int)c.V1 * (int)c.V0; c.LO = (uint)_r; c.HI = (uint)(_r >> 32); }
        c.V1 = (uint)((int)c.V1 >> 31);
        c.V0 = c.HI;
        c.V0 = c.V0 - c.V1;
        c.V1 = 0u | 0x0006u;
        c.T0 = c.V1 - c.V0;
    L800F6124:;
        c.A0 = c.S6 + 0u;
        c.A1 = c.S5 | 0x0008u;
        c.A2 = c.S2 + 0u;
        c.A3 = 0u | 0x002Fu;
        c.V1 = 0x80040000u;
        c.V1 = m.ReadU32((c.V1 - 0x3654u));
        c.V0 = 0u | 0x000Fu;
        m.WriteU32((c.SP + 0x10u), c.V0);
        c.V0 = 0u | 0x0070u;
        m.WriteU32((c.SP + 0x18u), c.V0);
        c.V0 = c.T0 + 0x1C8u;
        m.WriteU32((c.SP + 0x1Cu), c.V0);
        c.V0 = 0u | 0x001Fu;
        m.WriteU32((c.SP + 0x20u), c.V0);
        c.V0 = 0u | 0x0040u;
        m.WriteU32((c.SP + 0x14u), c.T1);
        m.WriteU32((c.SP + 0x28u), c.V0);
        m.WriteU32((c.SP + 0x2Cu), 0u);
        c.V1 = c.S3 ^ c.V1;
        c.V1 = c.V1 < 0x00000001u ? 1u : 0u;
        m.WriteU32((c.SP + 0x24u), c.V1);
        c.RA = 0x800F617Cu;
        SoTN.MenuDrawSprite(c, m);
    L800F617C:;
        c.S1 = c.S1 + 0x1u;
        c.S3 = c.S3 + 0x1u;
        c.V0 = (int)c.S1 < 30 ? 1u : 0u;
        if (c.V0 != 0u)
        {
            c.S4 = c.S4 + 0x1u;
            goto L800F5F30;
        }
        c.S4 = c.S4 + 0x1u;
        c.A0 = c.S6 + 0u;
        c.A3 = 0u | 0x00A8u;
        c.A2 = 0x80040000u;
        c.A2 = m.ReadU32((c.A2 - 0x3654u));
        c.V0 = 0u | 0x0012u;
        m.WriteU32((c.SP + 0x10u), c.V0);
        c.V0 = 0u | 0x0060u;
        m.WriteU32((c.SP + 0x14u), c.V0);
        m.WriteU32((c.SP + 0x18u), 0u);
        m.WriteU32((c.SP + 0x1Cu), 0u);
        c.V1 = c.A2 >> 31;
        c.V1 = c.A2 + c.V1;
        c.V1 = (uint)((int)c.V1 >> 1);
        c.V0 = c.V1 << 2;
        c.V0 = c.V0 + c.V1;
        c.V0 = c.V0 << 2;
        c.V0 = c.V0 - c.V1;
        c.S2 = c.S7 + c.V0;
        c.A2 = c.A2 & 0x0001u;
        c.A1 = c.A2 << 1;
        c.A1 = c.A1 + c.A2;
        c.A1 = c.A1 << 2;
        c.A1 = c.A1 - c.A2;
        c.A1 = c.A1 << 4;
        c.A1 = c.A1 | 0x0008u;
        c.A2 = c.S2 + 0x21u;
        c.RA = 0x800F61FCu;
        SoTN.MenuDrawRect(c, m);
        c.RA = m.ReadU32((c.SP + 0x50u));
        c.S7 = m.ReadU32((c.SP + 0x4Cu));
        c.S6 = m.ReadU32((c.SP + 0x48u));
        c.S5 = m.ReadU32((c.SP + 0x44u));
        c.S4 = m.ReadU32((c.SP + 0x40u));
        c.S3 = m.ReadU32((c.SP + 0x3Cu));
        c.S2 = m.ReadU32((c.SP + 0x38u));
        c.S1 = m.ReadU32((c.SP + 0x34u));
        c.S0 = m.ReadU32((c.SP + 0x30u));
        c.SP = c.SP + 0x58u;
        return;
    }

    // JP Familiar Support in Menu Part 2
    public static void MenuHandle(CpuContext c, IMemory m)
    {
        c.SP = c.SP - 0x28u;
        c.V0 = 0u | 0x0001u;
        m.WriteU32((c.SP + 0x24u), c.RA);
        m.WriteU32((c.SP + 0x20u), c.S2);
        m.WriteU32((c.SP + 0x1Cu), c.S1);
        m.WriteU32((c.SP + 0x18u), c.S0);
        c.At = 0x80130000u;
        m.WriteU32((c.At + 0x784Cu), 0u);
        c.At = 0x80130000u;
        m.WriteU32((c.At + 0x7614u), c.V0);
        c.RA = 0x800FBC54u;
        SoTN.func_800F97DC(c, m);
        c.V0 = 0x80090000u;
        c.V0 = m.ReadU32((c.V0 + 0x78F8u));
        c.V0 = c.V0 < 0x00000010u ? 1u : 0u;
        if (c.V0 != 0u)
        {
            goto L800FBC9C;
        }
        c.V0 = 0x80090000u;
        c.V0 = m.ReadU16((c.V0 + 0x7494u));
        c.V0 = c.V0 & 0x0800u;
        if (c.V0 == 0u)
        {
            goto L800FBC9C;
        }
        c.RA = 0x800FBC8Cu;
        SoTN.CheckIfAllButtonsAreAssigned(c, m);
        if (c.V0 == 0u)
        {
            c.V0 = 0u | 0x0003u;
            goto L800FC6FC;
        }
        c.V0 = 0u | 0x0003u;
    L800FBC94:;
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x78F8u), c.V0);
    L800FBC9C:;
        c.V1 = 0x80090000u;
        c.V1 = m.ReadU32((c.V1 + 0x78F8u));
        c.V0 = c.V1 < 0x00000108u ? 1u : 0u;
        if (c.V0 == 0u)
        {
            c.V0 = c.V1 << 2;
            goto L800FD34C;
        }
        c.V0 = c.V1 << 2;
        c.At = 0x800E0000u;
        c.At = c.At + c.V0;
        c.V0 = m.ReadU32((c.At - 0x38A4u));
        switch (c.V0)
        {
            case 0x800FBCCCu: goto L800FBCCC;
            case 0x800FBE60u: goto L800FBE60;
            case 0x800FBEACu: goto L800FBEAC;
            case 0x800FBF30u: goto L800FBF30;
            case 0x800FBF40u: goto L800FBF40;
            case 0x800FBF78u: goto L800FBF78;
            case 0x800FBF98u: goto L800FBF98;
            case 0x800FBFDCu: goto L800FBFDC;
            case 0x800FC014u: goto L800FC014;
            case 0x800FC048u: goto L800FC048;
            case 0x800FC080u: goto L800FC080;
            case 0x800FC190u: goto L800FC190;
            case 0x800FC220u: goto L800FC220;
            case 0x800FC2A4u: goto L800FC2A4;
            case 0x800FC2C4u: goto L800FC2C4;
            case 0x800FD344u: goto L800FD344;
            case 0x800FC300u: goto L800FC300;
            case 0x800FC8C4u: goto L800FC8C4;
            case 0x800FC8D4u: goto L800FC8D4;
            case 0x800FC94Cu: goto L800FC94C;
            case 0x800FCC04u: goto L800FCC04;
            case 0x800FCC70u: goto L800FCC70;
            case 0x800FCD34u: goto L800FCD34;
            case 0x800FCD64u: goto L800FCD64;
            case 0x800FCD98u: goto L800FCD98;
            case 0x800FCDA8u: goto L800FCDA8;
            case 0x800FCE00u: goto L800FCE00;
            case 0x800FCE74u: goto L800FCE74;
            case 0x800FCE94u: goto L800FCE94;
            case 0x800FD000u: goto L800FD000;
            case 0x800FD090u: goto L800FD090;
            case 0x800FD104u: goto L800FD104;
            case 0x800FD1DCu: goto L800FD1DC;
            case 0x800FD260u: goto L800FD260;
            case 0x800FC420u: goto L800FC420;
            case 0x800FC440u: goto L800FC440;
            case 0x800FC478u: goto L800FC478;
            case 0x800FC4A8u: goto L800FC4A8;
            case 0x800FC68Cu: goto L800FC68C;
            case 0x800FC728u: goto L800FC728;
            case 0x800FC764u: goto L800FC764;
            case 0x800FC7CCu: goto L800FC7CC;
            case 0x800FC82Cu: goto L800FC82C;
            case 0x800FC870u: goto L800FC870;
            case 0x800FC374u: goto L800FC374;
            case 0x800FC3A4u: goto L800FC3A4;
            case 0x800FC3C0u: goto L800FC3C0;
            case 0x800FC3D0u: goto L800FC3D0;
            case 0x800FC3D8u: goto L800FC3D8;
            default: Dispatcher.Call(c, m, c.V0); return;
        }
    L800FBCCC:;
        c.RA = 0x800FBCD4u;
        SoTN.CdSoundCommandQueueEmpty(c, m);
        if (c.V0 == 0u)
        {
            goto L800FD344;
        }
        c.A0 = 0u | 0x0010u;
        c.RA = 0x800FBCE4u;
        SoTN.PlaySfx(c, m);
        c.A0 = 0u + 0u;
        c.RA = 0x800FBCECu;
        SoTN.func_800EA5E4(c, m);
        c.RA = 0x800FBCF4u;
        SoTN.func_800FAC30(c, m);
        c.RA = 0x800FBCFCu;
        SoTN.func_800FB9BC(c, m);
        c.V0 = 0x80090000u;
        c.V0 = m.ReadU32((c.V0 + 0x7C00u));
        c.V1 = 0x80090000u;
        c.V1 = m.ReadU32((c.V1 + 0x7C04u));
        c.A0 = 0x80140000u;
        c.A0 = c.A0 - 0x6FA4u;
        m.WriteU32(c.A0, c.V0);
        c.At = 0x80140000u;
        m.WriteU32((c.At - 0x6FA0u), c.V1);
        c.V1 = 0x80090000u;
        c.V1 = m.ReadU32((c.V1 + 0x7C0Cu));
        c.V0 = 0u | 0x0019u;
        if (c.V1 != c.V0)
        {
            c.V0 = 0u | 0x00D8u;
            goto L800FBD40;
        }
        c.V0 = 0u | 0x00D8u;
        m.WriteU32(c.A0, c.V0);
        c.At = 0x80140000u;
        m.WriteU32((c.At - 0x6FA0u), c.V0);
    L800FBD40:;
        c.V0 = 0x80070000u;
        c.V0 = m.ReadU32((c.V0 - 0x343Cu));
        c.V1 = 0u + 0u;
        c.At = 0x80130000u;
        m.WriteU32((c.At + 0x795Cu), c.V0);
    L800FBD54:;
        c.At = 0x80090000u;
        c.At = c.At + c.V1;
        c.V0 = m.ReadU8((c.At + 0x7982u));
        c.V0 = c.V0 & 0x0080u;
        if (c.V0 == 0u)
        {
            goto L800FBD80;
        }
        c.V1 = c.V1 + 0x1u;
        c.V0 = (int)c.V1 < 8 ? 1u : 0u;
        if (c.V0 != 0u)
        {
            goto L800FBD54;
        }
    L800FBD80:;
        c.At = 0x80130000u;
        m.WriteU32((c.At + 0x75DCu), c.V1);
        c.V1 = 0u | 0x0007u;
        c.V0 = 0x80130000u;
        c.V0 = c.V0 + 0x75FCu;
    L800FBD94:;
        m.WriteU32(c.V0, 0u);
        c.V1 = c.V1 - 0x1u;
        if ((int)c.V1 >= 0)
        {
            c.V0 = c.V0 - 0x4u;
            goto L800FBD94;
        }
        c.V0 = c.V0 - 0x4u;
        c.V1 = 0u + 0u;
        c.A2 = 0x80130000u;
        c.A2 = c.A2 + 0x75DCu;
        c.A1 = 0u | 0x0001u;
        c.A0 = 0u + 0u;
    L800FBDB8:;
        c.At = 0x80090000u;
        c.At = c.At + c.V1;
        c.V0 = m.ReadU8((c.At + 0x7964u));
        c.V0 = c.V0 & 0x0001u;
        if (c.V0 == 0u)
        {
            c.V1 = c.V1 + 0x1u;
            goto L800FBDF8;
        }
        c.V1 = c.V1 + 0x1u;
        c.At = 0x800B0000u;
        c.At = c.At + c.A0;
        c.V0 = m.ReadU32((c.At - 0x78D4u));
        if (c.V0 == 0u)
        {
            c.V0 = c.V0 << 2;
            goto L800FBDF8;
        }
        c.V0 = c.V0 << 2;
        c.V0 = c.V0 + c.A2;
        m.WriteU32(c.V0, c.A1);
        m.WriteU32((c.A2 + 0x20u), c.A1);
    L800FBDF8:;
        c.V0 = (int)c.V1 < 30 ? 1u : 0u;
        if (c.V0 != 0u)
        {
            c.A0 = c.A0 + 0x10u;
            goto L800FBDB8;
        }
        c.A0 = c.A0 + 0x10u;
        c.A0 = 0x80090000u;
        c.A0 = m.ReadU32((c.A0 + 0x7C10u));
        c.V1 = 0x80090000u;
        c.V1 = m.ReadU8((c.V1 + 0x7A65u));
        c.V0 = c.A0 ^ 0x0032u;
        c.V0 = c.V0 < 0x00000001u ? 1u : 0u;
        c.V1 = c.V1 | c.V0;
        c.A0 = c.A0 ^ 0x0037u;
        c.V0 = 0x80090000u;
        c.V0 = m.ReadU8((c.V0 + 0x7A6Au));
        c.A0 = c.A0 < 0x00000001u ? 1u : 0u;
        c.At = 0x80130000u;
        m.WriteU32((c.At + 0x7600u), c.V1);
        c.V1 = 0x80090000u;
        c.V1 = m.ReadU32((c.V1 + 0x78F8u));
        c.V0 = c.V0 | c.A0;
        c.V1 = c.V1 + 0x1u;
        c.At = 0x80130000u;
        m.WriteU32((c.At + 0x7604u), c.V0);
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x78F8u), c.V1);
        goto L800FD344;
    L800FBE60:;
        c.RA = 0x800FBE68u;
        SoTN.func_801025F4(c, m);
        if (c.V0 == 0u)
        {
            goto L800FD344;
        }
        c.RA = 0x800FBE78u;
        SoTN.SetGPUBuffRGBZero(c, m);
        c.A0 = 0u | 0x0180u;
        c.RA = 0x800FBE80u;
        SoTN.SetFadeWidth(c, m);
        c.RA = 0x800FBE88u;
        SoTN.SetMenuDisplayBuffer(c, m);
        c.RA = 0x800FBE90u;
        SoTN.func_800FAC48(c, m);
        c.V0 = 0x80090000u;
        c.V0 = m.ReadU32((c.V0 + 0x78F8u));
        c.V1 = 0u | 0x0001u;
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x73ECu), c.V1);
        c.V0 = c.V0 + 0x1u;
        goto L800FCE64;
    L800FBEAC:;
        c.RA = 0x800FBEB4u;
        SoTN.func_80133950(c, m);
        if (c.V0 == 0u)
        {
            goto L800FD344;
        }
        c.V0 = 0x80090000u;
        c.V0 = m.ReadU32((c.V0 + 0x7910u));
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7910u), 0u);
        c.At = 0x80130000u;
        m.WriteU32((c.At + 0x7958u), c.V0);
        c.RA = 0x800FBEDCu;
        SoTN.func_800F6A48(c, m);
        c.RA = 0x800FBEE4u;
        SoTN.func_800F84CC(c, m);
        c.A0 = 0u | 0x0002u;
        c.RA = 0x800FBEECu;
        SoTN.SetFadeMode(c, m);
        c.A0 = 0x800A0000u;
        c.A0 = m.ReadU32((c.A0 + 0x2D64u));
        c.A1 = 0u + 0u;
        c.RA = 0x800FBEFCu;
        SoTN.func_800F98AC(c, m);
        c.A0 = 0u + 0u;
        c.RA = 0x800FBF04u;
        SoTN.func_800FABEC(c, m);
        c.A0 = 0u | 0x0001u;
        c.RA = 0x800FBF0Cu;
        SoTN.func_800FABEC(c, m);
        c.V0 = 0u | 0x0010u;
        c.At = 0x80130000u;
        m.WriteU32((c.At + 0x7608u), 0u);
        c.At = 0x80130000u;
        m.WriteU32((c.At + 0x760Cu), 0u);
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x78F8u), c.V0);
        goto L800FD344;
    L800FBF30:;
        c.A0 = 0u | 0x0001u;
        c.RA = 0x800FBF38u;
        SoTN.SetFadeMode(c, m);
        goto L800FCE54;
    L800FBF40:;
        c.RA = 0x800FBF48u;
        SoTN.func_801025F4(c, m);
        if (c.V0 == 0u)
        {
            goto L800FD344;
        }
        c.A0 = 0u | 0x0100u;
        c.RA = 0x800FBF58u;
        SoTN.SetFadeWidth(c, m);
        c.RA = 0x800FBF60u;
        SoTN.SetStageDisplayBuffer_dra(c, m);
        c.RA = 0x800FBF68u;
        SoTN.func_800FAC48(c, m);
        c.RA = 0x800FBF70u;
        SoTN.func_800EB6B4(c, m);
        goto L800FCE54;
    L800FBF78:;
        c.RA = 0x800FBF80u;
        SoTN.UpdateCapePalette(c, m);
        c.A0 = 0x80090000u;
        c.A0 = m.ReadU32((c.A0 + 0x7904u));
        c.RA = 0x800FBF90u;
        SoTN.LoadGfxAsync(c, m);
        goto L800FCE54;
    L800FBF98:;
        c.RA = 0x800FBFA0u;
        SoTN.func_800EB720(c, m);
        if (c.V0 != 0u)
        {
            goto L800FD344;
        }
        c.A0 = 0u + 0u;
        c.RA = 0x800FBFB0u;
        SoTN.LoadWeaponPrg(c, m);
        if (c.V0 == 0u)
        {
            goto L800FD344;
        }
        c.V0 = 0x80090000u;
        c.V0 = m.ReadU32((c.V0 + 0x78ACu));
        if (c.V0 != 0u)
        {
            goto L800FCE54;
        }
        c.A0 = 0u + 0u;
        c.RA = 0x800FBFD4u;
        SoTN.InitWeapon(c, m);
        goto L800FCE54;
    L800FBFDC:;
        c.V0 = 0x80090000u;
        c.V0 = m.ReadU32((c.V0 + 0x78ACu));
        if (c.V0 == 0u)
        {
            goto L800FCE54;
        }
        c.V0 = 0x80070000u;
        c.V0 = m.ReadU32((c.V0 - 0x3C50u));
        if (c.V0 != 0u)
        {
            goto L800FD344;
        }
        c.A0 = 0u + 0u;
        c.RA = 0x800FC00Cu;
        SoTN.InitWeapon(c, m);
        goto L800FCE54;
    L800FC014:;
        c.A0 = 0u | 0x0001u;
        c.RA = 0x800FC01Cu;
        SoTN.LoadWeaponPrg(c, m);
        if (c.V0 == 0u)
        {
            goto L800FD344;
        }
        c.V0 = 0x80090000u;
        c.V0 = m.ReadU32((c.V0 + 0x78ACu));
        if (c.V0 != 0u)
        {
            goto L800FCE54;
        }
        c.A0 = 0u | 0x0001u;
        c.RA = 0x800FC040u;
        SoTN.InitWeapon(c, m);
        goto L800FCE54;
    L800FC048:;
        c.V0 = 0x80090000u;
        c.V0 = m.ReadU32((c.V0 + 0x78ACu));
        if (c.V0 == 0u)
        {
            goto L800FCE54;
        }
        c.V0 = 0x80070000u;
        c.V0 = m.ReadU32((c.V0 - 0x3C50u));
        if (c.V0 != 0u)
        {
            goto L800FD344;
        }
        c.A0 = 0u | 0x0001u;
        c.RA = 0x800FC078u;
        SoTN.InitWeapon(c, m);
        goto L800FCE54;
    L800FC080:;
        c.V1 = 0x80070000u;
        c.V1 = m.ReadU32((c.V1 - 0x343Cu));
        if (c.V1 == 0u)
        {
            goto L800FC0A8;
        }
        c.V0 = 0x80130000u;
        c.V0 = m.ReadU32((c.V0 + 0x795Cu));
        if (c.V1 == c.V0)
        {
            goto L800FC0B8;
        }
    L800FC0A8:;
        c.RA = 0x800FC0B0u;
        SoTN.func_800FAB1C(c, m);
        c.V1 = 0x80070000u;
        c.V1 = m.ReadU32((c.V1 - 0x343Cu));
    L800FC0B8:;
        if (c.V1 == 0u)
        {
            goto L800FC134;
        }
        c.V0 = 0x80070000u;
        c.V0 = m.ReadU32((c.V0 + 0x3064u));
        if (c.V1 != c.V0)
        {
            goto L800FC154;
        }
        c.V0 = 0x80130000u;
        c.V0 = m.ReadU32((c.V0 + 0x795Cu));
        if (c.V1 == c.V0)
        {
            c.V0 = c.V1 - 0x1u;
            goto L800FC128;
        }
        c.V0 = c.V1 - 0x1u;
        c.V1 = c.V0 << 1;
        c.V1 = c.V1 + c.V0;
        c.V1 = c.V1 << 2;
        c.At = 0x80090000u;
        c.At = c.At + c.V1;
        c.A0 = m.ReadU32((c.At + 0x7C4Cu));
        c.V0 = (int)c.A0 < 9999 ? 1u : 0u;
        if (c.V0 == 0u)
        {
            c.V0 = c.A0 + 0x1u;
            goto L800FC120;
        }
        c.V0 = c.A0 + 0x1u;
        c.At = 0x80090000u;
        c.At = c.At + c.V1;
        m.WriteU32((c.At + 0x7C4Cu), c.V0);
    L800FC120:;
        c.A0 = 0u | 0x0001u;
        goto L800FC12C;
    L800FC128:;
        c.A0 = 0u | 0x0003u;
    L800FC12C:;
        c.RA = 0x800FC134u;
        SoTN.InitializeServant(c, m);
    L800FC134:;
        c.V0 = 0x80090000u;
        c.V0 = m.ReadU32((c.V0 + 0x78F8u));
        c.V0 = c.V0 + 0x2u;
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x78F8u), c.V0);
        goto L800FD344;
    L800FC154:;
        c.V0 = 0x80090000u;
        c.V0 = m.ReadU32((c.V0 + 0x78ACu));
        if (c.V0 == 0u)
        {
            c.V0 = 0u | 0x0001u;
            goto L800FCE54;
        }
        c.V0 = 0u | 0x0001u;
        c.At = 0x80070000u;
        m.WriteU32((c.At - 0x3C68u), c.V0);
        c.V0 = 0u | 0x001Bu;
        c.At = 0x80070000u;
        m.WriteU32((c.At - 0x4504u), c.V0);
        c.V0 = c.V1 - 0x1u;
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7918u), c.V0);
        goto L800FCE54;
    L800FC190:;
        c.V0 = 0x80090000u;
        c.V0 = m.ReadU32((c.V0 + 0x78ACu));
        if (c.V0 == 0u)
        {
            goto L800FC1C0;
        }
        c.V0 = 0x80070000u;
        c.V0 = m.ReadU32((c.V0 - 0x3C50u));
        if (c.V0 == 0u)
        {
            goto L800FC1C8;
        }
        goto L800FD344;
    L800FC1C0:;
        c.RA = 0x800FC1C8u;
        SoTN.func_800E6250(c, m);
    L800FC1C8:;
        c.A0 = 0u | 0x0001u;
        c.RA = 0x800FC1D0u;
        SoTN.InitializeServant(c, m);
        c.A0 = 0x80070000u;
        c.A0 = m.ReadU32((c.A0 - 0x343Cu));
        c.V1 = c.A0 - 0x1u;
        c.V0 = c.V1 << 1;
        c.V0 = c.V0 + c.V1;
        c.A1 = c.V0 << 2;
        c.At = 0x80090000u;
        c.At = c.At + c.A1;
        c.V1 = m.ReadU32((c.At + 0x7C4Cu));
        c.At = 0x80070000u;
        m.WriteU32((c.At + 0x3064u), c.A0);
        c.V0 = (int)c.V1 < 9999 ? 1u : 0u;
        if (c.V0 == 0u)
        {
            c.V0 = c.V1 + 0x1u;
            goto L800FCE54;
        }
        c.V0 = c.V1 + 0x1u;
        c.At = 0x80090000u;
        c.At = c.At + c.A1;
        m.WriteU32((c.At + 0x7C4Cu), c.V0);
        goto L800FCE54;
    L800FC220:;
        c.V0 = 0x80070000u;
        c.V0 = m.ReadU32((c.V0 - 0x3C50u));
        if (c.V0 != 0u)
        {
            goto L800FD344;
        }
        c.RA = 0x800FC23Cu;
        SoTN.CdSoundCommandQueueEmpty(c, m);
        if (c.V0 == 0u)
        {
            goto L800FD344;
        }
        c.V0 = 0x80130000u;
        c.V0 = m.ReadU32((c.V0 + 0x7958u));
        c.V1 = 0x80090000u;
        c.V1 = m.ReadU32((c.V1 + 0x7928u));
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7910u), c.V0);
        if (c.V1 != 0u)
        {
            goto L800FC26C;
        }
        c.A0 = 0u | 0x0011u;
        c.RA = 0x800FC26Cu;
        SoTN.PlaySfx(c, m);
    L800FC26C:;
        c.RA = 0x800FC274u;
        SoTN.CheckWeaponCombo(c, m);
        c.RA = 0x800FC27Cu;
        SoTN.func_800F53A4(c, m);
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x73ECu), 0u);
        c.RA = 0x800FC28Cu;
        SoTN.func_800FAC30(c, m);
        c.RA = 0x800FC294u;
        SoTN.func_800F86E4(c, m);
        c.A0 = 0u | 0x0001u;
        c.RA = 0x800FC29Cu;
        SoTN.func_8010A234(c, m);
        goto L800FCE54;
    L800FC2A4:;
        c.RA = 0x800FC2ACu;
        SoTN.func_80133950(c, m);
        if (c.V0 == 0u)
        {
            goto L800FD344;
        }
        c.A0 = 0u | 0x0002u;
        c.RA = 0x800FC2BCu;
        SoTN.SetFadeMode(c, m);
        goto L800FCE54;
    L800FC2C4:;
        c.RA = 0x800FC2CCu;
        SoTN.func_801025F4(c, m);
        if (c.V0 == 0u)
        {
            goto L800FD344;
        }
        c.A0 = 0u | 0x000Fu;
        c.RA = 0x800FC2DCu;
        SoTN.PlaySfx(c, m);
        c.A0 = 0u | 0x00A4u;
        c.RA = 0x800FC2E4u;
        SoTN.PlaySfx(c, m);
        c.A0 = 0u | 0x00A8u;
        c.RA = 0x800FC2ECu;
        SoTN.PlaySfx(c, m);
        c.V0 = 0u | 0x0001u;
        c.At = 0x80040000u;
        m.WriteU32((c.At - 0x365Cu), c.V0);
        goto L800FD344;
    L800FC300:;
        c.S1 = 0x80090000u;
        c.S1 = c.S1 + 0x7494u;
        c.V0 = m.ReadU16(c.S1);
        c.V0 = c.V0 & 0x0010u;
        if (c.V0 != 0u)
        {
            c.V0 = 0u | 0x0003u;
            goto L800FBC94;
        }
        c.V0 = 0u | 0x0003u;
        c.S0 = 0x80040000u;
        c.S0 = c.S0 - 0x3658u;
        c.A0 = c.S0 + 0u;
        c.A1 = 0u | 0x0005u;
        c.A2 = 0u + 0u;
        c.RA = 0x800FC334u;
        SoTN.MenuHandleCursorInput(c, m);
        c.V0 = m.ReadU16(c.S1);
        c.V0 = c.V0 & 0x0040u;
        if (c.V0 == 0u)
        {
            goto L800FD344;
        }
        c.A0 = m.ReadU32(c.S0);
        c.V0 = c.A0 < 0x00000005u ? 1u : 0u;
        if (c.V0 == 0u)
        {
            c.V0 = c.A0 << 2;
            goto L800FC3F4;
        }
        c.V0 = c.A0 << 2;
        c.At = 0x800E0000u;
        c.At = c.At + c.V0;
        c.V0 = m.ReadU32((c.At - 0x3484u));
        switch (c.V0)
        {
            case 0x800FC374u: goto L800FC374;
            case 0x800FC3A4u: goto L800FC3A4;
            case 0x800FC3C0u: goto L800FC3C0;
            case 0x800FC3D0u: goto L800FC3D0;
            case 0x800FC3D8u: goto L800FC3D8;
            default: Dispatcher.Call(c, m, c.V0); return;
        }
    L800FC374:;
        c.A0 = 0u | 0x0002u;
        c.RA = 0x800FC37Cu;
        SoTN.MenuShow(c, m);
        c.RA = 0x800FC384u;
        SoTN.func_800FB0FC(c, m);
        c.RA = 0x800FC38Cu;
        SoTN.func_800FADC0(c, m);
        c.A0 = 0u | 0x0003u;
        c.RA = 0x800FC394u;
        SoTN.MenuShow(c, m);
        c.A0 = 0u | 0x0004u;
        c.RA = 0x800FC39Cu;
        SoTN.MenuShow(c, m);
        c.V0 = 0u | 0x0040u;
        goto L800FC3EC;
    L800FC3A4:;
        c.V0 = 0x80130000u;
        c.V0 = m.ReadU32((c.V0 + 0x75DCu));
        if (c.V0 == 0u)
        {
            c.V0 = 0u | 0x0030u;
            goto L800FC3F4;
        }
        c.V0 = 0u | 0x0030u;
        goto L800FC3EC;
    L800FC3C0:;
        c.A0 = 0u + 0u;
        c.RA = 0x800FC3C8u;
        SoTN.func_800F9E18(c, m);
        c.V0 = 0u | 0x0020u;
        goto L800FC3EC;
    L800FC3D0:;
        c.V0 = 0u | 0x0100u;
        goto L800FC3EC;
    L800FC3D8:;
        c.V0 = 0x80130000u;
        c.V0 = m.ReadU32((c.V0 + 0x75FCu));
        if (c.V0 == 0u)
        {
            c.V0 = 0u | 0x0070u;
            goto L800FC3F4;
        }
        c.V0 = 0u | 0x0070u;
    L800FC3EC:;
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x78F8u), c.V0);
    L800FC3F4:;
        c.V1 = 0x80090000u;
        c.V1 = m.ReadU32((c.V1 + 0x78F8u));
        c.V0 = 0u | 0x0010u;
        if (c.V1 == c.V0)
        {
            goto L800FC718;
        }
        c.A0 = 0u + 0u;
        c.RA = 0x800FC410u;
        SoTN.MenuHide(c, m);
        c.A0 = 0u | 0x0001u;
        c.RA = 0x800FC418u;
        SoTN.MenuHide(c, m);
        goto L800FC67C;
    L800FC420:;
        c.A0 = 0u | 0x0021u;
        c.RA = 0x800FC428u;
        SoTN.func_800EA5E4(c, m);
        c.RA = 0x800FC430u;
        SoTN.func_800EAEA4(c, m);
        c.A0 = 0u | 0x000Fu;
        c.RA = 0x800FC438u;
        SoTN.MenuShow(c, m);
        goto L800FCE54;
    L800FC440:;
        c.V0 = 0x80090000u;
        c.V0 = m.ReadU16((c.V0 + 0x7494u));
        c.V0 = c.V0 & 0x0050u;
        if (c.V0 == 0u)
        {
            goto L800FD344;
        }
        c.A0 = 0u + 0u;
        c.RA = 0x800FC460u;
        SoTN.MenuShow(c, m);
        c.A0 = 0u | 0x0001u;
        c.RA = 0x800FC468u;
        SoTN.MenuShow(c, m);
        c.RA = 0x800FC470u;
        SoTN.func_800EAEA4(c, m);
        c.A0 = 0u | 0x000Fu;
        goto L800FCD18;
    L800FC478:;
        c.RA = 0x800FC480u;
        SoTN.func_800F82F4(c, m);
        c.A0 = 0u | 0x0004u;
        c.RA = 0x800FC488u;
        SoTN.MenuShow(c, m);
        c.A0 = 0u | 0x0007u;
        c.RA = 0x800FC490u;
        SoTN.MenuShow(c, m);
        c.V0 = 0x80090000u;
        c.V0 = m.ReadU32((c.V0 + 0x78F8u));
        c.V0 = c.V0 + 0x1u;
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x78F8u), c.V0);
    L800FC4A8:;
        c.S0 = 0x80040000u;
        c.S0 = c.S0 - 0x3620u;
        c.A0 = c.S0 + 0u;
        c.A1 = 0u | 0x0006u;
        c.A2 = 0u + 0u;
        c.RA = 0x800FC4C0u;
        SoTN.MenuHandleCursorInput(c, m);
        c.A0 = 0u | 0x0002u;
        c.RA = 0x800FC4C8u;
        SoTN.func_800F9808(c, m);
        c.V0 = m.ReadU32(c.S0);
        c.V1 = c.V0 + 0x1u;
        c.V0 = 0u | 0x0002u;
        if (c.V1 != c.V0)
        {
            c.V0 = 0u | 0x0003u;
            goto L800FC4F8;
        }
        c.V0 = 0u | 0x0003u;
        c.V0 = 0x80130000u;
        c.V0 = m.ReadU32((c.V0 + 0x7600u));
        if (c.V0 != 0u)
        {
            c.V0 = 0u | 0x0003u;
            goto L800FC4F8;
        }
        c.V0 = 0u | 0x0003u;
        c.V1 = 0u + 0u;
    L800FC4F8:;
        if (c.V1 != c.V0)
        {
            c.V0 = 0u | 0x0006u;
            goto L800FC518;
        }
        c.V0 = 0u | 0x0006u;
        c.V0 = 0x80130000u;
        c.V0 = m.ReadU32((c.V0 + 0x7604u));
        if (c.V0 != 0u)
        {
            c.V0 = 0u | 0x0006u;
            goto L800FC518;
        }
        c.V0 = 0u | 0x0006u;
        c.V1 = 0u + 0u;
    L800FC518:;
        if (c.V1 != c.V0)
        {
            c.A1 = 0u | 0x0002u;
            goto L800FC538;
        }
        c.A1 = 0u | 0x0002u;
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x4220u));
        if (c.V0 != 0u)
        {
            c.V0 = c.V1 << 2;
            goto L800FC53C;
        }
        c.V0 = c.V1 << 2;
        c.V1 = 0u + 0u;
    L800FC538:;
        c.V0 = c.V1 << 2;
    L800FC53C:;
        c.At = 0x800A0000u;
        c.At = c.At + c.V0;
        c.A0 = m.ReadU32((c.At + 0x2D48u));
        c.A2 = 0u + 0u;
        c.RA = 0x800FC550u;
        SoTN.func_800F99B8(c, m);
        c.V1 = 0x80090000u;
        c.V1 = m.ReadU16((c.V1 + 0x7494u));
        c.V0 = c.V1 & 0x0010u;
        if (c.V0 == 0u)
        {
            c.V0 = c.V1 & 0x0040u;
            goto L800FC588;
        }
        c.V0 = c.V1 & 0x0040u;
        c.A0 = 0u + 0u;
        c.RA = 0x800FC570u;
        SoTN.MenuShow(c, m);
        c.A0 = 0u | 0x0001u;
        c.RA = 0x800FC578u;
        SoTN.MenuShow(c, m);
        c.A0 = 0u | 0x0004u;
        c.RA = 0x800FC580u;
        SoTN.MenuHide(c, m);
        c.A0 = 0u | 0x0007u;
        goto L800FCD18;
    L800FC588:;
        if (c.V0 == 0u)
        {
            goto L800FD344;
        }
        c.V1 = 0x80040000u;
        c.V1 = m.ReadU32((c.V1 - 0x3620u));
        c.V0 = c.V1 < 0x00000006u ? 1u : 0u;
        if (c.V0 == 0u)
        {
            c.V0 = c.V1 << 2;
            goto L800FC668;
        }
        c.V0 = c.V1 << 2;
        c.At = 0x800E0000u;
        c.At = c.At + c.V0;
        c.V0 = m.ReadU32((c.At - 0x346Cu));
        switch (c.V0)
        {
            case 0x800FC5C0u: goto L800FC5C0;
            case 0x800FC5D0u: goto L800FC5D0;
            case 0x800FC5F4u: goto L800FC5F4;
            case 0x800FC618u: goto L800FC618;
            case 0x800FC628u: goto L800FC628;
            case 0x800FC638u: goto L800FC638;
            default: Dispatcher.Call(c, m, c.V0); return;
        }
    L800FC5C0:;
        c.A0 = 0u | 0x0009u;
        c.RA = 0x800FC5C8u;
        SoTN.MenuShow(c, m);
        c.V0 = 0u | 0x0102u;
        goto L800FC660;
    L800FC5D0:;
        c.V0 = 0x80130000u;
        c.V0 = m.ReadU32((c.V0 + 0x7600u));
        if (c.V0 == 0u)
        {
            goto L800FC668;
        }
        c.A0 = 0u | 0x000Au;
        c.RA = 0x800FC5ECu;
        SoTN.MenuShow(c, m);
        c.V0 = 0u | 0x0103u;
        goto L800FC660;
    L800FC5F4:;
        c.V0 = 0x80130000u;
        c.V0 = m.ReadU32((c.V0 + 0x7604u));
        if (c.V0 == 0u)
        {
            goto L800FC668;
        }
        c.A0 = 0u | 0x0008u;
        c.RA = 0x800FC610u;
        SoTN.func_800FABEC(c, m);
        c.V0 = 0u | 0x0104u;
        goto L800FC660;
    L800FC618:;
        c.A0 = 0u | 0x000Cu;
        c.RA = 0x800FC620u;
        SoTN.MenuShow(c, m);
        c.V0 = 0u | 0x0105u;
        goto L800FC660;
    L800FC628:;
        c.A0 = 0u | 0x000Bu;
        c.RA = 0x800FC630u;
        SoTN.MenuShow(c, m);
        c.V0 = 0u | 0x0106u;
        goto L800FC660;
    L800FC638:;
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x4220u));
        if (c.V0 == 0u)
        {
            goto L800FC668;
        }
        c.RA = 0x800FC654u;
        SoTN.SortTimeAttackEntries(c, m);
        c.A0 = 0u | 0x000Du;
        c.RA = 0x800FC65Cu;
        SoTN.MenuShow(c, m);
        c.V0 = 0u | 0x0107u;
    L800FC660:;
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x78F8u), c.V0);
    L800FC668:;
        c.V1 = 0x80090000u;
        c.V1 = m.ReadU32((c.V1 + 0x78F8u));
        c.V0 = 0u | 0x0101u;
        if (c.V1 == c.V0)
        {
            goto L800FC718;
        }
    L800FC67C:;
        c.A0 = 0u | 0x0633u;
        c.RA = 0x800FC684u;
        SoTN.PlaySfx(c, m);
        goto L800FD344;
    L800FC68C:;
        c.S0 = 0x80040000u;
        c.S0 = c.S0 - 0x3618u;
        c.A0 = c.S0 + 0u;
        c.A1 = 0u | 0x0007u;
        c.A2 = 0u + 0u;
        c.RA = 0x800FC6A4u;
        SoTN.MenuHandleCursorInput(c, m);
        c.A1 = 0u | 0x0008u;
        c.A2 = 0u | 0x0005u;
        c.A0 = m.ReadU32(c.S0);
        c.V0 = 0x80040000u;
        c.V0 = c.V0 - 0x3608u;
        c.A0 = c.A0 << 2;
        c.A0 = c.A0 + c.V0;
        c.RA = 0x800FC6C4u;
        SoTN.MenuHandleCursorInput(c, m);
        c.V0 = 0x80090000u;
        c.V0 = m.ReadU16((c.V0 + 0x7494u));
        c.V0 = c.V0 & 0x0050u;
        if (c.V0 == 0u)
        {
            goto L800FD344;
        }
        c.RA = 0x800FC6E4u;
        SoTN.CheckIfAllButtonsAreAssigned(c, m);
        if (c.V0 == 0u)
        {
            goto L800FC6FC;
        }
        c.A0 = 0u | 0x0633u;
        c.RA = 0x800FC6F4u;
        SoTN.PlaySfx(c, m);
        c.A0 = 0u | 0x0009u;
        goto L800FC8A8;
    L800FC6FC:;
        c.A0 = 0u | 0x0002u;
        c.RA = 0x800FC704u;
        SoTN.func_800F9808(c, m);
        c.A0 = 0x800E0000u;
        c.A0 = c.A0 - 0x38CCu;
        c.A1 = 0u | 0x0002u;
        c.A2 = 0u + 0u;
        c.RA = 0x800FC718u;
        SoTN.func_800F99B8(c, m);
    L800FC718:;
        c.A0 = 0u | 0x0686u;
        c.RA = 0x800FC720u;
        SoTN.PlaySfx(c, m);
        goto L800FD344;
    L800FC728:;
        c.A0 = 0x80040000u;
        c.A0 = c.A0 - 0x3508u;
        c.A1 = 0u | 0x0002u;
        c.A2 = 0u + 0u;
        c.RA = 0x800FC73Cu;
        SoTN.MenuHandleCursorInput(c, m);
        c.V0 = 0x80090000u;
        c.V0 = m.ReadU16((c.V0 + 0x7494u));
        c.V0 = c.V0 & 0x0050u;
        if (c.V0 == 0u)
        {
            goto L800FD344;
        }
        c.A0 = 0u | 0x0633u;
        c.RA = 0x800FC75Cu;
        SoTN.PlaySfx(c, m);
        c.A0 = 0u | 0x000Au;
        goto L800FC8A8;
    L800FC764:;
        c.S0 = 0x80040000u;
        c.S0 = c.S0 - 0x361Cu;
        c.A0 = c.S0 + 0u;
        c.A1 = 0u | 0x0006u;
        c.A2 = 0u + 0u;
        c.RA = 0x800FC77Cu;
        SoTN.MenuHandleCursorInput(c, m);
        c.A1 = 0u | 0x0020u;
        c.A2 = 0u | 0x0005u;
        c.A0 = m.ReadU32(c.S0);
        c.V0 = 0x80040000u;
        c.V0 = c.V0 - 0x3558u;
        c.A0 = c.A0 << 2;
        c.A0 = c.A0 + c.V0;
        c.RA = 0x800FC79Cu;
        SoTN.MenuHandleCursorInput(c, m);
        c.V0 = 0x80090000u;
        c.V0 = m.ReadU16((c.V0 + 0x7494u));
        c.V0 = c.V0 & 0x0050u;
        if (c.V0 == 0u)
        {
            goto L800FD344;
        }
        c.A0 = 0u | 0x0633u;
        c.RA = 0x800FC7BCu;
        SoTN.PlaySfx(c, m);
        c.A0 = 0u | 0x0008u;
        c.RA = 0x800FC7C4u;
        SoTN.func_800FAC0C(c, m);
        c.V0 = 0u | 0x0101u;
        goto L800FC8B4;
    L800FC7CC:;
        c.S0 = 0x80040000u;
        c.S0 = c.S0 - 0x3614u;
        c.A0 = c.S0 + 0u;
        c.A1 = 0u | 0x0003u;
        c.A2 = 0u + 0u;
        c.RA = 0x800FC7E4u;
        SoTN.MenuHandleCursorInput(c, m);
        c.A1 = 0u | 0x0010u;
        c.A2 = 0u | 0x0005u;
        c.A0 = m.ReadU32(c.S0);
        c.V0 = 0x80040000u;
        c.V0 = c.V0 - 0x3540u;
        c.A0 = c.A0 << 2;
        c.A0 = c.A0 + c.V0;
        c.RA = 0x800FC804u;
        SoTN.MenuHandleCursorInput(c, m);
        c.V0 = 0x80090000u;
        c.V0 = m.ReadU16((c.V0 + 0x7494u));
        c.V0 = c.V0 & 0x0050u;
        if (c.V0 == 0u)
        {
            goto L800FD344;
        }
        c.A0 = 0u | 0x0633u;
        c.RA = 0x800FC824u;
        SoTN.PlaySfx(c, m);
        c.A0 = 0u | 0x000Cu;
        goto L800FC8A8;
    L800FC82C:;
        c.A0 = 0x80040000u;
        c.A0 = c.A0 - 0x3504u;
        c.A1 = 0u | 0x0002u;
        c.A2 = 0u + 0u;
        c.RA = 0x800FC840u;
        SoTN.MenuHandleCursorInput(c, m);
        c.RA = 0x800FC848u;
        SoTN.func_800E493C(c, m);
        c.V0 = 0x80090000u;
        c.V0 = m.ReadU16((c.V0 + 0x7494u));
        c.V0 = c.V0 & 0x0050u;
        if (c.V0 == 0u)
        {
            goto L800FD344;
        }
        c.A0 = 0u | 0x0633u;
        c.RA = 0x800FC868u;
        SoTN.PlaySfx(c, m);
        c.A0 = 0u | 0x000Bu;
        goto L800FC8A8;
    L800FC870:;
        c.A0 = 0x80040000u;
        c.A0 = c.A0 - 0x3610u;
        c.A1 = 0u | 0x0010u;
        c.A2 = 0u | 0x0003u;
        c.RA = 0x800FC884u;
        SoTN.MenuHandleCursorInput(c, m);
        c.V0 = 0x80090000u;
        c.V0 = m.ReadU16((c.V0 + 0x7494u));
        c.V0 = c.V0 & 0x0050u;
        if (c.V0 == 0u)
        {
            goto L800FD344;
        }
        c.A0 = 0u | 0x0633u;
        c.RA = 0x800FC8A4u;
        SoTN.PlaySfx(c, m);
        c.A0 = 0u | 0x000Du;
    L800FC8A8:;
        c.RA = 0x800FC8B0u;
        SoTN.MenuHide(c, m);
        c.V0 = 0u | 0x0101u;
    L800FC8B4:;
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x78F8u), c.V0);
        goto L800FD344;
    L800FC8C4:;
        c.A0 = 0u | 0x0001u;
        c.RA = 0x800FC8CCu;
        SoTN.func_800F9E18(c, m);
        goto L800FCE54;
    L800FC8D4:;
        c.A0 = 0u | 0x0004u;
        c.RA = 0x800FC8DCu;
        SoTN.MenuShow(c, m);
        c.A0 = 0u | 0x0005u;
        c.RA = 0x800FC8E4u;
        SoTN.MenuShow(c, m);
        c.A0 = 0u | 0x0002u;
        c.RA = 0x800FC8ECu;
        SoTN.func_800F9808(c, m);
        c.S0 = 0x80040000u;
        c.S0 = m.ReadU32((c.S0 - 0x3654u));
        c.V0 = (int)c.S0 < 23 ? 1u : 0u;
        if (c.V0 != 0u)
        {
            goto L800FC908;
        }
        c.S0 = c.S0 + 0x2u;
    L800FC908:;
        c.At = 0x80090000u;
        c.At = c.At + c.S0;
        c.V0 = m.ReadU8((c.At + 0x7964u));
        c.V0 = c.V0 & 0x0001u;
        if (c.V0 == 0u)
        {
            c.V0 = c.S0 << 4;
            goto L800FC93C;
        }
        c.V0 = c.S0 << 4;
        c.At = 0x800B0000u;
        c.At = c.At + c.V0;
        c.A0 = m.ReadU32((c.At - 0x78DCu));
        c.A1 = 0u | 0x0002u;
        c.A2 = 0u + 0u;
        c.RA = 0x800FC93Cu;
        SoTN.func_800F99B8(c, m);
    L800FC93C:;
        c.A0 = 0u | 0x0002u;
        c.RA = 0x800FC944u;
        SoTN.func_800F9E18(c, m);
        goto L800FCE54;
    L800FC94C:;
        c.A1 = 0x92490000u;
        c.S0 = 0x80040000u;
        c.S0 = c.S0 - 0x3654u;
        c.A0 = m.ReadU32(c.S0);
        c.A1 = c.A1 | 0x2493u;
        c.V0 = c.A0 >> 31;
        c.V0 = c.A0 + c.V0;
        c.V0 = (uint)((int)c.V0 >> 1);
        c.V1 = c.V0 << 4;
        c.V1 = c.V1 - c.V0;
        c.V1 = c.V1 << 3;
        c.V1 = 0u - c.V1;
        { var _r = (long)(int)c.V1 * (int)c.A1; c.LO = (uint)_r; c.HI = (uint)(_r >> 32); }
        c.S1 = c.A0 + 0u;
        c.V0 = c.HI;
        c.V0 = c.V0 + c.V1;
        c.V0 = (uint)((int)c.V0 >> 3);
        c.V1 = (uint)((int)c.V1 >> 31);
        c.V0 = c.V0 - c.V1;
        c.At = 0x80130000u;
        m.WriteU16((c.At + 0x76C8u), (ushort)c.V0);
        c.V0 = (int)c.S1 < 23 ? 1u : 0u;
        if (c.V0 != 0u)
        {
            c.A0 = c.S0 + 0u;
            goto L800FC9B0;
        }
        c.A0 = c.S0 + 0u;
        c.S1 = c.S1 + 0x2u;
    L800FC9B0:;
        c.A1 = 0u | 0x001Cu;
        if (HasJPCard(m))
        {
            c.A1 = 0u | 0x001Eu;    // Selector Cursor
        }
        c.A2 = 0u | 0x0001u;
        c.RA = 0x800FC9BCu;
        SoTN.MenuHandleCursorInput(c, m);
        c.S0 = m.ReadU32(c.S0);
        c.V0 = (int)c.S0 < 23 ? 1u : 0u;
        if (c.V0 != 0u)
        {
            goto L800FC9D4;
        }
        c.S0 = c.S0 + 0x2u;
        if (HasJPCard(m))
        {
            c.S0 = c.S0 - 0x2u;     // Don't Skip
        }
    L800FC9D4:;
        c.V0 = 0x80090000u;
        c.V0 = m.ReadU16((c.V0 + 0x7494u));
        c.V0 = c.V0 & 0x0040u;
        if (c.V0 == 0u)
        {
            goto L800FCB30;
        }
        c.At = 0x80090000u;
        c.At = c.At + c.S0;
        c.V0 = m.ReadU8((c.At + 0x7964u));
        c.V0 = c.V0 & 0x0001u;
        if (c.V0 == 0u)
        {
            goto L800FCB30;
        }
        c.A0 = 0u | 0x0633u;
        c.RA = 0x800FCA10u;
        SoTN.PlaySfx(c, m);
        c.At = 0x80090000u;
        c.At = c.At + c.S0;
        c.V0 = m.ReadU8((c.At + 0x7964u));
        c.V0 = c.V0 ^ 0x0002u;
        c.At = 0x80090000u;
        c.At = c.At + c.S0;
        m.WriteU8((c.At + 0x7964u), (byte)c.V0);
        c.V0 = c.S0 << 4;
        c.At = 0x800B0000u;
        c.At = c.At + c.V0;
        c.V0 = m.ReadU32((c.At - 0x78D4u));
        if ((int)c.V0 <= 0)
        {
            c.A0 = 0u + 0u;
            goto L800FCB30;
        }
        c.A0 = 0u + 0u;
        c.V1 = 0x80090000u;
        c.V1 = c.V1 + 0x7964u;
        c.A1 = c.S0 + c.V1;
        c.A2 = c.V1 + 0x1Eu;
    L800FCA5C:;
        if (c.V1 == c.A1)
        {
            goto L800FCA8C;
        }
        c.At = 0x800B0000u;
        c.At = c.At + c.A0;
        c.V0 = m.ReadU32((c.At - 0x78D4u));
        if ((int)c.V0 <= 0)
        {
            goto L800FCA8C;
        }
        c.V0 = m.ReadU8(c.V1);
        c.V0 = c.V0 & 0x00FDu;
        m.WriteU8(c.V1, (byte)c.V0);
    L800FCA8C:;
        c.V1 = c.V1 + 0x1u;
        c.V0 = (int)c.V1 < (int)c.A2 ? 1u : 0u;
        if (c.V0 != 0u)
        {
            c.A0 = c.A0 + 0x10u;
            goto L800FCA5C;
        }
        c.A0 = c.A0 + 0x10u;
        c.At = 0x80090000u;
        c.At = c.At + c.S0;
        c.V0 = m.ReadU8((c.At + 0x7964u));
        c.V0 = c.V0 & 0x0002u;
        if (c.V0 == 0u)
        {
            c.V0 = c.S0 << 4;
            goto L800FCB28;
        }
        c.V0 = c.S0 << 4;
        c.At = 0x800B0000u;
        c.At = c.At + c.V0;
        c.V1 = m.ReadU32((c.At - 0x78D4u));
        c.V0 = 0u | 0x0005u;
        c.At = 0x80070000u;
        m.WriteU32((c.At - 0x343Cu), c.V1);
        if (c.V1 != c.V0)
        {
            c.S2 = 0u | 0x007Eu;
            goto L800FCB30;
        }
        c.S2 = 0u | 0x007Eu;
        c.V1 = 0x80090000u;
        c.V1 = c.V1 + 0x7C00u;
        c.V0 = m.ReadU32(c.V1);
        if (c.V0 != c.S2)
        {
            c.A0 = 0u | 0x007Eu;
            goto L800FCAFC;
        }
        c.A0 = 0u | 0x007Eu;
        m.WriteU32(c.V1, 0u);
        c.A1 = 0u + 0u;
        c.RA = 0x800FCAFCu;
        SoTN.AddToInventory(c, m);
    L800FCAFC:;
        c.V0 = 0x80090000u;
        c.V0 = m.ReadU32((c.V0 + 0x7C04u));
        if (c.V0 != c.S2)
        {
            c.A0 = 0u | 0x007Eu;
            goto L800FCB30;
        }
        c.A0 = 0u | 0x007Eu;
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x7C04u), 0u);
        c.A1 = 0u + 0u;
        c.RA = 0x800FCB20u;
        SoTN.AddToInventory(c, m);
        goto L800FCB30;
    L800FCB28:;
        c.At = 0x80070000u;
        m.WriteU32((c.At - 0x343Cu), 0u);
    L800FCB30:;
        if (c.S1 == c.S0)
        {
            goto L800FCB74;
        }
        c.A0 = 0u | 0x0002u;
        c.RA = 0x800FCB40u;
        SoTN.func_800F9808(c, m);
        c.At = 0x80090000u;
        c.At = c.At + c.S0;
        c.V0 = m.ReadU8((c.At + 0x7964u));
        c.V0 = c.V0 & 0x0001u;
        if (c.V0 == 0u)
        {
            c.V0 = c.S0 << 4;
            goto L800FCB74;
        }
        c.V0 = c.S0 << 4;
        c.At = 0x800B0000u;
        c.At = c.At + c.V0;
        c.A0 = m.ReadU32((c.At - 0x78DCu));
        c.A1 = 0u | 0x0002u;
        c.A2 = 0u + 0u;
        c.RA = 0x800FCB74u;
        SoTN.func_800F99B8(c, m);
    L800FCB74:;
        c.At = 0x80130000u;
        m.WriteU32((c.At + 0x7608u), 0u);
        c.At = 0x80090000u;
        c.At = c.At + c.S0;
        c.V0 = m.ReadU8((c.At + 0x7964u));
        c.V0 = c.V0 & 0x0001u;
        if (c.V0 == 0u)
        {
            c.V0 = c.S0 << 4;
            goto L800FCBC4;
        }
        c.V0 = c.S0 << 4;
        c.At = 0x800B0000u;
        c.At = c.At + c.V0;
        c.A0 = m.ReadU16((c.At - 0x78D8u));
        c.At = 0x800B0000u;
        c.At = c.At + c.V0;
        c.A1 = m.ReadU16((c.At - 0x78D6u));
        c.V0 = 0u | 0x0001u;
        c.At = 0x80130000u;
        m.WriteU32((c.At + 0x7608u), c.V0);
        c.A2 = 0u | 0x001Fu;
        c.RA = 0x800FCBC4u;
        SoTN.LoadEquipIcon(c, m);
    L800FCBC4:;
        c.V0 = 0x80090000u;
        c.V0 = m.ReadU16((c.V0 + 0x7494u));
        c.V0 = c.V0 & 0x0010u;
        if (c.V0 == 0u)
        {
            goto L800FD344;
        }
        c.A0 = 0u + 0u;
        c.RA = 0x800FCBE4u;
        SoTN.MenuShow(c, m);
        c.A0 = 0u | 0x0001u;
        c.RA = 0x800FCBECu;
        SoTN.MenuShow(c, m);
        c.A0 = 0u | 0x0004u;
        c.RA = 0x800FCBF4u;
        SoTN.MenuHide(c, m);
        c.A0 = 0u | 0x0005u;
        c.RA = 0x800FCBFCu;
        SoTN.MenuHide(c, m);
        c.V0 = 0u | 0x0010u;
        goto L800FCF94;
    L800FCC04:;
        c.A0 = 0u | 0x0004u;
        c.RA = 0x800FCC0Cu;
        SoTN.MenuShow(c, m);
        c.A0 = 0u | 0x0006u;
        c.RA = 0x800FCC14u;
        SoTN.MenuShow(c, m);
        c.A0 = 0u | 0x0002u;
        c.RA = 0x800FCC1Cu;
        SoTN.func_800F9808(c, m);
        c.RA = 0x800FCC24u;
        SoTN.func_800F9F40(c, m);
        c.A0 = 0u | 0x0002u;
        c.RA = 0x800FCC2Cu;
        SoTN.func_800F9808(c, m);
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x3624u));
        c.At = 0x80090000u;
        c.At = c.At + c.V0;
        c.S0 = m.ReadU8((c.At + 0x7982u));
        c.A1 = 0u | 0x0002u;
        c.S0 = c.S0 ^ 0x0080u;
        c.V0 = c.S0 << 3;
        c.V0 = c.V0 - c.S0;
        c.V0 = c.V0 << 2;
        c.At = 0x800B0000u;
        c.At = c.At + c.V0;
        c.A0 = m.ReadU32((c.At - 0x7BE8u));
        c.A2 = 0u + 0u;
        c.RA = 0x800FCC68u;
        SoTN.func_800F99B8(c, m);
        goto L800FCE54;
    L800FCC70:;
        c.S0 = 0x80040000u;
        c.S0 = c.S0 - 0x3624u;
        c.A0 = c.S0 + 0u;
        c.A1 = 0x80130000u;
        c.A1 = m.ReadU8((c.A1 + 0x75DCu));
        c.S1 = m.ReadU32(c.S0);
        c.A2 = 0u | 0x0003u;
        c.RA = 0x800FCC90u;
        SoTN.MenuHandleCursorInput(c, m);
        c.V0 = m.ReadU32(c.S0);
        if (c.S1 == c.V0)
        {
            goto L800FCCE4;
        }
        c.A0 = 0u | 0x0002u;
        c.RA = 0x800FCCA8u;
        SoTN.func_800F9808(c, m);
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x3624u));
        c.At = 0x80090000u;
        c.At = c.At + c.V0;
        c.S0 = m.ReadU8((c.At + 0x7982u));
        c.A1 = 0u | 0x0002u;
        c.S0 = c.S0 ^ 0x0080u;
        c.V0 = c.S0 << 3;
        c.V0 = c.V0 - c.S0;
        c.V0 = c.V0 << 2;
        c.At = 0x800B0000u;
        c.At = c.At + c.V0;
        c.A0 = m.ReadU32((c.At - 0x7BE8u));
        c.A2 = 0u + 0u;
        c.RA = 0x800FCCE4u;
        SoTN.func_800F99B8(c, m);
    L800FCCE4:;
        c.V0 = 0x80090000u;
        c.V0 = m.ReadU16((c.V0 + 0x7494u));
        c.V0 = c.V0 & 0x0010u;
        if (c.V0 == 0u)
        {
            goto L800FD344;
        }
        c.A0 = 0u + 0u;
        c.RA = 0x800FCD04u;
        SoTN.MenuShow(c, m);
        c.A0 = 0u | 0x0001u;
        c.RA = 0x800FCD0Cu;
        SoTN.MenuShow(c, m);
        c.A0 = 0u | 0x0004u;
        c.RA = 0x800FCD14u;
        SoTN.MenuHide(c, m);
        c.A0 = 0u | 0x0006u;
    L800FCD18:;
        c.RA = 0x800FCD20u;
        SoTN.MenuHide(c, m);
        c.V0 = 0u | 0x0010u;
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x78F8u), c.V0);
        goto L800FD344;
    L800FCD34:;
        c.RA = 0x800FCD3Cu;
        SoTN.func_801025F4(c, m);
        if (c.V0 == 0u)
        {
            goto L800FD344;
        }
        c.A0 = 0u | 0x0100u;
        c.RA = 0x800FCD4Cu;
        SoTN.SetFadeWidth(c, m);
        c.RA = 0x800FCD54u;
        SoTN.SetStageDisplayBuffer_dra(c, m);
        c.RA = 0x800FCD5Cu;
        SoTN.func_800FAC48(c, m);
        goto L800FCE54;
    L800FCD64:;
        c.V0 = 0x80070000u;
        c.V0 = m.ReadU32((c.V0 - 0x3C50u));
        if (c.V0 != 0u)
        {
            goto L800FD344;
        }
        c.At = 0x80050000u;
        m.WriteU8((c.At + 0x4318u), (byte)0u);
        c.At = 0x80040000u;
        m.WriteU8((c.At - 0x34DCu), (byte)0u);
        c.RA = 0x800FCD90u;
        SoTN.func_801083BC(c, m);
        goto L800FCE54;
    L800FCD98:;
        c.RA = 0x800FCDA0u;
        SoTN.func_800F5A90(c, m);
        goto L800FCE54;
    L800FCDA8:;
        c.RA = 0x800FCDB0u;
        SoTN.func_800F5A90(c, m);
        c.V0 = 0x80090000u;
        c.V0 = m.ReadU16((c.V0 + 0x7494u));
        c.V0 = c.V0 & 0x0050u;
        if (c.V0 == 0u)
        {
            goto L800FD344;
        }
        c.RA = 0x800FCDD0u;
        SoTN.func_801073C0(c, m);
        c.At = 0x80070000u;
        m.WriteU32((c.At - 0x3C68u), 0u);
        c.RA = 0x800FCDE0u;
        SoTN.SetGPUBuffRGBZero(c, m);
        c.A0 = 0u | 0x0180u;
        c.RA = 0x800FCDE8u;
        SoTN.SetFadeWidth(c, m);
        c.RA = 0x800FCDF0u;
        SoTN.SetMenuDisplayBuffer(c, m);
        c.RA = 0x800FCDF8u;
        SoTN.func_800FAC48(c, m);
        goto L800FCE54;
    L800FCE00:;
        c.A0 = 0u | 0x0002u;
        c.RA = 0x800FCE08u;
        SoTN.SetFadeMode(c, m);
        c.A0 = 0u | 0x0002u;
        c.RA = 0x800FCE10u;
        SoTN.func_800F9808(c, m);
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x3624u));
        c.At = 0x80090000u;
        c.At = c.At + c.V0;
        c.S0 = m.ReadU8((c.At + 0x7982u));
        c.S0 = c.S0 ^ 0x0080u;
        c.V0 = c.S0 << 3;
        c.V0 = c.V0 - c.S0;
        c.V0 = c.V0 << 2;
        c.At = 0x800B0000u;
        c.At = c.At + c.V0;
        c.A0 = m.ReadU32((c.At - 0x7BE8u));
        c.A1 = 0u | 0x0002u;
        c.RA = 0x800FCE4Cu;
        SoTN.func_800F98AC(c, m);
        c.RA = 0x800FCE54u;
        SoTN.func_800F9F40(c, m);
    L800FCE54:;
        c.V0 = 0x80090000u;
        c.V0 = m.ReadU32((c.V0 + 0x78F8u));
        c.V0 = c.V0 + 0x1u;
    L800FCE64:;
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x78F8u), c.V0);
        goto L800FD344;
    L800FCE74:;
        c.RA = 0x800FCE7Cu;
        SoTN.func_801025F4(c, m);
        if (c.V0 == 0u)
        {
            c.V0 = 0u | 0x0031u;
            goto L800FD344;
        }
        c.V0 = 0u | 0x0031u;
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x78F8u), c.V0);
        goto L800FD344;
    L800FCE94:;
        c.At = 0x80130000u;
        m.WriteU32((c.At + 0x7948u), 0u);
        c.RA = 0x800FCEA4u;
        SoTN.func_800FB0FC(c, m);
        c.S0 = 0x80040000u;
        c.S0 = c.S0 - 0x3650u;
        c.A0 = c.S0 + 0u;
        c.A1 = 0u | 0x0007u;
        c.S1 = m.ReadU32(c.S0);
        c.A2 = 0u + 0u;
        c.RA = 0x800FCEC0u;
        SoTN.MenuHandleCursorInput(c, m);
        c.A0 = 0u | 0x0001u;
        c.RA = 0x800FCEC8u;
        SoTN.MenuEquipHandlePageScroll(c, m);
        c.RA = 0x800FCED0u;
        SoTN.func_800FB0FC(c, m);
        c.V0 = m.ReadU32(c.S0);
        if (c.S1 == c.V0)
        {
            goto L800FCEE8;
        }
        c.RA = 0x800FCEE8u;
        SoTN.func_800FADC0(c, m);
    L800FCEE8:;
        c.V0 = 0x80090000u;
        c.V0 = m.ReadU16((c.V0 + 0x7494u));
        c.V0 = c.V0 & 0x0080u;
        if (c.V0 == 0u)
        {
            goto L800FCF40;
        }
        c.V0 = 0x80130000u;
        c.V0 = m.ReadU32((c.V0 + 0x75CCu));
        if (c.V0 != 0u)
        {
            goto L800FCF40;
        }
        c.A0 = 0u | 0x0633u;
        c.RA = 0x800FCF1Cu;
        SoTN.PlaySfx(c, m);
        c.A0 = 0u | 0x000Eu;
        c.RA = 0x800FCF24u;
        SoTN.MenuShow(c, m);
        c.V0 = 0u | 0x0041u;
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x78F8u), c.V0);
        c.At = 0x80130000u;
        m.WriteU32((c.At + 0x7618u), 0u);
        goto L800FD344;
    L800FCF40:;
        c.V1 = 0x80090000u;
        c.V1 = m.ReadU16((c.V1 + 0x7494u));
        c.V0 = c.V1 & 0x0010u;
        if (c.V0 == 0u)
        {
            c.V0 = c.V1 & 0x0040u;
            goto L800FCFAC;
        }
        c.V0 = c.V1 & 0x0040u;
        c.A0 = 0u | 0x0002u;
        c.RA = 0x800FCF60u;
        SoTN.MenuHide(c, m);
        c.A0 = 0u | 0x0003u;
        c.RA = 0x800FCF68u;
        SoTN.MenuHide(c, m);
        c.A0 = 0u | 0x0004u;
        c.RA = 0x800FCF70u;
        SoTN.MenuHide(c, m);
        c.A0 = 0u + 0u;
        c.RA = 0x800FCF78u;
        SoTN.MenuShow(c, m);
        c.A0 = 0u | 0x0001u;
        c.RA = 0x800FCF80u;
        SoTN.MenuShow(c, m);
        c.V0 = 0u | 0x0010u;
        c.At = 0x80130000u;
        m.WriteU32((c.At + 0x7844u), 0u);
        c.At = 0x80130000u;
        m.WriteU32((c.At + 0x7848u), 0u);
    L800FCF94:;
        c.At = 0x80130000u;
        m.WriteU32((c.At + 0x7608u), 0u);
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x78F8u), c.V0);
        goto L800FD344;
    L800FCFAC:;
        if (c.V0 == 0u)
        {
            goto L800FD344;
        }
        c.A0 = 0u | 0x0633u;
        c.RA = 0x800FCFBCu;
        SoTN.PlaySfx(c, m);
    L800FCFBC:;
        c.RA = 0x800FCFC4u;
        SoTN.func_800FB0FC(c, m);
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x3650u));
        c.V0 = (int)c.V0 < 2 ? 1u : 0u;
        if (c.V0 == 0u)
        {
            c.V0 = 0u | 0x0050u;
            goto L800FCFEC;
        }
        c.V0 = 0u | 0x0050u;
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x78F8u), c.V0);
        goto L800FD344;
    L800FCFEC:;
        c.V0 = 0u | 0x0060u;
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x78F8u), c.V0);
        goto L800FD344;
    L800FD000:;
        c.RA = 0x800FD008u;
        SoTN.func_800FB0FC(c, m);
        c.A0 = 0x80130000u;
        c.A0 = c.A0 + 0x7618u;
        c.A1 = 0u | 0x000Bu;
        c.A2 = 0u + 0u;
        c.RA = 0x800FD01Cu;
        SoTN.MenuHandleCursorInput(c, m);
        c.A0 = 0u + 0u;
        c.RA = 0x800FD024u;
        SoTN.MenuEquipHandlePageScroll(c, m);
        c.V1 = 0x80090000u;
        c.V1 = m.ReadU16((c.V1 + 0x7494u));
        c.V0 = c.V1 & 0x0040u;
        if (c.V0 == 0u)
        {
            c.V0 = c.V1 & 0x0010u;
            goto L800FD06C;
        }
        c.V0 = c.V1 & 0x0010u;
        c.A0 = 0u | 0x0633u;
        c.RA = 0x800FD044u;
        SoTN.PlaySfx(c, m);
        c.RA = 0x800FD04Cu;
        SoTN.func_800FBAC4(c, m);
        c.At = 0x80130000u;
        m.WriteU16((c.At + 0x768Cu), (ushort)0u);
        c.At = 0x80040000u;
        m.WriteU32((c.At - 0x364Cu), 0u);
        c.RA = 0x800FD064u;
        SoTN.func_800FB0FC(c, m);
        goto L800FD344;
    L800FD06C:;
        if (c.V0 == 0u)
        {
            goto L800FD344;
        }
        c.A0 = 0u | 0x000Eu;
        c.RA = 0x800FD07Cu;
        SoTN.MenuHide(c, m);
        c.V0 = 0u | 0x0040u;
        c.At = 0x80090000u;
        m.WriteU32((c.At + 0x78F8u), c.V0);
        goto L800FD344;
    L800FD090:;
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x364Cu));
        c.At = 0x80090000u;
        c.At = c.At + c.V0;
        c.V1 = m.ReadU8((c.At + 0x7A8Du));
        c.V0 = c.V1 << 1;
        c.V0 = c.V0 + c.V1;
        c.V0 = c.V0 << 2;
        c.V0 = c.V0 + c.V1;
        c.V0 = c.V0 << 2;
        c.At = 0x80090000u;
        c.At = c.At + c.V1;
        c.A1 = m.ReadU8((c.At + 0x798Au));
        c.At = 0x800A0000u;
        c.At = c.At + c.V0;
        c.A2 = m.ReadU32((c.At + 0x4B08u));
        c.At = 0x800A0000u;
        c.At = c.At + c.V0;
        c.A3 = m.ReadU16((c.At + 0x4B30u));
        c.At = 0x800A0000u;
        c.At = c.At + c.V0;
        c.V0 = m.ReadU16((c.At + 0x4B32u));
        c.A0 = 0x80040000u;
        c.A0 = c.A0 - 0x364Cu;
        m.WriteU32((c.SP + 0x10u), c.V0);
        c.RA = 0x800FD0FCu;
        SoTN.func_800FAEC4(c, m);
        c.A0 = 0u + 0u;
        c.RA = 0x800FD104u;
        SoTN.func_800FAF44(c, m);
    L800FD104:;
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x3650u));
        if (c.V0 != 0u)
        {
            c.V0 = 0u | 0x0001u;
            goto L800FD128;
        }
        c.V0 = 0u | 0x0001u;
        c.At = 0x80130000u;
        m.WriteU32((c.At + 0x75D0u), 0u);
        goto L800FD130;
    L800FD128:;
        c.At = 0x80130000u;
        m.WriteU32((c.At + 0x75D0u), c.V0);
    L800FD130:;
        c.A0 = 0x80040000u;
        c.A0 = c.A0 - 0x364Cu;
        c.S0 = 0x80090000u;
        c.S0 = c.S0 + 0x7A8Du;
        c.A1 = c.S0 + 0u;
        c.A3 = 0x80130000u;
        c.A3 = m.ReadU32((c.A3 + 0x75D0u));
        c.A2 = c.S0 - 0x103u;
        c.V0 = c.S0 + 0x173u;
        c.A3 = c.A3 << 2;
        c.A3 = c.A3 + c.V0;
        c.RA = 0x800FD160u;
        SoTN.func_800FB23C(c, m);
        c.V1 = c.V0 + 0u;
        c.V0 = 0u | 0x0002u;
        if (c.V1 != c.V0)
        {
            c.V0 = 0u | 0x0001u;
            goto L800FD33C;
        }
        c.V0 = 0u | 0x0001u;
        c.V0 = 0x80040000u;
        c.V0 = m.ReadU32((c.V0 - 0x364Cu));
        c.V0 = c.V0 + c.S0;
        c.V1 = m.ReadU8(c.V0);
        c.V0 = c.V1 << 1;
        c.V0 = c.V0 + c.V1;
        c.V0 = c.V0 << 2;
        c.V0 = c.V0 + c.V1;
        c.V0 = c.V0 << 2;
        c.At = 0x800A0000u;
        c.At = c.At + c.V0;
        c.A0 = m.ReadU32((c.At + 0x4B08u));
        c.At = 0x80090000u;
        c.At = c.At + c.V1;
        c.A1 = m.ReadU8((c.At + 0x798Au));
        c.At = 0x800A0000u;
        c.At = c.At + c.V0;
        c.A2 = m.ReadU16((c.At + 0x4B30u));
        c.At = 0x800A0000u;
        c.At = c.At + c.V0;
        c.A3 = m.ReadU16((c.At + 0x4B32u));
        c.RA = 0x800FD1D4u;
        SoTN.func_800FAD34(c, m);
        goto L800FD344;
    L800FD1DC:;
        c.A0 = 0x80130000u;
        c.A0 = m.ReadU32((c.A0 + 0x75D4u));
        c.V0 = 0x80040000u;
        c.V0 = c.V0 - 0x3648u;
        c.A0 = c.A0 << 2;
        c.A0 = c.A0 + c.V0;
        c.V0 = m.ReadU32(c.A0);
        c.V1 = 0x80130000u;
        c.V1 = m.ReadU32((c.V1 + 0x75D8u));
        c.V0 = c.V0 << 2;
        c.V0 = c.V0 + c.V1;
        c.V0 = m.ReadU32(c.V0);
        c.At = 0x80090000u;
        c.At = c.At + c.V0;
        c.V1 = m.ReadU8((c.At + 0x7B36u));
        c.V0 = c.V1 << 5;
        c.At = 0x800A0000u;
        c.At = c.At + c.V0;
        c.A2 = m.ReadU32((c.At + 0x771Cu));
        c.At = 0x800A0000u;
        c.At = c.At + c.V0;
        c.A3 = m.ReadU16((c.At + 0x7730u));
        c.At = 0x800A0000u;
        c.At = c.At + c.V0;
        c.V0 = m.ReadU16((c.At + 0x7732u));
        c.At = 0x80090000u;
        c.At = c.At + c.V1;
        c.A1 = m.ReadU8((c.At + 0x7A33u));
        m.WriteU32((c.SP + 0x10u), c.V0);
        c.RA = 0x800FD258u;
        SoTN.func_800FAEC4(c, m);
        c.A0 = 0u | 0x0001u;
        c.RA = 0x800FD260u;
        SoTN.func_800FAF44(c, m);
    L800FD260:;
        c.S0 = 0x80090000u;
        c.S0 = c.S0 + 0x7B36u;
        c.A1 = c.S0 + 0u;
        c.A2 = c.S0 - 0x103u;
        c.V0 = 0x80040000u;
        c.V0 = c.V0 - 0x3650u;
        c.S1 = c.V0 + 0x8u;
        c.V1 = 0x80130000u;
        c.V1 = m.ReadU32((c.V1 + 0x75D4u));
        c.A3 = m.ReadU32(c.V0);
        c.V0 = c.S0 + 0xD2u;
        c.A0 = c.V1 << 2;
        c.A0 = c.A0 + c.S1;
        c.A3 = c.A3 ^ 0x0006u;
        c.A3 = c.A3 < 0x00000001u ? 1u : 0u;
        c.A3 = c.A3 + c.V1;
        c.A3 = c.A3 << 2;
        c.A3 = c.A3 + c.V0;
        c.RA = 0x800FD2ACu;
        SoTN.func_800FB23C(c, m);
        c.V1 = c.V0 + 0u;
        c.V0 = 0u | 0x0002u;
        if (c.V1 != c.V0)
        {
            c.V0 = 0u | 0x0001u;
            goto L800FD33C;
        }
        c.V0 = 0u | 0x0001u;
        c.V0 = 0x80130000u;
        c.V0 = m.ReadU32((c.V0 + 0x75D4u));
        c.V0 = c.V0 << 2;
        c.V0 = c.V0 + c.S1;
        c.V0 = m.ReadU32(c.V0);
        c.V1 = 0x80130000u;
        c.V1 = m.ReadU32((c.V1 + 0x75D8u));
        c.V0 = c.V0 << 2;
        c.V0 = c.V0 + c.V1;
        c.V0 = m.ReadU32(c.V0);
        c.V0 = c.V0 + c.S0;
        c.V1 = m.ReadU8(c.V0);
        c.V0 = c.V1 << 5;
        c.At = 0x800A0000u;
        c.At = c.At + c.V0;
        c.A0 = m.ReadU32((c.At + 0x771Cu));
        c.At = 0x80090000u;
        c.At = c.At + c.V1;
        c.A1 = m.ReadU8((c.At + 0x7A33u));
        c.At = 0x800A0000u;
        c.At = c.At + c.V0;
        c.A2 = m.ReadU16((c.At + 0x7730u));
        c.At = 0x800A0000u;
        c.At = c.At + c.V0;
        c.A3 = m.ReadU16((c.At + 0x7732u));
        c.RA = 0x800FD334u;
        SoTN.func_800FAD34(c, m);
        goto L800FD344;
    L800FD33C:;
        if (c.V1 == c.V0)
        {
            goto L800FCFBC;
        }
    L800FD344:;
        c.V1 = 0x80090000u;
        c.V1 = m.ReadU32((c.V1 + 0x78F8u));
    L800FD34C:;
        c.V0 = c.V1 < 0x00000010u ? 1u : 0u;
        if (c.V0 == 0u)
        {
            c.V0 = c.V1 - 0x3u;
            goto L800FD368;
        }
        c.V0 = c.V1 - 0x3u;
        c.V0 = c.V0 < 0x00000002u ? 1u : 0u;
        if (c.V0 == 0u)
        {
            goto L800FD380;
        }
    L800FD368:;
        c.RA = 0x800FD370u;
        SoTN.MenuDraw(c, m);
        c.RA = 0x800FD378u;
        SoTN.func_800F9690(c, m);
        c.RA = 0x800FD380u;
        SoTN.func_800F96F4(c, m);
    L800FD380:;
        c.RA = m.ReadU32((c.SP + 0x24u));
        c.S2 = m.ReadU32((c.SP + 0x20u));
        c.S1 = m.ReadU32((c.SP + 0x1Cu));
        c.S0 = m.ReadU32((c.SP + 0x18u));
        c.SP = c.SP + 0x28u;
        return;
    }


    // Bounty Hunter
    // Relic Drop
    // Pre-emp
    static readonly UInt32[] RelicTable = { 0x801CA4CC, 0x801C6E24, 0x801C769C, 0x801C3B24, 0x801BE7F8, 0x801A3F58, 0x801CC06C, 0x801BF1A0, 0, 0x801D06D4, 0x801BE7B8, 0x801B3714, 0x801BF5B8, 0x801B27E8, 0, 0x801C6E24, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0x801BD408, 0x801AF0D0, 0x801A8C44, 0x801BA034, 0x801BCDD4, 0x801A0CD8, 0x801BA710, 0x801B9F94, 0, 0x801D02C8, 0x801ACC24, 0x801A85D8, 0x801B2CE8, 0x801B2F84, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0x801C7930 };

    public static bool func_800FF494(CpuContext c, IMemory m)
    {
        byte CUR_PRESET = m.ReadU8(0x8000C000);

        if (CUR_PRESET != (byte)PresetId.BountyHunter && CUR_PRESET != (byte)PresetId.Hitman)
            return true;    // Execute original function

        Int32 rnd;
        UInt32 RingOfArcanaCount;
        UInt32 EnemyDefAddress = c.A0;
        UInt32 total_lck = m.ReadU32(0x80097BE4);
        UInt16 rareItemId = m.ReadU16(EnemyDefAddress + 0x1A);
        UInt16 uncommonItemId = m.ReadU16(EnemyDefAddress + 0x1C);
        UInt32 rareItemDropRate = m.ReadU16(EnemyDefAddress + 0x1E);
        UInt32 uncommonItemDropRate = m.ReadU16(EnemyDefAddress + 0x20);
        UInt16 RelicNumber = 0; ;
        UInt16 DropRelic = 0;
        UInt32 EntitySlot = 78;
        UInt32 EntityListBase = 0x800733D8;
        UInt32 RelicUpdatePtr;
        byte StageId = m.ReadU8(0x800974a0);
        UInt16 SpawnX;
        UInt16 SpawnY;
        bool HasRelicDrop = false;

        if (rareItemId >= 0x9D && rareItemId < 0xBB)
            HasRelicDrop = true;
        if (uncommonItemId >= 0x9D && uncommonItemId < 0xBB)
            HasRelicDrop = true;

        if (HasRelicDrop == false)
            return true;

        // Get Arcana Count
        c.A0 = 0u | 0x004Bu;
        c.A1 = 0u | 0x0004u;
        SoTN.CheckEquipmentItemCount(c, m);
        RingOfArcanaCount = c.V0;

        SoTN.rand(c, m);
        rnd = (Int32)(c.V0 & 0xFF);

        SoTN.rand(c, m);
        rnd -= (Int32)((c.V0 & 0x1F) + (total_lck)) / 20;

        if (RingOfArcanaCount != 0)
        {
            rnd -= (Int32)rareItemDropRate * (Int32)RingOfArcanaCount;
        }

        if (rnd < rareItemDropRate || (m.ReadU8(0x80097490) & 1) == 1)
        {
            if (rareItemId >= 0x9D && rareItemId < 0xBB)  // If ItemID for Drop is Oranges to Green Tea
            {
                RelicNumber = rareItemId;                 // Save it and goto Relic Spawning Sequence.
                DropRelic = 1;
            }
            else
            {
                c.V0 = 0x40;            // Regular Rare Drop
                goto L_BHDROP_RETURN;
            }
        }
        else
        {
            // Dropping Uncommon or Common
            if (DropRelic == 0)
            {
                rnd -= (Int32)rareItemDropRate;
                if (RingOfArcanaCount != 0)
                {
                    rnd -= (Int32)uncommonItemDropRate * (Int32)RingOfArcanaCount;
                }
                SoTN.rand(c, m);
                rnd -= (Int32)((c.V0 & 0x1F) + total_lck) / 20;

                if (rnd >= uncommonItemDropRate)
                {
                    SoTN.rand(c, m);
                    rnd = (Int32)c.V0 % 28;
                    if (rareItemDropRate == 0)
                    {
                        rnd++;
                    }
                    if (uncommonItemDropRate == 0)
                    {
                        rnd++;
                    }
                    c.V0 = (UInt32)rnd + RingOfArcanaCount;
                    goto L_BHDROP_RETURN;
                }
                else
                {
                    if (uncommonItemId >= 0x9D && uncommonItemId < 0xBB)  // If ItemID for Drop is Oranges to Green Tea
                    {
                        RelicNumber = uncommonItemId;                     // Save it and goto Relic Spawning Sequence.
                        DropRelic = 1;
                    }
                    else
                    {
                        c.V0 = 0x20;
                        goto L_BHDROP_RETURN;
                    }
                }
            }
        }

        RelicNumber -= 0x9D;    // Rebase

        while (true)    // Find Entity Slot to Use.
        {
            if (m.ReadU16(EntityListBase + (EntitySlot * 0xBC) + 0x26) == 0)
                break;
            EntitySlot++;
            if (EntitySlot > 255)
            {
                c.V0 = 0;
                goto L_BHDROP_RETURN;
            }
        }

        RelicUpdatePtr = 0;
        RelicUpdatePtr = RelicTable[StageId];

        if (RelicUpdatePtr != 0)
        {
            m.WriteU32(EntityListBase + (EntitySlot * 0xBC) + 0x28, RelicUpdatePtr);
            m.WriteU16(EntityListBase + (EntitySlot * 0xBC) + 0x2C, 0);
            m.WriteU16(EntityListBase + (EntitySlot * 0xBC) + 0x26, 0xB);
            m.WriteU16(EntityListBase + (EntitySlot * 0xBC) + 0x30, RelicNumber);
            SpawnX = m.ReadU16(EntityListBase + 0x02);
            SpawnY = m.ReadU16(EntityListBase + 0x06);
            SpawnY -= 24;
            m.WriteU16(EntityListBase + (EntitySlot * 0xBC) + 0x02, SpawnX);
            m.WriteU16(EntityListBase + (EntitySlot * 0xBC) + 0x06, SpawnY);
        }

        c.V0 = 0;

    L_BHDROP_RETURN:
        return false;   // Don't Execute original function

    }   // End of Bounty Hunter Relic Drop

    // Reverse Library Card
    // Detects if Library Card should be changed to reverse Library Card
    // Attach to func_8010E42C
    public static void ReverseLibraryCard_func_8010E42C_Pre(CpuContext c, IMemory m)
    {
        byte CUR_PRESET = m.ReadU8(0x8000C000);

        // Check for the Down Arrow at the end of the Library Card name.
        if (m.ReadU8(0x800DD20C) != 0xE6)
            return;

        UInt16 PlayerInput = (UInt16)(m.ReadU16(0x80097490) & 0x4000);
        UInt32 SavedRichter = m.ReadU32(0x8003CA60);

        // Regular Library Card Behavior
        if (SavedRichter == 0 || PlayerInput == 0)
        {
            m.WriteU8(0x8000C001, 0);
            m.WriteU16(0x800A3C98, 0x7C0E);
            return;
        }

        // Reverse Library Card
        m.WriteU8(0x8000C001, 1);
        m.WriteU16(0x800A3C98, 0x88BE);
    }

    // Reverse Library Card
    // func_800F16D0 post
    public static void ReverseLibraryCard_func_800F16D0_Post(CpuContext c, IMemory m)
    {
        if (m.ReadU8(0x80097C98) == 0x06 && m.ReadU8(0x8000C001) == 1)   // RLBC Mode
        {
            if (c.V0 == 0x02)
            {
                c.V0 = 0x22;
            }

        }
        if (m.ReadU8(0x80097C98) == 0x05)   // Teleporting to Keep
        {
            m.WriteU16(0x800A3C98, 0x7C0E);
        }
    }

    // Reverse Library Card
    // Attach to func_800F223C
    public static void ReverseLibraryCard_func_800F223C_Pre(CpuContext c, IMemory m)
    {
        byte StageId = m.ReadU8(0x800974a0);

        if (StageId == 0x22 && m.ReadU8(0x8000C001) == 1)
        {
            m.WriteU8(0x800974a0, 0x02);
        }
    }

    // Detect Death Cutscene Removal Patch from sotn.io Rando.
    public static void NO3_EntityCutscene_Pre(CpuContext c, IMemory m)
    {
        if (m.ReadU32(0x801BEFB0) != 0x14400006)
            m.WriteU8(0x8003BE21, 1);
    }

}
