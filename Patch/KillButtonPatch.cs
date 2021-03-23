using AmongUsMoreRolesMod.Util;
using HarmonyLib;
using Reactor;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace AmongUsMoreRolesMod.Patch
{
    [HarmonyPatch(typeof(KillButtonManager), nameof(KillButtonManager.PerformKill))]
    public static class PerformKillPatch
    {
        public static bool Prefix()
        {
            if (RoleInfo.IsRole(PlayerControl.LocalPlayer, Roles.Sheriff) && RoleInfo.GetSheriffCDRemaining() <= 0)
            {
                double distance = Utilities.DistanceToClosestPlayer(PlayerControl.LocalPlayer);
                if (distance <= GameOptionsData.KillDistances[PlayerControl.GameOptions.KillDistance])
                {
                    // Time to execute the kill
                    PlayerControl victim = Utilities.GetClosestPlayer(PlayerControl.LocalPlayer);

                    PlayerControl.LocalPlayer.MurderPlayer(victim);
                    if (!victim.Data.IsImpostor)
                    {
                        // The sheriff must die for killing a crewmate
                        PlayerControl.LocalPlayer.MurderPlayer(PlayerControl.LocalPlayer);
                    }
                    RoleInfo.ResetSheriffCD();
                    return false;
                }
            }
            return true;
        }
    }

    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.MurderPlayer))]
    public static class MurderPlayerPatch
    {
        public static void Prefix(PlayerControl __instance, [HarmonyArgument(0)] PlayerControl victim)
        {
            if (RoleInfo.IsRole(__instance, Roles.Sheriff))
            {
                __instance.Data.IsImpostor = true;
                if (PlayerControl.LocalPlayer.PlayerId == __instance.PlayerId)
                {
                    Rpc<SheriffKillRpc>.Instance.Send(new SheriffKillRpc.Data(__instance.PlayerId, victim.PlayerId));
                }
            }
        }
        public static void Postfix(PlayerControl __instance)
        {
            if (RoleInfo.IsRole(__instance, Roles.Sheriff))
            {
                __instance.Data.IsImpostor = false;
            }
        }
    }
}
