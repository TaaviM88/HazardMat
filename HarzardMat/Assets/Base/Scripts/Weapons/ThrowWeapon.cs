using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class ThrowWeapon : MonoBehaviour
{
    [Header("Public References")]
    public GameObject hitEffect;
    [Header("Parameters")]
    //public LayerMask collisionLayer;
    public bool activated;
    public float rotationSpeed;
    public float damge;
    public bool canPulled = false;
    public bool isThrowed = false;
    public float rangeTime = 2;

    float orginalRangeTime;
    Rigidbody2D _rb2d;
    Vector3 throwDirection = Vector2.zero;
    Throw throwScript;
    CircleCollider2D _circleCollider2D;
    // Start is called before the first frame update
    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _rb2d.isKinematic = true;

        _circleCollider2D = GetComponent<CircleCollider2D>();
        orginalRangeTime = rangeTime;

        ToggleColliderTrigger(true);
    }

    public void ToggleColliderTrigger(bool v)
    {
        _circleCollider2D.isTrigger = v;
    }

    // Update is called once per frame
    void Update()
    {
        if(activated)
        {
            transform.localEulerAngles += Vector3.forward * rotationSpeed * Time.deltaTime;
            canPulled = false;
            if(isThrowed)
            {
                if (rangeTime > 0)
                {
                    rangeTime -= Time.deltaTime;
                }
                else
                {
                    //_rb2d.gravityScale = 1;
                    canPulled = true;
                    isThrowed = false;
                    throwScript.WeaponStartPull();
                }
            }
        }

       /* if(canPulled)
        {
            ResetRangeTimer();
        }*/
    }

    public void ThrowTheWeapon(float angle, float power, int looking)
    {
        /*throwDirection = direction;
        if(direction != Vector2.zero)
        {
            _rb2d.AddForce(new Vector2(direction.x, direction.y).normalized * power, ForceMode2D.Impulse);
        }
        else
        {
            _rb2d.AddForce(new Vector2(looking, 0) * power, ForceMode2D.Impulse);
        }*/
       transform.localRotation = Quaternion.Euler(0,0, angle);
        _rb2d.AddForce(transform.right * power, ForceMode2D.Impulse);

        ResetRangeTimer();
        isThrowed = true;
    }

    public void ResetRangeTimer()
    {
        rangeTime = orginalRangeTime;
    }

    public void SetCameraToFollowWeapon()
    {
        var vcam = GameObject.FindObjectOfType<CinemachineVirtualCamera>();
        vcam.Follow = this.gameObject.transform;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //ei ole enemylayer
        if(collision.gameObject.layer != 11)
        {
            //print(collision.gameObject.name);
            _rb2d.Sleep();
            _rb2d.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
            _rb2d.isKinematic = true;
            activated = false;
            canPulled = true;
            isThrowed = false;
        }
        
        if(collision.gameObject.layer == 11)
        {
            collision.gameObject.GetComponent<Sliceable>().Break();
            canPulled = true;
            isThrowed = false;
            throwScript.WeaponStartPull();
        }
    }

    public void AddThrowWeaponScript(Throw script)
    {
        throwScript = script;
    }
}
