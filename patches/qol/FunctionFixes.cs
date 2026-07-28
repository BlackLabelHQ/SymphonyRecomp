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
    // Scylla Softlock Door Fix
    // If the player defeats Scylla and enters the room behind before the water level reaches the top the door below will remain locked.
    // This hooks onto function 801A1BE8 in bo3 and checks if Scylla has been defeated and the door is still locked. It unlocks it if needed.
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
    // Olrox Extended Death Explosion Fix
    // If the player kills Olrox at a specific time when he attacks with his hands the explosion sequence will continue for 18 minutes due to
    // a timer underflow. This function hooks an entity function to see if Olrox is defeated and the timer has underflowed and corrects it.
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
    // Under certain conditions when leaving the room unlocked by hitting the four gears you can trigger a "Reverse Shiftline" and if you
    // leave the room using the bottom right exit you can become stuck in the floor. This is fixed by moving the entity so the Reverse Shiftline
    // condition does not occur. 
    public static void ClockCollisionFix(CpuContext c, IMemory m)
    {
        if (QualityOfLife.BugFixes == true)
        {
            m.WriteU16(0x80182476, 0x80);
        }
    }

    // Marble Gallery Large Room Scroll Bug Fix
    // In the large room that snakes back and forth if you kill Ctulhu in a specific way moving to the right you can cause an entity that changes
    // screen scrolling parameters to not spawn leaving you unable to go up. This is fixed by setting the spawn priority for this entity.
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

