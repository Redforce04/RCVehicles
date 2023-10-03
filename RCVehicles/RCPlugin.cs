namespace RCVehicles
{
    using System;
    using System.IO;
    using Exiled.API.Enums;
    using Exiled.API.Features;
    using HarmonyLib;
    using RCVehicles.Interfaces;

    public class RCPlugin : Plugin<RCConfig, RCTranslation>
    {
        public override string Author => "Redforce04";
        public override string Name => "RC Vehicles";
        public override string Prefix => "RC";

        public override PluginPriority Priority => PluginPriority.Last;
        public override Version Version => new Version(1,0,0);

        public static RCPlugin Singleton { get; private set; }
        internal Harmony Harmony { get; private set; }
        internal EventHandlers EventHandlers { get; private set; }

        public override void OnEnabled()
        {
            if (!Config.IsEnabled)
                return;
            
            Singleton = this;
            
            Harmony = new Harmony("me.redforce04.rcvehicles");
            Harmony.PatchAll();
            
            EventHandlers = new EventHandlers();
            EventHandlers.RegisterEventHandlers();

            /*if (!Directory.Exists(Config.SchematicLocation))
            {
                Directory.CreateDirectory(Config.SchematicLocation);
            }*/
            
            Vehicle.RegisterAllVehicles();
        }

        public override void OnDisabled()
        {
            Vehicle.RegisteredVehicles = null;
            
            EventHandlers.UnRegisterEventHandlers();
            EventHandlers = null;
            
            Harmony.UnpatchAll();
            Harmony = null;
            
            Singleton = null;
        }

        public override void OnRegisteringCommands()
        {
            base.OnRegisteringCommands();
            Log.Debug($"Registered {this.Commands.Count} commands.");
        }
    }
}