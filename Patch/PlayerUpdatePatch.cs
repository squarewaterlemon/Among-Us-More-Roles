using AmongUsMoreRolesMod.Util;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AmongUsMoreRolesMod
{
    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.FixedUpdate))]
    public static class PlayerUpdatePatch
    {
        public static void Postfix(PlayerControl __instance)
        {
            if (__instance != null)
            {
                if (RoleInfo.IsRole(__instance, Roles.Snitch))
                {
                    // Update where the snitch's arrows are pointing
                    List<VisualArrow> currentArrows = VisualArrow.GetArrows(RoleInfo.SNITCH_CHILD_ID);
                    if (currentArrows.Count > 0)
                    {
                        int index = 0;
                        foreach (PlayerControl player in PlayerControl.AllPlayerControls)
                        {
                            if (player.Data.IsImpostor && !player.Data.IsDead)
                            {
                                currentArrows[index++].ChangeTarget(player.transform.position);
                            }
                        }
                    }
                }
                if (__instance.Data.IsImpostor)
                {
                    // Update where the impostor's arrow is pointing
                    List<VisualArrow> currentArrows = VisualArrow.GetArrows(RoleInfo.IMPOSTOR_CHILD_ID);
                    PlayerControl snitch = RoleInfo.GetControlForRole(Roles.Snitch);
                    if (currentArrows.Count > 0 && snitch != null && !snitch.Data.IsDead)
                    {
                        currentArrows.First().ChangeTarget(snitch.transform.position);
                    }
                }
            }
            
        }
    }
}
