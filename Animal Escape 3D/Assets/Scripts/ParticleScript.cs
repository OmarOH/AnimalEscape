using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleScript : MonoBehaviour
{
    private PlayerControleScript player;
    private ParticleSystem walkingParticles;
    private bool spawnParticles;
    [SerializeField] private ParticleSystem jumpParticles;

    void Start()
    {
        walkingParticles = GetComponent<ParticleSystem>();
        player = transform.parent.GetComponent<PlayerControleScript>();
    }
    void Update()
    {
        if(player.JumpingState)
        {
            walkingParticles.Pause();
            SpawnJumpParticles();
        }
        else
        {
            walkingParticles.Play();
        }
    }

    void SpawnJumpParticles()
    {
        if(spawnParticles)
        {
            ParticleSystem jumpParts = Instantiate(jumpParticles, gameObject.transform.position, Quaternion.identity);
            StartCoroutine(DestroyAfterSeconds(jumpParts.gameObject, 1f));
        }
    }

    IEnumerator DestroyAfterSeconds(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(obj);
    }
}
