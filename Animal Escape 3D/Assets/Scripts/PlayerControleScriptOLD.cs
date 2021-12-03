using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControleScriptOLD : MonoBehaviour
{
    [SerializeField] private FloatingJoystick floatingJoystick;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float jumpForce, minimalSwipeDistance, speed, swipeThreshold;

    private Transform child;
    private Vector2 startTouchPos, endTouchPos, swipeDelta, lastDirection, swipeVelocity;
    private bool isGrounded, isDraging, jumpAllowed;
    private float distToGround, startTime, deltaTime;

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
            startTime = Time.time;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDraging = false;
            endTouchPos = Input.mousePosition;
            deltaTime = Time.time - startTime;
            Reset();
        }

        //Mobile inputs
        if(Input.touches.Length > 0)
        {
            if(Input.touches[0].phase == TouchPhase.Began)
            {
                isDraging = true;
                startTouchPos = Input.mousePosition;
                startTime = Time.time;
            } else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                isDraging = false;
                endTouchPos = Input.mousePosition;
                deltaTime = Time.time - startTime;
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

        //Check if grounded
        if(!Physics.Raycast(transform.position, -Vector2.up, distToGround + 0.1f))
        {
            isGrounded = false;
        } else {
            isGrounded = true;
        }

        //Velocity calculation
        if (deltaTime != 0)
        {
            Vector3 distance = startTouchPos - endTouchPos;
            swipeVelocity = distance / deltaTime;
            Debug.Log("startTime & deltaTime = " + startTime + " & " + deltaTime);
        }
        Debug.Log(swipeVelocity.magnitude);

        //Crossing The Deadzone
        if (swipeDelta.magnitude > minimalSwipeDistance && swipeVelocity.magnitude > swipeThreshold && isGrounded)
        {
            jumpAllowed = true;
            Reset();
        }
        else if (isGrounded)
        {
            Vector3 direction;
            direction = Vector3.forward * floatingJoystick.Vertical + Vector3.right * floatingJoystick.Horizontal;
            rb.velocity = direction * speed;

            lastDirection = direction;
        }

        //normalize
        //child.localRotation = Quaternion.LookRotation(lastDirection);

        if (jumpAllowed)
        {
            rb.AddForce(Vector3.up * jumpForce + Vector3.forward * jumpForce, ForceMode.Impulse);
            jumpAllowed = false;
        }
    }
    /*private IEnumerator SwipeTimer()
    {
        yield return WaitForSeconds(swipeTimer);
        canSwipe = true;
    }*/
    private void Reset()
    {
        startTouchPos = swipeDelta = Vector2.zero;
        isDraging = false;
        startTime = 0;
    }
}
