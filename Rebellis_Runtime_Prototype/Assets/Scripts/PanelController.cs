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

    [SerializeField] private float animationDuration = 0.1f;

    public Slider loadingSlider;
    public TMP_Text percentageText;
    private Coroutine loadingCoroutine;

    private Vector2 hiddenPosition;
    private Vector2 visiblePosition;

    public bool IsPanelOpen;
    public bool IsOnprogress;

    private void Start()
    {
        // Save positions
        visiblePosition = panel.anchoredPosition;
        hiddenPosition = new Vector2(visiblePosition.x, -1500);

        // Start panel hidden
        panel.anchoredPosition = hiddenPosition;

        // Button events
        showButton.onClick.AddListener(ShowPanel);
        closeButton.onClick.AddListener(ClosePanel);
        GenerateButton.onClick.AddListener(ShowProggress);
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

    public void ShowProggress()
    {
        ClosePanel();

        loadingSlider.transform.DOScale(1.8f, animationDuration).SetEase(Ease.InOutBounce);

        UpdatePercentage(0);
        StartFakeLoading();
    }


    public void StartFakeLoading()
    {
        if (loadingCoroutine != null) StopCoroutine(loadingCoroutine);
        loadingCoroutine = StartCoroutine(FakeLoad(0, 100, 5)); // Load to 40 in 40 seconds
        IsOnprogress = true;
    }

    public void ForceFill()
    {
        if (loadingCoroutine != null) StopCoroutine(loadingCoroutine);
        loadingCoroutine = StartCoroutine(FakeLoad(loadingSlider.value, 100, 1f)); // Force to 100 in 1 second
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
