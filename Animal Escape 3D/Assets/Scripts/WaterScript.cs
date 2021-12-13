using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterScript : MonoBehaviour
{
    [SerializeField] private ParticleSystem splashEffect;

    private void OnTriggerEnter(Collider collision)
    {
        Physics.IgnoreCollision(gameObject.GetComponent<BoxCollider>(), collision);
        if(collision.CompareTag("Player"))
        {
            ParticleSystem particles = Instantiate(splashEffect, new Vector3(collision.transform.position.x,collision.transform.position.y,collision.transform.position.z), Quaternion.identity);
            StartCoroutine(DestroyAfterSeconds(particles.gameObject, 2f));
            GameEvents.current.GameOver();
        }
    }

    IEnumerator DestroyAfterSeconds(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(obj);
    }
}
