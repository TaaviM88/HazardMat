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

    Transform target;
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

    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1;

        if (transform.position.x > target.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0, 180f, 0);
            isFlipped = true;
        }
        else if(transform.position.z <target.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
    }

    public void UpdateTargetTransform(Transform newtarget)
    {
        target = newtarget;
    }

    public Transform GetTargetTransform()
    {
        return target;
    }
}
