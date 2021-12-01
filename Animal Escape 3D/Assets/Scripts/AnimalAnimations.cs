﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalAnimations : MonoBehaviour
{
    [SerializeField] private MovementAnimations animator;
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private ParticleSystem walkingParticles;

    // Update is called once per frame
    void Update()
    {
        if(Mathf.Abs(rigidbody.velocity.magnitude) > 4.5f)
        {
            animator.SetAnimation(gameObject, "Run");
            walkingParticles.Play();
        }
        if(Mathf.Abs(rigidbody.velocity.magnitude) >= 3.7f && Mathf.Abs(rigidbody.velocity.magnitude) <= 4.5f)
        {
            animator.SetAnimation(gameObject, "Walk");
            walkingParticles.Play();
        }
        if(Mathf.Abs(rigidbody.velocity.magnitude) < 3.6f)
        {
            animator.SetAnimation(gameObject, "Idle");
        }
        if(rigidbody.velocity.y > 0f)
        {
            animator.SetAnimation(gameObject, "Jump");
        }
    }
}
