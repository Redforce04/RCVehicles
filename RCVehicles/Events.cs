// <copyright file="Log.cs" company="Redforce04#4091">
// Copyright (c) Redforce04. All rights reserved.
// </copyright>
// -----------------------------------------
//    Solution:         RCVehicles
//    Project:          RCVehicles
//    FileName:         Events.cs
//    Author:           Redforce04#4091
//    Revision Date:    09/26/2023 12:22 AM
//    Created Date:     09/26/2023 12:22 AM
// -----------------------------------------

using Exiled.Events.Features;
using RCVehicles.EventArgs;

namespace RCVehicles;

public class Events
{
    /// <summary>
    /// Invoked when a <see cref="Exiled.API.Features.Player"/> attempts to spawn a vehicle.
    /// </summary>
    public static Event<SpawningVehicleEventArgs> SpawningVehicle { get; set; } = new();
    
    /// <summary>
    /// Invoked when a <see cref="Exiled.API.Features.Player"/> attempts to start a vehicle.
    /// </summary>
    public static Event<StartingVehicleEventArgs> StartingVehicle { get; set; } = new();
    
    /// <summary>
    /// Invoked when a <see cref="Exiled.API.Features.Player"/> attempts to enter a vehicle.
    /// </summary>
    public static Event<EnteringVehicleEventArgs> EnteringVehicle { get; set; } = new();
    
    /// <summary>
    /// Invoked when a <see cref="Exiled.API.Features.Player"/> attempts to leave a vehicle.
    /// </summary>
    public static Event<LeavingVehicleEventArgs> LeavingVehicle { get; set; } = new();
    
    /// <summary>
    /// Invoked when a <see cref="Exiled.API.Features.Player"/> attempts to repair a vehicle.
    /// </summary>
    public static Event<RepairingVehicleEventArgs> RepairingVehicle { get; set; } = new();

    /// <summary>
    /// Invoked when a <see cref="Exiled.API.Features.Player"/> attempts to reload a vehicle.
    /// </summary>
    public static Event<ReloadingVehicleEventArgs> ReloadingVehicle { get; set; } = new();
    
    /// <summary>
    /// Invoked when a <see cref="Exiled.API.Features.Player"/> attempts to damage a vehicle.
    /// </summary>
    public static Event<DamagingVehicleEventArgs> DamagingVehicle { get; set; } = new();
    
    /// <summary>
    /// Invoked when a <see cref="Exiled.API.Features.Player"/> attempts to destroy a vehicle.
    /// </summary>
    public static Event<DestroyingVehicleEventArgs> DestroyingVehicle { get; set; } = new();
    
    /// <summary>
    /// Called when <see cref="Exiled.API.Features.Player"/> attempts to spawn a vehicle.
    /// </summary>
    /// <param name="ev">The <see cref="SpawningVehicleEventArgs"/> instance.</param>
    public static void OnSpawningVehicle(SpawningVehicleEventArgs ev) => SpawningVehicle.InvokeSafely(ev);

    /// <summary>
    /// Called when <see cref="Exiled.API.Features.Player"/> attempts to start a vehicle.
    /// </summary>
    /// <param name="ev">The <see cref="StartingVehicleEventArgs"/> instance.</param>
    public static void OnStartingVehicle(StartingVehicleEventArgs ev) => StartingVehicle.InvokeSafely(ev);
    
    /// <summary>
    /// Called when <see cref="Exiled.API.Features.Player"/> attempts to enter a vehicle.
    /// </summary>
    /// <param name="ev">The <see cref="EnteringVehicleEventArgs"/> instance.</param>
    public static void OnEnteringingVehicle(EnteringVehicleEventArgs ev) => EnteringVehicle.InvokeSafely(ev);
    
    /// <summary>
    /// Called when <see cref="Exiled.API.Features.Player"/> attempts to leave a vehicle.
    /// </summary>
    /// <param name="ev">The <see cref="LeavingVehicleEventArgs"/> instance.</param>
    public static void OnLeavingVehicle(LeavingVehicleEventArgs ev) => LeavingVehicle.InvokeSafely(ev);
    
    /// <summary>
    /// Called when <see cref="Exiled.API.Features.Player"/> attempts to repair a vehicle.
    /// </summary>
    /// <param name="ev">The <see cref="RepairingVehicleEventArgs"/> instance.</param>
    public static void OnRepairingVehicle(RepairingVehicleEventArgs ev) => RepairingVehicle.InvokeSafely(ev);
    
    /// <summary>
    /// Called when <see cref="Exiled.API.Features.Player"/> attempts to reload a vehicle.
    /// </summary>
    /// <param name="ev">The <see cref="ReloadingVehicleEventArgs"/> instance.</param>
    public static void OnReloadingVehicle(ReloadingVehicleEventArgs ev) => ReloadingVehicle.InvokeSafely(ev);
    
    /// <summary>
    /// Called when <see cref="Exiled.API.Features.Player"/> attempts to damage a vehicle.
    /// </summary>
    /// <param name="ev">The <see cref="DamagingVehicleEventArgs"/> instance.</param>
    public static void OnDamagingVehicle(DamagingVehicleEventArgs ev) => DamagingVehicle.InvokeSafely(ev);
    
    /// <summary>
    /// Called when <see cref="Exiled.API.Features.Player"/> attempts to destroy a vehicle.
    /// </summary>
    /// <param name="ev">The <see cref="DestroyingVehicleEventArgs"/> instance.</param>
    public static void OnDestroyingVehicle(DestroyingVehicleEventArgs ev) => DestroyingVehicle.InvokeSafely(ev);
}