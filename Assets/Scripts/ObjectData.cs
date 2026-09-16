using UnityEngine;

[CreateAssetMenu(fileName = "ObjectData", menuName = "ThermoRooms/Object Data")]
public class ObjectData : ScriptableObject
{
    public string objectName;
    public Sprite worldSprite;
    public Sprite popupImage;
    public bool isCorrectForRoom1Open;
    public bool isCorrectForRoom2Closed;
    public bool isCorrectForRoom3Isolated;
}