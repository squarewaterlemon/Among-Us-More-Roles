using AmongUsMoreRolesMod.Util;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace AmongUsMoreRolesMod
{
    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]

    static class HudPatch
    {
        public static void Postfix(HudManager __instance)
        {
            if (PlayerControl.LocalPlayer != null)
            {
                if (RoleInfo.IsRole(PlayerControl.LocalPlayer, Roles.Jester))
                {
                    PlayerControl.LocalPlayer.nameText.Color = RoleInfo.JesterColor;
                }
                else if (RoleInfo.IsRole(PlayerControl.LocalPlayer, Roles.Snitch))
                {
                    PlayerControl.LocalPlayer.nameText.Color = RoleInfo.SnitchColor;
                }
                else if (RoleInfo.IsRole(PlayerControl.LocalPlayer, Roles.Mechanic))
                {
                    PlayerControl.LocalPlayer.nameText.Color = RoleInfo.MechanicColor;
                }
                else if (RoleInfo.IsRole(PlayerControl.LocalPlayer, Roles.Witness))
                {
                    PlayerControl.LocalPlayer.nameText.Color = RoleInfo.WitnessColor;
                }
                else if (RoleInfo.IsRole(PlayerControl.LocalPlayer, Roles.Sheriff))
                {
                    PlayerControl.LocalPlayer.nameText.Color = RoleInfo.SheriffColor;

                    if (!PlayerControl.LocalPlayer.Data.IsDead && PlayerControl.LocalPlayer.CanMove)
                    {
                        // Enable sheriff kill button
                        __instance.KillButton.gameObject.SetActive(true);
                        __instance.KillButton.isActive = true;
                        __instance.KillButton.SetCoolDown(RoleInfo.GetSheriffCDRemaining() / 1000,
                            MoreRolesPlugin.SheriffKillCDOption.GetValue());

                        // Highlight the button if a player is near enough to kill
                        double distance = Utilities.DistanceToClosestPlayer(PlayerControl.LocalPlayer);
                        if (distance <= GameOptionsData.KillDistances[PlayerControl.GameOptions.KillDistance])
                        {
                            __instance.KillButton.SetTarget(Utilities.GetClosestPlayer(PlayerControl.LocalPlayer));
                        }
                        if (Input.GetKeyDown(KeyCode.Q))
                        {
                            __instance.KillButton.PerformKill();
                        }
                    }
                    else
                    {
                        // Disable kill button so it cannot be used, since the player can't move (in task, meeting, etc.)
                        __instance.KillButton.isActive = false;
                        // Hide kill button completely if player is dead
                        if (PlayerControl.LocalPlayer.Data.IsDead)
                        {
                            __instance.KillButton.gameObject.SetActive(false);
                        }
                    }
                }
            }
        }
    }

    [HarmonyPatch(typeof(MeetingHud), nameof(MeetingHud.Update))]
    static class MeetingHudPatch
    {
        public static void Postfix(MeetingHud __instance)
        {
            foreach (var pstate in __instance.playerStates)
            {
                if (pstate.TargetPlayerId == PlayerControl.LocalPlayer.PlayerId)
                {
                    if (RoleInfo.IsRole(PlayerControl.LocalPlayer, Roles.Jester))
                    {
                        pstate.NameText.Color = RoleInfo.JesterColor;
                    }
                    else if (RoleInfo.IsRole(PlayerControl.LocalPlayer, Roles.Snitch))
                    {
                        pstate.NameText.Color = RoleInfo.SnitchColor;
                    }
                    else if (RoleInfo.IsRole(PlayerControl.LocalPlayer, Roles.Mechanic))
                    {
                        pstate.NameText.Color = RoleInfo.MechanicColor;
                    }
                    else if (RoleInfo.IsRole(PlayerControl.LocalPlayer, Roles.Witness))
                    {
                        pstate.NameText.Color = RoleInfo.WitnessColor;
                    }
                    else if (RoleInfo.IsRole(PlayerControl.LocalPlayer, Roles.Sheriff))
                    {
                        pstate.NameText.Color = RoleInfo.SheriffColor;
                    }
                }
            }
        }
    }

    [HarmonyPatch(typeof(MeetingHud), nameof(MeetingHud.Close))]
    static class MeetingClosePatch
    {
        static void Postfix(MeetingHud __instance)
        {
            RoleInfo.ResetSheriffCD(10);
        }
    }
}
