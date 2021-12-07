using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateUI : MonoBehaviour
{
    public GameObject gameOverUI;
    public GameObject gameWonUI;

    void Start()
    {
        GameEvents.current.caught += GameOver;
        GameEvents.current.finishTrigger += GameWon;
    }

    void GameOver()
    {
        gameOverUI.SetActive(true);
    }

    void GameWon()
    {
        gameWonUI.SetActive(true);
    }


}
