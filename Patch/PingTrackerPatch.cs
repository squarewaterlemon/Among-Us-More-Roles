using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace AmongUsMoreRolesMod.Patch
{
    [HarmonyPatch(typeof(PingTracker), nameof(PingTracker.Update))]
    static class PingTrackerPatch
    {
        static void Postfix(PingTracker __instance)
        {
            __instance.text.Text += "\n\n\n\n\nMore Roles - squarewaterlemon\nReactor Essentials - DorCoMaNdO";
        }
    }
}
