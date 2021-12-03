using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Net : MonoBehaviour
{

    float timeElapsed;
    float lerpDuration = 0.1f;
    Vector3 startPos;

    GameObject player;
    public GameObject parent;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.transform.parent = transform;           
            other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            startPos = other.gameObject.transform.localPosition;
            StartCoroutine(Lerp());
            StartCoroutine(Caught());
        }
    }

    IEnumerator Lerp()
    {
        while (timeElapsed < lerpDuration)
        {
            player.transform.localPosition = Vector3.Lerp(startPos, new Vector3(0, 0, 0), timeElapsed / lerpDuration);
            player.transform.GetChild(1).localPosition = Vector3.Lerp(player.transform.GetChild(1).localPosition, 
                new Vector3(0, 0.03f, -0.05f), timeElapsed / lerpDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator Caught()
    {
        parent.tag = "Untagged";
        GameObject[] allfarmers = GameObject.FindGameObjectsWithTag("Farmer");
        foreach (GameObject farmer in allfarmers)
        {
            if (farmer.tag == "Farmer")
            {
                farmer.GetComponent<FarmerChase>().GameOver();
                farmer.GetComponent<FarmerChase>().enabled = false;
            }
        }
        yield return new WaitForSeconds(1f);
        GameEvents.current.GameOver();
    }
}
