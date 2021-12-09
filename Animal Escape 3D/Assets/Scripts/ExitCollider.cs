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
            particles.Play();
            GameEvents.current.GameWon();
            //gameObject.SetActive(false);
        }
    }
}
