using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;
using System;
using Rebellis;
using UnityEngine.Events;

public class RebelisAPIHandler : MonoBehaviour
{
    private const string BASE_URL = "https://api.rebellis.ai/api/v1/";
    private const string LOGIN_ENDPOINT = "account/login/";
    private const string PROMPT_ENDPOINT = "transaction/prompts/";
    private const string FBX_ENDPOINT = "transaction/create-fbx/";
    private const string UNITY_ENDPOINT = "transaction/create-unity/";

    public RebellisSetting setting;

    public UnityEvent<string,string> OnUnityFileReady;
    public UnityEvent<int> OnProgressStatusChanged;

    public void Login(Action<string> callback)
    {
        StartCoroutine(LoginCoroutine(setting.email, setting.password, callback));
    }

    private IEnumerator LoginCoroutine(string email, string password, Action<string> callback)
    {
        string url = BASE_URL + LOGIN_ENDPOINT;

        string jsonData = JsonUtility.ToJson(new LoginRequest(email, password));
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);

        using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
        {
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Login Success: " + request.downloadHandler.text);
                setting.userdata = JsonUtility.FromJson<RebellisUserModel>(request.downloadHandler.text);
                callback?.Invoke(request.downloadHandler.text);
            }
            else
            {
                Debug.LogError("Login Failed: " + request.error);
                callback?.Invoke(null);
            }
        }
    }

    public void SendPrompt(int workerModelId, string prompt, int repeatNumber)
    {
        StartCoroutine(SendPromptCoroutine(setting.userdata.access_token, workerModelId, prompt, repeatNumber));
        OnProgressStatusChanged?.Invoke(5);
    }

    private IEnumerator SendPromptCoroutine(string token, int workerModelId, string prompt, int repeatNumber)
    {
        string url = BASE_URL + PROMPT_ENDPOINT;

        string jsonData = JsonUtility.ToJson(new PromptRequest(workerModelId, prompt, repeatNumber));
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);

        using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
        {
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", "Bearer " + token);

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Prompt Success: " + request.downloadHandler.text);
                setting.Rebelliprompts = JsonUtility.FromJson<Rebelliprompts>(request.downloadHandler.text);
                StartCoroutine(FetchPromptCoroutine(FetchState.Prompt));
            }
            else
            {
                Debug.LogError("Prompt Failed: " + request.error);
            }
        }
    }

    private IEnumerator FetchPromptCoroutine(FetchState state)
    {
        string url = BASE_URL + PROMPT_ENDPOINT + setting.Rebelliprompts.id+"/";

        using (UnityWebRequest request = new UnityWebRequest(url, "GET"))
        {
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", "Bearer " + setting.userdata.access_token) ;

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Prompt Success: " + request.downloadHandler.text);
                setting.Rebelliprompts = JsonUtility.FromJson<Rebelliprompts>(request.downloadHandler.text);

                switch (state)
                {
                    case FetchState.Prompt:
                        if (setting.Rebelliprompts.prompt_response.prompt_request_step.Count == 0)
                        {
                            yield return new WaitForSeconds(0.75f);
                            StartCoroutine(FetchPromptCoroutine(state));
                            OnProgressStatusChanged?.Invoke(40);
                        }
                        else
                        {
                            StartCoroutine(FetchFBX());
                        }
                        break;
                    case FetchState.FBX:
                        if (setting.Rebelliprompts.prompt_response.prompt_request_step[0].fbx == string.Empty)
                        {
                            yield return new WaitForSeconds(0.75f);
                            StartCoroutine(FetchPromptCoroutine(state));
                            OnProgressStatusChanged?.Invoke(60);
                        }
                        else
                        {
                            StartCoroutine(FetchUnityFile());
                        }
                        break;
                    case FetchState.Unity:
                        if (setting.Rebelliprompts.prompt_response.prompt_request_step[0].unity == string.Empty)
                        {
                            yield return new WaitForSeconds(0.75f);
                            StartCoroutine(FetchPromptCoroutine(state));
                            OnProgressStatusChanged?.Invoke(80);
                        }
                        else
                        {
                            Debug.Log(setting.Rebelliprompts.prompt_response.prompt_request_step[0].unity);
                            OnUnityFileReady?.Invoke(setting.Rebelliprompts.prompt_response.prompt_request_step[0].unity, setting.Rebelliprompts.id);
                        }
                        break;
                }
            }
            else
            {
                Debug.LogError("Prompt Failed: " + request.error);
            }
        }
    }

    private IEnumerator FetchFBX()
    {
        string url = BASE_URL + FBX_ENDPOINT;

        string jsonData = JsonUtility.ToJson(new FBXRequest(setting.Rebelliprompts.prompt_response.prompt_request_step[0].id, 0));
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);

        using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
        {
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", "Bearer " + setting.userdata.access_token);

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Prompt Success: " + request.downloadHandler.text);
                StartCoroutine(FetchPromptCoroutine(FetchState.FBX));
            }
            else
            {
                Debug.LogError("Prompt Failed: " + request.error);
            }
        }
    }

    private IEnumerator FetchUnityFile()
    {
        string url = BASE_URL + UNITY_ENDPOINT;

        string jsonData = JsonUtility.ToJson(new FBXRequest(setting.Rebelliprompts.prompt_response.prompt_request_step[0].id, 0));
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);

        using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
        {
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", "Bearer " + setting.userdata.access_token);

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Prompt Success: " + request.downloadHandler.text);
                StartCoroutine(FetchPromptCoroutine(FetchState.Unity));
            }
            else
            {
                Debug.LogError("Prompt Failed: " + request.error);
            }
        }
    }
}

public enum FetchState
{
    Prompt,
    FBX,
    Unity
}


[System.Serializable]
public class LoginRequest
{
    public string email;
    public string password;

    public LoginRequest(string email, string password)
    {
        this.email = email;
        this.password = password;
    }
}


[System.Serializable]
public class FBXRequest
{
    public string prompt_response_step_id;
    public int height;

    public FBXRequest(string prompt_response_step_id, int height)
    {
        this.prompt_response_step_id = prompt_response_step_id;
        this.height = height;
    }
}

[System.Serializable]
public class PromptRequest
{
    public int worker_model_id;
    public string prompt;
    public int repeat_number;

    public PromptRequest(int workerModelId, string prompt, int repeatNumber)
    {
        this.worker_model_id = workerModelId;
        this.prompt = prompt;
        this.repeat_number = repeatNumber;
    }
}