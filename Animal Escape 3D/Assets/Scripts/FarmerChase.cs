﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FarmerChase : MonoBehaviour
{
    Vector3 targetPosition;
    bool isChasing = false;
    public NavMeshAgent agent;

    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        InvokeRepeating("SetDestination", 1f, 0.1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isChasing = true;
        }
    }

    private void SetDestination()
    {
        if (isChasing == true)
        {
            targetPosition = player.transform.position;
            agent.destination = targetPosition;
            CheckInJumpDistance();
        }
    }

    private void CheckInJumpDistance()
    {
        float dist = Vector3.Distance(gameObject.transform.position, player.transform.position);
        if (dist <= 5f)
        {
            Jump();
        }
    }

    private void Jump()
    {
        agent.speed = 10f;
    }
}
