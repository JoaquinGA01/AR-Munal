using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TextSpeech;
using UnityEngine.UI;

public class Controlador_Voz : MonoBehaviour
{
    // Start is called before the first frame update
    const string lenguage = "es-ES";

    [SerializeField]
    private string texto;

    private GameObject Info;
    private Obra_Informarcion obra;

    private static bool Mute;

    // Start is called before the first frame update
    void Start()
    {
        Setup(lenguage);
        TextToSpeech.Instance.onStartCallBack = OnSpeakStart;
        TextToSpeech.Instance.onDoneCallback = OnSpeakStop;
    }

    void Setup(string code)
    {
        TextToSpeech.Instance.Setting(code, 1, 1);

    }
    // Update is called once per frame
    void Update()
    {
        Mute = ControladorSonido.Sonido;
    }

    public void StartSpeeking()
    {
        if (Mute)
        {
            TextToSpeech.Instance.StartSpeak(texto);
        }
    }
    public void StopSpeeking()
    {
        TextToSpeech.Instance.StopSpeak();
    }
    public void OnSpeakStart()
    {
        Debug.Log("Iniciando...");
    }
    public void OnSpeakStop()
    {
        Debug.Log("Terminando...");
    }

    public void setTitulo(string Titulo)
    {
        Info = GameObject.Find("Informacion");
        obra = JsonUtility.FromJson<Obra_Informarcion>(Info.GetComponent<Informacion>().getInfoObras(Titulo));
        texto = obra.Panel_Informacion_Derecho;
    }

    public void Activar_Desactivar(){
        Mute = !Mute;
        if(Mute){
            ControladorSonido.Sonido = true;
            StartSpeeking();
        }else{
            ControladorSonido.Sonido = false;
            StopSpeeking();
        }
    }
}
