using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControleScript : MonoBehaviour
{
    [SerializeField] private FloatingJoystick floatingJoystick;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float jumpForce, swipeDistance, speed;

    private Transform child;
    private Vector2 startTouchPos, swipeDelta, oldDirection;
    private bool isGrounded, isDraging, jumpAllowed;
    private float distToGround;

    private void Start()
    {
        child = GetComponentInChildren<Transform>();
        distToGround = GetComponent<Collider>().bounds.extents.y;
    }
    void Update()
    {
        jumpAllowed = false;

        //Standalone inputs
        if (Input.GetMouseButtonDown(0))
        {
            isDraging = true;
            startTouchPos = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDraging = false;
            Reset();
        }

        //Mobile inputs
        if(Input.touches.Length > 0)
        {
            if(Input.touches[0].phase == TouchPhase.Began)
            {
                isDraging = true;
                startTouchPos = Input.mousePosition;
            } else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                isDraging = false;
                Reset();
            }
        }

        //Calculate the distance
        swipeDelta = Vector2.zero;
        if (isDraging)
        {
            if (Input.touches.Length > 0)
                swipeDelta = Input.touches[0].position - startTouchPos;
            else if (Input.GetMouseButton(0))
                swipeDelta = (Vector2)Input.mousePosition - startTouchPos;
        }

        if(!Physics.Raycast(transform.position, -Vector2.up, distToGround + 0.1f))
        {
            isGrounded = false;
        } else {
            isGrounded = true;
        }

        //Crossing The Deadzone
        if (swipeDelta.magnitude > swipeDistance)
        {
            jumpAllowed = true;
            Reset();
        }
        else if (isGrounded)
        {
            Vector3 direction;
            direction = Vector3.forward * floatingJoystick.Vertical + Vector3.right * floatingJoystick.Horizontal;
            rb.velocity = direction * speed;

            //direction = direction.normalized;
            //child.localRotation = Quaternion.LookRotation(direction);
        }

        if (jumpAllowed)
        {
            rb.AddForce(Vector3.up * jumpForce + Vector3.forward * jumpForce, ForceMode.Impulse);
        }
    }
    private void Reset()
    {
        startTouchPos = swipeDelta = Vector2.zero;
        isDraging = false;
    }
}
