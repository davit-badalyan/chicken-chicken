using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("UIManager is NULL");
            }

            return instance;
        }
    }
    
    public delegate void Action();
    public static event Action onGameOpen;
    public static event Action onGameStart;
    public static event Action onPlayerFail;
    public static event Action onGameResume;

    public GameObject backgroundPanel;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        ShowMainMenu();
    }

    public void ShowMainMenu()
    {
        backgroundPanel.SetActive(true);
        onGameOpen?.Invoke();
    }

    public void HideMainMenu()
    {
        backgroundPanel.SetActive(false);
        onGameStart?.Invoke();
    }

    public void ShowPauseMenu()
    {
        backgroundPanel.SetActive(true);
        onPlayerFail?.Invoke();
    }

    public void HidePauseMenu()
    {
        backgroundPanel.SetActive(false);
        onGameResume?.Invoke();
    }
}
