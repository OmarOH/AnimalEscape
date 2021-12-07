using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private ParticleSystem keeperParticles;

    void OnTriggerExit(Collider collider)
    {
        if(collider.CompareTag("Finish"))
        {
            ParticleSystem particles = Instantiate(keeperParticles, new Vector3(gameObject.transform.position.x,gameObject.transform.position.y + 1f,gameObject.transform.position.z), Quaternion.identity);
            StartCoroutine(DestroyAfterSeconds(particles.gameObject, 1f));
            /*
            collider.GetComponent<Animator>().enabled = false;
            Collider[] colls = collider.GetComponents<Collider>();
            foreach(Collider col in colls)
            {
                col.enabled = false;
            }
            */
        }
    }

    IEnumerator DestroyAfterSeconds(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(obj);
    }
}
