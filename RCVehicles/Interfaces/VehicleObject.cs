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

namespace RCVehicles.Interfaces;
using System.Collections.Generic;
using API;
using Components;
using Exiled.API.Features;
using Exiled.Permissions.Commands.Permissions;
using MapEditorReborn.API.Extensions;
using MapEditorReborn.API.Features.Objects;
using MapEditorReborn.Events.Handlers;
using MEC;
using Mirror;
using UnityEngine;

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
        Owner = ply;
        this.Id = VehicleObjectInstances.Count;
        this.MaxTurnSpeed = vehicle.MaxTurnSpeed;
        this.Mass = BaseVehicle.Mass;
        this.HitboxComponents = new List<object>();
        this.WeaponObjects = new List<VehicleWeaponObject>();
        foreach (var weapon in BaseVehicle.VehicleWeapons)
        {
            if (weapon is null)
                continue;
            var weaponObj = new VehicleWeaponObject(this, BaseVehicle, weapon);
            this.WeaponObjects.Add(weaponObj);
        }
        Log.Debug($"Spawning vehicle {Id}. Turning Speed: {MaxTurnSpeed}, Mass: {Mass}");
        _createAndSpawnVehicle();
        
    }

    public void RemoveVehicle()
    {
        this.Schematic.Destroy();
        if (this.BaseVehicle is IRemoteControlledVehicle rc)
        {
            foreach (Player ply in this.PlayersRidingVehicle)
            {
                rc.RcVehicle.RemovePlayer(ply);
            }
        }
    } 
    
    private void _createAndSpawnVehicle()
    {
        this.LoadSchematic(Owner.Position, Owner.Rotation);
        if (this.Schematic is null)
        {
            Log.Debug("Schematic is null.");
            return;
        }

        if (Schematic.gameObject is null)
        {
            Log.Debug("Schematic gameobject is null.");
            return;
        }

        this.Rigidbody = Schematic.gameObject.GetComponent<Rigidbody>();
        if (this.Rigidbody is null)
        {
            Log.Debug("Pre-existing rigidbody is null.");
            this.Rigidbody = Schematic.gameObject.AddComponent<Rigidbody>();
        }
        
        if (this.Rigidbody is null)
        {
            Log.Debug("Schematic rigidbody is null.");
            return;
        }
        
        Log.Debug("Schematic rigidbody has been set.");
        // ReSharper disable once BitwiseOperatorOnEnumWithoutFlags
        /*Timing.CallDelayed(3f, () =>
        {
            this.Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
        });*/
        this.Rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        
        this.Rigidbody.useGravity = true;
        this.Rigidbody.mass = this.Mass;
        
    }
    
    /// <summary>
    /// The type of <see cref="Vehicle"/> that this vehicle is.
    /// </summary>
    public Vehicle BaseVehicle { get; private set; }
    
    /// <summary>
    /// A list of <see cref="VehicleWeaponObject"/> that are associated with this vehicle.
    /// </summary>
    public List<VehicleWeaponObject> WeaponObjects { get; private set; }
    
    /// <summary>
    /// The schematic of the vehicle.
    /// </summary>
    public SchematicObject Schematic { get; private set; }
    
    /// <summary>
    /// An instance of the schematic which cannot be collided with.
    /// </summary>
    public SchematicObject NonCollidableSchematic { get; private set; }

    /// <summary>
    /// The control component for the vehicle.
    /// </summary>
    public VehicleControlInstance? ControlComponent { get; internal set; }
    
    /// <summary>
    /// The rigidbody of the vehicle.
    /// </summary>
    public Rigidbody? Rigidbody { get; private set; }
    
    
    /// <summary>
    /// A list of hitbox components, that players can use to damage the vehicle.
    /// </summary>
    public List<object> HitboxComponents { get; private set; }

    /// <summary>
    /// The Id of the vehicle.
    /// </summary>
    public int Id { get; set; }
    
    
    /// <summary>
    /// The player that owns the vehicle.
    /// </summary>
    // Must be set privately. Use a method to remove the player from the vehicle.
    public Player Owner { get; private set; }
    
    /// <summary>
    /// A list of all of the players that are riding inside the vehicle.
    /// </summary>
    public List<Player> PlayersRidingVehicle { get; private set; }

    public float Mass
    {
        get
        {
            if (this.Rigidbody is not null)
            {
                return this.Rigidbody.mass;
            }

            return BaseVehicle.Mass;
        }
        set
        {
            if (this.Rigidbody is not null)
            {
                this.Rigidbody.mass = value;
                return;
            }

            Log.Debug("Vehicle doesn't have a rigidbody. Mass cannot be set.");
        }
    }

    public float MaxTurnSpeed { get; set; }

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

    public void SetDriver(Player ply, bool removeCurrentDriverIfExists = false)
    {

        Log.Debug($"Setting player {ply.Nickname} as driver.");
        if (this.ControlComponent is not null && removeCurrentDriverIfExists)
        {
            this.RemoveDriver();
        }
        //LoadUncollidableSchematicForPlayer(ply);
        this.ControlComponent = VehicleControlInstance.CreateOrGet(this, ply);
    }

    public void RemoveDriver()
    {
        if (this.ControlComponent is not null)
        {
            //LoadCollidableSchematicForPlayer(this.ControlComponent.Player);
            
            Log.Debug($"Removing player {this.ControlComponent.Player.Nickname} from driver.");
            this.ControlComponent.Destroy();
            return;
        }

        Log.Debug("Driver cannot be removed, as there is no driver.");
    }
    public void LoadUncollidableSchematicForPlayer(Player ply)
    {
        foreach (NetworkIdentity networkIdentity in Schematic.NetworkIdentities)
        {
            ply.DestroyNetworkIdentity(networkIdentity);
        }
        foreach (NetworkIdentity networkIdentity in NonCollidableSchematic.NetworkIdentities)
        {
            ply.SpawnNetworkIdentity(networkIdentity);
        }
    }
    public void LoadCollidableSchematicForPlayer(Player ply)
    {
        foreach (NetworkIdentity networkIdentity in NonCollidableSchematic.NetworkIdentities)
        {
            ply.DestroyNetworkIdentity(networkIdentity);
        }
        foreach (NetworkIdentity networkIdentity in Schematic.NetworkIdentities)
        {
            ply.SpawnNetworkIdentity(networkIdentity);
        }
    }
    
    
    /// <summary>
    /// Used to load the schematic.
    /// </summary>
    // By default we can abstract functionality into the base class
    protected virtual void LoadSchematic(Vector3 position, Quaternion rotation)
    {
        Schematic = MapEditorReborn.API.Features.ObjectSpawner.SpawnSchematic(BaseVehicle.Schematic, position, rotation);
        /*NonCollidableSchematic = MapEditorReborn.API.Features.ObjectSpawner.SpawnSchematic(BaseVehicle.Schematic, position, rotation);
        var scale = NonCollidableSchematic.gameObject.transform.localScale;
        NonCollidableSchematic.gameObject.transform.localScale =
            new Vector3(-Mathf.Abs(scale.x), -Mathf.Abs(scale.y), -Mathf.Abs(scale.z));*/
    }
    
    
    
    
}