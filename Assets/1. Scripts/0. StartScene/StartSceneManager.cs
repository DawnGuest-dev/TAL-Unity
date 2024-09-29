using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneManager : Singleton<StartSceneManager>
{
    private void Start()
    {
        DawnManager.Instance.OnDawnAccountUpdated += OnAccountUpdated;
        TALManager.Instance.gameDataManager.OnTALGameDataUpdated += OnGameDataUpdate;
        TALManager.Instance.userDataManager.OnTALUserDataUpdated += OnUserDataUpdate;
    }

    private void OnAccountUpdated(DawnAccount account)
    {
        Debug.Log($"StartScene OnAccountUpdated");
        LoadTALData();
    }

    private void OnGameDataUpdate()
    {
        Debug.Log($"StartScene OnGameDataUpdate");
    }
    
    private void OnUserDataUpdate()
    {
        Debug.Log($"StartScene OnUserDataUpdate");
        SceneManager.LoadScene("MainMenuScene");
    }

    [Button("Load Data")]
    public async void LoadTALData()
    {
        // TAL Game Data
        await TALManager.Instance.gameDataManager.LoadTALGameData();
        
        // TAL User Data
        await TALManager.Instance.userDataManager.LoadTALUserData();
    }
}
