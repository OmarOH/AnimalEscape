using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotionScript : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform endGoalTarget;
    [Range(0, 360)]
    [SerializeField] private int rotation;

    private GameObject cameraObject;
    private void Start()
    {
        cameraObject = GetComponentInChildren<Camera>().gameObject;
    }
    private void FixedUpdate()
    {
        //Make sure the player is always looking at the level finnish
        //cameraObject.transform.LookAt(endGoalTarget.position);
        
        Vector3 directionEnd = player.position - endGoalTarget.position;
        directionEnd = directionEnd.normalized;
        Debug.Log(directionEnd);
        transform.localRotation = new Quaternion(0, 0, directionEnd.z, 0);
        /*if () {
            cameraObject.transform.RotateAround(transform.position, Vector3.up, rotation);
        }*/
        transform.Rotate(0f, rotation, 0f, Space.World);
    }
}
