using RecompOne.Runtime.Context;
using RecompOne.Runtime.Dispatch;
using RecompOne.Runtime.Memory;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using RecompOne.Runtime.Hle;

namespace Recompiled;

public static partial class FunctionFixes
{
    // Scylla Door Fix
    public static void ScyllaDoorFix(CpuContext c, IMemory m)
    {
        if (QualityOfLife.BugFixes == true)
        {
            byte doorByte = m.ReadU8(0x80180c66);
            UInt32 scyllaDefeat = m.ReadU32(0x8003CA3C);

            if (doorByte == 1 && scyllaDefeat > 0)
            {
                m.WriteU8(0x80180c66, 0x00);
            }
        }
    }
    // Olrox Explosions!!! Fix
    public static void OlroxExploFix(CpuContext c, IMemory m)
    {
        if (QualityOfLife.BugFixes == true)
        {
            UInt16 exploDuration = m.ReadU16(0x80077c64);
            UInt32 olroxDefeat = m.ReadU32(0x8003CA2C);

            if (exploDuration > 0x7000 && olroxDefeat > 0)
            {
                m.WriteU16(0x80077c64, 0x60);
            }
        }
    }
    // Clock Tower Softlock Fix
    public static void ClockCollisionFix(CpuContext c, IMemory m)
    {
        if (QualityOfLife.BugFixes == true)
        {
            m.WriteU16(0x80182476, 0x80);
        }
    }
    public static void ScreenScrollFix(CpuContext c, IMemory m)
    {
        if (QualityOfLife.BugFixes == true)
        {
            // Force screen scroll entity in Marble Gallery near Ctulhu to spawn
            if (m.ReadU8(0x800974a0) == 0x00)
            {
                m.WriteU8(0x80182f9f, 0xa0);
                m.WriteU8(0x80183e51, 0xa0);
            }
        }
    }
}

