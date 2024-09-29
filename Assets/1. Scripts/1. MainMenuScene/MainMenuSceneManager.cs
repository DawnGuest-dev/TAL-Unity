using System;
using UnityEngine;

public enum MainMenuState
{
    Ready, MainMenu
}

public class MainMenuSceneManager : Singleton<MainMenuSceneManager>
{
    public MainMenuState state = MainMenuState.Ready;

    public MainMenuSceneUIManager uiManager;

    private void Start()
    {
        Debug.Log("MainMenuSceneManager::Start");
    }

    private void Update()
    {
        if (Input.anyKeyDown && state == MainMenuState.Ready)
        {
            if (uiManager != null)
            {
                state = MainMenuState.MainMenu;
                uiManager.SetMainMenu();
            }
        }
    }
}
