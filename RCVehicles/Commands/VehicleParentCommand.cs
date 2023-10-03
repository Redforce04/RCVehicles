// <copyright file="Log.cs" company="Redforce04#4091">
// Copyright (c) Redforce04. All rights reserved.
// </copyright>
// -----------------------------------------
//    Solution:         RCVehicles
//    Project:          RCVehicles
//    FileName:         VehicleParentCommand.cs
//    Author:           Redforce04#4091
//    Revision Date:    09/26/2023 5:23 PM
//    Created Date:     09/26/2023 5:23 PM
// -----------------------------------------

namespace RCVehicles.Commands;

using System;
using System.Diagnostics.CodeAnalysis;
using CommandSystem;
using RCVehicles.Commands.VehicleCommands;

[CommandHandler(typeof(RemoteAdminCommandHandler))]
[CommandHandler(typeof(GameConsoleCommandHandler))]
public class VehicleParentCommand : ParentCommand
{
    public override string Command => "Vehicle";
    public override string Description => "Allows for spawning and modifying vehicles.";
    public override string[] Aliases => Array.Empty<string>();
    
    public VehicleParentCommand() => LoadGeneratedCommands();
    public override void LoadGeneratedCommands()
    {
        RegisterCommand(new List());
        RegisterCommand(new Spawn());
        RegisterCommand(new Remove());
    }

    protected override bool ExecuteParent(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        response = "Please enter a valid subcommand: \n";

        foreach (var x in this.Commands)
        {
            string args = "";
            if (x.Value is IUsageProvider usage)
            {
                foreach (string arg in usage.Usage)
                {
                    args += $"[{arg}] ";
                }
            }

            if (sender is not ServerConsoleSender)
                response += $"<color=yellow> {x.Key} {args}<color=white>-> {x.Value.Description}. \n";
            else
                response += $" {x.Key} {args} -> {x.Value.Description}. \n";
        }
        return false;
    }
}