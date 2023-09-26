﻿// <copyright file="Log.cs" company="Redforce04#4091">
// Copyright (c) Redforce04. All rights reserved.
// </copyright>
// -----------------------------------------
//    Solution:         RCVehicles
//    Project:          RCVehicles
//    FileName:         AbstractedTypeExtensions.cs
//    Author:           Redforce04#4091
//    Revision Date:    09/26/2023 2:41 PM
//    Created Date:     09/26/2023 2:41 PM
// -----------------------------------------

namespace RCVehicles.Extensions;
using System;
using System.Collections.Generic;
using System.Reflection;

public static class AbstractedTypeExtensions
{
    /// <summary>
    /// Every instance of the type found in any loaded assembly will be instantiated and returned into list form.
    /// </summary>
    /// <param name="type">The type to instantiate instances of.</param>
    /// <returns>A list of all found instances of <see cref="T"/>.</returns>
    public static List<T> InstantiateAllInstancesOfType<T>()
        where T : class
    {
        Type type = typeof(T);
        List<T> instanceList = new List<T>();

        foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            try
            {
                foreach (Type typeInstance in HarmonyLib.AccessTools.GetTypesFromAssembly(assembly))
                {
                    if (typeInstance.IsAbstract || typeInstance.IsInterface)
                        continue;

                    if (!typeInstance.IsSubclassOf(type))
                        continue;

                    try
                    {
                        if (Activator.CreateInstance(typeInstance) is not T instance)
                            continue;

                        instanceList.Add(instance);
                    }
                    catch
                    {
                        //
                    }
                }
            }
            catch
            {
                //
            }
        }

        return instanceList;
    }
}