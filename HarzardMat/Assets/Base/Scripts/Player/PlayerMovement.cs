using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class PlayerMovement : MonoBehaviour
{
    private Collision coll;
    private BetterJumping betterJumpScript;
    Rigidbody2D _rb2D;
    public GameObject ghost;
    //private GameObject cloneGhost;
    [Header("Parametres")]
    public float speed = 7;
    public float jumpForce = 14;
    public float slideSpeed = 3;
    //public float wallJumpLerp = 9.5f;
    //public float dashSpeed = 50;
    public float gravityScaleNumber = 3;
    public float knockbackForceX = 15f;
    public float knockbackForceY = 5f;
    [Space]
    [Header("Booleans")]
    public bool canMove = true, wallGrab, isDashing;

    private bool groundTouch;
    private bool hasDashed;
    private bool jumping = false;

    private Vector2 moveDir;
    public int side = 1;

    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<Collision>();
        _rb2D = GetComponent<Rigidbody2D>();
        betterJumpScript = GetComponent<BetterJumping>();
        InstantiateGhost();
    }


    // Update is called once per frame
    void Update()
    {
        CheckGround();
    }

    private void CheckGround()
    {

        if (coll.onGround && !groundTouch)
        {
            GroundTouch();
            groundTouch = true;
        }

        if (!coll.onGround && groundTouch)
        {
            groundTouch = false;
        }

        if(coll.onGround && !isDashing)
        {
            betterJumpScript.enabled = true;
        }
    }

    private void GroundTouch()
    {
        hasDashed = false;
        isDashing = false;
        //side = anim.sr.flipX ? -1 : 1;
        //jumpParticle.Play();
    }

    private void FixedUpdate()
    {
        Move();
        if(Input.GetButtonDown("Jump"))
        {
            Jump(false);
        }
        
    }

    private void Jump(bool attackJump)
    {
        if (!coll.onGround)
        {
            //Jump(Vector2.up, false, false);
            jumping = false;
            return;
        }
        else
        {
            _rb2D.velocity = new Vector2(_rb2D.velocity.x, 0);
            if(attackJump)
            {
                //do jump attack stuff here
                // rb.velocity += new Vector2(_WalkDir.x * speed, dir.y * jumpForce *0.8f);
            }
            else
            {
                _rb2D.velocity = new Vector2(moveDir.x * speed * 1.50f, jumpForce);
            }
        }
    }

    private void Move()
    {
        if(!canMove)
        {
            return;
        }

        if(wallGrab)
        {
            return;
        }

        float horizontalX = Input.GetAxis("Horizontal");
        float VerticalY = Input.GetAxis("Vertical");
        moveDir = new Vector2(horizontalX, VerticalY);
        if(moveDir.x != 0)
        {
            FindObjectOfType<GhostTrail>().ShowGhost();
        }
        _rb2D.velocity = new Vector2(moveDir.x * speed, _rb2D.velocity.y);
    }

    private void InstantiateGhost()
    {
        if (FindObjectOfType<GhostTrail>() == null)
        {
          Instantiate(ghost, new Vector2(-20, -20), Quaternion.identity).GetComponent<PlayerManager>();
        }
    }
}
