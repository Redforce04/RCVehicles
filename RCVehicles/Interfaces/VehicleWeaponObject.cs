// <copyright file="Log.cs" company="Redforce04#4091">
// Copyright (c) Redforce04. All rights reserved.
// </copyright>
// -----------------------------------------
//    Solution:         RCVehicles
//    Project:          RCVehicles
//    FileName:         VehicleWeaponObjects.cs
//    Author:           Redforce04#4091
//    Revision Date:    10/14/2023 5:39 PM
//    Created Date:     10/14/2023 5:39 PM
// -----------------------------------------

namespace RCVehicles.Interfaces;

using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Items;

public class VehicleWeaponObject
{
    internal VehicleWeaponObject(VehicleObject vehicle, Vehicle baseVehicle, VehicleWeapon baseWeapon)
    {
        Vehicle = vehicle;
        BaseVehicle = baseVehicle;
        BaseWeapon = baseWeapon;
        Health = BaseWeapon.Health;
        Ammo = BaseWeapon.MaxAmmo;
    }
    // todo - implement hitbox system.
    /// <summary>
    /// The base weapon instance that this weapon is.
    /// </summary>
    public VehicleWeapon BaseWeapon { get; private set; }
    /// <summary>
    /// The base vehicle instance that this weapon is attached to.
    /// </summary>
    public Vehicle BaseVehicle { get; private set; }
    
    /// <summary>
    /// The instance of the vehicle object this weapon is attached to.
    /// </summary>
    public VehicleObject Vehicle { get; private set; }
    
    /// <summary>
    /// How much health the weapon currently has.
    /// </summary>
    public float Health { get; set; }
    
    /// <summary>
    /// How much ammo the weapon currently has.
    /// </summary>
    public int Ammo { get; set; }
    
    /// <summary>
    /// An instance of the firearm that is used to shoot and process hitreg.
    /// </summary>
    public Firearm Firearm { get; set; }

    public void Destroy()
    {
        BaseWeapon.Destroy(this);
    }

    public void Damage()
    {
        BaseWeapon.Damage(this);
    }

    public void Reload()
    {
        BaseWeapon.Reload(this);
    }

    public void Fire()
    {
        BaseWeapon.Fire(this);
    }
}