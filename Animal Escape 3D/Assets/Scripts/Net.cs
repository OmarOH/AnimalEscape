using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Net : MonoBehaviour
{

    float timeElapsed;
    float lerpDuration = 0.5f;
    bool lerp = false;
    Vector3 startPos;

    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (timeElapsed < lerpDuration && lerp)
        {
            player.transform.localPosition = Vector3.Lerp(startPos, new Vector3(0,0,0), timeElapsed / lerpDuration);
            timeElapsed += Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<AnimalAnimations>().isCaught = true;
            other.transform.parent = transform;           
            other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            startPos = other.gameObject.transform.localPosition;
            lerp = true;
            StartCoroutine(Caught());
        }
    }

    IEnumerator Caught()
    {
        GameObject[] allfarmers = GameObject.FindGameObjectsWithTag("Farmer");
        foreach (GameObject farmer in allfarmers)
        {
            farmer.GetComponent<FarmerChase>().GameOver();
            farmer.GetComponent<FarmerChase>().enabled = false;
        }
        yield return new WaitForSeconds(1f);
        GameEvents.current.GameOver();
    }
}
