using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using static GameData;

namespace AmongUsMoreRolesMod
{
    static class RoleInfo
    {
        public static byte JesterId = 255;
        public static byte MechanicId = 255;
        public static byte SnitchId = 255;
        public static byte SheriffId = 255;
        public static byte WitnessId = 255;

        public static Color CrewmateColor = new Color(2f / 255, 136f / 255, 249f / 255);
        public static Color ImpostorColor = new Color(246f / 255, 6f / 255, 7f / 255);
        public static Color JesterColor = new Color(1, 0, 1);
        public static Color MechanicColor = new Color(252f / 255, 122f / 255, 0);
        public static Color SnitchColor = new Color(252f / 255, 207f / 255, 3f / 255);
        public static Color SheriffColor = new Color(1, 1, 0);
        public static Color WitnessColor = new Color(0, 1, 0);

        public const int SNITCH_CHILD_ID = 100;
        public const int IMPOSTOR_CHILD_ID = 200;

        public const string HighlightHex = "[#FF00FF]";

        // TEST: Should be private
        public static DateTime lastSheriffKillTime;

        public static bool IsRole(byte playerId, Roles role)
        {
            return role switch
            {
                Roles.Jester => playerId == JesterId,
                Roles.Snitch => playerId == SnitchId,
                Roles.Mechanic => playerId == MechanicId,
                Roles.Sheriff => playerId == SheriffId,
                Roles.Witness => playerId == WitnessId,
                _ => false
            };
        }
        public static bool IsRole(PlayerControl player, Roles role)
        {
            return IsRole(player.PlayerId, role);
        }
        public static bool IsRole(PlayerInfo player, Roles role)
        {
            return IsRole(player.PlayerId, role);
        }

        public static int GetSheriffCDRemaining()
        {
            if (lastSheriffKillTime == null) return 0;
            TimeSpan diff = DateTime.Now - lastSheriffKillTime;
            return (int) Math.Max(0, MoreRolesPlugin.SheriffKillCDOption.GetValue() * 1000 - diff.TotalMilliseconds);
        }
        public static void ResetSheriffCD()
        {
            lastSheriffKillTime = DateTime.Now;
        }
        public static void ResetSheriffCD(int extraSeconds)
        {
            lastSheriffKillTime = DateTime.Now.AddSeconds(extraSeconds);
        }

        public static List<PlayerControl> GetControlsForRole(Roles role)
        {
            return GetControlsForRole(role, PlayerControl.AllPlayerControls.ToArray().ToList());
        }
        public static PlayerControl GetControlForRole(Roles role)
        {
            return GetControlsForRole(role).FirstOrDefault();
        }

        public static List<PlayerControl> GetControlsForRole(Roles role, List<PlayerControl> controls)
        {
            return controls.Where(x => IsRole(x, role)).ToList();
        }
        public static PlayerControl GetControlForRole(Roles role, List<PlayerControl> controls)
        {
            return GetControlsForRole(role, controls).FirstOrDefault();
        }
    }

    public enum Roles 
    {
        Jester,
        Mechanic,
        Snitch,
        Sheriff,
        Witness
    }
}
