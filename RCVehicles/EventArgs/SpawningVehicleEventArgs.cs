// <copyright file="Log.cs" company="Redforce04#4091">
// Copyright (c) Redforce04. All rights reserved.
// </copyright>
// -----------------------------------------
//    Solution:         RCVehicles
//    Project:          RCVehicles
//    FileName:         VehicleSpawned.cs
//    Author:           Redforce04#4091
//    Revision Date:    09/26/2023 12:12 AM
//    Created Date:     09/26/2023 12:12 AM
// -----------------------------------------

namespace RCVehicles.EventArgs
{
    using Exiled.API.Features;
    using Exiled.Events.EventArgs.Interfaces;
    using RCVehicles.Interfaces;
    using Event = Exiled.Events.Features.Event;

    public class SpawningVehicleEventArgs : Event, IDeniableEvent, IPlayerEvent
    {
        /// <summary>
        /// Used to create an instance of <see cref="SpawningVehicleEventArgs"/>
        /// </summary>
        /// <param name="player">The <see cref="Player"/> spawning the vehicle.</param>
        /// <param name="vehicle">The <see cref="Vehicle"/> that is being spawned.</param>
        /// <param name="isAllowed">Whether the event is allowed to execute or not.</param>
        public SpawningVehicleEventArgs(Player player, Vehicle vehicle, bool isAllowed = true)
        {
            Player = player;
            Vehicle = vehicle;
            IsAllowed = isAllowed;
        }
        
        /// <summary>
        /// The vehicle being spawned.
        /// </summary>
        public Vehicle Vehicle { get; }
        
        /// <inheritdoc cref="IDeniableEvent.IsAllowed"/>
        public bool IsAllowed { get; set; }
        
        /// <inheritdoc cref="IPlayerEvent.Player"/>
        public Player Player { get; }
        
    }
}