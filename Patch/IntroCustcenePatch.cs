using HarmonyLib;
using UnityEngine;

namespace AmongUsMoreRolesMod
{

    [HarmonyPatch(typeof(IntroCutscene.CoBegin__d), nameof(IntroCutscene.CoBegin__d.MoveNext))]
    public static class IntroCustcenePatch
    {
        public static void Prefix(IntroCutscene.CoBegin__d __instance)
        {
            RunPatch(__instance);
        }
        public static void Postfix(IntroCutscene.CoBegin__d __instance)
        {
            RunPatch(__instance);
        }

        private static void RunPatch(IntroCutscene.CoBegin__d __instance)
        {
            if (RoleInfo.IsRole(PlayerControl.LocalPlayer, Roles.Jester))
            {
                __instance.__this.Title.Text = "Jester";
                __instance.__this.Title.Color = RoleInfo.JesterColor;
                __instance.__this.ImpostorText.Text = "Get voted out to win";
                __instance.__this.BackgroundBar.material.color = RoleInfo.JesterColor;
                // Team is only the jester
                __instance.yourTeam = new Il2CppSystem.Collections.Generic.List<PlayerControl>();
                __instance.yourTeam.Add(PlayerControl.LocalPlayer);
            }
            else if (RoleInfo.IsRole(PlayerControl.LocalPlayer, Roles.Mechanic))
            {
                __instance.__this.Title.Text = "Mechanic";
                __instance.__this.Title.Color = RoleInfo.MechanicColor;
                __instance.__this.ImpostorText.Text = "You are a crewmate that can vent";
                __instance.__this.BackgroundBar.material.color = RoleInfo.MechanicColor;
            }
            else if (RoleInfo.IsRole(PlayerControl.LocalPlayer, Roles.Snitch))
            {
                __instance.__this.Title.Text = "Snitch";
                __instance.__this.Title.Color = RoleInfo.SnitchColor;
                __instance.__this.ImpostorText.Text = "Finish your tasks to reveal the Impostors";
                __instance.__this.BackgroundBar.material.color = RoleInfo.SnitchColor;
            }
            else if (RoleInfo.IsRole(PlayerControl.LocalPlayer, Roles.Witness))
            {
                __instance.__this.Title.Text = "Witness";
                __instance.__this.Title.Color = RoleInfo.WitnessColor;
                __instance.__this.ImpostorText.Text = "Use your improved vision to gather information";
                __instance.__this.BackgroundBar.material.color = RoleInfo.WitnessColor;
            }
            else if (RoleInfo.IsRole(PlayerControl.LocalPlayer, Roles.Sheriff))
            {
                __instance.__this.Title.Text = "Sheriff";
                __instance.__this.Title.Color = RoleInfo.SheriffColor;
                __instance.__this.ImpostorText.Text = "Find and kill the Impostors";
                __instance.__this.BackgroundBar.material.color = RoleInfo.SheriffColor;
            }
        }
    }
}
