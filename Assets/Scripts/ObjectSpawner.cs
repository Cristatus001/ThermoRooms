using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class ObjectSpawner : MonoBehaviour
{
    [Header("Spawn Data")]
    public RoomSpawnData roomSpawnData;

    [Header("Spawn Points")]
    public Transform[] spawnPoints;

    [Header("Prefab")]
    public GameObject interactableObjectPrefab;

    void Start()
    {
        SpawnObjects();
    }

    void SpawnObjects()
    {
        List<ObjectData> selectedCorrect = GetRandomItems(roomSpawnData.correctObjectsPool, roomSpawnData.correctCountToSpawn);
        List<ObjectData> selectedWrong = GetRandomItems(roomSpawnData.wrongObjectsPool, roomSpawnData.wrongCountToSpawn);

        List<(ObjectData data, bool isCorrect)> allObjects = new List<(ObjectData, bool)>();
        foreach (var obj in selectedCorrect) allObjects.Add((obj, true));
        foreach (var obj in selectedWrong) allObjects.Add((obj, false));

        allObjects = allObjects.OrderBy(x => Random.value).ToList();
        List<Transform> shuffledSpawnPoints = spawnPoints.OrderBy(x => Random.value).ToList();

        for (int i = 0; i < allObjects.Count && i < shuffledSpawnPoints.Count; i++)
        {
            GameObject obj = Instantiate(interactableObjectPrefab, shuffledSpawnPoints[i].position, Quaternion.identity);
            InteractableObject interactable = obj.GetComponent<InteractableObject>();

            interactable.objectName = allObjects[i].data.objectName;
            interactable.objectImage = allObjects[i].data.popupImage;
            interactable.isCorrectObject = allObjects[i].isCorrect;

            SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
            if (sr != null)
                sr.sprite = allObjects[i].data.worldSprite;

            Transform chatBubbleImg = obj.transform.Find("ChatBubble/ObjectImage");
            if (chatBubbleImg != null)
            {
                SpriteRenderer bubbleSr = chatBubbleImg.GetComponent<SpriteRenderer>();
                bubbleSr.sprite = allObjects[i].data.popupImage;
            }
        }
    }

    List<ObjectData> GetRandomItems(ObjectData[] pool, int count)
    {
        return pool.OrderBy(x => Random.value).Take(count).ToList();
    }
}