// <copyright file="Log.cs" company="Redforce04#4091">
// Copyright (c) Redforce04. All rights reserved.
// </copyright>
// -----------------------------------------
//    Solution:         RCVehicles
//    Project:          RCVehicles
//    FileName:         Debug.cs
//    Author:           Redforce04#4091
//    Revision Date:    10/04/2023 1:28 PM
//    Created Date:     10/04/2023 1:28 PM
// -----------------------------------------

namespace RCVehicles.Commands;

using System;
using System.Diagnostics.CodeAnalysis;
using CommandSystem;

public class Debug : ParentCommand
{
    public override string Command => "Debug";
    public override string[] Aliases => new string[] { "dbg" };
    public override string Description => "Debug functions for developers.";
    public Debug() => LoadGeneratedCommands();
    public override void LoadGeneratedCommands()
    {
        RegisterCommand(new TurnMult());
        RegisterCommand(new Mass());
        RegisterCommand(new Control());
        RegisterCommand(new Relational());
    }

    protected override bool ExecuteParent(ArraySegment<string> arguments, ICommandSender sender, [UnscopedRef] out string response)
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