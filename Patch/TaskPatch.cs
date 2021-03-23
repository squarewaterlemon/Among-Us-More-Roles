using AmongUsMoreRolesMod.Util;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UIElements;

namespace AmongUsMoreRolesMod
{
    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.SetTasks))]
    public static class GiveTaskPatch
    {
        public static void Postfix(PlayerControl __instance)
        {
            if (RoleInfo.IsRole(__instance.PlayerId, Roles.Jester))
            {
                ImportantTextTask importantTask = new GameObject("Jester Tasks").AddComponent<ImportantTextTask>();
                importantTask.transform.SetParent(__instance.transform, false);
                importantTask.Text = RoleInfo.HighlightHex + "Get voted out.[]";
                __instance.myTasks.Insert(0, importantTask);
            }
        }
    }

    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.CompleteTask))]
    public static class CompleteTaskPatch
    {
        public static void Postfix(PlayerControl __instance, [HarmonyArgument(0)] uint index)
        {
            if (RoleInfo.IsRole(__instance, Roles.Snitch))
            {
                int tasksLeft = __instance.myTasks.ToArray().ToList()
                    .Where(x => !x.IsComplete).Count();
                if (tasksLeft == 1 && PlayerControl.LocalPlayer.Data.IsImpostor)
                {
                    // Add an arrow to the snitch for the impostor(s)
                    VisualArrow.Create(__instance.transform.position, RoleInfo.IMPOSTOR_CHILD_ID);
                    // Put in task list that the snitch can see them soon
                    ImportantTextTask importantTask = new GameObject("Impostor Snitch Notif").AddComponent<ImportantTextTask>();
                    importantTask.transform.SetParent(PlayerControl.LocalPlayer.transform, false);
                    importantTask.Text = RoleInfo.HighlightHex + __instance.name + " is the Snitch![]";
                    PlayerControl.LocalPlayer.myTasks.Insert(0, importantTask);
                }
                else if (tasksLeft == 0 && RoleInfo.IsRole(PlayerControl.LocalPlayer, Roles.Snitch))
                {
                    //System.Console.WriteLine("Adding arrow to impostor!");
                    // Add arrows to the impostors
                    foreach (PlayerControl player in PlayerControl.AllPlayerControls)
                    {
                        if (player.Data.IsImpostor && !player.Data.IsDead)
                        {
                            VisualArrow.Create(player.transform.position, RoleInfo.SNITCH_CHILD_ID);
                        }
                    }
                    // Put in the task list that they can now see the impostors
                    ImportantTextTask importantTask = new GameObject("Snitch Notif").AddComponent<ImportantTextTask>();
                    importantTask.transform.SetParent(__instance.transform, false);
                    importantTask.Text = RoleInfo.HighlightHex + "Your arrows point to the Impostors![]";
                    __instance.myTasks.Insert(0, importantTask);
                }
            }
        }
    }
}
