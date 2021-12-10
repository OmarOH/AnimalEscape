using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalieFarmer : MonoBehaviour
{
    Vector3 minPos;
    Vector3 maxPos;

    float timeElapsed;
    float secondTimer;
    float lerpDuration = 3;
    bool lerp = true;

    float rotateTimer;
    float rotateDuration = 0.5f;
    bool rotate = false;
    private Vector3 currentAngle;
    float targetAngle;
    public float multiplier;
    [SerializeField] private ParticleSystem keeperParticles;
    public GameObject body;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        minPos = new Vector3(transform.position.x - 5, transform.position.y, transform.position.z);
        maxPos = new Vector3(transform.position.x + 5, transform.position.y, transform.position.z);
        currentAngle = transform.eulerAngles;
        targetAngle = currentAngle.x - 90;
    }

    // Update is called once per frame
    void FixedUpdate()
    {      
        //Lerping back and forth
        if (timeElapsed < lerpDuration && lerp)
        {
            transform.position = Vector3.Lerp(minPos, maxPos, timeElapsed / lerpDuration);
            timeElapsed += Time.deltaTime;
        }
        else if(secondTimer < lerpDuration && lerp)
        {
            transform.position = Vector3.Lerp(maxPos, minPos, secondTimer / lerpDuration);
            secondTimer += Time.deltaTime;
        }
        else if(lerp)
        {
            secondTimer = 0;
            timeElapsed = 0;
        }

        //Rotating when knocked down
        if (rotateTimer < rotateDuration && rotate)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y + 0.005f, transform.position.z), timeElapsed / lerpDuration);
            transform.eulerAngles = new Vector3(Mathf.LerpAngle(currentAngle.x, targetAngle, rotateTimer / rotateDuration), currentAngle.y, currentAngle.z);
            body.transform.eulerAngles = new Vector3(body.transform.eulerAngles.x, 90, body.transform.eulerAngles.z);
            rotateTimer += Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ParticleSystem particles = Instantiate(keeperParticles, new Vector3(collision.transform.position.x,collision.transform.position.y + 1f,collision.transform.position.z), Quaternion.identity);
            StartCoroutine(DestroyAfterSeconds(particles.gameObject, 1f));
            GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            gameObject.GetComponent<Animator>().enabled = false;
            Vector3 dir = -(gameObject.transform.position - collision.gameObject.transform.position);
            collision.gameObject.GetComponent<Rigidbody>().AddForce(dir * multiplier, ForceMode.Impulse);
            //Physics.IgnoreCollision(gameObject.GetComponent<CapsuleCollider>(), collision.collider);
            rotate = true;
            lerp = false;

            collision.gameObject.GetComponent<PlayerControleScript>().enabled = false;
            collision.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;

            //Turn trail child off
            foreach (Transform tr in player.transform)
            {
                if (tr.tag == "Trail")
                {
                    tr.gameObject.GetComponent<ParticleSystem>().Pause();
                }
            }

            gameObject.GetComponent<CapsuleCollider>().enabled = false;


            StartCoroutine(ResetPlayerScript(collision.gameObject));
        }
    }

    IEnumerator ResetPlayerScript(GameObject pig)
    {
        yield return new WaitForSeconds(1f);
        pig.GetComponent<PlayerControleScript>().enabled = true;
        pig.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        pig.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        //Turn trail child off
        foreach (Transform tr in player.transform)
        {
            if (tr.tag == "Trail")
            {
                tr.gameObject.GetComponent<ParticleSystem>().Play();
            }
        }
    }

    IEnumerator DestroyAfterSeconds(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(obj);
    }
}
