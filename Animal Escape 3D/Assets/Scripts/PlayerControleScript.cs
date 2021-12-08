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
    private Vector3 oldDirection;
    private bool isGrounded, isDraging, jumpAllowed, isJumping, hasLanded = true;
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
        
        //Calculate the distance
        if (isDraging)
        {
            if (Input.GetMouseButton(0))
                swipeDelta = (Vector2)Input.mousePosition - startTouchPos;
        }

        //Object rotates in walking direction
        if (rb.velocity.magnitude > 0.6f)
        {
            if (!isJumping) {
                child.localRotation = Quaternion.LookRotation(rb.velocity);
            }
        }

        //Check if grounded
        RaycastHit hit;
        if (!Physics.Raycast(transform.position, -Vector2.up, out hit, distToGround + 0.1f))
        {
            isGrounded = false;
        }
        else
        {
            if (hit.transform.CompareTag("Ground"))
            {
                isGrounded = true;
                if (isJumping)
                {
                    rb.velocity = Vector3.zero;
                    ResetValues();
                    if (hasLanded)
                    {
                        isJumping = false;
                    }
                }
            }
        }

        //always move first
        if (isGrounded)
        {
            jumpAllowed = true;
            MoveCharacter();
        }

        if (Input.GetMouseButtonUp(0))
        {
            //Do jump if allowed
            if (swipeDelta.magnitude > minimalSwipeDistance && isGrounded && !swipeTimerPassed && jumpAllowed)
            {
                isJumping = true;
                hasLanded = false;
                StartCoroutine(raycastTimer());
                Jump();
            }
            ResetValues();
        }
    }
    private void Jump()
    {
        rb.AddForce(child.transform.up * jumpForce + child.transform.forward * jumpForce, ForceMode.Impulse);
    }
    private void MoveCharacter()
    {
        Vector3 direction;
        direction = Vector3.forward * floatingJoystick.Vertical + Vector3.right * floatingJoystick.Horizontal;
        rb.velocity = new Vector3(direction.x * speed, rb.velocity.y, direction.z * speed);
        oldDirection = direction;
    }
    private IEnumerator SwipeTimer()
    {
        yield return new WaitForSeconds(timeToSwipe);
        swipeTimerPassed = true;
    }
    private IEnumerator raycastTimer()
    {
        Debug.Log("started timer");
        yield return new WaitForSeconds(0.3f);
        Debug.Log("ended timer");
        hasLanded = true;
    }
    private void ResetValues()
    {
        startTouchPos = swipeDelta = Vector2.zero;
        isDraging = false;
        swipeTimerPassed = false;
        jumpAllowed = false;
    }
}
