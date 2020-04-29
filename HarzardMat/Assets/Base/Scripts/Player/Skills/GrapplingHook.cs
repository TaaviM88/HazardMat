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


                }
            }

        }
        else
        {
            Destroy(curHook);
            ropeActive = false;
            PlayerManager.Instance.canChangeAttackMode = true;
        }

    }

}
//Old
   /* public LineRenderer lr;
    private Vector3 grapplePoint;
    public LayerMask whatIsGrappleable;
    public float newGravityScale = 20f;
    public float maxDistance = 20f, jointDistancePercentage = 0.5f, jointDampingRatio = 0.5f, jointFrequency = 0.5f;

    //private SpringJoint2D joint2D;

    private DistanceJoint2D joint2D;

    private Vector3 currentGrapplePosition;
    PlayerMovement move;
    Rigidbody2D rb2d;
    private float orginalGravityScale;
    // Start is called before the first frame update
    void Start()
    {
        move = GetComponent<PlayerMovement>();
        rb2d = GetComponent<Rigidbody2D>();
        orginalGravityScale = rb2d.gravityScale;
        lr.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            PlayerManager.Instance.canChangeAttackMode = false;
            if(!lr.enabled)
            {
                lr.enabled = true;
            }
            StartGrapple();
        }

        if(Input.GetButtonUp("Fire1"))
        {
            StopGrapple();
            PlayerManager.Instance.canChangeAttackMode = true;
        }
    }

    private void LateUpdate()
    {
        DrawRope();
    }

    private void StartGrapple()
    {
        Vector2 castDir = new Vector2 (move.GetHorizontalInput().x, 1);
        //RaycastHit2D hit2D = Physics2D.Raycast(transform.position, castDir, maxDistance, whatIsGrappleable);
        if(castDir.y <=0)
        {
            return;
        }
        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, castDir, maxDistance);
        //Debug.Log(hit2D.collider.name + hit2D.collider.gameObject.layer);
        if(hit2D.collider != null)
        if (hit2D.collider.gameObject.layer == 8)
        {

            grapplePoint = hit2D.point;
            if(joint2D == null)
            {
                //joint2D = gameObject.AddComponent<SpringJoint2D>();
                joint2D = gameObject.AddComponent<DistanceJoint2D>();
            }
            joint2D.autoConfigureConnectedAnchor = false;
            joint2D.connectedAnchor = grapplePoint;

            float distanceFromPoint = Vector3.Distance(transform.position, grapplePoint);
            joint2D.enableCollision = true;
            joint2D.distance = distanceFromPoint * jointDistancePercentage;
            
            //distance joint
            //joint2D.maxDistanceOnly = true;

            //spring joint
            //joint2D.dampingRatio = jointDampingRatio;
            //joint2D.frequency = jointFrequency;

           // joint2D.breakForce = 500;
            lr.positionCount = 2;

            currentGrapplePosition = lr.gameObject.transform.position;
                rb2d.gravityScale = newGravityScale;
        }

    }

    private void StopGrapple()
    {
        lr.positionCount = 0;
        Destroy(joint2D);
        rb2d.gravityScale = orginalGravityScale;
    }

    void DrawRope()
    {
        if (!joint2D) return;

        currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, grapplePoint, Time.deltaTime * 8f);
        lr.SetPosition(0, lr.gameObject.transform.position);
        lr.SetPosition(1, currentGrapplePosition);
    }
    public bool IsGrappling()
    {
        return joint2D != null;
    }

    public Vector3 GetGrapplePoint()
    {
        return grapplePoint;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, maxDistance);
    }*/

