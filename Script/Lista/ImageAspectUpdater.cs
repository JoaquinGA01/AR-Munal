using UnityEngine;
using UnityEngine.UI;

public class ImageAspectUpdater : MonoBehaviour
{
    private Image image;
    private AspectRatioFitter aspectFitter;

    void Start()
    {
        image = GetComponent<Image>();
        aspectFitter = GetComponent<AspectRatioFitter>();
    }

    void Update()
    {
        if (image.sprite != null)
        {
            float newAspect = (float)image.sprite.texture.width / image.sprite.texture.height;
            aspectFitter.aspectRatio = newAspect;
        }
    }
}
