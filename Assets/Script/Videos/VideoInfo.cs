using UnityEngine;
using UnityEngine.Video;
using System.Collections;
using System.Collections.Generic;
public class VideoInfo : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    private List<VideoClip> videoClips = new List<VideoClip>();
    private int currentVideoIndex = 0;
    private ObserversData observersData;
    private Coroutine loadVideosCoroutine;

    void OnEnable()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        observersData = FindObjectOfType<ObserversData>();
        if (observersData == null)
        {
            Debug.LogError("ObserversData component not found.");
            return;
        }
        observersData.OnEstadoVideoObraChanged += OnNombreObraChanged;
        observersData.OnEstadoSonidoAnimacionChanged += OnSonidoAnimacionChanged;
        OnNombreObraChanged();
    }

    void OnDestroy()
    {
        if (observersData != null)
        {
            observersData.OnEstadoVideoObraChanged -= OnNombreObraChanged;
            observersData.OnEstadoSonidoAnimacionChanged -= OnSonidoAnimacionChanged;
        }
    }

    void OnNombreObraChanged()
    {
        if(!observersData.EstadoVideoObra){
            videoPlayer.Stop();
            if (loadVideosCoroutine != null)
            {
                StopCoroutine(loadVideosCoroutine);
                loadVideosCoroutine = null;
            }
            return;
        }
        string newNombreObra = observersData.Nombre_Obra;
        if (!string.IsNullOrEmpty(newNombreObra))
        {
            if (loadVideosCoroutine != null)
            {
                StopCoroutine(loadVideosCoroutine);
            }
            loadVideosCoroutine = StartCoroutine(LoadAndPlayVideosFromFolder(newNombreObra));
        }
        else
        {
            videoPlayer.Stop();
            Debug.Log("Nombre_Obra is null or empty. Waiting for a valid folder name.");
        }
    }
    void OnSonidoAnimacionChanged()
    {
        videoPlayer.SetDirectAudioMute(0, !observersData.EstadoSonidoAnimacion);
    }

    IEnumerator LoadAndPlayVideosFromFolder(string folderName)
    {
        videoClips.Clear();
        VideoClip[] clips = Resources.LoadAll<VideoClip>("Videos/" + folderName);
        if (clips.Length > 0)
        {
            videoClips.AddRange(clips);
            currentVideoIndex = 0;
            PlayNextVideo();
        }
        else
        {
            Debug.LogError("No videos found in the folder: " + folderName);
        }

        yield return null;
    }

    void PlayNextVideo()
    {
        if (currentVideoIndex < videoClips.Count)
        {
            videoPlayer.clip = videoClips[currentVideoIndex];
            videoPlayer.Play();
            videoPlayer.SetDirectAudioMute(0, !observersData.EstadoSonidoAnimacion);
            currentVideoIndex++;
            videoPlayer.loopPointReached += CheckForNextVideo;
        }
        else
        {
            currentVideoIndex = 0;
            PlayNextVideo();
        }
    }

    void CheckForNextVideo(VideoPlayer vp)
    {
        videoPlayer.loopPointReached -= CheckForNextVideo;
        PlayNextVideo();
    }
}
