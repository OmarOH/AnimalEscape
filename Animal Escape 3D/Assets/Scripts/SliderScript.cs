using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{
    public Slider slider;

    GameObject player;
    GameObject finish;

    float startDist;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        finish = GameObject.FindGameObjectWithTag("Finish");

        startDist = Vector3.Distance(player.transform.position, finish.transform.position);
        slider.maxValue = startDist;

        //InvokeRepeating("CheckDistance", 0f, 0.1f);
    }

    void CheckDistance()
    {
        float dist = Vector3.Distance(player.transform.position, finish.transform.position);
        slider.value = startDist - dist;
    }

    private void FixedUpdate()
    {
        if (!PlayerControleScript.gameWon)
            CheckDistance();
    }
}
