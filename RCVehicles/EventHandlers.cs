// <copyright file="Log.cs" company="Redforce04#4091">
// Copyright (c) Redforce04. All rights reserved.
// </copyright>
// -----------------------------------------
//    Solution:         RCVehicles
//    Project:          RCVehicles
//    FileName:         EventHandlers.cs
//    Author:           Redforce04#4091
//    Revision Date:    09/26/2023 3:22 PM
//    Created Date:     09/26/2023 3:22 PM
// -----------------------------------------

namespace RCVehicles;

using API;
using Components;
using Exiled.Events.EventArgs.Player;

/// <summary>
/// The base class for events that are required for the plugin to work properly.
/// </summary>
internal class EventHandlers
{
    internal EventHandlers(){ }

    internal void RegisterEventHandlers()
    {
        Exiled.Events.Handlers.Player.DryfiringWeapon += OnDryfire;
    }
    
    internal void UnRegisterEventHandlers()
    {
        
    }

    private void OnDryfire(DryfiringWeaponEventArgs ev)
    {
        if (!VehicleControlInstance.TryGetInstance(ev.Player, out var instance))
        {
            return;
        }

        ev.IsAllowed = false;
        foreach (var vehicleWeaponObject in instance.Vehicle.WeaponObjects)
        {
            vehicleWeaponObject.Fire();
        }
    }
}