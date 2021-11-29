using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FarmerChase : MonoBehaviour
{
    public Vector3 targetPosition;
    public bool isChasing = false;
    public NavMeshAgent agent;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        InvokeRepeating("SetDestination", 1f, 1f);
    }

    private void FixedUpdate()
    {
        
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
        }
    }
}
