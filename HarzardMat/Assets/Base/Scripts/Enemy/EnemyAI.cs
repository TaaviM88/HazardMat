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
                    pathfind.UpdateTargetTransform(startingPosition);

                }
                break;
            case State.Idle:
                    FindTarget();
                break;

            case State.ChaseTarget:
                //pathfindingMovement.MoveToTimer(PlayerManager.Instance.GetPlayerPosition());
                if (Vector3.Distance(transform.position, PlayerManager.Instance.transform.position) < attackRange && eAttack.GetCanAttack())
                {
                    //pathfindingMovement.StopMoving();
                    //shoot.FireWeapon((PlayerManager.Instance.GetPlayerPosition() - transform.position));
                    
                    anime.SetTrigger("Attack");
                    SetState(State.Attacking);
                    //Debug.Log("Attacking");
                }
                else
                {
                    SetState(State.Idle);
                }
                break;
            case State.Attacking:
                //Attacking

                break;
            case State.Spawning:
                //Spawn Animation
                break;
        }

        //Debug.Log(IsInSpawn() + " state " + state );

    }

    private Vector3 GetRoamingPosition()
    {
        throw new NotImplementedException();
    }

    private void FindTarget()
    {
        if(Vector3.Distance(transform.position,PlayerManager.Instance.transform.position) < targetRange)
        {
            anime.SetBool("isWalking", true);
            pathfind.UpdateTargetTransform(PlayerManager.Instance.transform.position);
            SetState(State.ChaseTarget);
        }
        else if(IsInSpawn())
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
