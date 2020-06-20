using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    //new
    public GameObject hook;
    public bool ropeActive;
    GameObject curHook;
    PlayerMovement move;
    public float maxDistance = 5;

    private Vector2 grapplePoint;
    // Start is called before the first frame update
    void Start()
    {
        move = GetComponent<PlayerMovement>();
    }


    private void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            ThrowGrapple();
        }
    }

    private void ThrowGrapple()
    {

        if (!ropeActive)
        {
            Vector2 destiny = new Vector2(move.GetHorizontalInput().x, 1);
         

            RaycastHit2D hit2D = Physics2D.Raycast(transform.position, destiny, maxDistance);

            if (hit2D.collider != null)
            {
                if (hit2D.collider.gameObject.layer == 8)
                {

                    grapplePoint = hit2D.point;

                    curHook = (GameObject)Instantiate(hook, transform.position, Quaternion.identity);
                    curHook.GetComponent<Rope>().destiny = grapplePoint;
                    ropeActive = true;
                    PlayerManager.Instance.canChangeAttackMode = false;
                    if(move.GetCurrentRopeState() == PlayerRopeState.none)
                    {
                        move.ChangePlayerRopeState(PlayerRopeState.hanging);
                    }

                }
            }

        }
        else
        {
            Destroy(curHook);
            ropeActive = false;
            PlayerManager.Instance.canChangeAttackMode = true;
            if (move.GetCurrentRopeState() == PlayerRopeState.hanging)
            {
                move.ChangePlayerRopeState(PlayerRopeState.none);
            }
        }

    }

}

