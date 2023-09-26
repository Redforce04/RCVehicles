// <copyright file="Log.cs" company="Redforce04#4091">
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

using Exiled.API.Features;
using Exiled.Events.EventArgs.Interfaces;
using Exiled.Events.Features;
using RCVehicles.Interfaces;

namespace RCVehicles.EventArgs;

public class StartingVehicleEventArgs : Event, IDeniableEvent, IPlayerEvent
{
    /// <summary>
    /// Used to create an instance of <see cref="StartingVehicleEventArgs"/>
    /// </summary>
    /// <param name="player">The <see cref="Player"/> starting the vehicle.</param>
    /// <param name="vehicle">The <see cref="Vehicle"/> that is being started.</param>
    /// <param name="isAllowed">Whether the event is allowed to execute or not.</param>
    public StartingVehicleEventArgs(Player player, Vehicle vehicle, bool isAllowed = true)
    {
        Player = player;
        Vehicle = vehicle;
        IsAllowed = isAllowed;
    }
    
    /// <summary>
    /// The vehicle being started.
    /// </summary>
    public Vehicle Vehicle { get; }
    
    /// <inheritdoc cref="IDeniableEvent.IsAllowed"/>
    public bool IsAllowed { get; set; }
        
    /// <inheritdoc cref="IPlayerEvent.Player"/>
    public Player Player { get; }
}