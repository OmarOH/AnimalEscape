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


    // Start is called before the first frame update
    void Start()
    {
        minPos = new Vector3(transform.position.x - 5, transform.position.y, transform.position.z);
        maxPos = new Vector3(transform.position.x + 5, transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {

        
        if (timeElapsed < lerpDuration)
        {
            transform.position = Vector3.Lerp(minPos, maxPos, timeElapsed / lerpDuration);
            timeElapsed += Time.deltaTime;
        }
        else if(secondTimer < lerpDuration)
        {
            transform.position = Vector3.Lerp(maxPos, minPos, secondTimer / lerpDuration);
            secondTimer += Time.deltaTime;
        }
        else
        {
            secondTimer = 0;
            timeElapsed = 0;
        }


    }
}
