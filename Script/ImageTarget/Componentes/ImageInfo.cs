using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class ImageInfo : MonoBehaviour
{
    [SerializeField]
    private GameObject Panel_1;
    [SerializeField]
    private GameObject Panel_2;
    [SerializeField]
    private GameObject VideoPanel;
    private ObserversData observersData;
    // Start is called before the first frame update
    void OnEnable()
    {
        observersData = FindObjectOfType<ObserversData>();
        observersData.OnEstadoVideoObraChanged += UpdateObraInfo;
    }

    // Update is called once per frame
    private void UpdateObraInfo()
    {
        if (VideoPanel != null)
        {
            MeshRenderer meshRenderer = VideoPanel.GetComponent<MeshRenderer>();
            VideoPlayer videoPlayer = VideoPanel.GetComponent<VideoPlayer>();
            if (videoPlayer != null || meshRenderer != null)
            {
                if(observersData.EstadoVideoObra){
                    VideoPanel.transform.localPosition += new Vector3(0, 0.001f, 0);
                }else{
                    VideoPanel.transform.localPosition -= new Vector3(0, 0.001f, 0);
                }
                meshRenderer.enabled = observersData.EstadoVideoObra;
                videoPlayer.enabled = observersData.EstadoVideoObra;
            }
        }

        if (Panel_1 != null || Panel_2 != null)
        {
            Panel_1.SetActive(observersData.EstadoVideoObra);
            Panel_2.SetActive(observersData.EstadoVideoObra);
        }
    }
}
