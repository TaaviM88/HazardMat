using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class PlayerMovement : MonoBehaviour
{
    private Collision coll;
    private BetterJumping betterJumpScript;
    private PlayerAnimationScript anime;
    Rigidbody2D _rb2D;
    public GameObject ghost;
    private GhostTrail cloneGhost;
    [Header("Parametres")]
    public float speed = 7;
    public float jumpForce = 14;
    public float slideSpeed = 3;
    //public float wallJumpLerp = 9.5f;
    //public float dashSpeed = 50;
    public float gravityScaleNumber = 3;
    public float knockbackForceX = 15f;
    public float knockbackForceY = 5f;
    public float wallJumpLerp = 10;
    [Space]
    [Header("Booleans")]
    public bool wallGrab, isDashing;

    private bool canMove = true;
    private bool groundTouch;
    private bool hasDashed;
    private bool jumping = false;
    private bool wallJumping = false;

    private float orginalGravityScale;
    private float horizontalX;
    private float VerticalY;

    private Vector2 moveDir;
    public int side = 1;

    PlayerRopeState ropeState;
    private float forceToAddAtRope = 25f;
    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<Collision>();
        _rb2D = GetComponent<Rigidbody2D>();
        betterJumpScript = GetComponent<BetterJumping>();
        anime = GetComponent<PlayerAnimationScript>();

        orginalGravityScale = _rb2D.gravityScale;
        InstantiateGhost();
    }


    // Update is called once per frame
    void Update()
    {
        CheckGround();

        horizontalX = Input.GetAxis("Horizontal");
        VerticalY = Input.GetAxis("Vertical");
        moveDir = new Vector2(horizontalX, VerticalY);

        if (Input.GetButtonDown("Jump"))
        {
            Jump(false);
            ReleaseWallGrab();
        }

    }

    public void ReleaseWallGrab()
    {
        if(wallGrab)
        {
            if (_rb2D.gravityScale != orginalGravityScale)
            {
                _rb2D.gravityScale = orginalGravityScale;
            }
            StartPlayerMovement();
            
            wallGrab = false;

            WallJump();
        }
      
    }
    public bool WallGrab()
    {
        if (coll.onWall && !coll.onGround && !coll.onCeiling)
        {
            if (side != coll.wallSide)
            {
                anime.Flip(side * -1);
                side *= -1;
            }
            wallGrab = true;
            //_rb2D.gravityScale = 0;
            _rb2D.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            wallJumping = false;
            //betterJumpScript.enabled = false;
            return true;
        }
        else
            return false;
    }

    private void WallJump()
    {
        if ((side == 1 && coll.onRightWall) || side == -1 && !coll.onRightWall)
        {
            side *= -1;
            anime.Flip(side);
            side *= -1;
        }

        Vector2 wallDir = coll.onRightWall ? Vector2.left : Vector2.right;

        _rb2D.velocity = new Vector2(_rb2D.velocity.x, 0);
        Vector2 dir = Vector2.up / 1.2f + wallDir / 1.2f;
        _rb2D.velocity += dir * jumpForce;
        wallJumping = true;
    }


    private void CheckGround()
    {
        if (coll.onGround && !groundTouch)
        {
            GroundTouch();
            groundTouch = true;
            wallJumping = false;
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
        if(ropeState == PlayerRopeState.none)
        {
            Move();
        }

        if(ropeState == PlayerRopeState.hanging)
        {
            RopeHangingMovement();
        }
        
    }

    private void Jump(bool attackJump)
    {
        if (!coll.onGround || !GetCanMove())
        {
            //Jump(Vector2.up, false, false);
            jumping = false;
            return;
        }
        else
        {
            //_rb2D.velocity = new Vector2(_rb2D.velocity.x, 0);
            if(attackJump)
            {
                //do jump attack stuff here
                // rb.velocity += new Vector2(_WalkDir.x * speed, dir.y * jumpForce *0.8f);
            }
            else
            {
                anime.SetTrigger("Jump");  
                _rb2D.velocity = new Vector2(moveDir.x * speed * 1.50f, jumpForce);
            }
        }
    }

    private void Move()
    {
        if (wallGrab)
        {
            return;
        }

        if (!GetCanMove())
        {
            _rb2D.velocity = new Vector2(0, _rb2D.velocity.y);
            return;
        }

        if(moveDir != Vector2.zero)
        {
            //FindObjectOfType<GhostTrail>().ShowGhost();
            //cloneGhost.ShowGhost();
        }

        if(moveDir.x >0)
        {
            side = 1;
            anime.Flip(side);
        }

        if(moveDir.x < 0)
        {
            side = -1;
            anime.Flip(side);
        }

        if(wallJumping)
        {
            _rb2D.velocity = Vector2.Lerp(_rb2D.velocity, (new Vector2(moveDir.x * speed/2, _rb2D.velocity.y)), wallJumpLerp * Time.deltaTime);
        }
        else
        {
            _rb2D.velocity = new Vector2(moveDir.x * speed, _rb2D.velocity.y);
            anime.SetHorizontalMovement(moveDir.x, moveDir.y, _rb2D.velocity.y);
        }
    
    }

    private void RopeHangingMovement()
    {

        if (horizontalX != 0)
        {
            _rb2D.AddForce(horizontalX * Vector2.right * forceToAddAtRope);
        }
    }

    private void InstantiateGhost()
    {
        if (FindObjectOfType<GhostTrail>() == null)
        {
            GameObject clone = Instantiate(ghost, new Vector2(-20, -20), Quaternion.identity);
            cloneGhost = clone.GetComponent<GhostTrail>();
        }
    }

    public int GetSide()
    {
        return side;
    }

    public Vector2 GetHorizontalInput()
    {
        return moveDir;
    }

    public Rigidbody2D GetRigidbody()
    {
        return _rb2D;
    }

    public void StopPlayerMovement()
    {
       _rb2D.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    public void FreezePlayerXMovement()
    {
        _rb2D.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
    }

    public void StartPlayerMovement()
    {
        _rb2D.constraints = RigidbodyConstraints2D.None;
        transform.localEulerAngles = new Vector3(0,0,0);
        _rb2D.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    public void ToggleCanMove(bool newbool)
    {
        canMove = newbool;
    }

    public bool GetCanMove()
    {
        return canMove;
    }

    public void ShowGhost()
    {
        cloneGhost.ShowGhost();
    }

    public void EnableGhost()
    {
        cloneGhost.enabled = true;
    }

    public void  DisableGhost()
    {
        cloneGhost.enabled = false;
    }

    public void AddForce(Vector2 force)
    {
        _rb2D.AddForce(force);
    }

    public void ChangePlayerRopeState(PlayerRopeState newState)
    {
        ropeState = newState;
    }

    public PlayerRopeState GetCurrentRopeState()
    {
        return ropeState;
    }

}
