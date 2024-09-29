using System;
using UnityEngine;

public class MainMenuSceneUIManager : MonoBehaviour
{
    public GameObject startCanvas;
    public GameObject mainCanvas;
    public GameObject loadCanvas;
    public GameObject settingCanvas;
    public GameObject popUpCanvas;
    
    private void Awake()
    {
        MainMenuSceneManager.Instance.uiManager = this;
    }

    public void SetMainMenu()
    {
        Debug.Log("MainMenuSceneUIManager.SetMainMenu()");
    }
}
