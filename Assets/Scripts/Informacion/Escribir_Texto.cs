using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Escribir_Texto : MonoBehaviour
{
    public TMP_Text texto;
    private float velocidadDeEscritura = 0.05f;

 
    public void IniciarTexto(string Informacion)
    {


        StartCoroutine(EscribirTexto(Informacion));

    }

    IEnumerator EscribirTexto(string textoAEscribir)
    {

        foreach (char letra in textoAEscribir)
        {
            texto.text += letra; 
            yield return new WaitForSeconds(velocidadDeEscritura); 
        }

    }
    public void Recarga(){
        texto.text="";

    }

}
