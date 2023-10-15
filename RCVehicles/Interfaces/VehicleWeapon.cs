// <copyright file="Log.cs" company="Redforce04#4091">
// Copyright (c) Redforce04. All rights reserved.
// </copyright>
// -----------------------------------------
//    Solution:         RCVehicles
//    Project:          RCVehicles
//    FileName:         VehicleWeapon.cs
//    Author:           Redforce04#4091
//    Revision Date:    10/14/2023 5:26 PM
//    Created Date:     10/14/2023 5:26 PM
// -----------------------------------------

namespace RCVehicles.Interfaces;

public class VehicleWeapon
{
    /// <summary>
    /// The name for the weapon. Used as the identifier.
    /// </summary>
    public virtual string Name { get; set; }
    
    /// <summary>
    /// The base vehicle that this weapon applies to.
    /// </summary>
    public Vehicle BaseVehicle { get; internal set; }

    /// <summary>
    /// The health that this weapon has.
    /// </summary>
    public virtual float Health { get; set; } = 100;
    
    /// <summary>
    /// The maximum amount of ammo the weapon can hold.
    /// </summary>
    public virtual int MaxAmmo { get; set; }
    
    
    /// <summary>
    /// Trigger the firing of the weapon.
    /// </summary>
    /// <param name="weaponObject">The instance of the weapon that is being fired.</param>
    public void Fire(VehicleWeaponObject weaponObject)
    {
        OnFiring(weaponObject);
    }
    
    /// <summary>
    /// Processes the firing of the weapon.
    /// </summary>
    /// <param name="weaponObject">The instance of the weapon that is being fired.</param>
    protected virtual void OnFiring(VehicleWeaponObject weaponObject)
    {
        if (weaponObject.Ammo <= 0)
        {
            return;
        }
        weaponObject.Ammo--;
        
    }

    /// <summary>
    /// Triggers the reloading of the weapon.
    /// </summary>
    /// <param name="weaponObject">The instance of the weapon that is being reloaded.</param>
    public void Reload(VehicleWeaponObject weaponObject)
    {
        OnReloading(weaponObject);
    }
    
    /// <summary>
    /// Process the reloading of the weapon.
    /// </summary>
    /// <param name="weaponObject">The instance of the weapon that is being reloaded.</param>
    protected virtual void OnReloading(VehicleWeaponObject weaponObject)
    {
        
    }

    /// <summary>
    /// Triggers the destruction of the weapon.
    /// </summary>
    /// <param name="weaponObject">The instance of the weapon that is being destroyed.</param>
    public void Destroy(VehicleWeaponObject weaponObject)
    {
        OnDestroying(weaponObject);
    }
    /// <summary>
    /// Processes the destruction of the weapon.
    /// </summary>
    /// <param name="weaponObject">The instance of the weapon that is being destroyed.</param>
    protected virtual void OnDestroying(VehicleWeaponObject weaponObject)
    {
        
    }
    
    /// <summary>
    /// Triggers the damaging of the weapon.
    /// </summary>
    /// <param name="weaponObject">The instance of the weapon that is being damaged.</param>
    public void Damage(VehicleWeaponObject weaponObject)
    {
        OnDamaging(weaponObject);
    }
    /// <summary>
    /// Processes the damaging of the weapon.
    /// </summary>
    /// <param name="weaponObject">The instance of the weapon that is being damaged.</param>
    protected virtual void OnDamaging(VehicleWeaponObject weaponObject)
    {
        
    }
    
}