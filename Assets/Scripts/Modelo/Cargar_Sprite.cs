using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cargar_Sprite : MonoBehaviour
{
    private string Titulo;
    // Start is called before the first frame update
    void Start()
    {
    }

    public void setTitulo(string t){
        Titulo = t+"/"+t;
        Debug.Log(Titulo);
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        // Carga el sprite de la imagen desde la carpeta Resources.
        Sprite miSprite = Resources.Load<Sprite>(Titulo);

        // Asigna el sprite cargado al SpriteRenderer.
        spriteRenderer.sprite = miSprite;   
    }

}
