using System;
using System.Collections.Generic;
using System.Text;
using RecompOne.Runtime.Context;
using RecompOne.Runtime.Memory;
using RecompOne.Runtime.Hle;

namespace Recompiled;

internal class QualityOfLife
{
    public static bool BugFixes;
    public static bool ClearFile;
    public static bool AntiFreeze;
    public static bool InfiniteWingSmash;
    public static bool EasyMode;

    public static void Apply(CpuContext c, IMemory m)
    {
        // Bug Fixes application
        if (QualityOfLife.BugFixes == true)
        {
            // Force screen scroll entity in Marble Gallery near Ctulhu to spawn
            if (m.ReadU8(0x800974a0) == 0x00)
            {
                m.WriteU8(0x80182f9f, 0xa0);
                m.WriteU8(0x80183e51, 0xa0);
            }
        }

        // clear File application
        if (QualityOfLife.ClearFile == true)
        {
            m.WriteU8(0x8003bde0, 0x02);
        }

        // Anti-Freeze application
        //Console.WriteLine("this runs");
        if (QualityOfLife.AntiFreeze == true)
        {
            //Console.WriteLine("this was true");
            if (m.ReadU8(0x80097420) == 0x03)
            {
                //Console.WriteLine("this was three");
                m.WriteU8(0x80097420, 0x00);
            }
        }

        // Infinite Wing Smash application
        if (QualityOfLife.InfiniteWingSmash == true)
        {
            m.WriteU8(0x80137ffc, 0x00);
        }
    }

    public static void EasySpellInput(CpuContext c, IMemory m)
    {
        // Easy Mode application
        // Spells
        if (QualityOfLife.EasyMode == true)
        {
            // ↑ + L2 makes Soul Steal go
            if (m.ReadU16(0x80097490) == 0x1001)
            {
                m.WriteU16(0x80138fd8, 0x07); // Soul Steal step 7
                m.WriteU16(0x80138fda, 0x10); // Soul Steal Timer = 10 fr
                m.WriteU16(0x80097494, 0x80); // Button Tapped = Sq
            }
        }
        {
            // ↓↓ + L2 makes Tetra Spirit go
            if (m.ReadU16(0x80097490) == 0x4001)
            {
                m.WriteU16(0x80138fd0, 0x07); // Soul Steal step 7
                m.WriteU16(0x80138fd2, 0x10); // Soul Steal Timer = 10 fr
                m.WriteU16(0x80097494, 0x80); // Button Tapped = Sq
            }
        }
        {
            // → | ← + L2 makes Hellfire go
            if (m.ReadU16(0x80097490) == 0x2001 || m.ReadU16(0x80097490) == 0x8001)
            {
                m.WriteU16(0x80138fcc, 0x04); // Soul Steal step 7
                m.WriteU16(0x80138fce, 0x10); // Soul Steal Timer = 10 fr
                m.WriteU16(0x80097494, 0x80); // Button Tapped = Sq
            }
        }
    }
    public static void EasyWingInput(CpuContext c, IMemory m)
    {
        if (QualityOfLife.EasyMode == true)
        {
            // L2 makes Bat go
            if ((UInt16)(m.ReadU16(0x80097494) & 0x0001) == 0x0001) // mask check for L2
            {
                m.WriteU16(0x80137ff4, 0x07); // Smash step 7
                m.WriteU16(0x80137ff8, 0x10); // Smash Timer = 10 fr
            }
        }
    }
    public static void EasyGravInput(CpuContext c, IMemory m)
    {
        bool EnactJump=false;
        if (QualityOfLife.EasyMode == true)
        {
            // L2 makes Boots go
            if ((UInt16)(m.ReadU16(0x80097494) & 0x0001) == 0x0001)
            {
                if (m.ReadU16(0x80073404) < 3)
                {
                    if ((UInt16)(m.ReadU16(0x80097490) & 0xc000) == 0xc000 || (UInt16)(m.ReadU16(0x80097490) & 0x6000) == 0x6000 || (UInt16)(m.ReadU16(0x80097490) & 0xf000) == 0x0000)
                    {
                        EnactJump = true;
                    }
                }
            }
            if ((UInt16)(m.ReadU16(0x80097494) & 0x0001) == 0x0001)
            {
                if (m.ReadU16(0x80073404) == 4 && (UInt16)(m.ReadU16(0x80072f64) & 0x0001) == 1)
                {
                    EnactJump = true;
                }
            }
            if (EnactJump == true)
            {
                c.A0 = 1;
                SoTN.HandleGravityBootsMP(c, m);
                if (c.V0 == 0)
                {
                    SoTN.DoGravityJump(c, m);
                }
            }
        }
    }
    public static bool EasyIFrames(CpuContext c, IMemory m)
    {
        if (c.A0 == 0x00 || QualityOfLife.EasyMode == false)
        {
            return true;
        }
        c.A1 += 0x04;
        return true;
    }
}
