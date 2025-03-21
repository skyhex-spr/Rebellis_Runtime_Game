using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class IntialMenuController : MonoBehaviour
{
    private RebelisAPIHandler _rebelisAPIHandler;
    private Coroutine _dots;

    public TextMeshProUGUI statusText;
    public TextMeshProUGUI MainText;
    private string baseText = "";
    private int dotCount = 0;

    void Start()
    {
        _rebelisAPIHandler = GetComponent<RebelisAPIHandler>();

        _rebelisAPIHandler.Login(HandleLoginResponse);

        _dots = StartCoroutine(AnimateDots());

    }

    void HandleLoginResponse(string response)
    {
        StopCoroutine(_dots);
        statusText.text = string.Empty;
        if (!string.IsNullOrEmpty(response))
        {
            Debug.Log("Received Response: " + response);
            MainText.text = $"Logged in";
        }
        else
        {
            Debug.LogError("Login failed or no response received.");
            MainText.text = "Login failed or no response received.";
        }
    }

    private IEnumerator AnimateDots()
    {
        while (true)
        {
            dotCount = (dotCount + 1) % 4; // Cycle through 0, 1, 2, 3 dots
            statusText.text = baseText + new string('.', dotCount);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
