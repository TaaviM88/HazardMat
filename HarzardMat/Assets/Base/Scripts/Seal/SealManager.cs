using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;
public class SealManager : MonoBehaviour
{
    public float dissolveTime = 1f;
    public float warpTime = 1f;
    bool canMove = false;
    SealSkillModeEnum.SkillMode skillMode;
    Vector3 startPoint;
    Material material;
    CapsuleCollider2D capsuleCollider2D;
    Collision _collision;
    Rigidbody2D _rb2D;
    //scripts
    SealMovement sealMovement;
    // Start is called before the first frame update
    void Start()
    {
        startPoint = transform.position;
        _rb2D = GetComponent<Rigidbody2D>();
        material = GetComponent<Renderer>().material;
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        _collision = GetComponent<Collision>();
        sealMovement = GetComponent<SealMovement>();
        var vcam = GameObject.FindObjectOfType<CinemachineVirtualCamera>();
        vcam.Follow = this.gameObject.transform;
        WarpToPosition(PlayerManager.Instance.spawnPoint.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        switch(skillMode)
        {
            //movement
            case SealSkillModeEnum.SkillMode.crawl:
                if(!sealMovement.enabled)
                {
                    sealMovement.enabled = true;
                }
                sealMovement.canMove = canMove;
                break;

            case SealSkillModeEnum.SkillMode.electricity:

                break;

            case SealSkillModeEnum.SkillMode.fat:
                
                break;

            case SealSkillModeEnum.SkillMode.flat:

                break;

            case SealSkillModeEnum.SkillMode.floating:

                break;

            case SealSkillModeEnum.SkillMode.fly:

                break;

            case SealSkillModeEnum.SkillMode.jump:

                break;

            case SealSkillModeEnum.SkillMode.oil:

                break;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            EndSummoning();
        }
        if (_collision.IsEverythingColliding())
        {
            EndSummoning();
        }
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
        Destroy(gameObject, warpTime);
    }
}
