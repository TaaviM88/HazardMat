using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Throw : MonoBehaviour
{
    private enum State
    {
        Idle,
        Throwing,
    }
    private State state;
    Rigidbody2D weaponRb2D;
    ThrowWeapon throwWeaponScript;
    PlayerMovement move;
    Material material;
    PlayerAnimationScript pAnimScript;
    float returnTime;
    
    private Vector3 origLockPos;
    private Vector3 origLockRot;
    private Vector3 pullPosition;
    private Vector3 arrowOrigPosition;
    [Header("Public References")]
    public Transform weapon;
    public Transform weaponSlot;
    public Transform curvePoint;
    public Transform arrow;
    [Space]
    [Header("Parameteres")]
    public float throwPower = 30;
    public float warpDuration = 2f;

    [Space]
    [Header("Bools")]
    public bool walking = true;

    public bool hasWeapon = true;
    public bool pulling = false;

    private bool wait = false;
    private bool isWarping = false;
    private bool isAiming = false;
    // Start is called before the first frame update
    void Start()
    {
        move = GetComponent<PlayerMovement>();
        material = GetComponent<Renderer>().material;

        weaponRb2D = weapon.GetComponent<Rigidbody2D>();
        throwWeaponScript = weapon.GetComponent<ThrowWeapon>();
        pAnimScript = GetComponent<PlayerAnimationScript>();
        throwWeaponScript.AddThrowWeaponScript(this);

        origLockPos = weapon.localPosition;
        origLockRot = weapon.localEulerAngles;

         arrowOrigPosition = arrow.localPosition;

        if(arrow.gameObject.activeInHierarchy)
        {
            arrow.gameObject.SetActive(false);
        }

        ChangeState(State.Idle);
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Idle:
                if (!move.GetCanMove())
                {
                    ToggleCanMoveThrow(true);
                    move.StartPlayerMovement();
                    hasWeapon = true;
                    pulling = false;
                    wait = false;
                    isWarping = false;
                    isAiming = false;
                }
                break;
            case State.Throwing:
                if (move.GetCanMove())
                {
                    ToggleCanMoveThrow(false);
                }

                break;
        }



        HasWeapon();
        PullWeapon();
        Aim();
        WaitWallGrab();

        weapon.gameObject.GetComponent<SpriteRenderer>().sortingOrder = PlayerManager.Instance.side;
    }

    private void HasWeapon()
    {
        if (hasWeapon)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                ChangeState(State.Throwing);
                PlayerManager.Instance.canChangeAttackMode = false;
                move.FreezePlayerXMovement();
                isAiming = true;
                throwWeaponScript.activated = true;
            }

            if (Input.GetButtonUp("Fire1"))
            {
                //WeaponThrow();
                //pAnimScript.SetTrigger("")
                isAiming = false;
            }
        }
        else
        {
            if (Input.GetButtonDown("Fire1") && throwWeaponScript.canPulled && !isWarping)
            {
                WeaponStartPull();
            }

            if (Input.GetButtonDown("Fire2") && throwWeaponScript.canPulled && !isWarping && !pulling)
            {
                WarpToWeapon();              
            }
            if(state != State.Throwing)
            {
                ChangeState(State.Throwing);
            }
            
        }
    }

    private void PullWeapon()
    {
        if (pulling)
        {
            if (returnTime < 1)
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

    private void WaitWallGrab()
    {
        if (wait)
        {
            if (!move.WallGrab())
            {
                move.WallGrab();
            }
        }
    }

    private void Aim()
    {
        if (isAiming)
        {
            AimArrow();

        }
        else
        {
            if (arrow.gameObject.activeInHierarchy)
            {
                arrow.gameObject.SetActive(false);
            }
        }
    }

    private void AimArrow()
    {
        if (!arrow.gameObject.activeInHierarchy)
        {
            arrow.gameObject.SetActive(true);
            //laukaistaan kerran animaatio. Pitää varmaan keksiä parempi paikka?
            pAnimScript.SetBool("isAiming",isAiming);
            pAnimScript.SetTrigger("StartAim");
        }

        if (arrow.localPosition != arrowOrigPosition)
        {
            arrow.localPosition = arrowOrigPosition;
        }

        if (move.side != 1)
        {
            arrow.GetComponent<Rigidbody2D>().rotation = 180;
        }

        else
        {
            arrow.GetComponent<Rigidbody2D>().rotation = 0;
        }

        if (move.GetHorizontalInput().y >= 0)
        {
            float angle = Vector3.Angle(move.GetHorizontalInput(), Vector3.right);
            Vector3 cross = Vector3.Cross(move.GetHorizontalInput(), Vector3.right).normalized;

            if (move.side != 1 && move.GetHorizontalInput() == Vector2.zero && !move.wallGrab)
            {
                arrow.GetComponent<Rigidbody2D>().rotation = 180;
            }
            else
            {
                arrow.GetComponent<Rigidbody2D>().rotation = angle;
            }

        }
        else
        {
            float angle = Vector3.Angle(move.GetHorizontalInput(), Vector3.right);
            Vector3 cross = Vector3.Cross(move.GetHorizontalInput(), Vector3.right).normalized;
            arrow.GetComponent<Rigidbody2D>().rotation = -angle;
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
        pAnimScript.SetTrigger("CatchWeapon");
        weapon.localScale = new Vector3(1, 1, 1);
        throwWeaponScript.ToggleColliderTrigger(true);
        PlayerManager.Instance.canChangeAttackMode = true;
        PlayerManager.Instance.SetCameraToFollowPlayer();
        ChangeState(State.Idle);
    }

    private void WarpToWeapon()
    {
        isWarping = true;
        move.ShowGhost();
        //move.DisableGhost();
        material.DOFloat(1, "_DissolveAmount", warpDuration);
        PlayerManager.Instance.DissolveSeal(warpDuration);
        transform.DOMove(weapon.position, warpDuration).SetEase(Ease.InExpo).OnComplete(() => FinishWarp());
        move.GetRigidbody().isKinematic= true;
        move.ReleaseWallGrab();
    }

    private void FinishWarp()
    {
        material.DOFloat(0, "_DissolveAmount", warpDuration);
        PlayerManager.Instance.DissolveSealBack(warpDuration);
        isWarping = false;
        Rigidbody2D rb = move.GetRigidbody();
        rb.isKinematic = false;
        //Remove if  you don't want to reset your Y velocity after warp
        rb.velocity = new Vector2(rb.velocity.x, 0);
        StartCoroutine(TryToWallGrab());
        WeaponStartPull();
    }

    IEnumerator TryToWallGrab()
    {
        wait = true;
        yield return new WaitForSeconds(.25f);
        wait = false;
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
        throwWeaponScript.SetCameraToFollowWeapon();
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

    public bool GetIsAming()
    {
        return isAiming;
    }

    private void ToggleCanMoveThrow(bool newBool)
    {
        move.ToggleCanMove(newBool);
    }

    private void ChangeState(State newState)
    {
        state = newState;
    }

    private void OnEnable()
    {
        if(!weapon.gameObject.activeInHierarchy)
        {
            weapon.gameObject.SetActive(true);
            
        }
    }

    private void OnDisable()
    {
        if (weapon.gameObject.activeInHierarchy)
        {
            weapon.gameObject.SetActive(false);
        }
    }
}
