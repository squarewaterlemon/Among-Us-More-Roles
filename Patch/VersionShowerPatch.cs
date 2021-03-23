using AmongUsMoreRolesMod.Util;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace AmongUsMoreRolesMod.Patch
{
    [HarmonyPatch(typeof(VersionShower), nameof(VersionShower.Start))]
    static class VersionShowerPatch
    {
        static void Postfix(VersionShower __instance)
        {
            __instance.text.Text += $"\n\n\n\n\n\n\n\n\n\n\n\n\nLoaded More Roles Mod (v{Utilities.Version}) by squarewaterlemon";
        }
    }
}
