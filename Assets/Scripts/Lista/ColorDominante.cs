using UnityEngine;
using UnityEngine.UI; // Asegúrate de incluir este espacio de nombres para trabajar con UI.

public class ColorDominante : MonoBehaviour
{
    public Image imageSource; // Asigna aquí tu objeto con la imagen X.
    public Image imageTarget; // Asigna aquí tu objeto con la imagen de fondo.

    void Start()
    {
        AplicarColorDominante();
    }

    public void AplicarColorDominante()
    {
        try
        {
            Color dominantColor = CalculateDominantColor(imageSource.sprite);
            imageTarget.color = dominantColor; // Aplica el color predominante al fondo.
        }
        catch (System.Exception)
        {
            // Si la imagen no es accesible, establece un color de fondo predeterminado.
            // El color que proporcionaste (292929) parece estar en hexadecimal. Vamos a convertirlo a Color de Unity.
            Color defaultColor = new Color32(0x29, 0x29, 0x29, 0xFF); // Asegúrate de que el último valor (FF) sea para la opacidad completa.
            imageTarget.color = defaultColor;
        }
    }

    Color CalculateDominantColor(Sprite sprite)
    {
        Color tempColor = new Color(0, 0, 0, 0);
        Texture2D texture = sprite.texture;
        Color[] colors = texture.GetPixels();

        long totalR = 0, totalG = 0, totalB = 0;

        foreach (Color color in colors)
        {
            totalR += (long)(color.r * 255);
            totalG += (long)(color.g * 255);
            totalB += (long)(color.b * 255);
        }

        int totalPixels = colors.Length;
        tempColor.r = totalR / totalPixels / 255f;
        tempColor.g = totalG / totalPixels / 255f;
        tempColor.b = totalB / totalPixels / 255f;
        tempColor.a = 1; // Ajusta la transparencia según sea necesario.

        return tempColor;
    }
}
