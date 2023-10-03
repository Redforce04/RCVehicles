// <copyright file="Log.cs" company="Redforce04#4091">
// Copyright (c) Redforce04. All rights reserved.
// </copyright>
// -----------------------------------------
//    Solution:         RCVehicles
//    Project:          RCVehicles
//    FileName:         Config.cs
//    Author:           Redforce04#4091
//    Revision Date:    09/25/2023 11:53 PM
//    Created Date:     09/25/2023 11:53 PM
// -----------------------------------------

namespace RCVehicles
{
    using System.IO;
    using Exiled.API.Interfaces;

    public class RCConfig : IConfig
    {
        public bool IsEnabled { get; set; } = true;

        public bool Debug { get; set; } = false;
        // public string SchematicLocation { get; set; } = Path.Combine(Exiled.API.Features.Paths.Configs, "RCVehicles", "Schematics");
    }
}