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
    public GameObject aimObject, dotObject;
    GameObject player;
    MovementAnimations ani;
    public GameObject net;

    public float jumpDistance = 6f, dotEndScale;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        ani = gameObject.GetComponent<MovementAnimations>();
        InvokeRepeating("SetDestination", 0.5f, 0.2f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            ani.SetAnimation(gameObject, "Run");
            isChasing = true;
            aimObject.SetActive(true);
            gameObject.GetComponent<SphereCollider>().enabled = false;
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
        if (dist <= jumpDistance)
        {
            Jump();
            isChasing = false;
        }
    }

    private void Jump()
    {
        ani.SetAnimation(gameObject, "Jump");
        agent.speed = 6f;
        agent.angularSpeed = 0f;

        Invoke("TurnNetOn", 0.1f);

        dotObject.SetActive(true);
        StartCoroutine(AimDotGrowth());

        StartCoroutine(DisableFarmer());
    }

    void TurnNetOn()
    {
        net.GetComponent<SphereCollider>().enabled = true;
    }

    IEnumerator DisableFarmer()
    {
        yield return new WaitForSeconds(1f);
        agent.enabled = false;
        ani.enabled = false;
        CancelInvoke();
        rb.constraints = RigidbodyConstraints.FreezeAll;
        gameObject.GetComponent<FarmerChase>().enabled = false;
        net.GetComponent<SphereCollider>().enabled = false;            
    }

    IEnumerator AimDotGrowth()
    {
        float elapsed = 0;
        float duration = 5f;
        Vector3 endScale = new Vector3(dotEndScale, dotEndScale, dotEndScale);
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            dotObject.transform.localScale = Vector3.Lerp(dotObject.transform.localScale, endScale, elapsed / duration);
            yield return null;
        }
    }

    public void GameOver()
    {
        agent.enabled = false;
        ani.SetAnimation(gameObject, "Idle");
        CancelInvoke();
    }

}
