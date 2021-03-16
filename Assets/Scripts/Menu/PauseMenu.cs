using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;

    private void Awake()
    {
        UIManager.onPlayerFail += Show;
        UIManager.onGameResume += Hide;
    }

    private void Show()
    {
        pauseMenuUI.SetActive(true);
    }
    
    private void Hide()
    {
        pauseMenuUI.SetActive(false);
    }

    public void Resume()
    {
        GameManager.Instance.ResumeGame();
    }

    public void Menu()
    {
        // TODO
    }

    public void Quit()
    {
        // TODO
    }
}
