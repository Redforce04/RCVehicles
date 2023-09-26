// <copyright file="Log.cs" company="Redforce04#4091">
// Copyright (c) Redforce04. All rights reserved.
// </copyright>
// -----------------------------------------
//    Solution:         RCVehicles
//    Project:          RCVehicles
//    FileName:         RemoteControlledVehicle.cs
//    Author:           Redforce04#4091
//    Revision Date:    09/26/2023 12:51 AM
//    Created Date:     09/26/2023 12:51 AM
// -----------------------------------------

using System.Collections.Generic;
using Exiled.API.Features;

namespace RCVehicles.Interfaces;

public class RcVehicle
{
    public RcVehicle()
    {
        PlayersBodies = new List<Npc>();
    }
    private List<Npc> PlayersBodies { get; set; }
    
    /// <summary>
    /// Spawns an npc in a player's position. This allows other players to see the person "piloting" the rc vehicle.
    /// </summary>
    /// <param name="ply"></param>
    public void SpawnNPCForPlayer(Player ply)
    {
        // check for npc death, player death, player leave, etc...
        Npc npc = Npc.Spawn($"{(ply.HasCustomName ? ply.CustomName : ply.Nickname)}*", ply.Role, 0, "", ply.Position);
        npc.Transform.rotation = ply.Transform.rotation;
        npc.
    }
    
    /// <summary>
    /// If true, the player can be killed while driving the vehicle.
    /// </summary>
    public bool PlayerCanBeKilledWhilePilotingVehicle { get; set; }
}