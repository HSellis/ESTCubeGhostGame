using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class FitPanelToScreen : MonoBehaviour
{
    [SerializeField] private Vector2 preferredSize = new Vector2(1200, 800);
    [SerializeField] private float screenPadding = 40f;

    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        FitPanel();
    }

    private void OnRectTransformDimensionsChange()
    {
        if (rectTransform != null)
            FitPanel();
    }

    private void FitPanel()
    {
        float availableWidth = Screen.width - screenPadding * 2f;
        float availableHeight = Screen.height - screenPadding * 2f;

        if (availableWidth <= 0f || availableHeight <= 0f)
            return;

        float scaleX = availableWidth / preferredSize.x;
        float scaleY = availableHeight / preferredSize.y;

        // Never enlarge beyond the preferred size
        float scale = Mathf.Min(1f, scaleX, scaleY);

        rectTransform.sizeDelta = preferredSize;
        rectTransform.localScale = new Vector3(scale, scale, 1f);
    }
}