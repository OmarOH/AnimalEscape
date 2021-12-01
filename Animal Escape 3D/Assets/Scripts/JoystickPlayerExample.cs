using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickPlayerExample : MonoBehaviour
{
    [SerializeField] private GameObject pigObject;

    private Vector3 oldDirection;

    public float speed;
    public FloatingJoystick floatingJoystick;
    public Rigidbody rb;
    private void Start()
    {
        oldDirection = Vector3.forward * floatingJoystick.Vertical + Vector3.right * floatingJoystick.Horizontal;
    }
    public void FixedUpdate()
    {
        Vector3 direction;
        if (Input.GetMouseButton(0))
        {
            direction = Vector3.forward * floatingJoystick.Vertical + Vector3.right * floatingJoystick.Horizontal;
            rb.velocity = direction * speed;

            oldDirection = direction;
        }
        else
        {
            direction = oldDirection;
        }
        direction = direction.normalized;
        pigObject.transform.rotation = Quaternion.LookRotation(direction);
    }
}