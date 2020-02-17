using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;
using System;

public class SealMovement : MonoBehaviour
{
    public float moveSpeed;
    public float dissolveTime = 1f;
    public float warpTime = 1f;
    Rigidbody2D _rb2D;
    float moveHorizontal;
    float originalScaleX;
    bool canMove = false;
    Vector3 startPoint;
    Material material;
    CapsuleCollider2D capsuleCollider2D;
    Collision _collision;
    // Start is called before the first frame update
    void Start()
    {
        startPoint = transform.position;
        _rb2D = GetComponent<Rigidbody2D>();
        material = GetComponent<Renderer>().material;
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        _collision = GetComponent<Collision>();
        var vcam = GameObject.FindObjectOfType<CinemachineVirtualCamera>();
        vcam.Follow = this.gameObject.transform;
        //vcam.m_Lens.FieldOfView = Mathf.Lerp(vcam.m_Lens.FieldOfView, 30, 10f);
        originalScaleX = transform.localScale.x;
        WarpToPosition(PlayerManager.Instance.spawnPoint.transform.position);
    }

    private void WarpToPosition(Vector3 newPosition)
    {
        canMove = false;
        capsuleCollider2D.isTrigger = true;
        material.DOFloat(1, "_DissolveAmount", dissolveTime);
        transform.DOMove(newPosition, warpTime).SetEase(Ease.InExpo).OnComplete(() => WarpComplete());
        _rb2D.isKinematic = true;
    }

    private void WarpComplete()
    {
        material.DOFloat(0, "_DissolveAmount", dissolveTime);
        canMove = true;
        _rb2D.isKinematic = false;
        capsuleCollider2D.isTrigger = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!canMove)
        {
            return;
        }
        moveHorizontal = Input.GetAxis("Horizontal");
        float rawHorizontal = Input.GetAxisRaw("Horizontal");
        if (rawHorizontal != 0)
        {
            transform.localScale = new Vector3((int)rawHorizontal * originalScaleX, transform.localScale.y, transform.localScale.z);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            EndSummoning();
        }
        if(_collision.IsEverythingColliding())
        {
            EndSummoning();
        }
    }

    private void FixedUpdate()
    {
        _rb2D.velocity = new Vector2(moveHorizontal * moveSpeed, _rb2D.velocity.y);
    }

    public void UpdateOriginalScaleX(int side)
    {
        originalScaleX = side;
    }

    private void EndSummoning()
    {
        
       Invoke("SummonHasEnded", warpTime);
        WarpToPosition(startPoint);
        DestroyObject();
    }

    private void SummonHasEnded()
    {
        PlayerManager.Instance.EndSummon();
    }

    public void DestroyObject()
    {
        Destroy(gameObject,warpTime);
    }


}
