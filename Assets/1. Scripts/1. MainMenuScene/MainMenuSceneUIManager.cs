using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class MainMenuSceneUIManager : MonoBehaviour
{
    [TabGroup("Canvas")]
    public GameObject startCanvas;
    [TabGroup("Canvas")]
    public GameObject mainCanvas;
    [TabGroup("Canvas")]
    public GameObject loadCanvas;
    [TabGroup("Canvas")]
    public GameObject settingCanvas;
    [TabGroup("Canvas")]
    public GameObject popUpCanvas;

    // [TabGroup("Start")]
    
    [TabGroup("Main")]
    public GameObject btnContinue;
    [TabGroup("Main")]
    public GameObject btnLoadGame;
    [TabGroup("Main")]
    public GameObject btnNewGame;
    [TabGroup("Main")]
    public GameObject btnSettings;
    [TabGroup("Main")]
    public GameObject btnCredits;
    [TabGroup("Main")]
    public GameObject btnQuitGame;

    [HideInInspector]
    public GameObject currentCanvas;
    
    private void Awake()
    {
        currentCanvas = startCanvas;
        MainMenuSceneManager.Instance.uiManager = this;
    }

    #region StartCanvas

    public void SetMainMenu()
    {
        Debug.Log("MainMenuSceneUIManager.SetMainMenu()");
        mainCanvas.SetActive(true);
        currentCanvas.SetActive(false);
        currentCanvas = mainCanvas;
    }

    #endregion
    
    #region MainCanvas

    public void OnClickNewGameButton()
    {
        TALSceneManager.Instance.LoadScene("InGameScene");
    }
    
    #endregion
    

}
