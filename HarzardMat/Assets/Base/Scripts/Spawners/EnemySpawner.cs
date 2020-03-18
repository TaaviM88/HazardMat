using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Parameters")]
    public EnemySpawnerType spawnType;
    public List<EnemyManager> spawnEnemies;
    public float spawnRate = 0.5f;
    public float nextSpawn = 0f;
    public int howManyEnemiesSpawns = 10;

    public float activationRange = 25f;
    public float cooldownTime = 10f;
    public Transform sensor;
    List<EnemyManager> spawnedEnemies = new List<EnemyManager>();
    int enemySpawnCount;
    int currentEnemyIndex = 0;
    int currentWave = 0;
    bool spawning = false, oneSpawn = false;
    // Start is called before the first frame update
    void Start()
    {
        enemySpawnCount = howManyEnemiesSpawns;

        if(enemySpawnCount == -1)
        {
            currentEnemyIndex = -2;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Vector3.Distance(sensor.position, PlayerManager.Instance.gameObject.transform.position) < activationRange)
        {
            GameObject newEnemy;
            switch (spawnType)
            {
                case EnemySpawnerType.Once:

                   if(!oneSpawn)
                    {
                        newEnemy = Instantiate(spawnEnemies[currentWave].gameObject, transform.position, Quaternion.identity) as GameObject;
                        oneSpawn = true;
                    }
                       
                     
                    //gameObject.SetActive(false);
                    break;
                case EnemySpawnerType.Wave:
                    if(!spawning && currentEnemyIndex < enemySpawnCount)
                    {
                       newEnemy = Instantiate(spawnEnemies[currentWave].gameObject, transform.position, Quaternion.identity) as GameObject;
                        currentEnemyIndex++;
                        StartCoroutine(NextSpawn());
                        if(currentEnemyIndex>= enemySpawnCount)
                        {
                            StartCoroutine(Cooldown());
                        }
                    }
                    break;

                case EnemySpawnerType.Infinite:
                    if (!spawning && currentEnemyIndex < enemySpawnCount)
                    {
                        newEnemy = Instantiate(spawnEnemies[currentWave].gameObject, transform.position, Quaternion.identity) as GameObject;
                        currentEnemyIndex++;
                        StartCoroutine(NextSpawn());
                    }
                    break;
            }
        }
        else
        {
            if(oneSpawn && spawnType == EnemySpawnerType.Once && !spawning)
            {
                StartCoroutine(Cooldown());
            }
        }
            

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(sensor.position, activationRange);
    }

    public void Reset()
    {
    
    }

    IEnumerator NextSpawn()
    {
        spawning = true;
        yield return new WaitForSeconds(spawnRate);
        spawning = false;
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(cooldownTime);
        currentEnemyIndex = 0;
        if(spawnType == EnemySpawnerType.Once)
        {
            oneSpawn = false;
        }
    }

}
