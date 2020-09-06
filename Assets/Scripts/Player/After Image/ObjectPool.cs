using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private GameObject prefab;
    [SerializeField] private int poolDefaultSize = 10;
    [SerializeField] private bool canGrow = true;

    public static ObjectPool Instance { get; private set; }
    private Queue<GameObject> availableObjects = new Queue<GameObject>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        GrowPool();
    }

    private void GrowPool()
    {
        for (int i = 0; i < poolDefaultSize; i++)
        {
            GameObject instanceToAdd = Instantiate(prefab, transform);

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
        if (canGrow && availableObjects.Count == 0)
        {
            GrowPool();
        }

        GameObject instance = availableObjects.Dequeue();
        instance.SetActive(true);

        return instance;
    }
}
