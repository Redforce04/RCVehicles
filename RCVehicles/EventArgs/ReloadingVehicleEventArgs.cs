﻿// <copyright file="Log.cs" company="Redforce04#4091">
// Copyright (c) Redforce04. All rights reserved.
// </copyright>
// -----------------------------------------
//    Solution:         RCVehicles
//    Project:          RCVehicles
//    FileName:         StartingVehicle.cs
//    Author:           Redforce04#4091
//    Revision Date:    09/26/2023 12:25 AM
//    Created Date:     09/26/2023 12:25 AM
// -----------------------------------------

namespace RCVehicles.EventArgs;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Interfaces;
using Exiled.Events.Features;
using RCVehicles.Interfaces;

public class ReloadingVehicleEventArgs : Event, IDeniableEvent, IPlayerEvent
{
    /// <summary>
    /// Used to create an instance of <see cref="ReloadingVehicleEventArgs"/>
    /// </summary>
    /// <param name="player">The <see cref="Player"/> reloading the vehicle.</param>
    /// <param name="weapon">The <see cref="VehicleWeapon"/> that is being reloaded.</param>
    /// <param name="isAllowed">Whether the event is allowed to execute or not.</param>
    public ReloadingVehicleEventArgs(Player player, VehicleWeapon weapon, bool isAllowed = true)
    {
        Player = player;
        Weapon = weapon;
        IsAllowed = isAllowed;
    }
    
    /// <summary>
    /// The vehicle being reloaded.
    /// </summary>
    public VehicleWeapon Weapon { get; }
    
    /// <inheritdoc cref="IDeniableEvent.IsAllowed"/>
    public bool IsAllowed { get; set; }
        
    /// <inheritdoc cref="IPlayerEvent.Player"/>
    public Player Player { get; }
}