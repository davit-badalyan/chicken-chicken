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
        if (onGameOpen != null)
        {
            onGameOpen();
        }
    }

    public void HideMainMenu()
    {
        if (onGameStart != null)
        {
            onGameStart();
        }
    }

    public void ShowPauseMenu()
    {
        if (onPlayerFail != null)
        {
            onPlayerFail();
        }
    }

    public void HidePauseMenu()
    {
        if (onGameResume != null)
        {
            onGameResume();
        }
    }
}
