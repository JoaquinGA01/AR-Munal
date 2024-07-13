using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PanelActivator : MonoBehaviour, IPointerDownHandler
{
    public Sprite imagen_play;
    public Sprite imagen_pause;
    private ObserversData observersData;

    void Start()
    {
    }

    void OnEnable()
    {
        observersData = FindObjectOfType<ObserversData>();
        if (observersData !=null)
        {
            observersData.OnEstadoVideoObraChanged += CambioDeEstado;
            GetComponent<Image>().sprite = imagen_play;
        }
    }
    void OnDisable(){
        if(observersData !=null){
            observersData.OnEstadoVideoObraChanged -= CambioDeEstado;
        }
    }

    void CambioDeEstado()
    {
        if (observersData.EstadoVideoObra)
        {
            GetComponent<Image>().sprite = imagen_pause;
        }
        else
        {
            GetComponent<Image>().sprite = imagen_play;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        observersData.EstadoVideoObra = !observersData.EstadoVideoObra;
    }
}
