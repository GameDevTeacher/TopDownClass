using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    private ObjectPool _objectPool;

    private void Start()
    {
        _objectPool = ObjectPool.instance;
    }

    void Update()
    {
        if (Keyboard.current.gKey.wasPressedThisFrame)
        {
            //var randomSpawn = new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f));
           _objectPool.SpawnFromPools("Gems", transform, Quaternion.identity);
        }
        if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            //var randomSpawn = new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f));
           _objectPool.SpawnFromPools("Coins", transform, Quaternion.identity);
        }
    }
}
