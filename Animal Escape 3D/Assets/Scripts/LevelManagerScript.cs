using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManagerScript: MonoBehaviour
{
    public static LevelManagerScript instance;
    [SerializeField] private RectTransform levelCompletePanel;
    [SerializeField] private Text levelText;
    [SerializeField] private Text completeLevelText;
    private void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        levelText.text = SceneManager.GetActiveScene().name;
    }

    public void UpdateLevelCompleteText()
    {
        levelCompletePanel.gameObject.SetActive(true);
        completeLevelText.text = "LEVEL " + PlayerPrefs.GetInt("leveltext").ToString() + " COMPLETE";
    }

    public void LoadNextScene()
    {
        Debug.Log("Going to next level");
        // Increase level.
        PlayerPrefs.SetInt("level", PlayerPrefs.GetInt("level") + 1);

        // Loop the levels.
        if (PlayerPrefs.GetInt("level") > Loader.totalLevels)
            PlayerPrefs.SetInt("level", 1);

        // Go to next level.
        SceneManager.LoadScene(PlayerPrefs.GetInt("level").ToString());
    }
    public void ReloadCurrentScene()
    {
        Debug.Log("Retrying level");
        SceneManager.LoadScene(PlayerPrefs.GetInt("level").ToString());
    }
}
