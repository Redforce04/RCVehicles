// <copyright file="Log.cs" company="Redforce04#4091">
// Copyright (c) Redforce04. All rights reserved.
// </copyright>
// -----------------------------------------
//    Solution:         RCVehicles
//    Project:          RCVehicles
//    FileName:         IRemoteControlledVehicle.cs
//    Author:           Redforce04#4091
//    Revision Date:    09/26/2023 12:51 AM
//    Created Date:     09/26/2023 12:51 AM
// -----------------------------------------

namespace RCVehicles.Interfaces;

/// <summary>
/// The wrapper interface for <see cref="RcVehicle"/>
/// </summary>
public interface IRemoteControlledVehicle
{
    /// <summary>
    /// Any information pertaining to the <see cref="RcVehicle"/> features.
    /// </summary>
    public RcVehicle RcVehicle { get; set; }
}