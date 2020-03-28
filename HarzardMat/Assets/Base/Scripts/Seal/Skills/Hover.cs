using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Hover : MonoBehaviour
{
  /*   [SerializeField] [Range(0, 100)] private float oscillationRate = 1;
     [SerializeField] [Range(0, 1)] private float oscillationRange = 1;
     [SerializeField] private float upperHeightLimit = 10;
     [SerializeField] private float lowerHeightLimit = 1;
     */
    public float moveSpeed = 4;
    public float hoverSpeed = 1;
    public float maxDistance = 2f;
    public float distance = 1f;
    public float maxForce = 4;
    public float frequency = 1f;
    public float magnitude = 25f;
    public LayerMask groundLayer;
    float moveHorizontal;
    float originalScaleX;
    Rigidbody2D _rb2D;
    private Vector3 forceVector;
    private Vector3 axis;
    private Vector3 pos;
    // Start is called before the first frame update

    private void Start()
    {
        _rb2D = GetComponent<Rigidbody2D>();
        originalScaleX = transform.localScale.x;
        pos = _rb2D.transform.right;
        axis = _rb2D.transform.up;
    }

    private void Update()
    {
        moveHorizontal = Input.GetAxis("Horizontal");
        float rawHorizontal = Input.GetAxisRaw("Horizontal");
        if (rawHorizontal != 0)
        {
            if (PlayerManager.Instance.side > 0)
                transform.localScale = new Vector3((int)rawHorizontal * originalScaleX, transform.localScale.y, transform.localScale.z);
            else
                transform.localScale = new Vector3(-1*(int)rawHorizontal * originalScaleX, transform.localScale.y, transform.localScale.z);
        }
    }

    private void FixedUpdate()
    {
        _rb2D.velocity = new Vector2(moveHorizontal * moveSpeed * pos.x, axis.y * Mathf.Sin(Time.time * frequency) * Time.deltaTime * magnitude);

        /* Toimiva versio
        _rb2D.velocity = new Vector2(moveHorizontal * moveSpeed, _rb2D.velocity.y);
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;
    

        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer);
        if (hit.collider != null)
        {
            if (hit.distance < maxDistance)
            {
                forceVector = Vector3.up * ((maxDistance - hit.distance) / maxDistance) * maxForce;

                _rb2D.AddForce(forceVector);
            }
        }
            
        Debug.DrawRay(position, direction, Color.green);
        hit = Physics2D.Raycast(position, direction, distance, groundLayer);
        */
    }

    /* RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, maxDistance); 

      if(hit.collider != null && hit.collider.gameObject.layer == groundLayer)
      {
          if (hit.distance < maxDistance)
          {
              forceVector = Vector2.up * ((maxDistance - hit.distance) / maxDistance) * maxForce;

              _rb2D.AddForce(forceVector);
          }
      }

      Debug.DrawLine(transform.position, hit.point);*/

    public void UpdateOriginalScaleX(int side)
    {
        originalScaleX = side;
        if(_rb2D != null)
        pos = _rb2D.transform.right;
    }
}

