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
    public bool isFlipped = false;
    public float seeRange = 5;
    Vector3 target;
    Rigidbody2D _rb2d;
    // Start is called before the first frame update
    void Awake()
    {
        enemy = GetComponent<EnemyManager>();
        _rb2d = GetComponent<Rigidbody2D>();
        //target = PlayerManager.Instance.transform;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        //_rb2d.position = Vector2.MoveTowards(_rb2d.transform.position, moveDir, speed * Time.deltaTime);
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

    public bool LookAtPlayer()
    {
        Vector3 rayCastStartPoint = transform.position + new Vector3(0, 0, 0);
     
        RaycastHit2D hit = Physics2D.Raycast(rayCastStartPoint, transform.localScale.x * transform.right, seeRange);
      
        if(hit.collider != null)
        {
            if (hit.collider.tag == "Player")
            {

                return true;
            }
        }
        else
        {
            FlipEnemy();
        }

        return false;

    }

    public void FlipEnemy()
    {
        Vector3 flipped = transform.localScale;
        flipped.x *= -1;
        transform.localScale = flipped;
        enemy.side = (int)flipped.x;
    }

    public void UpdateTargetTransform(Vector3 newtarget)
    {
        target = newtarget;
    }

    public Vector3 GetTargetTransform()
    {
        return target;
    }

}
