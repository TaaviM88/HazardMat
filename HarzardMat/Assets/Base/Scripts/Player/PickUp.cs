using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PickUp : MonoBehaviour
{
    // Start is called before the first frame update
    PlayerCarryingState state = PlayerCarryingState.None;

    public Transform carryPoint;
    public Transform interactivePoint;
    public float range = 1f;
    public float pickupSpeed = 1f;

    public LayerMask pickupLayer;
    GameObject carryingObj;
    private Vector3 orginalIteractivePosition, orginalCarryingPosition;
    void Awake()
    {

        orginalIteractivePosition = interactivePoint.localPosition;
        orginalCarryingPosition = carryPoint.localPosition;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(PlayerManager.Instance.side == 1 && interactivePoint.localPosition != orginalIteractivePosition)
        {
            interactivePoint.localPosition = orginalIteractivePosition;
            carryPoint.localPosition = orginalCarryingPosition;
        }
        
         if (PlayerManager.Instance.side == -1 && interactivePoint.localPosition == orginalIteractivePosition)
         {
            interactivePoint.localPosition = new Vector2(interactivePoint.localPosition.x * -1, interactivePoint.localPosition.y);
            carryPoint.localPosition = new Vector2(carryPoint.localPosition.x * -1, carryPoint.localPosition.y);
         }
    }

    public void PickUpObject() 
    {
        switch (state)
        {
            case PlayerCarryingState.None:
            Collider2D[] pickupObjects;

                pickupObjects = Physics2D.OverlapCircleAll(interactivePoint.position, range, pickupLayer);

                for (int i = 0; i < pickupObjects.Length; i++)
                {
                    if (pickupObjects[i].GetComponent<Pickable>() && pickupObjects[i].transform.parent == null && pickupObjects[i].GetComponent<Pickable>().CanLifted())
                    {
                        PlayerManager.Instance.FreezePlayerMovement();
                        carryingObj = pickupObjects[i].gameObject;

                        carryingObj.GetComponent<Pickable>().SetObjectSet(PickableState.lifted);

                        PlayerManager.Instance.canChangeAttackMode = false;
                        carryingObj.transform.SetParent(carryPoint);
                        carryingObj.transform.DOMove(carryPoint.position, pickupSpeed).SetEase(Ease.InFlash).OnComplete(() => SetPlayerCarrying());
                        break;
                    }
                }
                break;
            case PlayerCarryingState.Carry:
                PlayerManager.Instance.FreezePlayerMovement();
                carryingObj.transform.DOMove(interactivePoint.position, pickupSpeed).SetEase(Ease.InFlash).OnComplete(() => LowerObject());
                break;
        }
            

    }


    private void SetPlayerCarrying()
    {
        carryingObj.transform.localPosition = Vector2.zero;
        state = PlayerCarryingState.Carry;
        PlayerManager.Instance.StartPlayerMovement();
    }

    public void LowerObject()
    {
        carryingObj.GetComponent<Pickable>().SetObjectSet(PickableState.lowered);
        //carryingObj.GetComponent<Rigidbody2D>().isKinematic = false;
        //carryingObj.GetComponent<BoxCollider2D>().isTrigger = false;
        carryingObj.transform.parent = null;
        carryingObj = null;
        state = PlayerCarryingState.None;
        PlayerManager.Instance.StartPlayerMovement();
        PlayerManager.Instance.canChangeAttackMode = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(interactivePoint.position, range);
    }
}
