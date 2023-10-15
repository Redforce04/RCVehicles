// <copyright file="Log.cs" company="Redforce04#4091">
// Copyright (c) Redforce04. All rights reserved.
// </copyright>
// -----------------------------------------
//    Solution:         RCVehicles
//    Project:          RCVehicles
//    FileName:         PlayerControlVehicleComponent.cs
//    Author:           Redforce04#4091
//    Revision Date:    10/04/2023 11:39 AM
//    Created Date:     10/04/2023 11:39 AM
// -----------------------------------------

namespace RCVehicles.Components;

using System;
using System.Collections.Generic;
using API;
using Commands;
using Exiled.API.Features;
using Exiled.API.Features.Toys;
using Interfaces;
using MEC;
using UnityEngine;
using System.Linq;
using Player = Exiled.API.Features.Player;

public class VehicleController
{
    /// <summary>
    /// A list of vehicle instances currently being controlled.
    /// </summary>
    public static List<VehicleControlInstance> ControlInstances { get; private set; }

    public static VehicleController Singleton { get; private set; }
    public static float TurnMultiplier { get; set; } = 1f;
    internal static CoroutineHandle MovementProcessingCoroutineHandle { get; private set; }

    static VehicleController()
    {
        ControlInstances = new List<VehicleControlInstance>();
    }

    public static void StopVehicleCoroutines()
    {
        Singleton._running = false;
    }

    public VehicleController()
    {
        if (Singleton is not null)
        {
            return;
        }

        Singleton = this;
        Log.Debug("Starting Control Coroutine.");
        MovementProcessingCoroutineHandle =
            Timing.RunCoroutine(ProcessVehicleMovementCoroutine(), "VehicleMovementCoroutine");
    }

    private bool _running = true;

    public IEnumerator<float> ProcessVehicleMovementCoroutine()
    {
        int i = 0;
        const int skip = 80;
        Log.Debug("Control Coroutine Started.");
        List<VehicleControlInstance> instancesToRemove = new List<VehicleControlInstance>();
        while (_running)
        {
            bool log = (i == skip);
            foreach (var instance in ControlInstances)
            {
                if (instance.Player is null)
                {
                    Log.Debug("Removing Instance - Player is null");
                    instancesToRemove.Add(instance);
                    continue;
                }

                if (instance.Vehicle is null)
                {
                    Log.Debug("Removing Instance - Vehicle is null");
                    instancesToRemove.Add(instance);
                    continue;
                }

                _processPosition(instance, log);
                _processRotation(instance, log);
                _updateViewFinder(instance, log);
            }

            ProcessDebugInfo();
            foreach (var instance in instancesToRemove)
            {
                ControlInstances.Remove(instance);
            }
            
            if (i == skip)
            {
                i = -1;
            }

            i++;
            yield return Timing.WaitForOneFrame;
        }

        Log.Debug("Stopped - destroying instance.");
        Singleton = null;
    }

    private void ProcessDebugInfo()
    {
        if (Relational.Player is null)
        {
            Log.Debug("Player is null");
            return;
        }

        if (Relational.Primitive is null)
        {
            Log.Debug("Primitive is null");
            return;
        }

        VehicleObject? obj = VehicleObject.VehicleObjectInstances.FirstOrDefault();
        if (obj is null)
        {
            Log.Debug("VehicleObject is null");
            return;
        }
        Player ply = Relational.Player;
        Vector3 position = ply.CameraTransform.position +
                           (ply.CameraTransform.forward * 1);
        Relational.Primitive.Position = position;
        Relational.Primitive.Rotation = Quaternion.Euler(ply.Transform.forward);
        var transform = obj.Schematic.transform;
        Vector3 schematicPos = obj.Schematic.Position;
        Vector3 schematicRot = transform.rotation.eulerAngles;
        Vector3 schematicForward = transform.forward;
        
        
        Vector3 offsetPos = position - schematicPos;
        Vector3 offsetRot = Relational.Primitive.Rotation.eulerAngles - schematicRot;
        ply.ShowHint(new Hint($"Offset Pos: ({offsetPos.x:F}, {offsetPos.y:F}, {offsetPos.z:F})\nOffset Rot: ({offsetRot.x:F}, {offsetRot.y:F}, {offsetRot.z:F})", Timing.DeltaTime));
    }
    
    
    private void _updateViewFinder(VehicleControlInstance instance, bool log)
    {
        if (instance.Player.CurrentItem.Type != ItemType.GunRevolver)
        {
            instance.ViewFinder.Position = new Vector3(0, 700, 0);
            instance.ViewFinderLight.Position = new Vector3(0, 700, 0);
            return;
        }
        

        float distance = 10f;
        RaycastHit hit;
        if (Physics.Raycast(instance.Player.Transform.position, instance.Player.Transform.forward, out hit, 10))
        {
            distance = hit.distance - .3f;
        }

        Vector3 position = instance.Player.CameraTransform.position +
                           (instance.Player.CameraTransform.forward * distance);
        instance.ViewFinder.Position = position;
        instance.ViewFinderLight.Position = position;
        // instance.ViewFinder.Rotation = instance.Player.Transform.rotation;
    }

