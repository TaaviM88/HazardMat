using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public enum State
    {
        Spawning,
        Idle,
        Roaming,
        ChaseTarget,
        Attacking,
        Dying,
    }
    private Vector3 startingPosition;
    private Vector3 roamPosition;
    private EnemyPathfinding pathfind;
    private EnemyAttack eAttack;
    Animator anime;
    //private Shooting shoot;

    
    //public float randomMovementRangeX = 10f, randomMovementRangeY = 70f;
    public float targetRange = 9;
    public float attackRange = 3;
    public bool attacking = false;
    private State state;
    bool isAlive = true;

    // Start is called before the first frame update
    void Awake()
    {
        pathfind = GetComponent<EnemyPathfinding>();
        anime = GetComponent<Animator>();
        eAttack = GetComponent<EnemyAttack>();
        startingPosition = transform.position;
        //shoot = GetComponent<Shooting>();
        state = State.Spawning;
    }

    private void Start()
    {
        
        //roamPosition = GetRoamingPosition();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isAlive)
        {
            return;
        }

        switch (state)
        {
            default:
            case State.Roaming:
                //pathfind.MoveToTimer(roamPosition);
                if(IsInSpawn())
                {
                    SetState(State.Idle);
                }
                else
                {
                    //pathfind.MoveToTimer(startingPosition);
                    if(!FindTarget())
                    {
                        pathfind.UpdateTargetTransform(startingPosition);
                    }
                    
                }

                break;
            case State.Idle:
                if(!FindTarget())
                {
                    GotoSpawn();
                }                
                break;

            case State.ChaseTarget:
                //pathfindingMovement.MoveToTimer(PlayerManager.Instance.GetPlayerPosition());
                if (Vector3.Distance(transform.position, PlayerManager.Instance.transform.position) < attackRange && eAttack.GetCanAttack())
                {
                    //pathfindingMovement.StopMoving();
                    //shoot.FireWeapon((PlayerManager.Instance.GetPlayerPosition() - transform.position));


                    //SetState(State.Attacking);
                    //Debug.Log("Attacking");

                    return;
                }
                else
                {
                    SetState(State.Idle);
                }
                break;
            case State.Attacking:
                //Attacking
                if(Vector3.Distance(transform.position, PlayerManager.Instance.transform.position) < attackRange && eAttack.GetCanAttack() && !attacking)
                {
                    anime.SetTrigger("Attack");
                    attacking = true;
                }
                else
                {
                    SetState(State.Idle);
                }
                break;
            case State.Spawning:
                //Spawn Animation
                break;
        }

        Debug.Log(" state " + state );

    }

    private Vector3 GetRoamingPosition()
    {
        throw new NotImplementedException();
    }

    private bool FindTarget()
    {
        if (Vector3.Distance(transform.position, PlayerManager.Instance.transform.position) < targetRange)
        {
            anime.SetBool("isWalking", true);
            pathfind.UpdateTargetTransform(PlayerManager.Instance.transform.position);
            SetState(State.ChaseTarget);
            return true;
        }
        else
            return false;
        
    }

    private void GotoSpawn()
    {
        if (IsInSpawn())
        {
            anime.SetBool("isWalking", false);

        }
        else
        {
            anime.SetBool("isWalking", true);
            SetState(State.Roaming);
        }
    }

    public bool IsInSpawn()
    {
        bool isInSpawn;
        float reachedPositionDistance = 1f;
        if (Vector3.Distance(transform.position, startingPosition) < reachedPositionDistance)
        {
            isInSpawn = true;
        }
        else
        {
            isInSpawn = false;
        }
        return isInSpawn;
    }

    public static Vector3 GetRandomDir()
    {
        return new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized;
    }

    public void SetState(State newState)
    {
        state = newState;
    }

    public State GetState()
    {
        return state;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, targetRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    public void DoDamage()
    {
        //Debug.Log("Teen vahinkoa");
        eAttack.Attack();
    }

}
