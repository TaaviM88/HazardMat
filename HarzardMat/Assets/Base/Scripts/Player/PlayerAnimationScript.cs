using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationScript : MonoBehaviour
{
    private Animator anim;
    private PlayerMovement move;
    private Collision coll;
    [HideInInspector]
    public SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        coll = GetComponent<Collision>();
        move = GetComponent<PlayerMovement>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        /*anim.SetBool("OnGround", coll.onGround);
        anim.SetBool("OnWall", coll.onWall);
        anim.SetBool("onRightWall", coll.onRightWall);
        anim.SetBool("wallGrab", move.wallGrab);
        //anim.SetBool("wallSlide", move.wallSlide);
        anim.SetBool("canMove", move.canMove);
        anim.SetBool("isDashing", move.isDashing);*/
    }

    public void SetHorizontalMovement(float x, float y, float yVel)
    {
        anim.SetFloat("HorizontalAxis", x);
        anim.SetFloat("VerticalAxis", y);
        anim.SetFloat("VerticalVelocity", yVel);
        //anim.SetFloat("AttackButton", attackB);
        //anim.SetFloat("AttackButtonNegative", -y);
    }

    public void Flip(int side)
    {
        Debug.Log("Flipataan hahmoa");
        if (move.wallGrab)
        {
            if (side == -1 && sr.flipX)
            {

                Debug.Log("perutaan flippaus");
                return;

            }
            if (side == 1 && !sr.flipX)
            {

                Debug.Log("perutaan flippaus");
                return;
            }
        }
        bool state = (side == 1) ? false : true;

        sr.flipX = state;
    }
}
