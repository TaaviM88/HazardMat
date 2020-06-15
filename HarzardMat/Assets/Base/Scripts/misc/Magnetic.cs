using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnetic : MonoBehaviour
{

    SpriteRenderer spriteRenderer;
    public Color activatedColor;
    Color originalColor;
    //bool inside;
    public float radius = 5f;
    public float force = 1f;
    public MagneticType magneticType;
    MagneticState magneticState;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        //inside = false;
        magneticState = MagneticState.Inactive;
    }

    private void FixedUpdate()
    {

        if (magneticState == MagneticState.Activate)
        {
            Vector3 magnetField = transform.position - PlayerManager.Instance.gameObject.transform.position;
            float index = (radius - magnetField.magnitude / radius);
            if (magneticType == MagneticType.Push)
            {
                PlayerManager.Instance.AddForce(-force * magnetField * index);
            }
            else
            {
                PlayerManager.Instance.AddForce(force * magnetField * index);
            }
        }

        PlayerOnRage();
        //Debug.Log(inside);
    }

    public void PlayerOnRage()
    {
        if (Vector3.Distance(transform.position,  PlayerManager.Instance.gameObject.transform.position) < radius)
        {
            magneticState = MagneticState.Activate;
            spriteRenderer.color = Color.Lerp(spriteRenderer.color, activatedColor, 5 * Time.deltaTime);
        }
        else
        {
            magneticState = MagneticState.Inactive;
            spriteRenderer.color = Color.Lerp(spriteRenderer.color, originalColor, 5 * Time.deltaTime);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
