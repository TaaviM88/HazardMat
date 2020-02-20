using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;
public class PlayerManager : MonoBehaviour, ITakeDamage<float>, IDie
{
    public static PlayerManager Instance;
    //private enum AttackMode {WeaponThrow,SummonFamiliar};
    //private AttackMode attackMode = AttackMode.WeaponThrow;
    AttackModeEnum.AttackMode attackMode;
    PlayerMovement move;
    PlayerAnimationScript animeScript;
    [Header("References")]
    public GameObject seal;
    public GameObject sealBagPosition;
    public GameObject spawnPoint;
    [Space]
    [Header("Parametres")]
    public float health = 100;
    public float IframeCooldown = 0.5f;
    public bool canChangeAttackMode = true;
    public int side = 1;

    bool iframeTimerOn = false;
    bool isAlive = true;
    bool summoning = false;
    bool summoned = false;
    float maxHealth;
    float originalFieldofView = 0;
    Throw throwScript;
    Summon summon;

    // Start is called before the first frame update
    void Awake()
    {
        #region Singelton check
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        #endregion
        DontDestroyOnLoad(gameObject);
        throwScript = GetComponent<Throw>();
        move = GetComponent<PlayerMovement>();
        animeScript = GetComponent<PlayerAnimationScript>();
        summon = GetComponent<Summon>();
        SetCameraToFollowPlayer();
        maxHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        if (summoned)
        {
            return;
        }

        if (Input.GetButtonDown("Fire3"))
        {
            if(canChangeAttackMode)
            {
                ToggleAttackMode();
            }
        }
        
        switch(attackMode)
        {

            case AttackModeEnum.AttackMode.WeaponThrow:
                if(!throwScript.enabled)
                {
                    throwScript.EnableScript();
                    throwScript.enabled = true;
                }
                if(summon.enabled)
                {
                    summon.enabled = false;
                }

                break;

            case AttackModeEnum.AttackMode.SummonFamiliar:
                if(throwScript.enabled)
                {
                    throwScript.DisableScript();
                    throwScript.enabled = false;
                    if(!summon.enabled)
                    {
                        summon.enabled = true;
                    }
                    
                }
                
            break;

            case AttackModeEnum.AttackMode.None:
                if (throwScript.enabled)
                {
                    throwScript.DisableScript();
                    throwScript.enabled = false;
                }

                if (summon.enabled)
                {
                    summon.enabled = false;
                }

                break;
        }
        side = move.side;

        sealBagPosition.transform.localScale = new Vector3(side, sealBagPosition.transform.localScale.y, sealBagPosition.transform.localScale.z);
        //Debug.Log($"current attackMode {attackMode} and attackmode current int {(int)attackMode}. Length of enums{System.Enum.GetValues(typeof(AttackModeEnum.AttackMode)).Length}");
        //UIManager.Instance.UpdateCombatLog($"current attackMode {attackMode}");
    }

    public void ToggleAttackMode()
    {
        //Jos vaihdellaan kahden moden välillä  
        //attackMode = attackMode == AttackMode.WeaponThrow ? AttackMode.SummonFamiliar : AttackMode.WeaponThrow;

        if((int)attackMode < System.Enum.GetValues(typeof(AttackModeEnum.AttackMode)).Length -1)
        {
            attackMode++;
        }
        else
        {
            attackMode = 0;
        }
    }

    public void Summoning()
    {
        SetSummoning(true);
        //move.enabled = false;
        FreezePlayerMovement();
        move.enabled = false;
        throwScript.enabled = false;
        summon.enabled = false;
        summoned = true;
        sealBagPosition.gameObject.SetActive(false);
    }

    public void EndSummon()
    {
        SetSummoning(false);
        move.enabled = true;
        summon.enabled = true;
        summoned = false;
        SetCameraToFollowPlayer();
        sealBagPosition.gameObject.SetActive(true);
        StartPlayerMovement();
    }

    public void SetSummoning(bool newbool)
    {
        summoning = newbool;
    }

    public void GoToPosition(Vector2 pos)
    {
        gameObject.transform.position = pos;
    }

    public void Damage(float damageTaken)
    {
        throw new System.NotImplementedException();
    }

    public void Die()
    {
        throw new System.NotImplementedException();
    }

    public void SetCameraToFollowPlayer()
    {
        var vcam = GameObject.FindObjectOfType<CinemachineVirtualCamera>();
        vcam.Follow = this.gameObject.transform;
        /*if(originalFieldofView == 0)
        {
            originalFieldofView = vcam.m_Lens.FieldOfView;
        }

        if(vcam.m_Lens.FieldOfView != originalFieldofView)
        {
            
            vcam.m_Lens.FieldOfView = Mathf.Lerp (vcam.m_Lens.FieldOfView ,originalFieldofView,10);
            
        }*/
    }

    public void DissolveSeal(float duration)
    {
        seal.GetComponent<Renderer>().material.DOFloat(1, "_DissolveAmount", duration);
    }

    public void DissolveSealBack(float duration)
    {
        seal.GetComponent<Renderer>().material.DOFloat(0, "_DissolveAmount", duration);
    }

    public void FreezePlayerMovement()
    {
        move.canMove = false;
        move.FreezePlayerXMovement();
    }

    public void StartPlayerMovement()
    {
        move.StartPlayerMovement();
        move.canMove = true;
    }
}
