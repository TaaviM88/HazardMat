using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour, ITakeDamage<float>, IDie
{
    public float health = 2;
    public int side = 1;
    //public float timeToLive = 10f;

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
}
