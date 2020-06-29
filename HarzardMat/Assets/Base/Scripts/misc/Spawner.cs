using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject spawnObject;
    public int spawnerID = 0;
    
    private GameObject spawnObj;
    ISpawnerID<int> spawnIDScript;
    
    private void Start()
    {
        GameEvents.current.spawnObject += SpawnObject;

        SpawnObject(spawnerID);
    }

    private void SpawnObject(int id)
    {
        if(spawnerID == id)
        {
            spawnObj = Instantiate(spawnObject, transform.position, Quaternion.identity);
            spawnIDScript = spawnObj.GetComponent<ISpawnerID<int>>();

            if (spawnIDScript != null)
            {
                spawnIDScript.SetSpawnerID(spawnerID);
            }
        }

    }

    private void OnDestroy()
    {
        GameEvents.current.spawnObject -= SpawnObject;
    }

    private void OnDisable()
    {
        GameEvents.current.spawnObject -= SpawnObject;
    }
}
