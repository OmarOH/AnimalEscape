using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControleScript : MonoBehaviour
{
    [SerializeField] private FloatingJoystick floatingJoystick;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float jumpForce, minimalSwipeDistance, speed, timeToSwipe;
    [SerializeField] private Transform child;

    private Quaternion oldRotation;
    private Vector2 startTouchPos, swipeDelta;
    private bool isGrounded, isDraging, jumpAllowed, isJumping;
    private bool swipeTimerPassed = false;
    private float distToGround;

    public bool JumpingState{
        get{return !isGrounded;}
        set{isGrounded = value;}
    }

    private void Start()
    {
        distToGround = GetComponent<Collider>().bounds.extents.y;
        child.localRotation = new Quaternion(0,0,0,0);
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
            child.localRotation = Quaternion.LookRotation(rb.velocity);
            oldRotation = child.localRotation;
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
                    isJumping = false;
                    rb.velocity = Vector3.zero;
                    Debug.Log("BAHRF");
                    ResetValues();
                    child.transform.localRotation = Quaternion.Euler(0, child.transform.localRotation.y, 0);
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
    }
    private IEnumerator SwipeTimer()
    {
        yield return new WaitForSeconds(timeToSwipe);
        swipeTimerPassed = true;
        yield return null;
    }
    private void ResetValues()
    {
        startTouchPos = swipeDelta = Vector2.zero;
        isDraging = false;
        swipeTimerPassed = false;
        jumpAllowed = false;
    }
}
