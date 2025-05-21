<p align="center">
  <img src="https://bitnerdstudio.com/rebellis.jpg" alt="Project Logo" width="400">
</p>

<h1 align="center">Rebellis Runtime Prototype</h1>

<p align="center">
  <img src="https://img.shields.io/badge/version-1.0.0-a259ff?style=flat-square&logo=github" alt="Version Badge">
</p>

---

## 📑 Table of Contents

- [Installation](#-installation)
- [Usage](#-usage)
- [Features](#-features)
- [Code Reference](#-code-reference)
- [Built With](#-built-with)
- [Contributing](#-contributing)
- [Contact](#-contact)

---

## 📦 Installation

```bash
# Clone the repository
git clone https://github.com/skyhex-spr/Rebellis_Runtime_Game.git

# Open with Unity
Open the project in Unity 6 or later.
```

📁 **Project Configuration Path:**

```
Assets/Rebellis/RebellisSetting
```

🔐 **Setup Rebellis Account:**

Go to the path above and enter your email and password that you used during account registration.

---

## 🚀 Usage

🧭 **Start the Game**

```
Go to Assets/Scenes/Menu and run the game. Wait for the login screen, then click the 'Play' button to start.
```

🎨 **Generate Animation**

Click the **Generate** button, enter your prompt, and wait for the system to apply animations based on your input.

💡 **Tips for Best Experience:**

1. You can select specific characters to apply animations only to them.
2. You can move and rotate the camera around the map for better scene composition.

---

## 📋 Features

- ⚡ **Real-Time Animation Generation** – Seamlessly generate animations within your game using AI.
- 🧠 **Dynamic NPC Responses** – Enhance non-player characters with intelligent and context-aware reactions.
- 🎭 **Adaptive Character Behaviors** – Bring your characters to life with dynamic and responsive behavior logic.

🔧 **Need AI-powered animation tools?**  
Try our Unity plugin: [Rebellis Character Hub](https://character-hub.rebellis.ai/apihub/unity)

---

## 📚 Code Reference

This section provides insights into key classes and runtime logic within the Rebellis system.

### 📡 `RebelisAPIHandler`
Handles all core interactions with the Rebellis AI API, including login, prompt submission, FBX/Unity file generation, and data retrieval.

```csharp
public class RebelisAPIHandler : MonoBehaviour {
    private const string BASE_URL = "https://api.rebellis.ai/api/v1/";
    private const string LOGIN_ENDPOINT = "account/login/";
    private const string PROMPT_ENDPOINT = "transaction/prompts/";
    private const string FBX_ENDPOINT = "transaction/create-fbx/";
    private const string UNITY_ENDPOINT = "transaction/create-unity/";
    // Additional logic to authenticate and fetch content
}
```

> 📡 This class is responsible for sending authentication credentials, submitting animation prompts, and downloading the appropriate assets from Rebellis servers.

### 🧠 `RebellisAnimatorController`
This class listens for Unity asset events from the API handler and dynamically loads and applies downloaded animation clips at runtime.

```csharp
public class RebellisAnimatorController : MonoBehaviour {
    void Start() {
        animator = GetComponent<Animator>();
        _gameManager = FindAnyObjectByType<GameManager>();
        _effect = GetComponent<HighlightEffect>();

        _gameManager.RebelisAPIHandler.OnUnityFileReady.AddListener(OnNewAnimationArrived);
    }

    IEnumerator LoadAssetBundleFromURL(string url) {
        _gameManager.RebelisAPIHandler?.OnProgressStatusChanged?.Invoke(99);
        UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(url);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success) {
            Debug.LogError($"Failed to load AssetBundle: {request.error}");
            yield break;
        }

        AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(request);
        if (bundle == null) {
            Debug.LogError("Failed to load AssetBundle from the downloaded content.");
            yield break;
        }

        AnimationClip asset = bundle.LoadAsset<AnimationClip>(assetName);
        if (asset != null && Selected) {
            AnimatorOverrideController overrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
            overrideController["rebel"] = asset;
            animator.runtimeAnimatorController = overrideController;
            _effect.highlighted = false;
            Selected = false;
            _gameManager.PanelController.ForceFill();
        } else {
            Debug.LogError($"Asset '{assetName}' not found in AssetBundle.");
        }

        bundle.Unload(false);
    }
}
```

> 🧠 This class serves as the runtime bridge between asset delivery and gameplay execution. It listens to UnityEvents like `OnUnityFileReady` and ensures the animation is seamlessly injected into the current scene.

📁 **You can find both classes in:**
```
Assets/Rebellis/Script
```

---

## 🧰 Built With

- [Rebellis AI](https://demo.rebellis.ai/)
- [Unity 6](https://unity.com/releases/unity-6)

---

## 🤝 Contributing

Contributions, issues and feature requests are welcome!  
Feel free to open a pull request or submit an issue to improve this project.

---

## 📬 Contact

For questions or feedback, visit our website: [https://rebellis.ai](https://rebellis.ai)

---

<p align="center">
  <em>Made with ❤️ by Rebellis AI</em>
</p>
