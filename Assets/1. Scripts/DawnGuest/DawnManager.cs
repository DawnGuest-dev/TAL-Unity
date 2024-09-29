using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class DawnAccount
{
    public int userId;
    public string userName;
    public string accessToken;
    public string refreshToken;
}

public class DawnManager : Singleton<DawnManager>
{
    [Title("Dawn Settings")]
    [SerializeField]
    private string authServerUrl;
    
    [Title("Account")]
    [SerializeField]
    private DawnAccount myAccount = new DawnAccount();
    
    // Callbacks
    public event Action<DawnAccount> OnDawnAccountUpdated;
    
    public override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    [Title("Test")]
    [Button("GetDawnProfile")]
    public async void GetDawnProfile()
    {
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            myAccount = new DawnAccount
            {
                userId = 999,
                userName = "testUser",
                accessToken = "accessToken",
                refreshToken = "refreshToken"
            };
            Debug.Log($"Load Dummy User: {myAccount.userName}");
            
            OnDawnAccountUpdated?.Invoke(myAccount);
            return;
        }
        
        var profileJson = await HttpModule.GetAsync(authServerUrl + "/profile", myAccount.accessToken);
        var profileData = JsonUtility.FromJson<DawnHttpResponse.DawnProfileResponse>(profileJson);
        if (myAccount == null)
            myAccount = new DawnAccount();

        myAccount.userId = profileData.id;
        myAccount.userName = profileData.username;
        myAccount.refreshToken = profileData.refresh_token;
        
        OnDawnAccountUpdated?.Invoke(myAccount);
    }

    public int GetMyUserId()
    {
        return myAccount.userId;
    }

    public string GetMyUsername()
    {
        return myAccount.userName;
    }

    public string GetMyRefreshToken()
    {
        return myAccount.refreshToken;
    }

    private void Start()
    {
        myAccount = new DawnAccount();
        if (Application.platform == RuntimePlatform.WindowsPlayer)
        {
            if (ParseCommandLineArgs())
            {
                GetDawnProfile();
            }
            else
            {
                Debug.LogError($"No AccessToken");
            }
        }
        else
        {
            GetDawnProfile();
        }
    }
    
    private bool ParseCommandLineArgs()
    {
        string[] args = Environment.GetCommandLineArgs();
        
        foreach (var arg in args)
        {
            if (arg.StartsWith("--accessToken="))
            {
                myAccount.accessToken = arg.Substring("--accessToken=".Length);
            }
            else if (arg.StartsWith("--refreshToken="))
            {
                myAccount.refreshToken = arg.Substring("--refreshToken=".Length);
            }
        }

        return string.IsNullOrEmpty(myAccount.accessToken);
    }
}
