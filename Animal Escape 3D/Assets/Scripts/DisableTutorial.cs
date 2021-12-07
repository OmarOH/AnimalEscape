using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableTutorial : MonoBehaviour
{
    public GameObject secondTutorialUI;
    public int id = 0;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(DisableUI());
        }
    }

    IEnumerator DisableUI()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
        if (id == 1)
        {
            secondTutorialUI.SetActive(true);
        }
    }
}
