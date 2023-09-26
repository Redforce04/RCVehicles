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

namespace RCVehicles.Interfaces
{
    using System.Collections.Generic;
    using System.Linq;
    using Exiled.API.Features;
    using MapEditorReborn.API.Features.Serializable;
    using RCVehicles.EventArgs;

    /// <summary>
    /// The base class for defining a vehicle.
    /// </summary>
    public abstract class Vehicle
    {
#region Static Items
        static Vehicle()
        {
            RegisteredVehicles = new List<Vehicle>();
        }
    #region Static Properties
        public static List<Vehicle> RegisteredVehicles { get; internal set; }
    #endregion
    #region Static Methods
        /// <summary>
        /// Registers all found instances of <see cref="Vehicle"/>
        /// </summary>
        public static void RegisterAllVehicles()
        {
            RegisteredVehicles = Extensions.AbstractedTypeExtensions.InstantiateAllInstancesOfType<Vehicle>();
        }
    #endregion
#endregion
#region Abstract API
        /// <summary>
        /// The name of the vehicle.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// The author of the vehicle.
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// The name of the schematic to load.
        /// </summary>
        public virtual string SchematicName => "";
        
        /// <summary>
        /// The schematic for the vehicle.
        /// </summary>
        public virtual SchematicSerializable Schematic => new SchematicSerializable(this.SchematicName);

        /// <summary>
        /// How many players can be inside the vehicle.
        /// </summary>
        public virtual int MaxPlayerCount { get; set; } = 1; 
        

        /// <summary>
        /// Allows or denies a player from entering a vehicle.
        /// </summary>
        /// <param name="vehicleObject">The instance of the <see cref="VehicleObject"/> that the player is trying to enter.</param>
        /// <param name="ply">The <see cref="Player"/> trying to enter the vehicle.</param>
        /// <returns>True if the player can enter the vehicle. False if the player is unable to enter the vehicle.</returns>
        public bool CanPlayerEnterVehicle(VehicleObject vehicleObject, Player ply)
        {
            // Ensure player is actually a player
            if (ply.IsNPC || ply.IsHost)
                return false;
            
            // Ensure player is not in any other vehicles or owns any other vehicles.
            if (VehicleObject.VehicleObjectInstances.Any(vehicle => 
                    vehicle.PlayersRidingVehicle.Contains(ply) || vehicle.Owner == ply))
                return false;
                
            // Ensure that there is enough room in the vehicle.
            if (vehicleObject.PlayersRidingVehicle.Count >= MaxPlayerCount)
                return false;

            // Ensure the vehicle doesnt have custom logic.
            if (!CanPlayerEnterVehicle(vehicleObject, ply))
                return false;

            // Run the event
            EnteringVehicleEventArgs ev = new EnteringVehicleEventArgs(ply, vehicleObject);
            Events.OnEnteringingVehicle(ev);
            if (!ev.IsAllowed)
                return false;
            
            return true;
        }

        /// <summary>
        /// Can be used to prevent players from entering a vehicle.
        /// </summary>
        /// <param name="vehicleObject">The instance of the <see cref="VehicleObject"/> that is being entered.</param>
        /// <param name="ply">The <see cref="Player"/> who is attempting to enter the vehicle.</param>
        /// <returns></returns>
        protected virtual bool PlayerIsAbleToEnterVehicle(VehicleObject vehicleObject, Player ply)
        {
            return true;
        }
        
        /// <summary>
        /// Spawns the vehicle.
        /// </summary>
        public void SpawnVehicle(Player ply = null)
        {
            // Null player means that the server is the owner.
            if (ply is null)
            {
                ply = Server.Host;
            }
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
            
            // Load the schematic
            LoadSchematic();
            
            // Does the player "become vehicle"
            if (this is IRemoteControlledVehicle rc)
            {
                if (!ply.IsNPC && !ply.IsHost)
                {
                    rc.RcVehicle.SpawnNPCForPlayer(ply);
                }
            }
            
            // Does a broadcast need to be issued?
        }

        protected virtual bool IsPlayerAllowedToSpawnVehicle(Player ply = null)
        {
            return true;
        }
#endregion
    }
}
