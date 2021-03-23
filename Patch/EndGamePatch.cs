using AmongUsMoreRolesMod.Util;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace AmongUsMoreRolesMod.Patch
{
    [HarmonyPatch(typeof(EndGameManager), nameof(EndGameManager.Start))]
    public static class EndGamePatch
    {
        public static string WinnerText;
        public static Color WinnerColor;
        public static Color BgColor;
        public static StingerTypes StingerType;
        public static bool IsSpecialCase;
        public static WinnerTypes WinnerType;

        public static void Prefix(EndGameManager __instance)
        {
            switch (TempData.EndReason)
            {
                case GameOverReason.HumansByTask:
                case GameOverReason.HumansByVote:
                    WinnerType = WinnerTypes.Crewmates;
                    SetValues("", RoleInfo.CrewmateColor, RoleInfo.CrewmateColor, StingerTypes.Crew);
                    if (PlayerControl.LocalPlayer.Data.IsImpostor || RoleInfo.IsRole(PlayerControl.LocalPlayer, Roles.Jester))
                    {
                        WinnerText = "Defeat";
                    }
                    else
                    {
                        WinnerText = "Victory";
                    }
                    break;
                case GameOverReason.ImpostorByKill:
                case GameOverReason.ImpostorBySabotage:
                case GameOverReason.ImpostorByVote:
                    WinnerType = WinnerTypes.Impostors;
                    SetValues("", RoleInfo.ImpostorColor, RoleInfo.ImpostorColor, StingerTypes.Impostor);
                    if (PlayerControl.LocalPlayer.Data.IsImpostor)
                    {
                        WinnerText = "Victory";
                    }
                    else
                    {
                        WinnerText = "Defeat";
                    }
                    break;
            }
            SetWinners();
            OverrideScreen(__instance, false);
        }
        public static void Postfix(EndGameManager __instance)
        {
            OverrideScreen(__instance, true);

            RoleInfo.JesterId = RoleInfo.WitnessId = RoleInfo.SnitchId = RoleInfo.MechanicId =
                RoleInfo.SheriffId = 255;
            VisualArrow.ClearArrows();
        }

        private static void OverrideScreen(EndGameManager __instance, bool closeAtEnd)
        {
            if (IsSpecialCase)
            {
                switch (StingerType)
                {
                    case StingerTypes.Crew:
                        __instance.DisconnectStinger = __instance.CrewStinger;
                        break;
                    case StingerTypes.Impostor:
                        __instance.DisconnectStinger = __instance.ImpostorStinger;
                        break;
                }

                __instance.WinText.Text = WinnerText;
                __instance.WinText.Color = WinnerColor;
                __instance.BackgroundBar.material.color = BgColor;

                if (closeAtEnd)
                {
                    IsSpecialCase = false;
                }
            }
        }
        private static void SetWinners()
        {
            Il2CppSystem.Collections.Generic.List<WinningPlayerData> winners = new
                Il2CppSystem.Collections.Generic.List<WinningPlayerData>();
            List<PlayerControl> allPlayers = Utilities.PlayerControls;

            switch (WinnerType)
            {
                case WinnerTypes.Crewmates:
                    for (int i = 0; i < allPlayers.Count; i++)
                    {
                        if (!RoleInfo.IsRole(allPlayers[i], Roles.Jester) && !allPlayers[i].Data.IsImpostor)
                        {
                            winners.Add(new WinningPlayerData(allPlayers[i].Data));
                        }
                    }
                    break;
                case WinnerTypes.Impostors:
                    for (int i = 0; i < allPlayers.Count; i++)
                    {
                        if (allPlayers[i].Data.IsImpostor)
                        {
                            winners.Add(new WinningPlayerData(allPlayers[i].Data));
                        }
                    }
                    break;
                case WinnerTypes.Jester:
                    System.Console.WriteLine("0");
                    winners.Add(new WinningPlayerData(RoleInfo.GetControlForRole(Roles.Jester, allPlayers).Data));
                    System.Console.WriteLine("1");
                    break;
            }
            TempData.winners = winners;
        }

        public static void SetValues(string winText, Color bgColor, Color winnerColor, StingerTypes stinger)
        {
            WinnerText = winText;
            BgColor = bgColor;
            WinnerColor = winnerColor;
            StingerType = stinger;
            IsSpecialCase = true;
        }
    }
}
