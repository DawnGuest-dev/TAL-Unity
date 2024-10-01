using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
        
        TALSceneManager.Instance.ChangeScene("TestMap");
    }
}
