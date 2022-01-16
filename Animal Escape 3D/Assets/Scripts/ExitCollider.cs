using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitCollider : MonoBehaviour
{
    [SerializeField] private ParticleSystem particles;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerPrefs.SetInt("level", PlayerPrefs.GetInt("level") + 1);
            particles.Play();
            GameEvents.current.GameWon();
            GoalieFarmer.instance.KnockOver();
            //gameObject.SetActive(false);
        }
    }
}