    private void _processPosition(VehicleControlInstance instance, bool log)
    {

    }

    private void _processRotation(VehicleControlInstance instance, bool log)
    {

        // The current rotation of the vehicle
        float curVehicleRotation = instance.Vehicle.Schematic.gameObject.transform.rotation.eulerAngles.y +
                                   instance.Vehicle.BaseVehicle.TurnOffset;

        if (RCPlugin.Singleton.Config.AimMode == AimMode.Raycast)
        {
            RaycastHit hit;
            float distance = 10f;
            Vector3 position = instance.Player.CameraTransform.position +
                               (instance.Player.CameraTransform.forward * distance);
            if (Physics.Raycast(instance.Player.Transform.position, instance.Player.Transform.forward, out hit, 10))
            {
                position = hit.point;
            }

            if (Mathf.Abs(curVehicleRotation - position.y) <= 0.0f)
            {
                updateViewFinder(true, instance);
                return;
            }
            updateViewFinder(false, instance);

            Vector3 curPos = new Vector3(0, curVehicleRotation, 0);
            Vector3 targetPos = new Vector3(0, position.y, 0);
            Vector3 newPos = instance.Vehicle.Schematic.gameObject.transform.rotation.eulerAngles;
            float y = Vector3.RotateTowards(curPos, targetPos, instance.MaxTurnSpeed * Mathf.Deg2Rad * Timing.DeltaTime, 0).y;
            newPos.y = y;
            newPos.x = 0;
            newPos.z = 0;
            instance.Vehicle.Schematic.gameObject.transform.rotation = Quaternion.LookRotation(newPos);
            return;
        }

        // Current rotation of the player.
        float curPlyRotation = instance.Player.Transform.rotation.eulerAngles.y;

        // The difference from the player rotation to the vehicle rotation. 
        float rotDiff = curPlyRotation - curVehicleRotation; // * TurnMultiplier * Timing.DeltaTime;
        if (rotDiff > 180)
        {
            rotDiff = (360 - rotDiff) * -1;
        }

        // Absolute value of the rotation difference.
        float absRotDiff = Math.Abs(rotDiff);

        // Too much turning.
        if (instance.MaxTurnSpeed > 0 && absRotDiff > instance.MaxTurnSpeed)
        {
            float amountToRotate = (rotDiff > 0 ? instance.MaxTurnSpeed : instance.MaxTurnSpeed * -1);
            instance.Vehicle.Schematic.gameObject.transform.Rotate(0, amountToRotate, 0);
            updateViewFinder(false, instance);

        }
        // Turn speed is allowed. Maybe add a curve to the end of the turn.
        else if (instance.MaxTurnSpeed > 0 && absRotDiff > 0.1f)
        {
            instance.Vehicle.Schematic.gameObject.transform.Rotate(0, rotDiff, 0);
            updateViewFinder(true, instance);
        }
    }

    private void updateViewFinder(bool locked, VehicleControlInstance instance)
    {
        if (instance.Player.CurrentItem.Type != ItemType.GunRevolver)
        {
            return;
        }

        if (locked && !instance.PreviouslyStill)
        {
            instance.PreviouslyStill = true;
            try
            {
                instance.ViewFinder.Color = Color.green;
                instance.ViewFinderLight.Color = Color.green;
            }
            catch (Exception e)
            {
                Log.Error($"Viewfinder2 exception: {e}");
            }
        }
        else if(instance.PreviouslyStill)
        {
            instance.PreviouslyStill = false;
            try
            {
                instance.ViewFinder.Color = Color.red;
                instance.ViewFinderLight.Color = Color.red;
            }
            catch (Exception e)
            {
                Log.Error($"Viewfinder exception: {e}");
            }
        }
    }
}

