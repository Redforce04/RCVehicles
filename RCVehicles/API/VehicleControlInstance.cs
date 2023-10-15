// <copyright file="Log.cs" company="Redforce04#4091">
// Copyright (c) Redforce04. All rights reserved.
// </copyright>
// -----------------------------------------
//    Solution:         RCVehicles
//    Project:          RCVehicles
//    FileName:         VehicleControlInstance.cs
//    Author:           Redforce04#4091
//    Revision Date:    10/04/2023 3:47 PM
//    Created Date:     10/04/2023 3:47 PM
// -----------------------------------------

namespace RCVehicles.API;

using System.Collections.Generic;
using Components;
using System.Linq;
using Exiled.API.Features;
using Exiled.API.Features.Toys;
using Interfaces;
using UnityEngine;
using Light = Exiled.API.Features.Toys.Light;

/// <summary>
/// Contains a list of control settings for a vehicle.
/// </summary>
public class VehicleControlInstance
{
    public static bool TryGetInstance(VehicleObject vehicle, out VehicleControlInstance instance)
    {
        instance = null;
        if (vehicle.ControlComponent is not null)
        {
            instance = vehicle.ControlComponent;
            return true;
        }
        return false;
    }

    public static bool TryGetInstance(Player ply, out VehicleControlInstance instance)
    {
        instance = VehicleController.ControlInstances.FirstOrDefault(x => x.Player == ply);
        return instance is not null;
    }
    public static VehicleControlInstance CreateOrGet(VehicleObject vehicle, Player ply)
    {
        var instance = VehicleController.ControlInstances.FirstOrDefault(vc => vc.Vehicle == vehicle);
        if (instance is not null)
        {
            return instance;
        }

        instance = new VehicleControlInstance(vehicle, ply);
        VehicleController.ControlInstances.Add(instance);
        return instance;

    }

    /// <summary>
    /// Initializes an instance of <see cref="VehicleControlInstance"/>
    /// </summary>
    /// <param name="vehicleObject">Sets the <see cref="Vehicle"/> instance.</param>
    /// <param name="ply">Sets the <see cref="Player"/> instance.</param>
    private VehicleControlInstance(VehicleObject vehicleObject, Player ply)
    {
        Log.Debug("Vehicle Control Instance Created.");
        Vehicle = vehicleObject;
        Player = ply;
        
        ViewFinder = Primitive.Create( PrimitiveType.Sphere, new Vector3(0,700,0), Vector3.zero, new Vector3(0.3f, 0.3f, 0.3f), true);
        ViewFinder.Color = Color.red;
        ViewFinder.Collidable = false;
        ViewFinderLight = Light.Create(ply.Position, Vector3.zero, new Vector3(0.25f, 0.25f, 0.25f), true);
        ViewFinderLight.Color = Color.red;
        ViewFinderLight.Range = 2f;
        ViewFinderLight.Intensity = 1;
        ViewFinderLight.ShadowEmission = false;

    }
    
    /// <summary>
    /// The instance of the vehicle being controlled.
    /// </summary>
    public VehicleObject Vehicle { get; internal set; }

    /// <summary>
    /// How fast the vehicle is able to turn. -1 is disabled.
    /// </summary>
    public float MaxTurnSpeed => Vehicle.MaxTurnSpeed;
    
    /// <summary>
    /// The player controlling the vehicle.
    /// </summary>
    public Player Player { get; internal set; }
    
    /// <summary>
    /// The viewfinder primitive.
    /// </summary>
    public Primitive ViewFinder { get; set; }
    /// <summary>
    /// The viewfinder light / glow.
    /// </summary>
    public Light ViewFinderLight { get; set; }

    /// <summary>
    /// Used for viewfinder color functions.
    /// </summary>
    public bool PreviouslyStill { get; set; } = false;

    /// <summary>
    /// Fires a specified weapon.
    /// </summary>
    public void Fire()
    {
        
    }
    
    /// <summary>
    /// Removes this object from a vehicle and destroys the instance.
    /// </summary>
    public void Destroy()
    {
        VehicleController.ControlInstances.Remove(this);
        Vehicle.ControlComponent = null; 
        ViewFinder.Destroy();
    }
    

}