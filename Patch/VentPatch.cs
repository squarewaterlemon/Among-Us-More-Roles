using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace AmongUsMoreRolesMod
{
    [HarmonyPatch(typeof(Vent), nameof(Vent.CanUse))]
    public static class VentPatch
    {
        public static bool Prefix(Vent __instance, ref float __result, [HarmonyArgument(0)] GameData.PlayerInfo pc,
            [HarmonyArgument(1)] out bool canUse, [HarmonyArgument(2)] out bool couldUse)
        {
            couldUse = !pc.IsDead && (pc.IsImpostor || RoleInfo.IsRole(pc, Roles.Mechanic));
            __result = Vector2.Distance(PlayerControl.LocalPlayer.GetTruePosition(), __instance.transform.position);
            canUse = couldUse && __result < __instance.UsableDistance;
            return false;
        }
    }
}
