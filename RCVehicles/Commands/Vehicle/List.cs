// <copyright file="Log.cs" company="Redforce04#4091">
// Copyright (c) Redforce04. All rights reserved.
// </copyright>
// -----------------------------------------
//    Solution:         RCVehicles
//    Project:          RCVehicles
//    FileName:         List.cs
//    Author:           Redforce04#4091
//    Revision Date:    09/26/2023 5:41 PM
//    Created Date:     09/26/2023 5:41 PM
// -----------------------------------------

namespace RCVehicles.Commands.VehicleCommands;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using CommandSystem;
using Interfaces;

public class List : ICommand
{
    public string Command => "List";
    public string Description => "Lists the available vehicles to spawn.";
    public string[] Aliases => Array.Empty<string>();
    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, [UnscopedRef] out string response)
    {
        response = "Available Vehicles: \n";
        foreach (var vehicle in Vehicle.RegisteredVehicles)
        {
            if(sender is not ServerConsoleSender)
                response += $"  {vehicle.Name} - {vehicle.Description} \n";
            else
                response += $"  <color=yellow>{vehicle.Name}</color> - {vehicle.Description} \n";
        }

        return true;
    }

}