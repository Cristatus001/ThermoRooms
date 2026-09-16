using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class ObjectDataBulkCreator : EditorWindow
{
    private struct ObjectEntry
    {
        public string assetName;
        public string objectName;
        public bool room1;
        public bool room2;
        public bool room3;

        public ObjectEntry(string asset, string name, bool r1, bool r2, bool r3)
        {
            assetName = asset;
            objectName = name;
            room1 = r1;
            room2 = r2;
            room3 = r3;
        }
    }

    private static List<ObjectEntry> GetAllEntries()
    {
        return new List<ObjectEntry>
        {
            // Room 1 - Open Systems (correct)
            new ObjectEntry("Obj_BoilingTeaOpenCup", "Boiling tea in open cup", true, false, false),
            new ObjectEntry("Obj_BurningCandle", "Burning candle", true, false, false),
            new ObjectEntry("Obj_OpenPerfumeBottle", "Open perfume bottle", true, false, false),
            new ObjectEntry("Obj_HumanBreathing", "Human breathing", true, false, false),
            new ObjectEntry("Obj_HotSoupBowl", "Hot soup in bowl", true, false, false),
            new ObjectEntry("Obj_OpenAquarium", "Open aquarium", true, false, false),
            new ObjectEntry("Obj_RunningCeilingFan", "Running ceiling fan motor", true, false, false),
            new ObjectEntry("Obj_OpenFlowerVase", "Open flower vase with water", true, false, false),
            new ObjectEntry("Obj_GasStoveFlame", "Gas stove flame", true, false, false),
            new ObjectEntry("Obj_OpenJuiceGlass", "Open juice glass with straw", true, false, false),

            // Room 2 - Closed Systems (correct)
            new ObjectEntry("Obj_SealedWaterBottle", "Sealed water bottle", false, true, false),
            new ObjectEntry("Obj_ClosedPressureCooker", "Closed pressure cooker", false, true, false),
            new ObjectEntry("Obj_AirtightFoodContainer", "Airtight food container", false, true, false),
            new ObjectEntry("Obj_SealedJamBottle", "Sealed jam bottle", false, true, false),
            new ObjectEntry("Obj_ClosedSoftDrinkCan", "Closed soft drink can", false, true, false),
            new ObjectEntry("Obj_LPGCylinderClosed", "LPG gas cylinder with valve closed", false, true, false),
            new ObjectEntry("Obj_ClosedSyringe", "Closed syringe", false, true, false),
            new ObjectEntry("Obj_SealedPerfumeBottle", "Sealed perfume bottle", false, true, false),
            new ObjectEntry("Obj_BatteryCell", "Battery cell", false, true, false),
            new ObjectEntry("Obj_InflatedSealedFootball", "Inflated sealed football", false, true, false),

            // Room 3 - Isolated Systems (correct)
            new ObjectEntry("Obj_ThermosFlask", "Thermos flask", false, false, true),
            new ObjectEntry("Obj_VacuumFlask", "Vacuum flask", false, false, true),
            new ObjectEntry("Obj_InsulatedIceBox", "Insulated ice box", false, false, true),
            new ObjectEntry("Obj_ScientificCalorimeter", "Scientific calorimeter", false, false, true),
            new ObjectEntry("Obj_SpaceSatelliteChamber", "Space satellite thermally insulated chamber", false, false, true),
            new ObjectEntry("Obj_IceCoolerThickInsulation", "Ice cooler with thick insulation", false, false, true),
            new ObjectEntry("Obj_VacuumInsulatedFoodJar", "Vacuum-insulated food jar", false, false, true),
            new ObjectEntry("Obj_CryogenicStorageContainer", "Cryogenic storage container", false, false, true),
            new ObjectEntry("Obj_InsulatedVaccineCarrier", "Insulated vaccine carrier box", false, false, true),
            new ObjectEntry("Obj_DoubleWallVacuumBottle", "Double-wall vacuum bottle", false, false, true),
        };
    }

    [MenuItem("ThermoRooms/Bulk Create Object Data")]
    public static void CreateAllObjectData()
    {
        string folderPath = "Assets/Data/ScriptableObjects";

        if (!AssetDatabase.IsValidFolder(folderPath))
        {
            Debug.LogError("Folder not found: " + folderPath + ". Please create it first.");
            return;
        }

        List<ObjectEntry> entries = GetAllEntries();
        int createdCount = 0;
        int skippedCount = 0;

        foreach (var entry in entries)
        {
            string assetPath = folderPath + "/" + entry.assetName + ".asset";

            if (AssetDatabase.LoadAssetAtPath<ObjectData>(assetPath) != null)
            {
                skippedCount++;
                continue; // already exists, don't overwrite
            }

            ObjectData data = ScriptableObject.CreateInstance<ObjectData>();
            data.objectName = entry.objectName;
            data.isCorrectForRoom1Open = entry.room1;
            data.isCorrectForRoom2Closed = entry.room2;
            data.isCorrectForRoom3Isolated = entry.room3;

            AssetDatabase.CreateAsset(data, assetPath);
            createdCount++;
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log($"Bulk creation complete. Created: {createdCount}, Skipped (already existed): {skippedCount}");
    }
}