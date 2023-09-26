// <copyright file="Log.cs" company="Redforce04#4091">
// Copyright (c) Redforce04. All rights reserved.
// </copyright>
// -----------------------------------------
//    Solution:         RCVehicles
//    Project:          RCVehicles
//    FileName:         IVehicle.cs
//    Author:           Redforce04#4091
//    Revision Date:    09/26/2023 12:01 AM
//    Created Date:     09/26/2023 12:01 AM
// -----------------------------------------

using System.Collections.Generic;
using Exiled.API.Features;
using Exiled.Events.Features;
using MapEditorReborn.Events.Handlers;
using RCVehicles.EventArgs;

namespace RCVehicles.Interfaces
{
    /// <summary>
    /// The base class for a vehicle.
    /// </summary>
    public class Vehicle
    {
        /// <summary>
        /// The schematic for the vehicle.
        /// </summary>
        public virtual Schematic Schematic { get; private set; }
        
        
        /// <summary>
        /// Used to load the schematic.
        /// </summary>
        public void LoadSchematic()
        {
            
        }

        /// <summary>
        /// Spawns the vehicle.
        /// </summary>
        public void SpawnVehicle(Player ply = null)
        {
            // Does player need prereqs to spawn the vehicle?
            if (!IsPlayerAllowedToSpawnVehicle())
            {
                return;
            }
            
            // Call an event to see if the player is allowed to spawn the vehicle.
            SpawningVehicleEventArgs ev = new SpawningVehicleEventArgs(ply, this);
            Events.OnSpawningVehicle(ev);
            if (!ev.IsAllowed)
            {
                return;
            }
            
            // Does the player "become vehicle"
            if (this is IRemoteControlledVehicle rc)
            {
                rc.RcVehicle.
            }
            // Does an NPC need to be spawned on the player's location? ("remote control" - they aren't actually in it)
            _internalSetPlayerAsVehicle();
            
            // Does a broadcast need to be issued?

        }

        private void _setNPCInPlace()
        {
            
        }
        

        protected virtual bool IsPlayerAllowedToSpawnVehicle(Player ply = null)
        {
            return true;
        }
        
        
        
        
        
    }
}