using UnityEngine;
using UnityEngine.Video;
using System.Collections;
using System.IO;
using System.Collections.Generic;

public class DynamicVideoPlayer : MonoBehaviour
{
    private VideoPlayer videoPlayer;
    private List<string> videoFiles;
    private int currentVideoIndex = 0;

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();

        // Obtener el nombre de la carpeta desde ObserversData
        string nombreCarpeta = FindObjectOfType<ObserversData>().Nombre_Obra;
        ShowToast("Nombre: " + nombreCarpeta);

        // Ruta completa a la carpeta de videos
        string path = Path.Combine(Application.streamingAssetsPath, "Videos", nombreCarpeta);
        ShowToast("Path: " + path);

        #if UNITY_ANDROID
        StartCoroutine(LoadVideoFilesFromStreamingAssets(nombreCarpeta));
        #else
        // Para otras plataformas, usar directamente Directory.GetFiles
        videoFiles = new List<string>(Directory.GetFiles(path, "*.mp4"));

        if (videoFiles.Count > 0)
        {
            Debug.Log("Videos encontrados: " + videoFiles.Count);
            foreach (var video in videoFiles)
            {
                Debug.Log("Video encontrado: " + video);
                ShowToast("Video encontrado: " + video);
            }

            // Reproducir el primer video encontrado
            PlayNextVideo();
        }
        else
        {
            Debug.LogError("No se encontraron videos en la carpeta: " + path);
            ShowToast("No se encontraron videos en la carpeta: " + path);
        }
        #endif
    }

    IEnumerator LoadVideoFilesFromStreamingAssets(string folderName)
    {
        string srcPath = Path.Combine(Application.streamingAssetsPath, "Videos", folderName);
        string destPath = Path.Combine(Application.persistentDataPath, folderName);

        if (!Directory.Exists(destPath))
        {
            Directory.CreateDirectory(destPath);
        }

        // Copiar todos los archivos de video desde StreamingAssets a persistentDataPath
        string[] files = Directory.GetFiles(srcPath, "*.mp4");
        foreach (string file in files)
        {
            string fileName = Path.GetFileName(file);
            string destFile = Path.Combine(destPath, fileName);

            if (!File.Exists(destFile))
            {
                byte[] data = File.ReadAllBytes(file);
                File.WriteAllBytes(destFile, data);
            }
        }

        // Obtener la lista de archivos de video desde persistentDataPath
        videoFiles = new List<string>(Directory.GetFiles(destPath, "*.mp4"));

        if (videoFiles.Count > 0)
        {
            Debug.Log("Videos encontrados: " + videoFiles.Count);
            foreach (var video in videoFiles)
            {
                Debug.Log("Video encontrado: " + video);
                ShowToast("Video encontrado: " + video);
            }

            // Reproducir el primer video encontrado
            PlayNextVideo();
        }
        else
        {
            Debug.LogError("No se encontraron videos en la carpeta: " + destPath);
            ShowToast("No se encontraron videos en la carpeta: " + destPath);
        }

        yield break;
    }

    void PlayNextVideo()
    {
        if (videoFiles.Count > 0)
        {
            // Obtener la ruta del siguiente video
            string videoPath = "file://" + videoFiles[currentVideoIndex];

            // Asignar la ruta al VideoPlayer
            videoPlayer.url = videoPath;

            // Preparar y reproducir el video
            videoPlayer.Prepare();
            videoPlayer.Play();

            // Suscribirse al evento de fin de video para cargar el siguiente
            videoPlayer.loopPointReached += OnVideoEndReached;

            // Mostrar un toast para indicar que se está reproduciendo un video
            ShowToast("Reproduciendo video: " + Path.GetFileName(videoFiles[currentVideoIndex]));
        }
    }

    void OnVideoEndReached(VideoPlayer vp)
    {
        // Incrementar el índice para el próximo video
        currentVideoIndex = (currentVideoIndex + 1) % videoFiles.Count;

        // Reproducir el siguiente video
        PlayNextVideo();
    }

    void OnDestroy()
    {
        // Asegúrate de limpiar eventos al destruir el objeto
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached -= OnVideoEndReached;
        }
    }

    void ShowToast(string message)
    {
        try
        {
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

            if (currentActivity != null)
            {
                AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");
                currentActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
                {
                    AndroidJavaObject context = currentActivity.Call<AndroidJavaObject>("getApplicationContext");
                    AndroidJavaObject toastObject = toastClass.CallStatic<AndroidJavaObject>("makeText", context, message, toastClass.GetStatic<int>("LENGTH_SHORT"));
                    toastObject.Call("show");
                }));
            }
            else
            {
                Debug.LogError("Current activity is null.");
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("Exception showing toast: " + e.Message);
        }
    }
}
