using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControleScript : MonoBehaviour
{
    [SerializeField] private FloatingJoystick floatingJoystick;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform child;
    [SerializeField] private float jumpForce, minimalSwipeDistance, speed, timeToSwipe;
    [SerializeField] private MovementAnimations animator;
    [HideInInspector] public bool isCaught = false;
    private Vector2 startTouchPos, swipeDelta;
    private Vector3 oldDirection, finnishStartPos;
    private bool isGrounded, isDraging, jumpAllowed, isJumping, gameWon;
    private bool hasLanded = true;
    private bool swipeTimerPassed = false;
    private float distToGround;

    public bool IsJumping
    {
        get{return isJumping;}
    }

    public bool IsGrounded
    {
        get{return isGrounded;}
    }

    Vector3 direction;

    private void Start()
    {
        distToGround = GetComponent<Collider>().bounds.extents.y;
        GameEvents.current.finishTrigger += onGameWon;
    }
    void Update()
    {
        if(isCaught)
        {
            print("asdasdasdasd");
            animator.SetAnimation(gameObject, "Attack");
        }
        
        else if(!isJumping)
        {
            if(rb.velocity.magnitude < 0.3f) 
            {
                animator.SetAnimation(gameObject, "Idle");
            }
            else
            {
                animator.SetAnimation(gameObject, "Run");
            }
        }
        else
        {
            animator.SetAnimation(gameObject, "Jump");
        }
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
        if (rb.velocity.magnitude > 0.6f && !gameWon)
        {
            if (!isJumping) {
                child.localRotation = Quaternion.LookRotation(direction);
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
        if (isGrounded && !gameWon)
        {
            jumpAllowed = true;
            MoveCharacter();
        }

        if (Input.GetMouseButtonUp(0))
        {
            //Do jump if allowed
            if (swipeDelta.magnitude > minimalSwipeDistance && isGrounded && !swipeTimerPassed && jumpAllowed && !gameWon)
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
        yield return new WaitForSeconds(0.3f);
        hasLanded = true;
    }
    private IEnumerator FinnishWalkLerp()
    {
        float zDist = 50;
        Vector3 endPos = new Vector3(finnishStartPos.x, finnishStartPos.y, finnishStartPos.z + zDist);

        float elapsed = 0;
        float duration = 4;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            transform.position = Vector3.Lerp(finnishStartPos, endPos, elapsed / duration);
            child.rotation = Quaternion.Euler(Vector3.forward);
            yield return null;
        }
        transform.position = endPos;
    }
    private void ResetValues()
    {
        startTouchPos = swipeDelta = Vector2.zero;
        isDraging = false;
        swipeTimerPassed = false;
        jumpAllowed = false;
    }
    private void onGameWon()
    {
        gameWon = true;
        finnishStartPos = transform.position;
        StartCoroutine(FinnishWalkLerp());
    }
}
