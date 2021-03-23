using BepInEx;
using BepInEx.Configuration;
using BepInEx.IL2CPP;
using Essentials;
using Essentials.Options;
using HarmonyLib;
using Reactor;
using UnityEngine;

namespace AmongUsMoreRolesMod
{
    [BepInPlugin(Id)]
    [BepInProcess("Among Us.exe")]
    [BepInDependency(EssentialsPlugin.Id)]
    public class MoreRolesPlugin : BasePlugin
    {
        public const string Id = "com.squarewaterlemon.morerolesmod";

        public static BepInEx.Logging.ManualLogSource log;

        public Harmony Harmony { get; } = new Harmony(Id);

        public static CustomToggleOption JesterToggleOption = CustomOption.AddToggle("Jester Enabled", false);
        public static CustomToggleOption MechanicToggleOption = CustomOption.AddToggle("Mechanic Enabled", false);
        public static CustomToggleOption SnitchToggleOption = CustomOption.AddToggle("Snitch Enabled", false);
        public static CustomToggleOption WitnessToggleOption = CustomOption.AddToggle("Witness Enabled", false);
        public static CustomToggleOption SheriffToggleOption = CustomOption.AddToggle("Sheriff Enabled", false);
        public static CustomNumberOption WitnessVisionOption = CustomOption.AddNumber("Witness Vision", 5f, 0.25f, 5f, 0.25f);
        public static CustomNumberOption SheriffKillCDOption = CustomOption.AddNumber("Sheriff Kill Cooldown", 30f, 10f, 60f, 2.5f);

        public override void Load()
        {
            RegisterInIl2CppAttribute.Register();
            RegisterCustomRpcAttribute.Register(this);

            log = Log;

            log.LogMessage("squarewaterlemon More Roles Mod Loaded!");

            // Credit given in the Ping Tracker & Github
            CustomOption.ShamelessPlug = false;

            Harmony.PatchAll();
        }
    }
}
