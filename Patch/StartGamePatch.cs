using AmongUsMoreRolesMod.Util;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace AmongUsMoreRolesMod.Patch
{
    [HarmonyPatch(typeof(ShipStatus), nameof(ShipStatus.Start))]
    public static class StartGamePatch
    {
        static void Postfix(ShipStatus __instance)
        {
            //RoleInfo.SetSheriffCooldown(10);
            RoleInfo.ResetSheriffCD();

            List<PlayerControl> players = new List<PlayerControl>();
            foreach (PlayerControl player in PlayerControl.AllPlayerControls)
            {
                players.Add(player);
            }
            Utilities.PlayerControls = players;
        }
    }
}
