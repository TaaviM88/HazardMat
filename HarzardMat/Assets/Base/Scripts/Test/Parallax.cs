using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class Parallax : MonoBehaviour
{
    [SerializeField] private Vector2 parallaxEffectMultiplier;
    private Transform cameraTransform;
    private Vector3 lastCameraPosition;
    // Start is called before the first frame update
    void Start()
    {
        var vcam = GameObject.FindObjectOfType<CinemachineVirtualCamera>();
       cameraTransform = vcam.transform ;
        lastCameraPosition = cameraTransform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;
        transform.position += new Vector3 (deltaMovement.x * parallaxEffectMultiplier.x, deltaMovement.y * parallaxEffectMultiplier.y,0);
        lastCameraPosition = cameraTransform.position;
    }
}
