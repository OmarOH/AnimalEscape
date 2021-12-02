using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FarmerChase : MonoBehaviour
{
    Vector3 targetPosition;
    bool isChasing = false;
    public NavMeshAgent agent;
    public Rigidbody rb;

    GameObject player;   
    MovementAnimations ani;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        ani = gameObject.GetComponent<MovementAnimations>();
        InvokeRepeating("SetDestination", 1f, 0.1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            ani.SetAnimation(gameObject, "Run");
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
            isChasing = false;
        }
    }

    private void Jump()
    {
        ani.SetAnimation(gameObject, "Jump");
        agent.speed = 6f;
        StartCoroutine(DisableFarmer());
    }

    IEnumerator DisableFarmer()
    {
        yield return new WaitForSeconds(1f);
        agent.enabled = false;
        CancelInvoke();
        rb.constraints = RigidbodyConstraints.FreezeAll;
        gameObject.GetComponent<FarmerChase>().enabled = false;        
    }

}
