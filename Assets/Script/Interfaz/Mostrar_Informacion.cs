using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mostrar_Informacion : MonoBehaviour
{
    public Image Panel_Image;
    public Sprite Imagen_Info;
    public Sprite Imagen_Instrucciones;
    private bool Estado;
    // Start is called before the first frame update
    void Start()
    {
        Estado = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Cambiar(){
        Estado = !Estado;
        if(Estado){
            Panel_Image.sprite = Imagen_Info;
        }else{
            Panel_Image.sprite = Imagen_Instrucciones;
        }
        Panel_Image.preserveAspect = true;
    }
}
