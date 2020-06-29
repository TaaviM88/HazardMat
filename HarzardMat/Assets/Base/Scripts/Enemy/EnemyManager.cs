using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour, ITakeDamage<float>, IDie, ISpawnerID<int>
{
    public float health = 2;
    public int side = 1;

    private int spawnerid;
    EnemySpawner mySpawner;
    //public float timeToLive = 10f;

    private void Start()
    {

    }

    public void Damage(float damage)
    {
        health = Mathf.Min(health - damage, health);

        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public void FlipEnemy()
    {
        side *= -1;
        transform.localScale = new Vector3(side, transform.localScale.y, transform.localScale.z);
    }

    public void GiveMySpawner(EnemySpawner spawner)
    {
        mySpawner = spawner;
    }

    private void OnDestroy()
    {
        if(mySpawner != null)
        {
            mySpawner.gameObject.SetActive(true);
        }
    }

    public void SetSpawnerID(int newID)
    {
        throw new System.NotImplementedException();
    }
}
