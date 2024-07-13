using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CambiarEstadoVoz : MonoBehaviour
{
    public Image image;
    public Sprite Activo;
    public Sprite Inactivo;
    private ObserversData observersData;
    // Start is called before the first frame update
    void OnEnable()
    {
        image.GetComponent<Image>();
        observersData = FindObjectOfType<ObserversData>();
        if(observersData != null){
            observersData.OnEstadoSonidoVozChanged += cambiarEstadoObra;
        }
        cambiarEstadoObra();
    }
    void cambiarEstadoObra(){
        if(observersData.EstadoSonidoVoz){
            image.sprite = Activo;
        }
    }

    public void CambiarEstado(){
        if(observersData.EstadoSonidoVoz){
            image.sprite = Inactivo;
        }else{
            image.sprite = Activo;
        }
        observersData.EstadoSonidoVoz = !observersData.EstadoSonidoVoz;
    }
}
