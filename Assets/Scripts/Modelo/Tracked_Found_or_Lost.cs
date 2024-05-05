using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tracked_Found_or_Lost : MonoBehaviour
{
    public GameObject Controlador_Informacion;
    public GameObject Controlador_Imagen;
    private GameObject Informacion;
    private GameObject Controlador_Canvas;

    private GameObject Controlador_Voz;
    public string Titulo;
    public string[] videoNames;
    // Start is called before the first frame update
    void Start()
    {
        Informacion = GameObject.Find("Informacion");
        Controlador_Canvas = GameObject.Find("Controlador_Escenas");
        Controlador_Voz = GameObject.Find("Controlador_Voz");
        Controlador_Informacion.GetComponent<Controlador_Informacion>().setTitulo(Titulo);
        Controlador_Informacion.GetComponent<Controlador_Informacion>().Iniciar();
        Controlador_Informacion.GetComponent<Controlador_Informacion>().setTituloInformacion();
        Controlador_Imagen.GetComponent<Interaccion_Obras>().setListaVideos(videoNames);
        Controlador_Imagen.GetComponent<Interaccion_Obras>().setFolder(Titulo);
        Controlador_Imagen.GetComponent<Cargar_Sprite>().setTitulo(Titulo);
    }


    // Update is called once per frame
    void Update()
    {

    }

    public void Found()
    {
        Controlador_Voz.GetComponent<Controlador_Voz>().setTitulo(Titulo);
        Controlador_Canvas.GetComponent<Controlador_Escenas>().setBandera(false);
        Controlador_Informacion.GetComponent<Controlador_Informacion>().Iniciar();
        Controlador_Informacion.GetComponent<Controlador_Informacion>().setEstado(true);
    }

    public void Lost()
    {
        Controlador_Canvas.GetComponent<Controlador_Escenas>().setBandera(true);
        Controlador_Informacion.GetComponent<Controlador_Informacion>().Terminar();
        Controlador_Informacion.GetComponent<Controlador_Informacion>().setEstado(false);
        Informacion.GetComponent<Informacion>().setEstado(false);
        Controlador_Voz.GetComponent<Controlador_Voz>().StopSpeeking();
    }
}
