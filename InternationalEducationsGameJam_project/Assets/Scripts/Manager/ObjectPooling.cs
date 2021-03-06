﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class ObjectPoolItem
{
    public GameObject objectToPool;
    public int amountToPool;
    public bool Expand;
}

public class ObjectPooling : MonoBehaviour
{
    public static ObjectPooling m_Instance;
    private List<GameObject> pooledObjects;

    public List<ObjectPoolItem> ItemsToPool;
    private void Awake()
    {
        m_Instance = this;
        pooledObjects = new List<GameObject>();

        foreach (ObjectPoolItem item in ItemsToPool)
        {
            for (int i = 0; i < item.amountToPool; i++)
            {
                GameObject obj = (GameObject)Instantiate(item.objectToPool);
                obj.SetActive(false);
                pooledObjects.Add(obj);
            }
        }
    }
    public GameObject GetPooledObject(string tag,Transform Position)
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy && pooledObjects[i].CompareTag(tag))
            {
                pooledObjects[i].SetActive(true);
                pooledObjects[i].gameObject.transform.position = Position.position;
                return pooledObjects[i];
            }
        }
        foreach (ObjectPoolItem item in ItemsToPool)
        {
            if (item.objectToPool.CompareTag(tag))
            {
                if (item.Expand)
                {
                    GameObject obj = (GameObject)Instantiate(item.objectToPool,Position);
                    pooledObjects.Add(obj);
                    return obj;

                }
            }
        }
            return null;
    }
}
