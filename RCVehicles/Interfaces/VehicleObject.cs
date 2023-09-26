// <copyright file="Log.cs" company="Redforce04#4091">
// Copyright (c) Redforce04. All rights reserved.
// </copyright>
// -----------------------------------------
//    Solution:         RCVehicles
//    Project:          RCVehicles
//    FileName:         VehicleObject.cs
//    Author:           Redforce04#4091
//    Revision Date:    09/26/2023 2:13 PM
//    Created Date:     09/26/2023 2:13 PM
// -----------------------------------------

using System.Collections.Generic;
using Exiled.API.Features;
using MapEditorReborn.API.Features.Objects;
using UnityEngine;

namespace RCVehicles.Interfaces;

public class VehicleObject
{
    /// <summary>
    /// A list of all currently spawned vehicle object instances.
    /// </summary>
    public static List<VehicleObject> VehicleObjectInstances { get; set; }
    
    /// <summary>
    /// Static Constructor.
    /// </summary>
    static VehicleObject()
    {
        VehicleObjectInstances = new List<VehicleObject>();
    }
    
    /// <summary>
    /// Used to create a new instance of a vehicle object.
    /// </summary>
    /// <param name="vehicle">The type of vehicle to spawn.</param>
    /// <param name="ply">The player who is spawning the vehicle.</param>
    internal VehicleObject(Vehicle vehicle, Player ply)
    {
        BaseVehicle = vehicle;
    }
    
    /// <summary>
    /// The type of <see cref="Vehicle"/> that this vehicle is.
    /// </summary>
    public Vehicle BaseVehicle { get; private set; }
    
    /// <summary>
    /// The schematic of the vehicle.
    /// </summary>
    public SchematicObject Schematic { get; private set; }

    public List<object> HitboxComponents { get; private set; }
    
    /// <summary>
    /// The player that owns the vehicle.
    /// </summary>
    // Must be set privately. Use a method to remove the player from the vehicle.
    public Player Owner { get; private set; }
    
    /// <summary>
    /// A list of all of the players that are riding inside the vehicle.
    /// </summary>
    public List<Player> PlayersRidingVehicle { get; private set; }
    
    /// <summary>
    /// Adds a player to the vehicle.
    /// </summary>
    /// <param name="ply">The <see cref="Player"/> to add to the vehicle.</param>
    public void AddPlayerToVehicle(Player ply)
    {
        if (!BaseVehicle.CanPlayerEnterVehicle(this, ply))
            return;
        
        PlayersRidingVehicle.Add(ply);
    }
    
    /// <summary>
    /// Removes a player from the vehicle.
    /// </summary>
    /// <param name="ply">The <see cref="Player"/> to remove from the vehicle.</param>
    public void RemovePlayerFromVehicle(Player ply)
    {
        
    }
    
    /// <summary>
    /// Used to load the schematic.
    /// </summary>
    // By default we can abstract functionality into the base class
    protected virtual void LoadSchematic(Vector3 position, Quaternion rotation)
    {
        Schematic = MapEditorReborn.API.Features.ObjectSpawner.SpawnSchematic(BaseVehicle.Schematic, position, rotation);
    }
    
    
    
}