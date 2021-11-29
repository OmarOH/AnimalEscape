using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotionScript : MonoBehaviour
{
    [SerializeField] private Transform GameFinnishTarget;
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 offset;
    private void Start()
    {
        offset = new Vector3(player.position.x, player.position.y + 8.0f, player.position.z + 7.0f);
    }
    private void FixedUpdate()
    {
        //Make sure the camera is always above the player
        //Vector3 desiredPosition = playerTransform.position + Vector3.up * offset.y;
        //transform.position = desiredPosition;

        //Make sure the player is always looking at the level finnish
        transform.LookAt(GameFinnishTarget.position);
    }
}
