using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftArea : MonoBehaviour
{

    private void OnTriggerEnter(Collider collision)
    {
        Physics.IgnoreCollision(gameObject.GetComponent<BoxCollider>(), collision);
        if (collision.CompareTag("Player"))
        {
            GameEvents.current.GameOver();
            PlayerControleScript.speed = 0;
        }
    }
}
