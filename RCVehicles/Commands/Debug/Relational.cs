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
using Exiled.API.Features.Toys;
using Exiled.Permissions.Extensions;
using Interfaces;
using UnityEngine;

public class Relational : ICommand
{
    public string Command => "relational";
    public string[] Aliases => Array.Empty<string>();
    public string Description => "Processes relational distances transforms etc...";
    public string Permission => "rc.debug";
    public static Player? Player { get; set; }
    public static Primitive? Primitive { get; set; }
    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, [UnscopedRef] out string response)
    {
        if (!sender.CheckPermission(Permission))
        {
            response = "You dont have permission to use this command.";
            return false;
        }
        Player ply = Player.Get(sender);

        if (Player is null)
        {
            if (Primitive is null)
            {
                Primitive = Primitive.Create(PrimitiveType.Sphere, null, null, new Vector3(0.2f, 0.2f, 0.2f), true);
                Primitive.Collidable = false;
                Primitive.Color = Color.cyan;
            }
            
            Player = ply;
        }
        else
        {
            if (Primitive is not null)
                Primitive = null;
            Player = null;
        }
        
        response = $"Relational data toggled.";
        return true;

        showDefaultArgs:
        response += $"\nCommand \"{this.Command}\" Usage:";
        response += $"\"Vehicle Debug {this.Command} \"";
        return false;
    }

}