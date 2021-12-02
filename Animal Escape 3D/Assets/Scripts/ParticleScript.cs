using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleScript : MonoBehaviour
{
    private PlayerControleScript player;
    private ParticleSystem parts;

    void Start()
    {
        parts = GetComponent<ParticleSystem>();
        player = transform.parent.GetComponent<PlayerControleScript>();
    }
    void Update()
    {
        if(player.JumpingState)
        {
            parts.Pause();
        }
        else
        {
            parts.Play();
        }
    }
}
