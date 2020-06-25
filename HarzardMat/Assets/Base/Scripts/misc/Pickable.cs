using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour
{
   public PickableState state = PickableState.none;
   protected Rigidbody2D rb;
   protected BoxCollider2D boxCollider;
   protected bool canBeLifted = true;
    // Start is called before the first frame update
    protected void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
   protected virtual void Update()
    {
        switch (state)
        {
            case PickableState.none:

                break;

            case PickableState.lifted:
                rb.isKinematic = true;
                boxCollider.isTrigger = true;

                break;

            case PickableState.lowered:
                rb.isKinematic = false;
                boxCollider.isTrigger = false;
                break;

        }
    }

    public void SetObjectSet(PickableState newState)
    {
        state = newState;
    }

    public bool CanLifted ()
    {
        return canBeLifted;
    }
}
