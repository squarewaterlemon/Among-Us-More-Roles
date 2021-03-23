using AmongUsMoreRolesMod.Util;
using Hazel;
using Reactor;
using System;
using System.Collections.Generic;
using System.Text;

namespace AmongUsMoreRolesMod
{
    #region Custom Role RPCs
    [RegisterCustomRpc]
    public class SetJesterRpc : PlayerCustomRpc<MoreRolesPlugin, SetJesterRpc.Data>
    {
        public SetJesterRpc(MoreRolesPlugin plugin) : base(plugin)
        {

        }

        public readonly struct Data
        {
            public readonly byte JesterId;

            public Data(byte jesterId)
            {
                JesterId = jesterId;
            }
        }

        public override RpcLocalHandling LocalHandling => RpcLocalHandling.None;

        public override void Write(MessageWriter writer, Data data)
        {
            writer.Write(data.JesterId);
        }

        public override Data Read(MessageReader reader)
        {
            return new Data(reader.ReadByte());
        }

        public override void Handle(PlayerControl innerNetObject, Data data)
        {
            Plugin.Log.LogWarning($"{innerNetObject.Data.PlayerId} sent byte {data.JesterId}");
            RoleInfo.JesterId = data.JesterId;
        }
    }

    [RegisterCustomRpc]
    public class SetSnitchRpc : PlayerCustomRpc<MoreRolesPlugin, SetSnitchRpc.Data>
    {
        public SetSnitchRpc(MoreRolesPlugin plugin) : base(plugin)
        {

        }

        public readonly struct Data
        {
            public readonly byte SnitchId;

            public Data(byte snitchId)
            {
                SnitchId = snitchId;
            }
        }

        public override RpcLocalHandling LocalHandling => RpcLocalHandling.None;

        public override void Write(MessageWriter writer, Data data)
        {
            writer.Write(data.SnitchId);
        }

        public override Data Read(MessageReader reader)
        {
            return new Data(reader.ReadByte());
        }

        public override void Handle(PlayerControl innerNetObject, Data data)
        {
            Plugin.Log.LogWarning($"{innerNetObject.Data.PlayerId} sent byte {data.SnitchId}");
            RoleInfo.SnitchId = data.SnitchId;
        }
    }

    [RegisterCustomRpc]
    public class SetMechanicRpc : PlayerCustomRpc<MoreRolesPlugin, SetMechanicRpc.Data>
    {
        public SetMechanicRpc(MoreRolesPlugin plugin) : base(plugin)
        {

        }

        public readonly struct Data
        {
            public readonly byte MechanicId;

            public Data(byte mechanicId)
            {
                MechanicId = mechanicId;
            }
        }

        public override RpcLocalHandling LocalHandling => RpcLocalHandling.None;

        public override void Write(MessageWriter writer, Data data)
        {
            writer.Write(data.MechanicId);
        }

        public override Data Read(MessageReader reader)
        {
            return new Data(reader.ReadByte());
        }

        public override void Handle(PlayerControl innerNetObject, Data data)
        {
            Plugin.Log.LogWarning($"{innerNetObject.Data.PlayerId} sent byte {data.MechanicId}");
            RoleInfo.MechanicId = data.MechanicId;
        }
    }

    [RegisterCustomRpc]
    public class SetWitnessRpc : PlayerCustomRpc<MoreRolesPlugin, SetWitnessRpc.Data>
    {
        public SetWitnessRpc(MoreRolesPlugin plugin) : base(plugin)
        {

        }

        public readonly struct Data
        {
            public readonly byte WitnessId;

            public Data(byte id)
            {
                WitnessId = id;
            }
        }

        public override RpcLocalHandling LocalHandling => RpcLocalHandling.None;

        public override void Write(MessageWriter writer, Data data)
        {
            writer.Write(data.WitnessId);
        }

        public override Data Read(MessageReader reader)
        {
            return new Data(reader.ReadByte());
        }

        public override void Handle(PlayerControl innerNetObject, Data data)
        {
            Plugin.Log.LogWarning($"{innerNetObject.Data.PlayerId} sent byte {data.WitnessId}");
            RoleInfo.WitnessId = data.WitnessId;
        }
    }

    [RegisterCustomRpc]
    public class SetSheriffRpc : PlayerCustomRpc<MoreRolesPlugin, SetSheriffRpc.Data>
    {
        public SetSheriffRpc(MoreRolesPlugin plugin) : base(plugin)
        {

        }

        public readonly struct Data
        {
            public readonly byte Id;

            public Data(byte id)
            {
                Id = id;
            }
        }

        public override RpcLocalHandling LocalHandling => RpcLocalHandling.None;

        public override void Write(MessageWriter writer, Data data)
        {
            writer.Write(data.Id);
        }

        public override Data Read(MessageReader reader)
        {
            return new Data(reader.ReadByte());
        }

        public override void Handle(PlayerControl innerNetObject, Data data)
        {
            Plugin.Log.LogWarning($"{innerNetObject.Data.PlayerId} sent byte {data.Id}");
            RoleInfo.SheriffId = data.Id;
        }
    }
    #endregion

    [RegisterCustomRpc]
    public class SheriffKillRpc : PlayerCustomRpc<MoreRolesPlugin, SheriffKillRpc.Data>
    {
        public SheriffKillRpc(MoreRolesPlugin plugin) : base(plugin)
        {

        }

        public readonly struct Data
        {
            public readonly byte KillerId;
            public readonly byte VictimId;

            public Data(byte killerId, byte victimId)
            {
                KillerId = killerId;
                VictimId = victimId;
            }
        }

        public override RpcLocalHandling LocalHandling => RpcLocalHandling.None;

        public override void Write(MessageWriter writer, Data data)
        {
            writer.Write(data.KillerId);
            writer.Write(data.VictimId);
        }

        public override Data Read(MessageReader reader)
        {
            return new Data(reader.ReadByte(), reader.ReadByte());
        }

        public override void Handle(PlayerControl innerNetObject, Data data)
        {
            Plugin.Log.LogWarning($"{innerNetObject.Data.PlayerId} sent bytes {data.KillerId} & {data.VictimId}");
            PlayerControl killer = Utilities.GetPlayerById(data.KillerId);
            PlayerControl victim = Utilities.GetPlayerById(data.VictimId);
            killer.MurderPlayer(victim);
        }
    }
}
