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
    void FixedUpdate()
    {
        if(player.IsGrounded)
        {
            walkingParticles.Play();
            spawnParticles = true;
        }

        else if(player.IsJumping && spawnParticles)
        {
            walkingParticles.Pause();
            SpawnJumpParticles();
            spawnParticles = false;
        }
    }

    void SpawnJumpParticles()
    {
        StartCoroutine(InstatiateParticles());
    }

    IEnumerator InstatiateParticles()
    {
        ParticleSystem jumpParts = Instantiate(jumpParticles, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.2f,gameObject.transform.position.z), Quaternion.identity);
        StartCoroutine(DestroyAfterSeconds(jumpParts.gameObject, 1f));
        yield return new WaitForSeconds(1f);
    }
    IEnumerator DestroyAfterSeconds(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(obj);
    }
}
