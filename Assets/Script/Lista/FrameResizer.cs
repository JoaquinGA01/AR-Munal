using UnityEngine;
using UnityEngine.UI;

public class FrameResizer : MonoBehaviour
{
 public Image targetImage; // El primer elemento con el script ImageAspectUpdater
    public float sizeMultiplier = 1.1f; // Factor para hacer el segundo elemento más grande

    private RectTransform targetRectTransform;
    private RectTransform thisRectTransform;

    void Start()
    {
        targetRectTransform = targetImage.GetComponent<RectTransform>();
        thisRectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (targetImage.sprite != null)
        {
            float targetWidth = targetRectTransform.rect.width;
            float targetHeight = targetRectTransform.rect.height;

            thisRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, targetWidth * sizeMultiplier);
            thisRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, targetHeight * sizeMultiplier);
        }
    }
}
