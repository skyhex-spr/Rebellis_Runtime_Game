using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;
using TMPro;

public class PanelController : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private RectTransform panel;
    [SerializeField] private Button showButton;
    [SerializeField] private Button GenerateButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private TMP_InputField PromptViewText;

    [SerializeField] private float animationDuration = 0.1f;

    public Slider loadingSlider;
    public TMP_Text percentageText;
    private Coroutine loadingCoroutine;

    private Vector2 hiddenPosition;
    private Vector2 visiblePosition;

    public bool IsPanelOpen;
    public bool IsOnprogress;

    private GameManager _gameManager;

    private int targetProgress;

    private void Start()
    {

        _gameManager = FindAnyObjectByType<GameManager>();


        _gameManager.RebelisAPIHandler.OnProgressStatusChanged.AddListener(OnProgressUpdate);
        // Save positions
        visiblePosition = panel.anchoredPosition;
        hiddenPosition = new Vector2(visiblePosition.x, -1500);

        // Start panel hidden
        panel.anchoredPosition = hiddenPosition;

        // Button events
        showButton.onClick.AddListener(ShowPanel);
        closeButton.onClick.AddListener(ClosePanel);
        GenerateButton.onClick.AddListener(StartGeneration);
    }

    private void ShowPanel()
    {
        if (IsOnprogress)
            return;

        panel.DOAnchorPos(visiblePosition, animationDuration)
             .SetEase(Ease.OutBack);

        IsPanelOpen = true;

        showButton.transform.DOScale(0, animationDuration).SetEase(Ease.InBack);
    }

    private void ClosePanel()
    {
        panel.DOAnchorPos(hiddenPosition, animationDuration)
             .SetEase(Ease.InBack);
        showButton.transform.DOScale(1, animationDuration).SetEase(Ease.OutBack);

        IsPanelOpen = false;
    }

    public void StartGeneration()
    {
        if (PromptViewText.text == string.Empty || string.IsNullOrWhiteSpace(PromptViewText.text))
            return;

        if (_gameManager.AreAllDeselctedAvatars())
            _gameManager.SelectAllAvatars();

        UpdatePercentage(0);
        loadingSlider.value = 0;
        targetProgress = 0;

        _gameManager.RebelisAPIHandler.SendPrompt(1, PromptViewText.text,1);

        ClosePanel();

        loadingSlider.transform.DOScale(1.8f, animationDuration).SetEase(Ease.InOutBounce);
    }

    private void OnProgressUpdate(int percentage)
    {

        if (percentage > targetProgress)
        {
            loadingSlider.value = targetProgress;
            UpdatePercentage(targetProgress);

            targetProgress = percentage;
            if (loadingCoroutine != null) StopCoroutine(loadingCoroutine);
            loadingCoroutine = StartCoroutine(FakeLoad(loadingSlider.value, percentage, 15)); 
            IsOnprogress = true;
        }

 
    }

    public void ForceFill()
    {
        if (loadingCoroutine != null) StopCoroutine(loadingCoroutine);
        loadingCoroutine = StartCoroutine(FakeLoad(loadingSlider.value, 100, 1f)); // Force to 100 in 1 second
    }

    public void StopGenerateLoading()
    {
        if (loadingCoroutine != null)
        StopCoroutine(loadingCoroutine);

        loadingSlider.transform.DOScale(0, animationDuration).SetEase(Ease.InOutBounce);
        IsOnprogress = false;
    }

    private IEnumerator FakeLoad(float start, float target, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float progress = Mathf.Lerp(start, target, elapsed / duration);
            loadingSlider.value = progress;
            UpdatePercentage(progress);
            yield return null;
        }

        loadingSlider.value = target;
        UpdatePercentage(target);

        if (target == 100)
        { 
          loadingSlider.transform.DOScale(0, animationDuration).SetEase(Ease.InOutBounce);
          IsOnprogress = false;
        }
    }


    private void UpdatePercentage(float value)
    {
        int percentage = Mathf.RoundToInt(value);
        percentageText.text = percentage + "%";
    }
}
