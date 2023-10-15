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
    using System.ComponentModel;
    using System.IO;
    using Exiled.API.Interfaces;

    public class RCConfig : IConfig
    {
        public bool IsEnabled { get; set; } = true;

        public bool Debug { get; set; } = false;
        
        [Description("The method of aiming to use. Available Modes: Raycast and Transform.")]
        public AimMode AimMode { get; set; } = AimMode.Transform;
    }

    public enum AimMode
    {
        [Description("The vehicle will aim wherever the player aims.")]
        Raycast,
        [Description("The vehicle will aim in the same general direction as the player.")]
        Transform
    }
}