using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEditor.U2D;
using System.Collections.Generic;
using System;
[CustomEditor(typeof(Informacion))]
public class InformacionEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector(); // Dibuja el inspector por defecto

        Informacion script = (Informacion)target; // Obtiene el script del objeto seleccionado

        if (GUILayout.Button("Crear Carpetas de Obras")) // Si se presiona el botón
        {
            CrearCarpetasDeObras(script);
        }

        if (GUILayout.Button("Configurar Imágenes")) // Nuevo botón para configurar imágenes
        {
            ConfigurarImagenes();
        }
        if (GUILayout.Button("Copiar Lista de Obras")) // Nuevo botón para copiar la lista
        {
            InformacionEditorUtilities.CopiedInformacionObras = new List<Obra_Informarcion>(script.Informacion_Obras);
            Debug.Log("Lista de obras copiada");
        }
        if (GUILayout.Button("Guardar Lista de Obras en TXT")) // Nuevo botón para guardar la lista en un archivo TXT
        {
            GuardarInformacionEnTxt(script);
        }
        if (GUILayout.Button("Importar Lista de Obras desde TXT")) // Nuevo botón para importar la lista desde un archivo TXT
        {
            ImportarInformacionDesdeTxt(script);
        }
    }

    private void CrearCarpetasDeObras(Informacion script)
    {
        foreach (Obra_Informarcion obra in script.Informacion_Obras) // Itera sobre la lista de obras
        {
            // Crear carpeta en Resources
            string resourcePath = Path.Combine("Assets/Resources", obra.Nombre_Obra);
            CrearCarpetaSiNoExiste(resourcePath);

            // Crear carpeta en StreamingAssets/Animaciones
            string streamingAssetsPath = Path.Combine("Assets/StreamingAssets/Animaciones", obra.Nombre_Obra);
            CrearCarpetaSiNoExiste(streamingAssetsPath);
        }

        AssetDatabase.Refresh(); // Actualiza el AssetDatabase para mostrar las nuevas carpetas en Unity
    }

    private void CrearCarpetaSiNoExiste(string path)
    {
        if (!Directory.Exists(path)) // Si la carpeta no existe
        {
            Directory.CreateDirectory(path); // Crea la carpeta físicamente
            AssetDatabase.ImportAsset(path); // Importa la carpeta a Unity
            Debug.Log("Carpeta creada: " + path);
        }
        else
        {
            Debug.LogWarning("La carpeta ya existe: " + path);
        }
    }

    private void ConfigurarImagenes()
    {
        string resourcesPath = Path.Combine(Application.dataPath, "Resources");

        DirectoryInfo dir = new DirectoryInfo(resourcesPath);
        DirectoryInfo[] subDirs = dir.GetDirectories();

        foreach (DirectoryInfo subDir in subDirs)
        {
            FileInfo[] files = subDir.GetFiles("*.*", SearchOption.AllDirectories);

            foreach (FileInfo file in files)
            {
                if (file.Extension == ".png" || file.Extension == ".jpg")
                {
                    string assetPath = "Assets" + file.FullName.Substring(Application.dataPath.Length).Replace('\\', '/');
                    TextureImporter importer = AssetImporter.GetAtPath(assetPath) as TextureImporter;

                    if (importer != null)
                    {
                        importer.textureType = TextureImporterType.Sprite;
                        importer.isReadable = true;
                        importer.spriteImportMode = SpriteImportMode.Single;
                        AssetDatabase.ImportAsset(assetPath, ImportAssetOptions.ForceUpdate);
                    }
                }
            }
        }

        AssetDatabase.Refresh();
    }
    private void GuardarInformacionEnTxt(Informacion script)
    {
        List<Obra_Informarcion> obras = script.Informacion_Obras;
        string json = JsonUtility.ToJson(new SerializableListWrapper<Obra_Informarcion>(obras), true);

        string filePath = Path.Combine(Application.dataPath, "Informacion.txt");
        File.WriteAllText(filePath, json);

        Debug.Log("Información guardada en: " + filePath);
        AssetDatabase.Refresh(); // Refresca el AssetDatabase para que el archivo aparezca en Unity
    }

    // Clase auxiliar para envolver la lista en un objeto serializable

    private void ImportarInformacionDesdeTxt(Informacion script)
    {
        string filePath = Path.Combine(Application.dataPath, "Informacion.txt");
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            SerializableListWrapper<Obra_Informarcion> data = JsonUtility.FromJson<SerializableListWrapper<Obra_Informarcion>>(json);
            script.Informacion_Obras = data.items ?? new List<Obra_Informarcion>();

            Debug.Log("Lista de obras importada con éxito desde " + filePath);
        }
        else
        {
            Debug.LogError("El archivo " + filePath + " no existe.");
        }
    }

}

[Serializable]
public class SerializableListWrapper<T>
{
    public List<T> items;
    public SerializableListWrapper(List<T> items)
    {
        this.items = items;
    }
}