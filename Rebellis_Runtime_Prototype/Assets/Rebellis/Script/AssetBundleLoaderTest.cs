using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class AssetBundleLoaderTest : MonoBehaviour
{

    public Animator animator;
    // URL to the AssetBundle (replace with your actual URL)
    private string bundleURL = "https://bitnerdstudio.com/test/067d45f5-e28f-7424-8000-9ae7a9e11b43";
    private string assetName = "067d45f5-e28f-7424-8000-9ae7a9e11b43"; // Replace with the actual asset name in the bundle

    void Start()
    {
        animator = GetComponent<Animator>();
        // Start the coroutine to load the AssetBundle
        StartCoroutine(LoadAssetBundleFromURL(bundleURL));
    }

    IEnumerator LoadAssetBundleFromURL(string url)
    {
        yield return new WaitForSeconds(1);
        Debug.Log($"Attempting to load AssetBundle from URL: {url}");
        UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(url);

        // Start the request
        yield return request.SendWebRequest();

        // Check if the request was successful
        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"Failed to load AssetBundle: {request.error}");
            yield break;
        }

        // Get the AssetBundle from the request
        AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(request);
        if (bundle == null)
        {
            Debug.LogError("Failed to load AssetBundle from the downloaded content.");
            yield break;
        }

        Debug.Log("AssetBundle successfully downloaded and loaded!");

        // Load a specific asset from the AssetBundle
        AnimationClip asset = bundle.LoadAsset<AnimationClip>(assetName);
        if (asset != null)
        {
            Debug.Log($"Successfully loaded asset: {assetName}");

            // Apply the AnimationClip to the Animator
            AnimatorOverrideController overrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
            overrideController["rebel"] = asset; // Replace "rebel" with your original clip name
            animator.runtimeAnimatorController = overrideController;

            Debug.Log($"AnimationClip '{assetName}' applied to the Animator.");
        }
        else
        {
            Debug.LogError($"Asset '{assetName}' not found in AssetBundle.");
        }

        // Unload the AssetBundle but keep loaded assets
        bundle.Unload(false);
    }
}
