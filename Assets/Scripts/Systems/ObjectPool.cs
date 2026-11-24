using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string key;
        public List<GameObject> objectPool = new List<GameObject>();
        public GameObject objectToSpawn;
        public Transform objectToParent;
        public int spawnCount;
    }
    public List<Pool> pools;
    
    public static ObjectPool instance; // SINGLETON
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        InitialisePools();
    }

    private void InitialisePools()
    {
        foreach (var pool in pools)
        {
            for (int i = 0; i < pool.spawnCount; i++)
            {
                var obj = Instantiate(
                    pool.objectToSpawn,
                    pool.objectToParent, true);
                obj.SetActive(false);
                pool.objectPool.Add(obj);
            }
        }
    }

    private bool IsItemActive(Pool pool)
    {
        return pool.objectPool.All(item => item.activeInHierarchy);
    }
    
    private GameObject GetInactiveObject(Pool pool)
    {
        return pool.objectPool.Find(item => !item.activeInHierarchy) ?? pool.objectPool.First() ?? pool.objectPool.Last();
    }
    
    public GameObject SpawnFromPools(string key, Transform spawnPoint, Quaternion spawnRotation)
    {
        GameObject objectToSpawn = null;
        foreach (var pool in pools)
        {
            if (pool.key == key)
            {
                if (IsItemActive(pool))
                {
                    objectToSpawn = Instantiate(pool.objectToSpawn, pool.objectToParent, true);
                    pool.objectPool.Add(objectToSpawn);
                    pool.spawnCount++;
                }
                else
                {
                    objectToSpawn = GetInactiveObject(pool);
                    objectToSpawn.SetActive(true);
                }

                objectToSpawn.transform.position = spawnPoint.position;
                objectToSpawn.transform.rotation = spawnRotation;

            }
        }
        return objectToSpawn;
    }

    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.position = Vector2.zero;
    }

    public IEnumerator ReturnToPool(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);
        ReturnToPool(obj);
    }
}