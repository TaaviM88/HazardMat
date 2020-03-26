using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    public LineRenderer lr;
    private Vector3 grapplePoint;
    public LayerMask whatIsGrappleable;
    private float maxDistance = 20f;
    private SpringJoint2D joint2D;

    private Vector3 currentGrapplePosition;
    PlayerMovement move;
    // Start is called before the first frame update
    void Start()
    {
        move = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            StartGrapple();
        }
        else if(Input.GetButtonUp("Fire1"))
        {
            StopGrapple();
        }
    }

    private void LateUpdate()
    {
        DrawRope();
    }

    private void StartGrapple()
    {
        Vector2 castDir = new Vector2 (move.GetHorizontalInput().x,1);
        //RaycastHit2D hit2D = Physics2D.Raycast(transform.position, castDir, maxDistance, whatIsGrappleable);
        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, castDir, maxDistance);
        Debug.Log(hit2D.collider.name + hit2D.collider.gameObject.layer);
        if (hit2D.collider.gameObject.layer == 8)
        {
            Debug.Log(hit2D.collider.name + "fuck yes");
            grapplePoint = hit2D.point;
            joint2D = gameObject.AddComponent<SpringJoint2D>();
            joint2D.autoConfigureConnectedAnchor = false;
            joint2D.connectedAnchor = grapplePoint;

            float distanceFromPoint = Vector3.Distance(transform.position, grapplePoint);
            joint2D.distance = distanceFromPoint * 0.5f;
            joint2D.dampingRatio = 0.1f;
            joint2D.frequency = 1f;
            lr.positionCount = 2;
            currentGrapplePosition = lr.gameObject.transform.position;    
        }

    }

    private void StopGrapple()
    {
        lr.positionCount = 0;
        Destroy(joint2D);
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
}
