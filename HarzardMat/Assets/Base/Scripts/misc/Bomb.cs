using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Pickable, ITakeDamage<float>
{
    public float explosionForce = 5f;
    public float explosionDamage = 50f;
    public float timer = 3f;
    public ParticleSystem explosionParticle;
    bool isTriggered = false;
    SpriteRenderer sprite;
    bool startFlickering = false;
    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }
    protected override void Update()
    {
        base.Update();

        switch (state)
        {
            case PickableState.none:

                break;

            case PickableState.lifted:
                rb.isKinematic = true;
                boxCollider.isTrigger = true;

                break;

            case PickableState.lowered:
                rb.isKinematic = false;
                boxCollider.isTrigger = false;
                if (!isTriggered)
                {
                    //Explode();
                    StartCoroutine(TimeToExplode());
                    isTriggered = true;
                    canBeLifted = false;
                }
               
                break;
        }

        if (startFlickering)
            sprite.color = new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f));
    }


    IEnumerator TimeToExplode()
    {
        startFlickering = true;
        yield return new WaitForSeconds(timer);
        Explode();
    }

    private void Explode()
    {
        Collider2D[] objectToDamage;
        objectToDamage = Physics2D.OverlapCircleAll(transform.position, explosionForce);
        ITakeDamage<float> a;
        for (int i = 0; i < objectToDamage.Length; i++)
        {
            a = objectToDamage[i].GetComponent<ITakeDamage<float>>();
            if(a != null)
            {
                a.Damage(explosionDamage);
            }
          
        }

        Instantiate(explosionParticle.gameObject, transform.position, Quaternion.identity);
        DestroyGameobj();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionForce);
    }

    public void Damage(float damage)
    {
        DestroyGameobj();
    }
}
