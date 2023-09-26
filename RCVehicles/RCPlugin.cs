using System;
using Exiled.API.Enums;
using Exiled.API.Features;

namespace RCVehicles
{
    public class RCPlugin : Plugin<RCConfig, RCTranslation>
    {
        public override string Author => "Redforce04";
        public override string Name => "RC Vehicles";
        public override string Prefix => "RC";

        public override PluginPriority Priority => PluginPriority.Default;
        public override Version Version => new Version(1,0,0);
        
        
        
        
        public override void OnEnabled()
        {
            
        }

        public override void OnDisabled()
        {
            
        }

        public override void OnReloaded()
        {
            
        }
        
    }
}