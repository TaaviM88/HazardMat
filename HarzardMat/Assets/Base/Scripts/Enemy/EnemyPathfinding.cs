using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using DG.Tweening;
public class EnemyPathfinding : MonoBehaviour
{
    public float speed = 1f;
    private EnemyManager enemy;
    private List<Vector3> pathVectorList;
    private int currentPathIndex;
    private float pathfindingTimer;
    private Vector3 moveDir;
    private Vector3 lastMoveDir;

    Rigidbody2D _rb2d;
    // Start is called before the first frame update
    void Awake()
    {
        enemy = GetComponent<EnemyManager>();
        _rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        _rb2d.position = Vector2.MoveTowards(_rb2d.transform.position, moveDir, speed * Time.deltaTime);
    }

    public void MoveTo(Vector3 targetPosition)
    {
        SetTargetPosition(targetPosition);
    }
    public void StopMoving()
    {
        moveDir = Vector3.zero;
    }

    public void MoveToTimer(Vector3 targetPosition)
    {
        if (pathfindingTimer <= 0f)
        {
            SetTargetPosition(targetPosition);
        }
    }

    private void SetTargetPosition(Vector3 targetPosition)
    {
        moveDir = (Vector2)targetPosition;
        //moveDir = (this.transform.position - targetPosition).normalized;
    }
}
