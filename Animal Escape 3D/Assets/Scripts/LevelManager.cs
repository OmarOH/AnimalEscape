using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public Text levelText;
    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        levelText.text = SceneManager.GetActiveScene().name;
    }

    public void ReloadCurrentScene()
    {
        Debug.Log("Retrying level");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadNextScene()
    {
        PlayerPrefs.SetInt("level", PlayerPrefs.GetInt("level") + 1);

        // Loop the levels.
        if (PlayerPrefs.GetInt("level") > Loader.totalLevels)
            PlayerPrefs.SetInt("level", 1);

        // Go to next level.
        SceneManager.LoadScene(PlayerPrefs.GetInt("level").ToString());
    }
}
