using UnityEngine;
using UnityEngine.UI;

public class PostureBar : MonoBehaviour
{
    [SerializeField] private Image barImage;
    private float maxWidth;

    [SerializeField] private Color lowPostureColor ;
    [SerializeField] private Color highPostureColor;

    private void Awake()
    {
        maxWidth = barImage.rectTransform.sizeDelta.x;
        SetBarWidth(0f);
    }

    public void UpdatePostureBar(float currentPosture, float maxPosture)
    {
        float percent = Mathf.Clamp01(currentPosture / maxPosture);
        float width = (currentPosture / maxPosture) * maxWidth;
        SetBarWidth(width);
        barImage.color = Color.Lerp(lowPostureColor, highPostureColor, percent);
    }

    private void SetBarWidth(float width)
    {
        var size = barImage.rectTransform.sizeDelta;
        size.x = width;
        barImage.rectTransform.sizeDelta = size;
    }
}
