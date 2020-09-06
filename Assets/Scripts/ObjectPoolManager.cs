using System;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectPoolType
{
    AFTER_IMAGE,
    BLOOD_PARTICLE_EFFECT
}

[Serializable]
public class ObjectPoolInfo
{
    [Header("Object Pool Info")]
    public ObjectPoolType type;
    public int poolSize = 10;
    public bool canGrow = true;
    public GameObject prefab;
    public GameObject container;

    public Queue<GameObject> pool = new Queue<GameObject>();
}

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager Instance { get; private set; }

    [Header("Object Pools")]
    [SerializeField] private List<ObjectPoolInfo> allPools;

    private void Awake()
    {
        Instance = this;

        FillPools();
    }

    private void FillPools()
    {
        foreach (ObjectPoolInfo pool in allPools)
        {
            GrowPool(pool);
        }
    }

    private void GrowPool(ObjectPoolInfo poolInfo)
    {
        int poolSize = poolInfo.poolSize;
        GameObject prefab = poolInfo.prefab;
        GameObject container = poolInfo.container;
        Queue<GameObject> pool = poolInfo.pool;

        for (int i = 0; i < poolSize; i++)
        {
            GameObject instance = Instantiate(prefab, container.transform);

            instance.SetActive(false);

            pool.Enqueue(instance);
        }
    }

    public void AddToPool(GameObject instance, ObjectPoolType type)
    {
        instance.SetActive(false);

        ObjectPoolInfo poolInfo = allPools.Find(pool => pool.type.Equals(type));

        poolInfo.pool.Enqueue(instance);
    }

    public GameObject GetFromPool(ObjectPoolType type)
    {
        ObjectPoolInfo poolInfo = allPools.Find(p => p.type.Equals(type));
        Queue<GameObject> pool = poolInfo.pool;

        if (poolInfo.canGrow && pool.Count == 0)
        {
            GrowPool(poolInfo);
        }

        GameObject instance = pool.Dequeue();
        instance.SetActive(true);

        return instance;
    }
}
