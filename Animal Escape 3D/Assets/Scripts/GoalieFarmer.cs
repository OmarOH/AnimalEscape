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
    float rotateDuration = 1f;
    bool rotate = false;
    private Vector3 currentAngle;
    float targetAngle;

    // Start is called before the first frame update
    void Start()
    {
        minPos = new Vector3(transform.position.x - 5, transform.position.y, transform.position.z);
        maxPos = new Vector3(transform.position.x + 5, transform.position.y, transform.position.z);
        currentAngle = transform.eulerAngles;
        targetAngle = currentAngle.x - 90;
    }

    // Update is called once per frame
    void Update()
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
            transform.eulerAngles = new Vector3(Mathf.LerpAngle(currentAngle.x, targetAngle, rotateTimer / rotateDuration), currentAngle.y, currentAngle.z);
            rotateTimer += Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Vector3 dir = -(gameObject.transform.position - collision.gameObject.transform.position);
            collision.gameObject.GetComponent<Rigidbody>().AddForce(dir * 10f, ForceMode.Impulse);
            Physics.IgnoreCollision(gameObject.GetComponent<CapsuleCollider>(), collision.collider);
            rotate = true;
            lerp = false;
        }
    }
}
