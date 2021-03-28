using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("GameManager is NULL");
            }

            return instance;
        }
    }

    private static bool gameStarted = false;
    public bool GameStarted
    {
        get => gameStarted;
    }

    private static bool playerFailed;
    public bool PlayerFailed
    {
        get => playerFailed;
    }

    private void Awake()
    {
        instance = this;
    }

    public void StartGame()
    {
        gameStarted = true;
        playerFailed = false;
        UIManager.Instance.HideMainMenu();
    }

    public void PauseGame()
    {
        playerFailed = true;
        UIManager.Instance.ShowPauseMenu();
    }

    public void ResumeGame()
    {
        playerFailed = false;
        Player.Instance.Resume();
        UIManager.Instance.HidePauseMenu();
    }
}
