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
    [Header("References")]
    public GameObject seal;

    [Space]
    [Header("Parametres")]
    public float health = 100;
    public float IframeCooldown = 0.5f;
    public bool canChangeAttackMode = true;
    public int side = 1;

    bool iframeTimerOn = false;
    bool isAlive = true;
    float maxHealth;
    Throw throwScript;

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
        maxHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire3"))
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
                
                break;
            case AttackModeEnum.AttackMode.SummonFamiliar:
                if(throwScript.enabled)
                {
                    throwScript.DisableScript();
                    throwScript.enabled = false;
                }
                
            break;

            case AttackModeEnum.AttackMode.None:
                if (throwScript.enabled)
                {
                    throwScript.DisableScript();
                    throwScript.enabled = false;
                }
                
            break;
        }
        side = move.side;
        //Debug.Log($"current attackMode {attackMode} and attackmode current int {(int)attackMode}. Length of enums{System.Enum.GetValues(typeof(AttackModeEnum.AttackMode)).Length}");
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
    }

    public void DissolveSeal(float duration)
    {
        seal.GetComponent<Renderer>().material.DOFloat(1, "_DissolveAmount", duration);
    }

    public void DissolveSealBack(float duration)
    {
        seal.GetComponent<Renderer>().material.DOFloat(0, "_DissolveAmount", duration);
    }
}
