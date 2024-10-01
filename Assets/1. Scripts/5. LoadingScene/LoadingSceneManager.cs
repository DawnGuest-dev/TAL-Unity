using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneManager : MonoBehaviour
{
    public Slider progressBar;
    public AnimationCurve progressCurve;

    private float _fakeProgress = 0f;
    private float _timeElapsed = 0f;
    
    public float loadingDuration = 5f;

    private void Start()
    {
        StartCoroutine(LoadNextScene());
    }

    private IEnumerator LoadNextScene()
    {
        string nextSceneName = TALSceneManager.NextScene;
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(nextSceneName);
        loadOperation.allowSceneActivation = false;

        while (!loadOperation.isDone)
        {
            // 로딩 진행률 업데이트
            _timeElapsed += Time.deltaTime;
            _fakeProgress = progressCurve.Evaluate(_timeElapsed / loadingDuration);
            
            float realProgress = Mathf.Clamp01(loadOperation.progress / 0.9f);
            
            float progress = Mathf.Min(_fakeProgress, realProgress);
            
            progressBar.value = progress;

            if (progress >= 1f && loadOperation.progress >= 0.9f)
            {
                yield return new WaitForSeconds(1f);
                loadOperation.allowSceneActivation = true;
            }

            yield return null;
        }
        
        SceneManager.UnloadSceneAsync("LoadingScene");

    }
}
