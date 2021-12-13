using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameStateCameraScript : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    void Start()
    {
        GameEvents.current.finishTrigger += GameWon;
    }

    private void GameWon()
    {
        virtualCamera.Follow = null;
    }
}
