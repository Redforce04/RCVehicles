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

using Exiled.API.Interfaces;

namespace RCVehicles
{
    public class RCConfig : IConfig
    {
        public bool IsEnabled { get; set; }
        public bool Debug { get; set; }
    }
}