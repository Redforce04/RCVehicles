// <copyright file="Log.cs" company="Redforce04#4091">
// Copyright (c) Redforce04. All rights reserved.
// </copyright>
// -----------------------------------------
//    Solution:         RCVehicles
//    Project:          RCVehicles
//    FileName:         DamagingVehicle.cs
//    Author:           Redforce04#4091
//    Revision Date:    09/26/2023 12:24 AM
//    Created Date:     09/26/2023 12:24 AM
// -----------------------------------------

namespace RCVehicles.EventArgs;
using Exiled.API.Features;
using Exiled.API.Features.DamageHandlers;
using Exiled.Events.EventArgs.Interfaces;
using Exiled.Events.Features;
using RCVehicles.Interfaces;

public class DamagingVehicleEventArgs : Event, IDeniableEvent, IAttackerEvent
{
    /// <summary>
    /// Used to create an instance of <see cref="DamagingVehicleEventArgs"/>
    /// </summary>
    /// <param name="attacker">The <see cref="Attacker"/> damaging the vehicle.</param>
    /// <param name="player">The <see cref="Player"/> inside or controlling the vehicle.</param>
    /// <param name="vehicle">The <see cref="Vehicle"/> that is being damaged.</param>
    /// <param name="isAllowed">Whether the event is allowed to execute or not.</param>
    public DamagingVehicleEventArgs(Player attacker, Player player, Vehicle vehicle, CustomDamageHandler damageHandler, bool isAllowed = true)
    {
        Attacker = attacker;
        Player = player;
        Vehicle = vehicle;
        DamageHandler = damageHandler;
        IsAllowed = isAllowed;
    }
    
    /// <summary>
    /// The vehicle being damaged.
    /// </summary>
    public Vehicle Vehicle { get; }
    
    /// <inheritdoc cref="IDeniableEvent.IsAllowed"/>
    public bool IsAllowed { get; set; }

    /// <inheritdoc cref="IAttackerEvent.Player"/>
    public Player Player { get; }

    /// <inheritdoc cref="IAttackerEvent.Attacker"/>
    public Player Attacker { get; }
    
    /// <inheritdoc cref="IAttackerEvent.DamageHandler"/>
    public CustomDamageHandler DamageHandler { get; set; }
}