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
using System.Linq;
using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using Interfaces;

public class Remove : ICommand, IUsageProvider
{
    public string Command => "Remove";
    public string Description => "Spawns a vehicle.";
    public string[] Aliases => new string[] { "Destroy" };
    public string[] Usage => new[] { "%player%" };
    public const string Permission = "rc.vehicle.destroy";
    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, [UnscopedRef] out string response)
    {
        if (!sender.CheckPermission(Permission))
        {
            response = "You dont have permission to use this command.";
            return false;
        }

        if (arguments.Count < 1)
        {
            response = "You must specify the player to remove the vehicle from.";
            goto showDefaultArgs;
        }


        Player ply = Player.Get(arguments.At(0));
        if (ply is null)
        {
            response = $"Could not find player \"{arguments.At(0)}\".";
            goto showDefaultArgs;
        }

        VehicleObject? obj = VehicleObject.VehicleObjectInstances.FirstOrDefault(x => x.PlayersRidingVehicle.Contains(ply));
        if (obj is null)
        {
            response = $"Could not find a vehicle with {ply.Nickname} in it.";
            return false;
        }
        obj.RemoveVehicle();
        
        response = $"Successfully removed vehicle from player {ply.Nickname}";
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