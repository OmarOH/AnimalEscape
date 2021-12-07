using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void ReloadCurrentScene()
    {
        Debug.Log("Retrying level");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
