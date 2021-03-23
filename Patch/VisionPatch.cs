using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace AmongUsMoreRolesMod.Patch
{
    [HarmonyPatch(typeof(ShipStatus), nameof(ShipStatus.CalculateLightRadius))]
    static class VisionPatch
    {
        static void Postfix(ShipStatus __instance, [HarmonyArgument(0)] GameData.PlayerInfo playerInfo,
            ref float __result)
        {
            if (RoleInfo.IsRole(playerInfo, Roles.Witness))
            {
                __result = MoreRolesPlugin.WitnessVisionOption.GetValue() * __instance.MaxLightRadius;
            }
        }
    }
}
