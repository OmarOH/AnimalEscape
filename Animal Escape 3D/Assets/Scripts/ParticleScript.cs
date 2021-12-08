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
        spawnParticles = true;
    }
    void Update()
    {
        if(player.JumpingState && spawnParticles)
        {
            walkingParticles.Pause();
            SpawnJumpParticles();
        }
        else
        {
            walkingParticles.Play();
            if(!player.JumpingState && !spawnParticles)
            {
                walkingParticles.Pause();
                spawnParticles = true;
            }
        }
    }

    void SpawnJumpParticles()
    {
        StartCoroutine(InstatiateParticles());
        spawnParticles = false;
    }

    IEnumerator InstatiateParticles()
    {
        ParticleSystem jumpParts = Instantiate(jumpParticles, gameObject.transform.position, Quaternion.identity);
        StartCoroutine(DestroyAfterSeconds(jumpParts.gameObject, 1f));
        yield return new WaitForSeconds(1f);
    }
    IEnumerator DestroyAfterSeconds(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(obj);
    }
}
