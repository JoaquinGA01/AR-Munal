using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Video;

public class ObjectInteraction : MonoBehaviour, IPointerClickHandler
{
    private float lastClickTime;
    private const float doubleClickThreshold = 0.3f;

    private ObserversData observersData;

    void OnEnable(){
        observersData = FindObjectOfType<ObserversData>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (Time.time - lastClickTime < doubleClickThreshold)
        {
            OnDoubleClick(eventData);
            lastClickTime = 0;
        }
        else
        {
            lastClickTime = Time.time;
        }
    }

    private void OnDoubleClick(PointerEventData eventData)
    {
        observersData.EstadoVideoObra = !observersData.EstadoVideoObra;
    }
}
