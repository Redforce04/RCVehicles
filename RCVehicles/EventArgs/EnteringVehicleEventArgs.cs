// <copyright file="Log.cs" company="Redforce04#4091">
// Copyright (c) Redforce04. All rights reserved.
// </copyright>
// -----------------------------------------
//    Solution:         RCVehicles
//    Project:          RCVehicles
//    FileName:         DestroyingVehicle.cs
//    Author:           Redforce04#4091
//    Revision Date:    09/26/2023 12:25 AM
//    Created Date:     09/26/2023 12:25 AM
// -----------------------------------------

using Exiled.API.Features;
using Exiled.API.Features.DamageHandlers;
using Exiled.Events.EventArgs.Interfaces;
using Exiled.Events.Features;
using RCVehicles.Interfaces;

namespace RCVehicles.EventArgs;

public class EnteringVehicleEventArgs : Event, IDeniableEvent, IPlayerEvent
{
    /// <summary>
    /// Used to create an instance of <see cref="EnteringVehicleEventArgs"/>
    /// </summary>
    /// <param name="player">The <see cref="Player"/> inside or controlling the vehicle.</param>
    /// <param name="vehicleObject">The instance of the <see cref="Interfaces.VehicleObject"/> that is being entered.</param>
    /// <param name="isAllowed">Whether the event is allowed to execute or not.</param>
    public EnteringVehicleEventArgs(Player player, VehicleObject vehicleObject, bool isAllowed = true)
    {
        Player = player;
        VehicleObject = vehicleObject;
        IsAllowed = isAllowed;
    }
    
    /// <summary>
    /// The instance of the vehicle object being entered.
    /// </summary>
    public VehicleObject VehicleObject { get; }
    
    /// <inheritdoc cref="IDeniableEvent.IsAllowed"/>
    public bool IsAllowed { get; set; }

    /// <inheritdoc cref="IAttackerEvent.Player"/>
    public Player Player { get; }

}