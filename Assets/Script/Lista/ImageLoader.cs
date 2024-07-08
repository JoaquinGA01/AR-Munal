using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;

public class ImageLoader : MonoBehaviour
{
    private Image targetImage;  // Referencia al componente Image
    private ObserversData observersData;

    void Start()
    {
        targetImage = GetComponent<Image>();  // Obtiene el componente Image en el mismo objeto

        if (targetImage != null)
        {
            targetImage.preserveAspect = true; // Asegura que la imagen preserve sus proporciones
        }

        observersData = FindObjectOfType<ObserversData>();
        if (observersData != null)
        {
            // Suscríbete al evento OnNombreObraChanged
            observersData.OnNombreObraChanged += UpdateImage;
            // Llama a UpdateImage para cargar la imagen inicial
            UpdateImage();
        }
    }

    private void UpdateImage()
    {
        // Obtén el nombre de la obra desde observersData
        string imageName = observersData.Nombre_Obra;

        // Intenta cargar la imagen desde la carpeta "Resources/Marcadores" y sus subcarpetas
        Sprite loadedSprite = LoadSpriteFromResources("Marcadores", imageName);
        if (loadedSprite != null)
        {
            targetImage.sprite = loadedSprite;
            targetImage.preserveAspect = true; // Asegura que la imagen preserve sus proporciones
            targetImage.rectTransform.localScale = new Vector3(.8f, .8f, .8f);
        }
    }

    private Sprite LoadSpriteFromResources(string rootPath, string imageName)
    {
        // Carga todos los recursos en la carpeta "Marcadores" y sus subcarpetas
        Sprite[] allSprites = Resources.LoadAll<Sprite>(rootPath);

        // Busca la imagen con el nombre especificado
        foreach (Sprite sprite in allSprites)
        {
            if (sprite.name == imageName)
            {
                return sprite;
            }
        }

        // Si no se encuentra la imagen, intenta buscar en subcarpetas
        string[] subdirectories = Directory.GetDirectories(Path.Combine(Application.dataPath, "Resources", rootPath), "*", SearchOption.AllDirectories);
        foreach (string subdirectory in subdirectories)
        {
            string relativePath = rootPath + subdirectory.Replace(Path.Combine(Application.dataPath, "Resources", rootPath), "").Replace("\\", "/");

            allSprites = Resources.LoadAll<Sprite>(relativePath);
            foreach (Sprite sprite in allSprites)
            {
                if (sprite.name == imageName)
                {
                    return sprite;
                }
            }
        }

        return null;  // Devuelve null si no se encuentra la imagen
    }
}
