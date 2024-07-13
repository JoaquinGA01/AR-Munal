using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DominantColorCalculator : MonoBehaviour
{
    public Image sourceImage;   // Imagen de la que se calculará el color predominante
    public Image targetImage;   // Imagen a la que se le asignará el color con transparencia

    private ObserversData observersData;

    void Start()
    {
        if (sourceImage != null && targetImage != null)
        {
            StartCoroutine(CalculateAndSetDominantColor());
        }
        observersData = FindObjectOfType<ObserversData>();
        if (observersData != null)
        {
            observersData.OnNombreObraChanged += UpdateImage;
        }
    }

    void UpdateImage(){
        Debug.Log("Cambio");
        StartCoroutine(CalculateAndSetDominantColor());
    }
    private IEnumerator CalculateAndSetDominantColor()
    {
        yield return new WaitForEndOfFrame(); // Espera al final del frame para asegurarse de que la imagen esté renderizada

        // Obtiene la textura de la imagen fuente
        Texture2D texture = sourceImage.sprite.texture;
        if (texture == null)
        {
            Debug.LogError("La imagen fuente no tiene una textura válida.");
            yield break;
        }

        // Calcula el color predominante
        Color dominantColor = CalculateDominantColor(texture);

        // Asigna el color con transparencia del 50% al targetImage
        dominantColor.a = 0.9f;
        targetImage.color = dominantColor;
    }

    private Color CalculateDominantColor(Texture2D texture)
    {
        Color[] colors = texture.GetPixels();
        int totalPixels = colors.Length;

        float r = 0, g = 0, b = 0;

        foreach (Color color in colors)
        {
            r += color.r;
            g += color.g;
            b += color.b;
        }

        r /= totalPixels;
        g /= totalPixels;
        b /= totalPixels;

        return new Color(r, g, b);
    }
}
