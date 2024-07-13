using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CambiarEstadoSonido : MonoBehaviour
{
    public Image image;
    public Sprite Activo;
    public Sprite Inactivo;

    private ObserversData observersData;

    void OnEnable()
    {
        image.GetComponent<Image>();
        observersData = FindObjectOfType<ObserversData>();
        if (observersData.EstadoSonidoAnimacion)
        {
            image.sprite = Activo;
        }
        else
        {
            image.sprite = Inactivo;
        }
    }
    public void CambiarEstado()
    {
        if (observersData.EstadoSonidoAnimacion)
        {
            image.sprite = Inactivo;
        }
        else
        {
            image.sprite = Activo;
        }
        observersData.EstadoSonidoAnimacion = !observersData.EstadoSonidoAnimacion;
    }
}
