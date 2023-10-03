// <copyright file="Log.cs" company="Redforce04#4091">
// Copyright (c) Redforce04. All rights reserved.
// </copyright>
// -----------------------------------------
//    Solution:         RCVehicles
//    Project:          RCVehicles
//    FileName:         Tank.cs
//    Author:           Redforce04#4091
//    Revision Date:    09/26/2023 5:46 PM
//    Created Date:     09/26/2023 5:46 PM
// -----------------------------------------

namespace RCVehicles.ProvidedVehicles;

using Exiled.API.Features;
using Interfaces;

public class Tank : Vehicle
{
    public override string Name => "Tank";
    public override string Description => "A big beefy tank to carry players along";
    public override string Author => "Redforce04 and Scout Trooper";
    public override string SchematicName => "Tank1";
    public override int MaxPlayerCount { get; set; } = 3;
}