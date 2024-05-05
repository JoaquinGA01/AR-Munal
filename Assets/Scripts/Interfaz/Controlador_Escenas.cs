using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controlador_Escenas : MonoBehaviour
{
    public GameObject Canvas_Principal;
    public GameObject Canvas_Proyeccion;
    public bool Bandera;
    // Start is called before the first frame update
    void Start()
    {
        Canvas_Proyeccion.SetActive(false);
        Canvas_Principal.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Bandera)
        {
            Canvas_Proyeccion.SetActive(false);
            Canvas_Principal.SetActive(true);
        }
        else
        {
            Canvas_Principal.SetActive(false);
            Canvas_Proyeccion.SetActive(true);
        }
    }

    public void setBandera(bool b)
    {
        Bandera = b;
    }
}
