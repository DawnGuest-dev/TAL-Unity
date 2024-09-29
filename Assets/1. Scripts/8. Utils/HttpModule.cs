using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Threading.Tasks;

public static class HttpModule
{
    // GET 요청
    public static async Task<string> GetAsync(string url, string accessToken = null)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            if(accessToken != null)
                webRequest.SetRequestHeader("Authorization", $"Bearer {accessToken}");
            
            // 요청 보내기
            var operation = webRequest.SendWebRequest();

            // 요청이 완료될 때까지 대기
            while (!operation.isDone)
            {
                await Task.Yield();
            }

            // 오류 처리
            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"GET 요청 실패: {webRequest.error}");
                return null;
            }

            return webRequest.downloadHandler.text;
        }
    }

    // POST 요청
    public static async Task<string> PostAsync(string url, string jsonData, string accessToken = null)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.PostWwwForm(url, jsonData))
        {
            if(accessToken != null)
                webRequest.SetRequestHeader("Authorization", $"Bearer {accessToken}");
            webRequest.SetRequestHeader("Content-Type", "application/json"); // JSON 형식으로 설정
            webRequest.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(jsonData));
            webRequest.downloadHandler = new DownloadHandlerBuffer();

            // 요청 보내기
            var operation = webRequest.SendWebRequest();

            // 요청이 완료될 때까지 대기
            while (!operation.isDone)
            {
                await Task.Yield();
            }

            // 오류 처리
            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"POST 요청 실패: {webRequest.error}");
                return null;
            }

            return webRequest.downloadHandler.text;
        }
    }

    // DELETE 요청
    public static async Task<string> DeleteAsync(string url)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Delete(url))
        {
            // 요청 보내기
            var operation = webRequest.SendWebRequest();

            // 요청이 완료될 때까지 대기
            while (!operation.isDone)
            {
                await Task.Yield();
            }

            // 오류 처리
            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"DELETE 요청 실패: {webRequest.error}");
                return null;
            }

            return webRequest.downloadHandler.text;
        }
    }
}
