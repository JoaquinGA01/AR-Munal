using UnityEditor;
using UnityEngine;
using System.IO;
using Vuforia;
using System.Collections.Generic;

public class Add_Tools_Vuforia : MonoBehaviour
{
    [MenuItem("Tools/Vuforia/Configurar BD")]
    public static void ConfigurarBD()
    {
        string sourcePath = "Assets/Editor/Vuforia/ImageTargetTextures";
        string targetPath = "Assets/Resources/Marcadores";
        if (!Directory.Exists(sourcePath))
        {
            Debug.LogError($"La ruta de origen no existe: {sourcePath}");
            return;
        }
        if (!Directory.Exists(targetPath))
        {
            Directory.CreateDirectory(targetPath);
        }
        string[] subDirectories = Directory.GetDirectories(sourcePath);
        foreach (string subDir in subDirectories)
        {
            string folderName = new DirectoryInfo(subDir).Name;
            string targetSubDir = Path.Combine(targetPath, folderName);
            if (!Directory.Exists(targetSubDir))
            {
                Directory.CreateDirectory(targetSubDir);
            }
            string[] files = Directory.GetFiles(subDir);
            foreach (string file in files)
            {
                string fileName = Path.GetFileName(file);
                string targetFileName = fileName.Replace("_scaled", "");
                string targetFilePath = Path.Combine(targetSubDir, targetFileName);
                File.Copy(file, targetFilePath, true);
            }
        }
        AssetDatabase.Refresh();
        Debug.Log("Configuración de la base de datos completada.");
    }

    [MenuItem("Tools/Vuforia/Configurar Targets")]
    public static void ConfigurarTargets()
    {
        string targetPath = "Assets/Resources/Marcadores";
        if (!Directory.Exists(targetPath))
        {
            Debug.LogError($"La ruta de destino no existe: {targetPath}");
            return;
        }
        string[] imageFiles = Directory.GetFiles(targetPath, "*.*", SearchOption.AllDirectories);
        foreach (string imageFile in imageFiles)
        {
            if (imageFile.EndsWith(".png") || imageFile.EndsWith(".jpg"))
            {
                string relativePath = GetRelativeAssetPath(imageFile);
                TextureImporter textureImporter = AssetImporter.GetAtPath(relativePath) as TextureImporter;

                if (textureImporter != null)
                {
                    textureImporter.textureType = TextureImporterType.Sprite;
                    textureImporter.isReadable = true;
                    textureImporter.SaveAndReimport();
                }
            }
        }
        AssetDatabase.Refresh();
        Debug.Log("Configuración de Targets completada.");
    }

    [MenuItem("Tools/Vuforia/Agregar Image Target")]
    public static void AgregarImageTarget()
    {
        string targetPath = "Assets/Resources/Marcadores";
        if (!Directory.Exists(targetPath))
        {
            Debug.LogError($"La ruta de destino no existe: {targetPath}");
            return;
        }

        GameObject targetsParent = GameObject.Find("Targets");
        if (targetsParent == null)
        {
            targetsParent = new GameObject("Targets");
        }

        string[] imageFiles = Directory.GetFiles(targetPath, "*.*", SearchOption.AllDirectories);
        foreach (string imageFile in imageFiles)
        {
            if (imageFile.EndsWith(".png") || imageFile.EndsWith(".jpg"))
            {
                GameObject imageTarget = new GameObject(Path.GetFileNameWithoutExtension(imageFile));
                imageTarget.AddComponent<ImageTargetBehaviour>();

                var observerEventHandler = imageTarget.AddComponent<DefaultObserverEventHandler>();
                observerEventHandler.StatusFilter = DefaultObserverEventHandler.TrackingStatusFilter.Tracked;

                imageTarget.transform.parent = targetsParent.transform;
            }
        }

        Debug.Log("Componentes Image Target y DefaultObserverEventHandler agregados dentro del objeto Targets.");
    }

    [MenuItem("Tools/Vuforia/Crear Elementos Lista")]
    public static void CrearElementosLista()
    {
        string targetPath = "Assets/Resources/Marcadores";
        if (!Directory.Exists(targetPath))
        {
            Debug.LogError($"La ruta de destino no existe: {targetPath}");
            return;
        }

        ArtWorkDataStorage artWorkDataStorage = Resources.Load<ArtWorkDataStorage>("ArtWorkDataStorage");
        if (artWorkDataStorage == null)
        {
            Debug.LogError("No se encontró un ScriptableObject ArtWorkDataStorage. Asegúrate de que exista en la ruta especificada.");
            return;
        }

        string[] imageFiles = Directory.GetFiles(targetPath, "*.*", SearchOption.AllDirectories);
        foreach (string imageFile in imageFiles)
        {
            if (imageFile.EndsWith(".png") || imageFile.EndsWith(".jpg"))
            {
                string imageName = Path.GetFileNameWithoutExtension(imageFile);
                ArtWorkData newArtWork = new ArtWorkData
                {
                    Nombre_Target = imageName,
                    Nombre_Obra = "Obra_" + imageName // Cambia esto según el criterio que prefieras para nombrar el elemento
                };
                artWorkDataStorage.ArtWorks.Add(newArtWork);
            }
        }

        // Marca el ScriptableObject como sucio para que Unity sepa que necesita guardarse
        EditorUtility.SetDirty(artWorkDataStorage);
        AssetDatabase.SaveAssets();

        Debug.Log("Elementos creados y añadidos a la lista en ArtWorkDataStorage.");
    }

    private static string GetRelativeAssetPath(string absolutePath)
    {
        absolutePath = Path.GetFullPath(absolutePath).Replace("\\", "/");
        string dataPath = Path.GetFullPath(Application.dataPath).Replace("\\", "/");

        if (absolutePath.StartsWith(dataPath))
        {
            string relativePath = "Assets" + absolutePath.Substring(dataPath.Length);
            return relativePath;
        }
        return null;
    }
}
