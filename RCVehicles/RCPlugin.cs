namespace RCVehicles
{
    using System;
    using Exiled.API.Enums;
    using Exiled.API.Features;
    using HarmonyLib;
    using RCVehicles.Interfaces;

    public class RCPlugin : Plugin<RCConfig, RCTranslation>
    {
        public override string Author => "Redforce04";
        public override string Name => "RC Vehicles";
        public override string Prefix => "RC";

        public override PluginPriority Priority => PluginPriority.Default;
        public override Version Version => new Version(1,0,0);
        
        internal Harmony Harmony { get; private set; }
        internal EventHandlers EventHandlers { get; private set; }

        public override void OnEnabled()
        {
            if (!Config.IsEnabled)
                return;
            
            Harmony = new Harmony("me.redforce04.rcvehicles");
            Harmony.PatchAll();
            
            EventHandlers = new EventHandlers();
            EventHandlers.RegisterEventHandlers();
            
            Vehicle.RegisterAllVehicles();
        }

        public override void OnDisabled()
        {
            Vehicle.RegisteredVehicles = null;
            
            EventHandlers.UnRegisterEventHandlers();
            EventHandlers = null;
            
            Harmony.UnpatchAll();
            Harmony = null;
        }

        public override void OnReloaded()
        {
            
        }
        
    }
}