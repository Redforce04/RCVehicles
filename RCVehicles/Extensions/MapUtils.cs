// <copyright file="Log.cs" company="Redforce04#4091">
// Copyright (c) Redforce04. All rights reserved.
// </copyright>
// -----------------------------------------
//    Solution:         RCVehicles
//    Project:          RCVehicles
//    FileName:         MapUtils.cs
//    Author:           Redforce04#4091
//    Revision Date:    09/26/2023 5:19 PM
//    Created Date:     09/26/2023 5:19 PM
// -----------------------------------------

namespace RCVehicles.Extensions;

using System.IO;
using MapEditorReborn.API.Features.Serializable;
using Utf8Json;

public static class MapUtils
{
    /*public static SchematicObjectDataList GetSchematicDataByName(this string schematicName)
    {
        string dirPath = Path.Combine(RCPlugin.Singleton.Config.SchematicLocation, schematicName);

        // Log.Debug($"Path exists: {Directory.Exists(dirPath)}, Directory Path: {dirPath}");
        if (!Directory.Exists(dirPath))
            return null;

        string schematicPath = Path.Combine(dirPath, $"{schematicName}.json");
        // Log.Debug($"File Exists: {File.Exists(schematicPath)}, Schematic Path: {schematicPath}");
        if (!File.Exists(schematicPath))
            return null;

        SchematicObjectDataList data =
            JsonSerializer.Deserialize<SchematicObjectDataList>(File.ReadAllText(schematicPath));
        data.Path = dirPath;

        return data;
    }*/
}