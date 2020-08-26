using System.Collections.Generic;
using UnityEngine;

public class PlayerAfterImagePool : MonoBehaviour
{
    [SerializeField] private GameObject playerAfterImagePrefab;

    public static PlayerAfterImagePool Instance { get; private set; }
    private Queue<GameObject> availableObjects = new Queue<GameObject>();

    private void Awake()
    {
        Instance = this;
        GrowPool();
    }

    private void GrowPool()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject instanceToAdd = Instantiate(playerAfterImagePrefab, transform);

            AddToPool(instanceToAdd);
        }
    }

    public void AddToPool(GameObject instance)
    {
        instance.SetActive(false);

        availableObjects.Enqueue(instance);
    }

    public GameObject GetFromPool()
    {
        if (availableObjects.Count == 0)
        {
            GrowPool();
        }

        GameObject instance = availableObjects.Dequeue();
        instance.SetActive(true);

        return instance;
    }
}
