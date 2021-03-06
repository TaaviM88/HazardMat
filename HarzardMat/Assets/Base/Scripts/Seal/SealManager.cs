﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;
public class SealManager : MonoBehaviour
{
    public float dissolveTime = 1f;
    public float warpTime = 1f;
    bool canMove = false;
    SealSkillState skillMode = SealSkillState.crawl;
    Vector3 startPoint;
    Material material;
    CapsuleCollider2D capsuleCollider2D;
    Collision _collision;
    Rigidbody2D _rb2D;
    //public MonoBehaviour[] scripts;
    public List<MonoBehaviour> scripts = new List<MonoBehaviour>();
    //scripts
    SealMovement sealMovement;
    SealJump jumpScript;
    Hover hoverScript;

    // Start is called before the first frame update
    void Awake()
    {
        startPoint = transform.position;
        _rb2D = GetComponent<Rigidbody2D>();
        material = GetComponent<Renderer>().material;
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        _collision = GetComponent<Collision>();
        sealMovement = GetComponent<SealMovement>();
        jumpScript = GetComponent<SealJump>();
        hoverScript = GetComponent<Hover>();

        scripts.Add(sealMovement);
        scripts.Add(jumpScript);
        scripts.Add(hoverScript);
        var vcam = GameObject.FindObjectOfType<CinemachineVirtualCamera>();
        vcam.Follow = this.gameObject.transform;
        WarpToPosition(PlayerManager.Instance.spawnPoint.transform.position);
        DisableAllSkillScripts();
        //scripts = gameObject.GetComponents<MonoBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        switch(skillMode)
        {
            //movement
            case SealSkillState.crawl:
                EnableScript(sealMovement.ToString());
                
                break;

            case SealSkillState.electricity:

                break;

            case SealSkillState.fat:
                
                break;

            case SealSkillState.flat:

                break;

            case SealSkillState.hover:
                //X-akseli
                EnableScript(hoverScript.ToString());
                hoverScript.UpdateOriginalScaleX(PlayerManager.Instance.side);
                break;

            case SealSkillState.FloatingVertical:
                //Y-akseli
                break;

            case SealSkillState.fly:

                break;

            case SealSkillState.jump:
                EnableScript(jumpScript.ToString());
                jumpScript.UpdateOriginalScaleX(PlayerManager.Instance.side);
                break;

            case SealSkillState.oil:

                break;
        }

        if(skillMode == SealSkillState.hover)
        {
            _rb2D.gravityScale = 0;
        }
        else
        {
            _rb2D.gravityScale = 1;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            EndSummoning();
        }
        if (_collision.IsEverythingColliding())
        {
            EndSummoning();
        }

        if(Input.GetKeyDown(KeyCode.M))
        {
            ToggleSkill();
        }
    }

    public void ChangeSkill(SealSkillState newSkill)
    {
        skillMode = newSkill;
    }

    public void ToggleSkill()
    {

        if ((int)skillMode < System.Enum.GetValues(typeof(SealSkillState)).Length - 1)
        {
            skillMode++;
        }
        else
        {
            skillMode = 0;
        }

        GameEvents.current.UpdateBattleLog("Seal mode is: " + skillMode.ToString());
        //Debug.Log(skillMode);
    }

    private void EnableScript(string scriptName)
    {
        foreach (MonoBehaviour script in scripts)
        {

            if(script.ToString() == scriptName)
            {
                script.enabled = true;
            }
            else 
            {
                script.enabled = false;
            }
        }
    }

    private void DisableAllSkillScripts()
    {

        foreach (MonoBehaviour script in scripts)
        {
            script.enabled = false;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<GiveSkillToSeal>())
        {
            ChangeSkill(collision.GetComponent<GiveSkillToSeal>().GetSkill());
        }
    }
}

