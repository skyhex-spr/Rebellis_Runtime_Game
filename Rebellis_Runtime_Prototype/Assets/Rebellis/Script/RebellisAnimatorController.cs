using HighlightPlus;
using NUnit.Framework.Internal;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class RebellisAnimatorController : MonoBehaviour
{
    [HideInInspector]
    public Animator animator;
    // URL to the AssetBundle (replace with your actual URL)
    private string bundleURL = "https://rebellis-transaction-results.s3.eu-central-1.amazonaws.com/067dadb9-ab13-7ca4-8000-909a1101d9e6/067dadbb-046b-704a-8000-39089af19038";
    private string assetName = "067dadb9-ab13-7ca4-8000-909a1101d9e6"; // Replace with the actual asset name in the bundle

    private GameManager _gameManager;
    private HighlightEffect _effect;

    public bool Selected;
    void Start()
    {
        animator = GetComponent<Animator>();

        _gameManager = FindAnyObjectByType<GameManager>();

        _effect = GetComponent<HighlightEffect>();

        _gameManager.RebelisAPIHandler.OnUnityFileReady.AddListener(OnNewAnimationArrived);
    }

    public void ToggleAvatar(bool forceTrue = false)
    {
        _effect.highlighted = !_effect.highlighted;

        if (forceTrue)
        {
            _effect.highlighted = true;
        }

        Selected = _effect.highlighted;
    }

    private void OnNewAnimationArrived(string url, string name)
    {
        bundleURL = url;
        assetName = name;
        StartCoroutine(LoadAssetBundleFromURL(bundleURL));
    }

    IEnumerator LoadAssetBundleFromURL(string url)
    {
        _gameManager.RebelisAPIHandler?.OnProgressStatusChanged?.Invoke(99);

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
            if (Selected)
            {
                Debug.Log($"Successfully loaded asset: {assetName}");

                // Apply the AnimationClip to the Animator
                AnimatorOverrideController overrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
                overrideController["rebel"] = asset; // Replace "rebel" with your original clip name
                animator.runtimeAnimatorController = overrideController;

                Debug.Log($"AnimationClip '{assetName}' applied to the Animator.");

                _effect.highlighted = false;
                Selected = false;
                _gameManager.PanelController.ForceFill();
            }
        }
        else
        {
            Debug.LogError($"Asset '{assetName}' not found in AssetBundle.");
        }

        // Unload the AssetBundle but keep loaded assets
        bundle.Unload(false);
    }
}
