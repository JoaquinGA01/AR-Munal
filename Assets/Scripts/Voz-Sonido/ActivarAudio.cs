using UnityEngine;
using UnityEngine.UI;

public class ActivarAudio : MonoBehaviour
{
    public Sprite activeImage; // Imagen a mostrar cuando el objeto esté activo
    public Sprite inactiveImage; // Imagen a mostrar cuando el objeto esté inactivo
    private Image buttonImage; // Componente Image del botón
    public static bool isObjectActive;
    void Start()
    {
        buttonImage = GetComponent<Image>();
        if(ControladorSonido.Sonido){
            buttonImage.sprite = activeImage;
            isObjectActive = true;
        }else{
            buttonImage.sprite = inactiveImage;
            isObjectActive = false;
        }
    }
    void Update(){
        isObjectActive = ControladorSonido.Sonido;
    }
    public void VerificarEstado()
    {
        if (isObjectActive)
        {
            buttonImage.sprite = inactiveImage;
            isObjectActive = false;
        }
        else
        {
            buttonImage.sprite = activeImage;
            isObjectActive = true;
        }
    }
}
