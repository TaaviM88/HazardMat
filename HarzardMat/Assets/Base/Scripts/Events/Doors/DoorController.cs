using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class DoorController : MonoBehaviour
{
    public int id;
    public float moveSpeed = 4;
    public Vector2 moveToPos;
    Vector2 originalpos;
    bool isOpen = false;
    // Start is called before the first frame update
    void Start()
    {
        originalpos = transform.localPosition;
        GameEvents.current.onDoorwayTriggerEnter += OnDoorwayOpen;
        GameEvents.current.onDoorwayTriggerExit += OnDoorwayClose;
    }

    private void OnDoorwayClose(int id)
    {
        if(id == this.id)
        {
            if (isOpen)
            {
                transform.DOLocalMoveY(originalpos.y, moveSpeed);
                isOpen = false;
            }
        }
    }

    private void OnDoorwayOpen(int id)
    {
        if(id == this.id)
        {
            if (!isOpen)
            {
                transform.DOLocalMoveY(moveToPos.y, moveSpeed);
                isOpen = true;
            }
        }
    }

   /*private void OnEnable()
    {
        GameEvents.current.onDoorwayTriggerEnter += OnDoorwayOpen;
        GameEvents.current.onDoorwayTriggerExit += OnDoorwayClose;
    }*/

    private void OnDestroy()
    {
        GameEvents.current.onDoorwayTriggerEnter -= OnDoorwayOpen;
        GameEvents.current.onDoorwayTriggerExit -= OnDoorwayClose;
    }

    private void OnDisable()
    {
        GameEvents.current.onDoorwayTriggerEnter -= OnDoorwayOpen;
        GameEvents.current.onDoorwayTriggerExit -= OnDoorwayClose;
    }
}
