using HarmonyLib;
using Hazel;
using Reactor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnhollowerBaseLib;

namespace AmongUsMoreRolesMod
{
    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.RpcSetInfected))]
    class SetInfectedPatch
    {
        public static void Postfix([HarmonyArgument(0)] Il2CppReferenceArray<GameData.PlayerInfo> infected)
        {
            if (AmongUsClient.Instance.AmHost)
            {
                List<PlayerControl> crew = PlayerControl.AllPlayerControls.ToArray().ToList();
                crew.RemoveAll(x => x.Data.IsImpostor);
                // Attempt to add a jester
                if (MoreRolesPlugin.JesterToggleOption.GetValue() && crew.Count > 0)
                {
                    Random random = new Random();
                    int jesterIndex = random.Next(0, crew.Count);
                    RoleInfo.JesterId = crew[jesterIndex].PlayerId;

                    Rpc<SetJesterRpc>.Instance.Send(new SetJesterRpc.Data(RoleInfo.JesterId));
                    crew.RemoveAt(jesterIndex);
                }
                // Attempt to add a snitch
                if (MoreRolesPlugin.SnitchToggleOption.GetValue() && crew.Count > 0)
                {
                    Random random = new Random();
                    int snitchIndex = random.Next(0, crew.Count);
                    RoleInfo.SnitchId = crew[snitchIndex].PlayerId;

                    Rpc<SetSnitchRpc>.Instance.Send(new SetSnitchRpc.Data(RoleInfo.SnitchId));
                    crew.RemoveAt(snitchIndex);
                }
                // Attempt to add a mechanic
                if (MoreRolesPlugin.MechanicToggleOption.GetValue() && crew.Count > 0)
                {
                    Random random = new Random();
                    int mechIndex = random.Next(0, crew.Count);
                    RoleInfo.MechanicId = crew[mechIndex].PlayerId;

                    Rpc<SetMechanicRpc>.Instance.Send(new SetMechanicRpc.Data(RoleInfo.MechanicId));
                    crew.RemoveAt(mechIndex);
                }
                // Attempt to add a witness
                if (MoreRolesPlugin.WitnessToggleOption.GetValue() && crew.Count > 0)
                {
                    Random random = new Random();
                    int witnessIndex = random.Next(0, crew.Count);
                    RoleInfo.WitnessId = crew[witnessIndex].PlayerId;

                    Rpc<SetWitnessRpc>.Instance.Send(new SetWitnessRpc.Data(RoleInfo.WitnessId));
                    crew.RemoveAt(witnessIndex);
                }
                // Attempt to add a sheriff
                if (MoreRolesPlugin.SheriffToggleOption.GetValue() && crew.Count > 0)
                {
                    Random random = new Random();
                    int sheriffIndex = random.Next(0, crew.Count);
                    RoleInfo.SheriffId = crew[sheriffIndex].PlayerId;

                    Rpc<SetSheriffRpc>.Instance.Send(new SetSheriffRpc.Data(RoleInfo.SheriffId));
                    crew.RemoveAt(sheriffIndex);
                }
            }
        }
    }
}
