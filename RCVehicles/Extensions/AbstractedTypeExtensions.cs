// <copyright file="Log.cs" company="Redforce04#4091">
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
using System.Linq;
using System.Reflection;

public static class AbstractedTypeExtensions
{
    /// <summary>
    /// Every instance of the type found in any loaded assembly will be instantiated and returned into list form.
    /// </summary>
    /// <param name="type">The type to instantiate instances of.</param>
    /// <returns>A list of all found instances of <see cref="type"/>.</returns>
    public static List<object> InstantiateAllInstancesOfType(this Type type)
    {
        try
        {

            if (type == null)
            {
                return new List<object>();
            }

            List<object> instanceList = new List<object>();
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                try
                {

                    foreach (Type typeInstance in assembly.DefinedTypes)
                    {
                        if (typeInstance.IsAbstract || typeInstance.IsInterface)
                            continue;

                        if (typeInstance.BaseType != type)
                        {
                            continue;
                        }

                        object instance = Activator.CreateInstance(typeInstance);
                        if (instance is null)
                        {
                            continue;
                        }

                        try
                        {
                            instance = Convert.ChangeType(instance, type);
                        }
                        catch (Exception e)
                        {
                            continue;
                        }

                        instanceList.Add(instance);
                    }
                }
                catch (Exception)
                {
                    // uh oh - just iterate past and ignore :trol:
                }
            }

            return instanceList;
        }
        catch (Exception)
        {
            // this would be really bad but we can just catch and return empty to say null safe.
            return new List<object>();
        }
    }

    /// <summary>
    /// Every instance of the type found in any loaded assembly will be instantiated and returned into list form.
    /// </summary>
    /// <typeparam name="T">The type to instantiate instances of.</typeparam>
    /// <returns>A list of all found instances of <see cref="T"/>.</returns>
    public static List<T> InstantiateAllInstancesOfType<T>()
    {
        try
        {
            List<T> nullSafe = (List<T>)InstantiateAllInstancesOfType(typeof(T)).Cast<List<T>>();
            return nullSafe;
        }
        catch (Exception)
        {
            return new List<T>();
        }
    }
}