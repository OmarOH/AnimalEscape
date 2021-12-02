using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private ParticleSystem keeperParticles;

    void OnTriggerEnter(Collider collider)
    {
        if(collider.CompareTag("Finish"))
        {
            ParticleSystem particles = Instantiate(keeperParticles, gameObject.transform.position, Quaternion.identity);
            StartCoroutine(DestroyAfterSeconds(particles.gameObject, 1f));
        }
    }

    IEnumerator DestroyAfterSeconds(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(obj);
    }
}
