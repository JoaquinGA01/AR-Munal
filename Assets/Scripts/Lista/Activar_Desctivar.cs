using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activar_Desctivar : MonoBehaviour
{
    public GameObject Canvas1;
    public GameObject Canvas2;


    public void Accion(){
        Canvas1.SetActive(!Canvas1.activeInHierarchy);
        Canvas2.SetActive(!Canvas2.activeInHierarchy);
    }
}



