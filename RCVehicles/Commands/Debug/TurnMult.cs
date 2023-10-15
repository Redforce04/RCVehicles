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
using API;
using CommandSystem;
using Components;
using Exiled.Permissions.Extensions;
using Interfaces;

public class TurnMult : ICommand, IUsageProvider
{
    public string Command => "multiplier";
    public string[] Aliases => new string[] { "mult" };
    public string Description => "Changes the multiplier for turning arguments. If no vehicle is specified, the global multiplier will be used.";
    public string[] Usage { get; } = new string[] { "Vehicle Id*", "New Multiplier" };
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
            response = $"Current Turn float multiplier: {VehicleController.TurnMultiplier}";
            
        }

        if (arguments.Count < 2)
        {

            if (!float.TryParse(arguments.At(0), out float newMult))
            {
                response = "You must specify a valid float for the new multiplier.";
                goto showDefaultArgs;
            }
            VehicleController.TurnMultiplier = newMult;

            response = $"The Global vehicle turn float multiplier has been set to {newMult}.";
            return true;
        }
        if (!int.TryParse(arguments.At(0), out int vehicleId))
        {
            response = "You must specify a valid vehicle instance to modify.";
            goto showDefaultArgs;
        }

        var obj = VehicleObject.VehicleObjectInstances.FirstOrDefault(x => x.Id == vehicleId);
        if (obj is null)
        {
            response = $"Could not find vehicle \"{arguments.At(0)}\". Perhaps you meant to specify a global turn modifier?";
            goto showDefaultArgs;
        }

        if (!float.TryParse(arguments.At(1), out float mult))
        {
            response = $"Could not parse multiplier \"{arguments.At(1)}\"";
            goto showDefaultArgs;
        }

        obj.MaxTurnSpeed = mult;
        response = $"The max turn speed has been set to {mult}.";
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