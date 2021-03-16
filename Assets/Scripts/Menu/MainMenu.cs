using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuUI;
    
    void Awake()
    {
        UIManager.onGameOpen += Show;
        UIManager.onGameStart += Hide;
    }

    private void Show()
    {
        mainMenuUI.SetActive(true);
    }
    
    private void Hide()
    {
        mainMenuUI.SetActive(false);
    }

    public void Play()
    {
        GameManager.Instance.StartGame();
    }

    public void Options()
    {
        // TODO
    }

    public void Quit()
    {
        // TODO
    }
}
