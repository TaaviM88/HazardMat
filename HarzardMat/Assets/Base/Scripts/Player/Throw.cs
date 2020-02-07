﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Throw : MonoBehaviour
{
    private Rigidbody2D weaponRb2D;
    ThrowWeapon throwWeaponScript;
    PlayerMovement move;
    private float returnTime;
    private Vector3 origLockPos;
    private Vector3 origLockRot;
    private Vector3 pullPosition;

    [Header("Public References")]
    public Transform weapon;
    public Transform weaponSlot;
    public Transform curvePoint;

    [Space]
    [Header("Parameteres")]
    public float throwPower = 30;
    public float warpDuration = 2f;

    [Space]
    [Header("Bools")]
    public bool walking = true;
    public bool aming = false;
    public bool hasWeapon = true;
    public bool pulling = false;

    private bool isWarping = false;
    // Start is called before the first frame update
    void Start()
    {
        move = GetComponent<PlayerMovement>();
        weaponRb2D = weapon.GetComponent<Rigidbody2D>();
        throwWeaponScript = weapon.GetComponent<ThrowWeapon>();
        throwWeaponScript.AddThrowWeaponScript(this);
        origLockPos = weapon.localPosition;
        origLockRot = weapon.localEulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        if(hasWeapon)
        {
            if(Input.GetButtonDown("Fire1"))
            {
                throwWeaponScript.activated = true;
                move.canMove = false;
                move.StopPlayerMovement();
                PlayerManager.Instance.canChangeAttackMode = false;
            }

            if(Input.GetButtonUp("Fire1"))
            {
                WeaponThrow();
            }

            
        }
        else
        {
            if(Input.GetButtonDown("Fire1") && throwWeaponScript.canPulled && !isWarping)
            {
                WeaponStartPull();
            }

            if(Input.GetButtonDown("Fire2") && throwWeaponScript.canPulled && !isWarping && !pulling)
            {
                WarpToWeapon();
                isWarping = true;
            }
        }

        if(pulling)
        {
            if(returnTime <1)
            {
                weapon.position = GetQuadraticCurvePoint(returnTime, pullPosition, curvePoint.position, weaponSlot.position);
                returnTime += Time.deltaTime * 1.5f;    
            }
            else
            {
                WeaponCatch();
            }
        }
    }

    private void WeaponCatch()
    {
        returnTime = 0;
        pulling = false;
        weapon.parent = weaponSlot;
        throwWeaponScript.activated = false;
        weapon.localEulerAngles = origLockRot;
        weapon.localPosition = origLockPos;
        hasWeapon = true;
        weapon.localScale = new Vector3(1, 1, 1);
        throwWeaponScript.ToggleColliderTrigger(true);
        move.canMove = true;
        move.StartPlayerMovement();
        PlayerManager.Instance.canChangeAttackMode = true;
    }

    private void WarpToWeapon()
    {
        move.canMove = false;
        //material.DOFloat(1, "_DissolveAmount", warpDuration);
        transform.DOMove(weapon.position, warpDuration).SetEase(Ease.InExpo).OnComplete(() => FinishWarp());
        Rigidbody2D rb = move.GetRigidbody();
        rb.isKinematic= true;
    }

    private void FinishWarp()
    {
        //material.DOFloat(0, "_DissolveAmount", 0.2f);
        move.canMove = true;
        isWarping = false;
        Rigidbody2D rb = move.GetRigidbody();
        rb.isKinematic = false;
        WeaponStartPull();
    }

    public void WeaponStartPull()
    {
        pullPosition = weapon.position;
        weaponRb2D.Sleep();
        weaponRb2D.collisionDetectionMode = CollisionDetectionMode2D.Discrete;
        weaponRb2D.isKinematic = true;
        weapon.DORotate(new Vector3(0, 0, -90), .2f).SetEase(Ease.InOutSine);
        weapon.DOBlendableLocalRotateBy(Vector2.right * 90, .5f);
        throwWeaponScript.activated = true;
        pulling = true;

    }

    private void WeaponThrow()
    {
        hasWeapon = false;
        throwWeaponScript.activated = true;
        weaponRb2D.isKinematic = false;
        weaponRb2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        weaponRb2D.gravityScale = 0;
        weapon.parent = null;
        weapon.eulerAngles = Vector3.zero;
        throwWeaponScript.ToggleColliderTrigger(false);
        throwWeaponScript.ThrowTheWeapon(move.GetHorizontalInput(), throwPower,move.side);
        throwWeaponScript.ResetRangeTimer();
    }

    public Vector3 GetQuadraticCurvePoint(float t, Vector2 p0, Vector2 p1, Vector2 p2)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        return (uu * p0) + (2 * u * t * p1) + (tt * p2);
    }

    public void DisableScript()
    {
        weapon.gameObject.SetActive(false);
    }

    public void EnableScript()
    {
        weapon.gameObject.SetActive(true);
    }
}
