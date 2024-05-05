using UnityEngine;
using UnityEngine.Video;
using System.IO;


public class Interaccion_Obras : MonoBehaviour
{
    public GameObject videoPlane; // Plane con el componente VideoPlayer
    public VideoPlayer videoPlayer;
    public string videoFolder = "Animaciones/";
    public string[] videoNames;

    private int currentVideoIndex = 0; // Índice del vídeo actual
    private bool isVideoPlaying = false; // Estado de reproducción del video
    private int tapCount = 0; // Contador de toques
    private float doubleTapTimer; // Temporizador para el doble toque

    private void Start()
    {
        // Configuración inicial para el video
        videoPlane.SetActive(false);
        videoPlayer.playOnAwake = false;
        videoPlayer.source = VideoSource.Url;
        videoPlayer.loopPointReached += EndReached;
    }

    private void Update()
    {
        // Verificar interacción con el modelo
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            tapCount++;
            if (tapCount == 1)
            {
                doubleTapTimer = Time.time; // Inicia el temporizador en el primer toque
            }

            if (tapCount == 2 && Time.time - doubleTapTimer <= 0.5f) // 0.5 segundos para el doble toque
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
                {
                    // Comprobar si el video está reproduciéndose o no
                    if (isVideoPlaying)
                    {
                        PauseAndResetVideo();
                    }
                    else
                    {
                        PlayVideo(0);
                    }
                }
                tapCount = 0; // Reinicia el contador de toques
            }
            else if (Time.time - doubleTapTimer > 0.5f) // Si pasa más de 0.5 segundos, reinicia
            {
                tapCount = 0;
            }
        }
    }

    public void PlayVideo(int videoIndex)
    {
        if (videoIndex >= 0 && videoIndex < videoNames.Length)
        {
            currentVideoIndex = videoIndex;
            PlayCurrentVideo();
        }
        else
        {
            Debug.LogError("Índice de video fuera de rango.");
        }
    }

    private void PlayCurrentVideo()
    {
        try
        {
            videoPlane.SetActive(true);
            string path = System.IO.Path.Combine(Application.streamingAssetsPath, videoFolder, videoNames[currentVideoIndex] + ".mp4");
            videoPlayer.url = path;
            videoPlayer.Play();
            isVideoPlaying = true;
        }
        catch (IOException  error)
        {
            videoPlane.SetActive(false);
             isVideoPlaying = false;
        }
        return;
    }

    private void PauseAndResetVideo()
    {
        videoPlayer.Stop();
        videoPlane.SetActive(false);
        currentVideoIndex = 0;
        isVideoPlaying = false;
    }

    private void EndReached(VideoPlayer vp)
    {
        currentVideoIndex++;
        if (currentVideoIndex >= videoNames.Length)
        {
            currentVideoIndex = 0; // Reinicia el índice para volver a empezar la lista
        }
        PlayCurrentVideo(); // Continúa con el siguiente video o reinicia la lista
    }

    public void setFolder(string f)
    {
        videoFolder = "Animaciones/" + f;
    }
    public void setListaVideos(string[] lista)
    {
        videoNames = lista;
    }
}
