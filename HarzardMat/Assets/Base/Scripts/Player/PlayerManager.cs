using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, ITakeDamage<float>, IDie
{
    public static PlayerManager Instance;
    //private enum AttackMode {WeaponThrow,SummonFamiliar};
    //private AttackMode attackMode = AttackMode.WeaponThrow;
    AttackModeEnum.AttackMode attackMode;
    [Header("Parametres")]
    public float health = 100;
    public float IframeCooldown = 0.5f;
    public bool canChangeAttackMode = true;
    private bool iframeTimerOn = false;
    bool isAlive = true;
    private float maxHealth;
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
        }
        Debug.Log($"current attackMode {attackMode} and attackmode current int {(int)attackMode}. Length of enums{System.Enum.GetValues(typeof(AttackModeEnum.AttackMode)).Length}");
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
            attackMode = AttackModeEnum.AttackMode.WeaponThrow;
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

}
