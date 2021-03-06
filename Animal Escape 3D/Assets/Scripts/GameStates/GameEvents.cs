using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    private void Awake()
    {
        current = this;
    }

    public event Action finishTrigger;
    public void GameWon()
    {
        if (finishTrigger != null)
        {
            finishTrigger();
        }
    }

    public event Action caught;
    public void GameOver()
    {
        if (caught != null)
        {
            caught();
        }
    }
}
