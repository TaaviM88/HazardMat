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
    public float maxForce = 4;
    public LayerMask groundLayer;
    float moveHorizontal;
    float originalScaleX;
    Rigidbody2D _rb2D;
    private Vector3 forceVector;
    public float timer = 0.1f;
    public float height = 4;
    private float orginalHeight;
    // Start is called before the first frame update

    private void Start()
    {
        orginalHeight = transform.position.y;
        _rb2D = GetComponent<Rigidbody2D>();
        _rb2D.gravityScale = 1;
        //StartCoroutine(Timer());
    }

    private void Update()
    {
        //transform.position = ClampHeight((Vector3.up * Mathf.Cos(Time.time * oscillationRate) * ClampRange(oscillationRange)) + transform.position);
        moveHorizontal = Input.GetAxis("Horizontal");
        Vector3 _newPosition = transform.position;
        _newPosition.y -= Mathf.Sin(Time.time) * Time.deltaTime * height;
        if (transform.position.y + orginalHeight < _newPosition.y || transform.position.y - orginalHeight > _newPosition.y)
        {
            //transform.position = _newPosition;
        }

        float y = Mathf.PingPong(transform.position.y, transform.position.y + height);

        transform.position += new Vector3(0, y, 0);
    }
    private void FixedUpdate()
    {

        _rb2D.velocity = new Vector2(moveHorizontal * moveSpeed, _rb2D.velocity.y);
/*
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;
        float distance = 1.0f;

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
        RaycastHit2D hit2 = Physics2D.Raycast(position, direction, distance, groundLayer);
        */
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

    }

    IEnumerator Timer()
    {
        while(true)
        {
            forceVector = Vector2.up * maxForce;

           // _rb2D.AddForce(forceVector);
            yield return new WaitForSeconds(timer);
            
        }
        
    }

   /* private float ClampRange(float value)
    {
        if (transform.position.y > upperHeightLimit)
            upperHeightLimit = transform.position.y;
        if (transform.position.y < lowerHeightLimit)
            lowerHeightLimit = transform.position.y - lowerHeightLimit;
        if (upperHeightLimit < lowerHeightLimit)
            upperHeightLimit = lowerHeightLimit + 0.1f;
        if (lowerHeightLimit > upperHeightLimit)
            lowerHeightLimit = upperHeightLimit + 0.1f;
        if (value != ((upperHeightLimit + lowerHeightLimit) / 2) - 0.25f)
            value = ((upperHeightLimit + lowerHeightLimit) / 2) - 0.25f;
        if (value != value * oscillationRange)
            value *= oscillationRange;

        value *= 0.01f;

        return value;
    }

    private Vector3 ClampHeight(Vector3 value)
    {
        if (value.y < lowerHeightLimit)
            value.y = lowerHeightLimit;
        if (value.y > upperHeightLimit)
            value.y = upperHeightLimit;
        return value;
    }
    */

}

