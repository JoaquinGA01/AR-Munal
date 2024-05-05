using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Informacion : MonoBehaviour
{
    private bool Escribiendo = false;
    public List<Obra_Informarcion> Informacion_Obras = new List<Obra_Informarcion>();

    public string getInfoObras(string nombre_Obra){
        foreach (Obra_Informarcion obra in Informacion_Obras){
            if(obra.Nombre_Obra.Equals(nombre_Obra)){
                return JsonUtility.ToJson(obra);
            }
        }
        return "";
    }

    public bool getEstado(){
        return Escribiendo;
    }
    public void setEstado(bool e){
        Escribiendo = e;
    }
}
