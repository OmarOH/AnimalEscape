using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControleScript : MonoBehaviour
{
    [SerializeField] private FloatingJoystick floatingJoystick;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float jumpForce, minimalSwipeDistance, speed, timeToSwipe;
    [SerializeField] private Transform child;

    private Vector2 startTouchPos, swipeDelta;
    private bool isGrounded, isDraging, jumpAllowed;
    private bool swipeTimerPassed = false;
    private float distToGround;

    public bool JumpingState{
        get{return !isGrounded;}
        set{isGrounded = value;}
    }

    private void Start()
    {
        distToGround = GetComponent<Collider>().bounds.extents.y;
    }
    void Update()
    {
        //Standalone inputs
        if (Input.GetMouseButtonDown(0))
        {
            isDraging = true;
            startTouchPos = Input.mousePosition;
            StartCoroutine(SwipeTimer());
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
                StartCoroutine(SwipeTimer());
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

        //Check if grounded
        if(!Physics.Raycast(transform.position, -Vector2.up, distToGround + 0.1f))
        {
            isGrounded = false;
        } else {
            isGrounded = true;
        }

        //always move first
        if (isGrounded)
        {
            jumpAllowed = true;
            MoveCharacter();
        }

        //Do jump if allowed
        if (swipeDelta.magnitude > minimalSwipeDistance && isGrounded && !swipeTimerPassed && jumpAllowed)
        {
            jumpAllowed = false;
            Jump();
            Reset();
        }

        //Object rotates in walking direction
        child.localRotation = Quaternion.LookRotation(rb.velocity);
    }
    private void Jump()
    {  
        rb.AddForce(Vector3.up * jumpForce + Vector3.forward * jumpForce, ForceMode.Impulse);
    }
    private void MoveCharacter()
    {
        Vector3 direction;
        direction = Vector3.forward * floatingJoystick.Vertical + Vector3.right * floatingJoystick.Horizontal;
        rb.velocity = new Vector3(direction.x * speed, rb.velocity.y, direction.z * speed);
    }
    private IEnumerator SwipeTimer()
    {
        yield return new WaitForSeconds(timeToSwipe);
        swipeTimerPassed = true;
        yield return null;
    }
    private void Reset()
    {
        startTouchPos = swipeDelta = Vector2.zero;
        isDraging = false;
        swipeTimerPassed = false;
    }
}
