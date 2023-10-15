// <copyright file="Log.cs" company="Redforce04#4091">
// Copyright (c) Redforce04. All rights reserved.
// </copyright>
// -----------------------------------------
//    Solution:         RCVehicles
//    Project:          RCVehicles
//    FileName:         Spawn.cs
//    Author:           Redforce04#4091
//    Revision Date:    09/26/2023 5:25 PM
//    Created Date:     09/26/2023 5:25 PM
// -----------------------------------------

namespace RCVehicles.Commands.VehicleCommands;

using System;
using System.Diagnostics.CodeAnalysis;
using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using Interfaces;

public class Spawn : ICommand, IUsageProvider
{
    public string Command => "Spawn";
    public string Description => "Spawns a vehicle.";
    public string[] Aliases => Array.Empty<string>();
    public string[] Usage => new[] { "%player%", "Vehicle Name" };
    public const string Permission = "rc.vehicle.spawn";
    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, [UnscopedRef] out string response)
    {
        if (!sender.CheckPermission(Permission))
        {
            response = "You dont have permission to use this command.";
            return false;
        }

        if (arguments.Count < 1)
        {
            response = "You must specify a player to spawn the vehicle on.";
            goto showDefaultArgs;
        }
        if (arguments.Count < 2)
        {
            response = "You must specify a vehicle to spawn.";
            goto showDefaultArgs;
        }


        Player ply = Player.Get(arguments.At(0));
        if (ply is null)
        {
            response = $"Could not find player \"{arguments.At(0)}\".";
            goto showDefaultArgs;
        }

        Vehicle? vehicle = Vehicle.Get(arguments.At(1));
        if (vehicle is null)
        {
            response = $"Could not find vehicle \"{arguments.At(1)}\".";
            goto showDefaultArgs;
        }
        var vehicleObject = vehicle.SpawnVehicle(ply);

        if (vehicleObject is null)
        {
            response = $"Could not spawn vehicle {vehicle.Name}. Try checking console for more information.";
            return false;
        }
        vehicleObject.SetDriver(ply);
        response = $"Successfully spawned vehicle {vehicle.Name} for player {ply.Nickname}. (Vehicle Id: {vehicleObject.Id})";
        return true;
        
        showDefaultArgs:
        response += $"\nCommand \"{this.Command}\" Usage:";
        response += $"\"Vehicle {this.Command} \"";
        foreach (string arg in this.Usage)
        {
            response += $"[{arg}] ";
        }
        return false;
    }

}