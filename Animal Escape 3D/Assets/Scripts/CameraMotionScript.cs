using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotionScript : MonoBehaviour
{
    [SerializeField] private Transform GameFinnishTarget;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Vector3 offset;
    
    private void FixedUpdate()
    {
        //Make sure the camera is always above the player
        Vector3 desiredPosition = playerTransform.position + Vector3.up * offset.y;
        transform.position = desiredPosition;

        //Make sure the player is always looking at the level finnish
        transform.LookAt(GameFinnishTarget.position);
    }
}
