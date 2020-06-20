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

    PlayerSkillState attackMode;
    PlayerMovement move;
    PlayerAnimationScript animeScript;
    Throw throwScript;
    Summon summon;
    GrapplingHook grapplingHook;
    Collision coll;
    PickUp pickUp;
    [Header("References")]
    public GameObject seal;
    public GameObject sealBagPosition;
    public GameObject spawnPoint;
    public List<MonoBehaviour> scripts = new List<MonoBehaviour>();

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
        grapplingHook = GetComponent<GrapplingHook>();
        coll = GetComponent<Collision>();
        pickUp = GetComponent<PickUp>();
        SetCameraToFollowPlayer();
        maxHealth = health;

        scripts.Add(throwScript);
        scripts.Add(summon);
        scripts.Add(grapplingHook);


    }

    void Start()
    {
        GameEvents.current.enablePlayerSkillState += EnableAttackMode;
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

            case PlayerSkillState.WeaponThrow:

                if(!throwScript.enabled)
                    EnableScripts(throwScript.ToString());
                break;

            case PlayerSkillState.SummonFamiliar:
                if (!summon.enabled)
                    EnableScripts(summon.ToString());       
            break;

            case PlayerSkillState.GrapplingHook:
                if(!grapplingHook.enabled)
                EnableScripts(grapplingHook.ToString());
                break;

            case PlayerSkillState.None:
                EnableScripts("");
                if (Input.GetButtonDown("Fire1"))
                {
                    pickUp.PickUpObject();
                }
                break;
        }

        side = move.side;

        sealBagPosition.transform.localScale = new Vector3(side, sealBagPosition.transform.localScale.y, sealBagPosition.transform.localScale.z);
        coll.LookSide(side);
        //Debug.Log($"current attackMode {attackMode} and attackmode current int {(int)attackMode}. Length of enums{System.Enum.GetValues(typeof(AttackModeEnum.AttackMode)).Length}");
        //UIManager.Instance.UpdateCombatLog($"current attackMode {attackMode}");
    }

    public void ToggleAttackMode()
    {
        //Jos vaihdellaan kahden moden välillä  
        //attackMode = attackMode == AttackMode.WeaponThrow ? AttackMode.SummonFamiliar : AttackMode.WeaponThrow;
        if((int)attackMode < System.Enum.GetValues(typeof(PlayerSkillState)).Length -1)
        {
            attackMode++;
        }
        else
        {
            attackMode = 0;
        }

        GameEvents.current.UpdateBattleLog("Player mode is: " + attackMode.ToString());
    }


    public void EnableAttackMode(PlayerSkillState skill)
    {
        if (canChangeAttackMode)
        {
            attackMode = skill;
            GameEvents.current.UpdateBattleLog("Player mode is: " + attackMode.ToString());
        }
        else
        {
            GameEvents.current.UpdateBattleLog(" Can't  change player skill mode");
        }
        
    }

    private void EnableScripts(string scriptName)
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

    public void Summoning()
    {
        canChangeAttackMode = false;
        SetSummoning(true);
        //move.enabled = false;
        FreezePlayerMovement();
        move.enabled = false;
        //throwScript.enabled = false;
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
        canChangeAttackMode = true;
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
        if (!iframeTimerOn)
        {
            if (damageTaken < maxHealth)
            {
                health = Mathf.Max(0, health - damageTaken);
                Debug.Log(health);
                if (health <= 0)
                {
                    Die();
                }

                //move.KnockBack();
                //StartCoroutine(IFramesTimer());
            }
            else
            {
                health = Mathf.Max(0, health - damageTaken);

                if (health <= 0)
                {
                    Die();
                }
            }
        }
    }

    public void Die()
    {
        if (isAlive == true)
        {
            /*isAlive = false;
            move.canMove = false;
            move.PauseMovements();
            GameManager.Instance.PlayerDies();
            */
            Debug.Log("Player Dies");
        }
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
        move.ToggleCanMove(false);
        move.FreezePlayerXMovement();
    }

    public void StartPlayerMovement()
    {
        move.StartPlayerMovement();
        move.ToggleCanMove(true);
    }

    public void AddForce(Vector2 force)
    {
        move.AddForce(force);
    }
}
