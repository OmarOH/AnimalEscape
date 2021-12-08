using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalAnimations : MonoBehaviour
{
    [SerializeField] private MovementAnimations animator;
    [SerializeField] private Rigidbody rigidbody;
    [HideInInspector] public bool isCaught;

    // Update is called once per frame
    void Update()
    {
        print(Mathf.Abs(rigidbody.velocity.magnitude));
        if(Mathf.Abs(rigidbody.velocity.magnitude) > 4.5f)
        {
            animator.SetAnimation(gameObject, "Run");
        }
        if(Mathf.Abs(rigidbody.velocity.magnitude) >= 1f && Mathf.Abs(rigidbody.velocity.magnitude) <= 4.5f)
        {
            animator.SetAnimation(gameObject, "Walk");
        }
        if(Mathf.Abs(rigidbody.velocity.magnitude) < 0.9f)
        {
            animator.SetAnimation(gameObject, "Idle");
        }
        if(rigidbody.velocity.y > 0f)
        {
            animator.SetAnimation(gameObject, "Jump");
        }
        if(isCaught)
        {
            animator.SetAnimation(gameObject, "Attack");
        }
    }
}
