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
        //shoot = GetComponent<Shooting>();
        state = State.Spawning;
    }

    private void Start()
    {
        startingPosition = transform.position;
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

                float reachedPositionDistance = 1f;
                if (Vector3.Distance(transform.position, roamPosition) < reachedPositionDistance)
                {
                    roamPosition = GetRoamingPosition();
                }
                FindTarget();
                break;
            case State.Idle:
                FindTarget();
                break;

            case State.ChaseTarget:
                //pathfindingMovement.MoveToTimer(PlayerManager.Instance.GetPlayerPosition());
                if (Vector3.Distance(transform.position, PlayerManager.Instance.transform.position) < attackRange)
                {
                    //pathfindingMovement.StopMoving();
                    //shoot.FireWeapon((PlayerManager.Instance.GetPlayerPosition() - transform.position));
                    anime.SetTrigger("Attack");
                    Debug.Log("Attacking");
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

        Debug.Log(state);
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
            pathfind.UpdateTargetTransform(PlayerManager.Instance.transform);
            SetState(State.ChaseTarget);
        }
        else
        {
            anime.SetBool("isWalking", false);
        }
    }

    public static Vector3 GetRandomDir()
    {
        return new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized;
    }

    public void SetState(State newState)
    {
        state = newState;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, targetRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

}
