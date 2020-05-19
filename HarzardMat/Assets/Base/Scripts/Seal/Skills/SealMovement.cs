using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class SealMovement : MonoBehaviour
{
    public float moveSpeed;
   
    Rigidbody2D _rb2D;
    float moveHorizontal;
    float originalScaleX;
    public bool canMove = true;


    // Start is called before the first frame update
    void Start()
    { 
        _rb2D = GetComponent<Rigidbody2D>();
        //vcam.m_Lens.FieldOfView = Mathf.Lerp(vcam.m_Lens.FieldOfView, 30, 10f);
        originalScaleX = transform.localScale.x;
    }


    // Update is called once per frame
    void Update()
    {
        if(!canMove)
        {
            return;
        }

        moveHorizontal = Input.GetAxis("Horizontal");
        float rawHorizontal = Input.GetAxisRaw("Horizontal");
        if (rawHorizontal != 0)
        {
            if (PlayerManager.Instance.side == 1)
            {
                transform.localScale = new Vector3((int)rawHorizontal * originalScaleX, transform.localScale.y, transform.localScale.z);
            }
            else
            {
                transform.localScale = new Vector3(-(int)rawHorizontal * originalScaleX, transform.localScale.y, transform.localScale.z);
            }
        }
    }

    private void FixedUpdate()
    {
        
            _rb2D.velocity = new Vector2(moveHorizontal * moveSpeed, _rb2D.velocity.y);
    }

    public void UpdateOriginalScaleX(int side)
    {
        originalScaleX = side;
    }

}
