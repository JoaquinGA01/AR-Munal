using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Controlador_Informacion : MonoBehaviour
{
    private string titulo;
    private GameObject Info;

    public GameObject Panel_Izquierdo;
    public GameObject Panel_Derecho;
    public TMP_Text  Titulo_Informacion;

    private bool Escribir = false;
    private Obra_Informarcion obra;

    void Start(){
        Info = GameObject.Find("Informacion");
        if(titulo != "" && titulo != null){
            obra = JsonUtility.FromJson<Obra_Informarcion>(Info.GetComponent<Informacion>().getInfoObras(titulo));
        }
        
    }
    void Update()
    {
        if (Info.GetComponent<Informacion>().getEstado() && Escribir)
        {
            Panel_Derecho.SetActive(true);
            Panel_Izquierdo.SetActive(true);
            Panel_Derecho.GetComponent<Escribir_Texto>().IniciarTexto(obra.Panel_Informacion_Derecho);
            Panel_Izquierdo.GetComponent<Escribir_Texto>().IniciarTexto(obra.Panel_Informacion_Izquierdo);
            setEstado(false);
            Info.GetComponent<Informacion>().setEstado(false);
        }
    }

    public void Iniciar()
    {
        Debug.Log(titulo);
        obra = JsonUtility.FromJson<Obra_Informarcion>(Info.GetComponent<Informacion>().getInfoObras(titulo));
    }

    public void Terminar()
    {
        Panel_Derecho.SetActive(false);
        Panel_Izquierdo.SetActive(false);
        Panel_Derecho.GetComponent<Escribir_Texto>().Recarga();
        Panel_Izquierdo.GetComponent<Escribir_Texto>().Recarga();
    }

    public void setEstado(bool e)
    {
        Escribir = e;
    }

    public void setTitulo(string t){
        titulo = t;
    }

    public void setTituloInformacion(){
        string Info = "Titulo: " + obra.Nombre_Obra + "\n" + "Autor: " + obra.Autor;
        Titulo_Informacion.text = Info;
    }
}
