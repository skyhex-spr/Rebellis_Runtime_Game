using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // For UI support

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private string sceneName;
    private AsyncOperation asyncLoad;

    private void Start()
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            StartCoroutine(PreloadScene());
        }


        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OpenScene);
        }
    }

    private System.Collections.IEnumerator PreloadScene()
    {
        asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false; 
        while (!asyncLoad.isDone)
        {
            if (asyncLoad.progress >= 0.9f)
            {
                Debug.Log("Scene preloaded and ready!");
                yield break; 
            }
            yield return null;
        }
    }

    public void OpenScene()
    {
        if (asyncLoad != null && asyncLoad.progress >= 0.9f)
        {
            asyncLoad.allowSceneActivation = true;
        }
        else
        {
            Debug.LogWarning("Scene is not ready yet!");
        }
    }
}