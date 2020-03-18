using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatController : MonoBehaviour
{
    EnemyManager emanager;
    SpriteRenderer renderer2D;
    // Start is called before the first frame update
    void Start()
    {
        emanager = GetComponent<EnemyManager>();
        renderer2D = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //jos ei ole pelaaja johon osuttiin
        if (!collision.IsTouchingLayers(10))
        {
            Debug.Log(collision.gameObject.name);
            emanager.FlipEnemy();
            /*if(emanager.side ==1)
            {
                renderer2D.flipX = true;
            }
            else
            {
                renderer2D.flipX = false;
            }*/
        }
    }
}
