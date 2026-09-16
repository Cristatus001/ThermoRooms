using UnityEngine;

[CreateAssetMenu(fileName = "FactsData", menuName = "ThermoRooms/Facts Data")]
public class FactsData : ScriptableObject
{
    [TextArea(3, 5)]
    public string[] facts;
}