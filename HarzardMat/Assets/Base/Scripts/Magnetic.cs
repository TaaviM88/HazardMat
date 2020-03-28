using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnetic : MonoBehaviour
{

    SpriteRenderer spriteRenderer;
    public Color activatedColor;
    Color originalColor;
    bool inside;
    public float radius = 5f;
    public float force = 1f;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        inside = false;
    }

    private void Update()
    {

        if (inside)
        {
            Vector3 magnetField = transform.position - PlayerManager.Instance.gameObject.transform.position;
            float index = (radius - magnetField.magnitude / radius);
            PlayerManager.Instance.AddForce(-force * magnetField * index);
        }

        PlayerOnRage();

        Debug.Log(inside);
    }

    public void PlayerOnRage()
    {
        if (Vector3.Distance(transform.position,  PlayerManager.Instance.gameObject.transform.position) < radius)
        {
            inside = true;
            spriteRenderer.color = Color.Lerp(spriteRenderer.color, activatedColor, 5 * Time.deltaTime);
        }
        else
        {
            inside = false;
            spriteRenderer.color = Color.Lerp(spriteRenderer.color, originalColor, 5 * Time.deltaTime);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
