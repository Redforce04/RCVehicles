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
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using Interfaces;

public class Control : ICommand, IUsageProvider
{
    public string Command => "control";
    public string[] Aliases => Array.Empty<string>();
    public string Description => "Allows you to control a vehicle.";
    public string[] Usage { get; } = new string[] { "Vehicle Id" };
    public string Permission => "rc.debug";
    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, [UnscopedRef] out string response)
    {
        if (!sender.CheckPermission(Permission))
        {
            response = "You dont have permission to use this command.";
            return false;
        }

        if (arguments.Count < 1)
        {
            response = $"You must select a vehicle to control.";
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
            response = $"Could not find vehicle {vehicleId}.";
            string vehicleList = "";
            foreach (var vehicle in VehicleObject.VehicleObjectInstances)
            {
                vehicleList += $"{vehicle.Id}, ";
            }

            response += $"Available vehicles: {(vehicleList.Length > 1 ? (vehicleList+ ",").Replace(", ,", "") : "")}";
            goto showDefaultArgs;
        }

        var ply = Player.Get(sender);
        if (obj.ControlComponent is not null && obj.ControlComponent.Player == ply)
        {
            obj.RemoveDriver();
            response = "Successfully removed your control of the vehicle.";
            return true;
        }
        
        obj.SetDriver(ply, true);
        response = $"Successfully set you as the new driver of the vehicle.";
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