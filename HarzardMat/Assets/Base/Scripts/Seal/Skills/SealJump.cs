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
    float originalScaleX;
    //private int side = 1;
    private Vector2 moveDir;
    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<Collision>();
        _rb2D = GetComponent<Rigidbody2D>();
        //originalScaleX = PlayerManager.Instance.side;
        originalScaleX  = transform.localScale.x; 
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
            //_rb2D.velocity = new Vector2(moveDir.x * moveSpeed, _rb2D.velocity.y);
            return;
        }
        else
        {
            _rb2D.velocity = new Vector2(moveDir.x * moveSpeed * 1.50f, jumpForce);
        }

        float rawHorizontal = Input.GetAxisRaw("Horizontal");
        if (rawHorizontal != 0)
        {
            if(PlayerManager.Instance.side > 0)
            {
                transform.localScale = new Vector3((int)rawHorizontal * originalScaleX, transform.localScale.y, transform.localScale.z);
            }
            else
            {
                transform.localScale = new Vector3(-1*(int)rawHorizontal * originalScaleX, transform.localScale.y, transform.localScale.z);
            }

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

    public void UpdateOriginalScaleX(int side)
    {
        originalScaleX = side;
    }
}
