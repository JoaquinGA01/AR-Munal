using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReproducirVoz : MonoBehaviour
{
    public GameObject Iconovoz;
    private ObserversData observersData;
    private Voz voz;
    void Start()
    {
        voz = GetComponent<Voz>();
        observersData = FindObjectOfType<ObserversData>();
        if (observersData != null)
        {
            observersData.OnEstadoVideoObraChanged += Reproducir;
            observersData.OnEstadoSonidoVozChanged += Reproducir;
        }
    }

    public void Reproducir()
    {
        if(!observersData.EstadoVideoObra){
            voz.StopSpeaking();
            return;
        }
        if (observersData.EstadoSonidoVoz)
        {
            string nombreObra = observersData.Nombre_Obra;
            if (!string.IsNullOrEmpty(nombreObra))
            {
                ArtWorkData artWork = observersData.ArtWorks.Find(obra => obra.Nombre_Target == nombreObra);
                if (artWork != null)
                {
                    voz.StartSpeaking(artWork.Informacion_Audio);
                }
            }
            else
            {
                voz.StopSpeaking();
            }
        }
        else
        {
            voz.StopSpeaking();
        }
    }
}
