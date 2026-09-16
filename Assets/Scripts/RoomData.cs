using UnityEngine;

[CreateAssetMenu(fileName = "RoomData", menuName = "ThermoRooms/Room Data")]
public class RoomData : ScriptableObject
{
    public string roomTitle;        // e.g. "FIND OPEN SYSTEMS"
    public FactsData factsData;     // the facts for this room
}