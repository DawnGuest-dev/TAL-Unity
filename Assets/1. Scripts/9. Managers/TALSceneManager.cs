using UnityEngine.SceneManagement;

public class TALSceneManager : Singleton<TALSceneManager>
{
    public string currentScene = "StartScene";

    public static string NextScene;

    public void ChangeScene(string sceneToLoad)
    {
        NextScene = sceneToLoad;
        SceneManager.LoadScene("LoadingScene", LoadSceneMode.Additive);
    }

    public void LoadScene(string sceneName, LoadSceneMode mode = LoadSceneMode.Single)
    {
        SceneManager.LoadScene(sceneName, mode);
    }
}
