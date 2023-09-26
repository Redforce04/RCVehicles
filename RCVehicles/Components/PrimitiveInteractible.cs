// <copyright file="Log.cs" company="Redforce04#4091">
// Copyright (c) Redforce04. All rights reserved.
// </copyright>
// -----------------------------------------
//    Solution:         RCVehicles
//    Project:          RCVehicles
//    FileName:         PrimitiveInteractible.cs
//    Author:           Redforce04#4091
//    Revision Date:    09/26/2023 2:39 PM
//    Created Date:     09/26/2023 2:39 PM
// -----------------------------------------

using Interactables;
using Interactables.Verification;
using Mirror;

namespace RCVehicles.Components;

public class PrimitiveInteractible : NetworkBehaviour, IInteractable
{
    public IVerificationRule VerificationRule => StandardDistanceVerification.Default;

    [Server]
    public void ServerInteract(ReferenceHub ply, byte colliderId)
    {
        // Exiled.API.Features.Log.Debug($"Interactible has been pressed.");
    }
}