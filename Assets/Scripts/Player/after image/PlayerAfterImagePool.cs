using System.Collections.Generic;
using UnityEngine;

public class PlayerAfterImagePool : MonoBehaviour
{
    [SerializeField] private GameObject playerAfterImagePrefab;

    public static PlayerAfterImagePool instance { get; private set; }
    private Queue<GameObject> activeObjects = new Queue<GameObject>();

    private void Awake()
    {
        instance = this;
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

        activeObjects.Enqueue(instance);
    }

    public GameObject GetFromPool()
    {
        if (activeObjects.Count == 0)
        {
            GrowPool();
        }

        GameObject instance = activeObjects.Dequeue();
        instance.SetActive(true);

        return instance;
    }
}
