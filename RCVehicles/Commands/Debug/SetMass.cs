// <copyright file="Log.cs" company="Redforce04#4091">
// Copyright (c) Redforce04. All rights reserved.
// </copyright>
// -----------------------------------------
//    Solution:         RCVehicles
//    Project:          RCVehicles
//    FileName:         TurnMult.cs
//    Author:           Redforce04#4091
//    Revision Date:    10/04/2023 1:29 PM
//    Created Date:     10/04/2023 1:29 PM
// -----------------------------------------

namespace RCVehicles.Commands;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using CommandSystem;
using Components;
using Exiled.Permissions.Extensions;
using Interfaces;

public class Mass : ICommand, IUsageProvider
{
    public string Command => "mass";
    public string[] Aliases => Array.Empty<string>();
    public string Description => "Changes the mass for a vehicle.";
    public string[] Usage { get; } = new string[] { "Vehicle Id", "New Multiplier" };
    public string Permission => "rc.debug";
    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, [UnscopedRef] out string response)
    {
        if (!sender.CheckPermission(Permission))
        {
            response = "You dont have permission to use this command.";
            return false;
        }

        if (arguments.Count < 2)
        {
            response = $"You must select a vehicle to change the mass of.";
            goto showDefaultArgs;
        }
        if (!int.TryParse(arguments.At(0), out int vehicleId))
        {
            response = "You must specify a valid vehicle instance to modify.";
            goto showDefaultArgs;
        }

        var obj = VehicleObject.VehicleObjectInstances.FirstOrDefault(x => x.Id == vehicleId);
        if (obj is null)
        {
            response = $"Could not find vehicle \"{arguments.At(0)}\".";
            goto showDefaultArgs;
        }

        if (!float.TryParse(arguments.At(1), out float mult))
        {
            response = $"Could not parse mass \"{arguments.At(1)}\"";
            goto showDefaultArgs;
        }

        obj.Mass = mult;
        response = $"The mass of the vehicle has been set to {mult}.";
        return true;

        showDefaultArgs:
        response += $"\nCommand \"{this.Command}\" Usage:";
        response += $"\"Vehicle Debug {this.Command} \"";
        foreach (string arg in this.Usage)
        {
            response += $"[{arg}] ";
        }
        return false;
    }

}