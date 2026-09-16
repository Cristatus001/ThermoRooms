using UnityEngine;

[CreateAssetMenu(fileName = "RoomSpawnData", menuName = "ThermoRooms/Room Spawn Data")]
public class RoomSpawnData : ScriptableObject
{
    public ObjectData[] correctObjectsPool;
    public ObjectData[] wrongObjectsPool;

    public int correctCountToSpawn = 5;
    public int wrongCountToSpawn = 5;
}