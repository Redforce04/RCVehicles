// <copyright file="Log.cs" company="Redforce04#4091">
// Copyright (c) Redforce04. All rights reserved.
// </copyright>
// -----------------------------------------
//    Solution:         RCVehicles
//    Project:          RCVehicles
//    FileName:         TankWeapon.cs
//    Author:           Redforce04#4091
//    Revision Date:    10/14/2023 6:01 PM
//    Created Date:     10/14/2023 6:01 PM
// -----------------------------------------

namespace RCVehicles.ProvidedVehicles;

using System.Collections.Generic;
using System.Numerics;
using AdminToys;
using CustomPlayerEffects;
using Decals;
using Exiled.API.Features;
using Exiled.API.Features.DamageHandlers;
using Interfaces;
using InventorySystem.Items.Firearms;
using InventorySystem.Items.Firearms.BasicMessages;
using InventorySystem.Items.Firearms.Modules;
using MEC;
using Exiled.API.Extensions;
using Exiled.API.Features.Toys;
using PlayerStatsSystem;
using UnityEngine;
using Utils.Networking;
using YamlDotNet.Serialization;
using Firearm = Exiled.API.Features.Items.Firearm;
using Player = Exiled.API.Features.Player;
using Vector3 = UnityEngine.Vector3;

public class TankWeapon : VehicleWeapon
{
    public override string Name { get; set; } = "Turret";
    public override int MaxAmmo { get; set; } = 500;
    public override float Health { get; set; } = 300f;
    public float FireRateDelay { get; set; } = 0.1f;
    public int BulletsPerBurst { get; set; } = 10;
    private float DamagePerBullet { get; set; } = 10f;
    private float MaxDistance { get; set; } = 30f;
    private CoroutineHandle BurstCoroutine { get; set; }
    protected override void OnFiring(VehicleWeaponObject weaponObject)
    {
        if (BurstCoroutine.IsRunning)
        {
            // Log.Debug("Burst Coroutine Is already running.");
            return;
        }
        Log.Debug("Started Burst Coroutine.");
        BurstCoroutine = Timing.RunCoroutine(FireBurst(weaponObject), "Burst-Firing-Coroutine");
    }

    private void ProcessBullet(VehicleWeaponObject weaponObject)
    {
        // todo = add offset
        var schematic = weaponObject.Vehicle.Schematic;
        Vector3 transform = schematic.transform.forward;
        transform.y += weaponObject.BaseVehicle.TurnOffset;
        //Ray ray, RaycastHit hit
        Ray ray = new Ray(schematic.Position,transform);
        if (!Physics.Raycast(ray, out RaycastHit hit, MaxDistance, StandardHitregBase.HitregMask))
        {
            return;
        }
        IDestructible destructible;
        foreach (Player ply in Player.List)
        {
            ply.PlayGunSound(schematic.Position, ItemType.GunLogicer, 100, 0);
        }
        if (hit.collider.TryGetComponent<IDestructible>(out destructible))
        {
            if (destructible.Damage(DamagePerBullet, new CustomReasonDamageHandler("Hit by a bullet", DamagePerBullet), hit.point))
            {
                ReferenceHub referenceHub;
                if (!ReferenceHub.TryGetHubNetID(destructible.NetworkId, out referenceHub) || !referenceHub.playerEffectsController.GetEffect<Invisible>().IsEnabled)
                {
                    // Player ply;
                    // ply.ShowHitMarker(1f);
                    // Hitmarker.SendHitmarker(weap, 1f);
                }
                // base.PlaceBloodDecal(ray, hit, destructible);
                Map.PlaceBlood(hit.point, ray.direction);
            }
        }
        else
        {
            var primitive = Exiled.API.Features.Toys.Primitive.Create(PrimitiveType.Sphere, hit.point, null, new Vector3(0.1f, 0.1f, 0.1f), true);
            primitive.Color = Color.blue;
            primitive.Collidable = false;
            new GunDecalMessage(hit.point + (ray.origin - hit.point).normalized, ray.direction, DecalPoolType.Bullet).SendToAuthenticated(0);
        }
    }
    private IEnumerator<float> FireBurst(VehicleWeaponObject weaponObject)
    {
        Log.Debug("Firing Bullets.");
        for (int i = 0; i < BulletsPerBurst; i++)
        {
            if (weaponObject.Ammo <= 0)
            {
                Log.Debug("Out of ammo");
                yield break;
            }
            ProcessBullet(weaponObject);
            weaponObject.Ammo--;
            yield return Timing.WaitForSeconds(FireRateDelay);
        }
        Log.Debug("Done Firing.");
    }

    protected override void OnDamaging(VehicleWeaponObject weaponObject)
    {
        base.OnDamaging(weaponObject);
    }
}