using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [Header("Parametres")]
    public Transform attackpos;
    public float attackRange = 3,  damage = 10f,  attackCooldown = 1f;
    private bool canAttack = true;
    public LayerMask Players;
    [Header("Polish")]
    public ParticleSystem attackParcticle;

    Rigidbody2D rb2D;
    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack()
    {
        if(canAttack)
        {
            Collider2D[] playersToDamage = Physics2D.OverlapCircleAll(attackpos.position, attackRange, Players);
            for (int i = 0; i < playersToDamage.Length; i++)
            {
                playersToDamage[i].GetComponent<PlayerManager>().Damage(damage);

                StartCoroutine(AttackDownCoolDown());
            }
        }
       
    }

    IEnumerator AttackDownCoolDown()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackpos.position, attackRange);
    }
}
