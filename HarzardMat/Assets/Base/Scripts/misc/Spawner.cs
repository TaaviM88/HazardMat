using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject spawnObject;
    public int spawnerID = 0;
    public float cooldownTimer = 3f;
    private GameObject spawnObj;
    private bool readyToSpawnObj = true;
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
                //readyToSpawnObj = false;
                StartCoroutine(SpawnCoolDown());
        }

    }

    IEnumerator SpawnCoolDown()
    {

        yield return new WaitForSeconds(3);
        spawnObj = Instantiate(spawnObject, transform.position, Quaternion.identity);
        spawnIDScript = spawnObj.GetComponent<ISpawnerID<int>>();

        spawnIDScript?.SetSpawnerID(spawnerID);
        //SpawnObject(spawnerID);
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
