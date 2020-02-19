using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SealJump : MonoBehaviour
{
    private Collision coll;
    private Rigidbody2D _rb2D;
    public float jumpForce = 14;
    public float moveSpeed = 7;
    //private bool jumping = false;
    private bool groundTouch;
    private int side = 1;
    private Vector2 moveDir;
    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<Collision>();
        _rb2D = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        float horizontalX = Input.GetAxis("Horizontal");
        float VerticalY = Input.GetAxis("Vertical");
        moveDir = new Vector2(horizontalX, VerticalY);

      

        //Jump();
    }

    private void FixedUpdate()
    {
        if (!coll.onGround)
        {
            //jumping = false;
            _rb2D.velocity = new Vector2(moveDir.x * moveSpeed, _rb2D.velocity.y);
            return;
        }
        else
        {
            _rb2D.velocity = new Vector2(moveDir.x * moveSpeed * 1.50f, jumpForce);
        }
    }

    private void Jump()
    {
        if(!coll.onGround)
        {
            //jumping = false;
            return;
        }
        else
        {
            _rb2D.velocity = new Vector2(moveDir.x * moveSpeed * 1.50f, jumpForce);
        }
    }

    private void CheckGround()
    {
        if (coll.onGround && !groundTouch)
        {
            groundTouch = true;
        }

        if (!coll.onGround && groundTouch)
        {
            groundTouch = false;
        }
    }
}
