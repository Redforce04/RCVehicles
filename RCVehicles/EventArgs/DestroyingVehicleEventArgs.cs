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

public class DestroyingVehicleEventArgs : Event, IDeniableEvent, IAttackerEvent
{
    /// <summary>
    /// Used to create an instance of <see cref="DestroyingVehicleEventArgs"/>
    /// </summary>
    /// <param name="attacker">The <see cref="Attacker"/> destroying the vehicle.</param>
    /// <param name="player">The <see cref="Player"/> inside or controlling the vehicle.</param>
    /// <param name="vehicle">The <see cref="Vehicle"/> that is being destroyed.</param>
    /// <param name="isAllowed">Whether the event is allowed to execute or not.</param>
    public DestroyingVehicleEventArgs(Player attacker, Player player, Vehicle vehicle, CustomDamageHandler damageHandler, bool isAllowed = true)
    {
        Attacker = attacker;
        Player = player;
        Vehicle = vehicle;
        DamageHandler = damageHandler;
        IsAllowed = isAllowed;
    }
    
    /// <summary>
    /// The vehicle being destroyed.
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