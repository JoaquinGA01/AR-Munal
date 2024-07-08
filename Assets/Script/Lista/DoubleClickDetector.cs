using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class DoubleClickDetector : MonoBehaviour, IPointerClickHandler
{
    private float lastClickTime = 0f;
    private float doubleClickThreshold = 0.3f;

    private ArtWorkDataStorage artWorkDataStorage;
    private List<string> nombreTargetList;
    private int currentIndex = 0;

    private ObserversData observersData;

    void Start()
    {
        artWorkDataStorage = Resources.Load<ArtWorkDataStorage>("ArtWorkDataStorage");
        observersData = FindObjectOfType<ObserversData>();
        if (artWorkDataStorage != null)
        {
            nombreTargetList = new List<string>();
            foreach (ArtWorkData artwork in artWorkDataStorage.ArtWorks)
            {
                nombreTargetList.Add(artwork.Nombre_Target);
            }

            if (nombreTargetList.Count > 0)
            {
                observersData.Nombre_Obra = nombreTargetList[currentIndex];
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        float timeSinceLastClick = Time.time - lastClickTime;
        if (timeSinceLastClick <= doubleClickThreshold)
        {
            DetectDoubleClick(eventData);
        }
        lastClickTime = Time.time;
    }

    private void DetectDoubleClick(PointerEventData eventData)
    {
        Vector2 clickPosition = eventData.position;
        if (clickPosition.x > Screen.width / 2)
        {
            // Aquí puedes poner la lógica para doble clic en el lado derecho
            MoveRight();
        }
        else
        {
            // Aquí puedes poner la lógica para doble clic en el lado izquierdo
            MoveLeft();
        }
    }

    private void MoveRight()
    {
        if (nombreTargetList != null && nombreTargetList.Count > 0)
        {
            currentIndex = (currentIndex + 1) % nombreTargetList.Count;
            observersData.Nombre_Obra = nombreTargetList[currentIndex];
        }
    }

    private void MoveLeft()
    {
        if (nombreTargetList != null && nombreTargetList.Count > 0)
        {
            currentIndex = (currentIndex - 1 + nombreTargetList.Count) % nombreTargetList.Count;
            observersData.Nombre_Obra = nombreTargetList[currentIndex];
        }
    }
}
