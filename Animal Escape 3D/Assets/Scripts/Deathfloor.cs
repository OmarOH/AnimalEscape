using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deathfloor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameEvents.current.GameOver();

            GameObject[] allfarmers = GameObject.FindGameObjectsWithTag("Farmer");
            foreach (GameObject farmer in allfarmers)
            {
                if (farmer.tag == "Farmer")
                {
                    farmer.GetComponent<FarmerChase>().GameOver();
                    farmer.GetComponent<FarmerChase>().enabled = false;
                }
            }
        }
    }
}
