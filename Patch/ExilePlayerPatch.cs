using AmongUsMoreRolesMod.Patch;
using AmongUsMoreRolesMod.Util;
using HarmonyLib;
using Reactor;
using Reactor.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace AmongUsMoreRolesMod
{
    [HarmonyPatch(typeof(ExileController), nameof(ExileController.Begin))]
    public static class ExilePlayerBeginPatch
    {
        public static void Postfix(ExileController __instance, [HarmonyArgument(0)] GameData.PlayerInfo target)
        {
            if (target != null && RoleInfo.IsRole(target, Roles.Jester))
            {
                if (PlayerControl.GameOptions.ConfirmImpostor)
                {
                    __instance.completeString = $"{target.PlayerName} was the Jester.";
                }
            }
        }
    }

    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.Exiled))]
    public static class ExilePlayerExiledPatch
    {
        public static void Prefix(PlayerControl __instance)
        {
            if (RoleInfo.IsRole(__instance, Roles.Jester))
            {
                OnJesterEject();
                if (AmongUsClient.Instance.AmHost)
                {
                    ShipStatus.Instance.enabled = true;
                    ShipStatus.RpcEndGame(GameOverReason.HumansDisconnect, true);
                }
            }
        }

        public static void OnJesterEject()
        {
            if (RoleInfo.IsRole(PlayerControl.LocalPlayer, Roles.Jester))
            {
                EndGamePatch.SetValues("Victory", RoleInfo.JesterColor, RoleInfo.JesterColor, StingerTypes.Impostor);
            }
            else
            {
                EndGamePatch.SetValues("Defeat", RoleInfo.JesterColor, RoleInfo.JesterColor, StingerTypes.Impostor);
            }
            EndGamePatch.WinnerType = WinnerTypes.Jester;
        }
    }

    public enum StingerTypes
    {
        Crew,
        Impostor
    }

    public enum WinnerTypes
    {
        Crewmates,
        Impostors,
        Jester
    }
}
