using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour,  ITakeDamage<float>, IDie
{
    public GameObject brokenWall_Object;

    public void Damage(float damage)
    {
        Die();
    }

    public void Die()
    {
        Instantiate(brokenWall_Object, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

}

