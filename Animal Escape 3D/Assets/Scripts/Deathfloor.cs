using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Deathfloor : MonoBehaviour
{
    public GameObject virtualCamera;
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

            virtualCamera.SetActive(false);
            other.GetComponent<PlayerControleScript>().enabled = false;
            other.gameObject.SetActive(false);
        }
    }
}
