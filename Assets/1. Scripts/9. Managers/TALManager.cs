public class TALManager : Singleton<TALManager>
{
    public TALGameDataManager gameDataManager;
    public TALUserDataManager userDataManager;
    
    
    public override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
        
        gameDataManager = GetComponent<TALGameDataManager>();
        userDataManager = GetComponent<TALUserDataManager>();
    }
}
