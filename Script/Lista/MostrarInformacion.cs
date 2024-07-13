using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MostrarInformacion : MonoBehaviour
{
    public GameObject Panel_Informacion;
    public GameObject panel1;
    public GameObject panel2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void MostrarInfo(){
        panel1.SetActive(!panel1.activeSelf);
        panel2.SetActive(!panel2.activeSelf);
        Panel_Informacion.SetActive(!Panel_Informacion.activeSelf);
    }
}
