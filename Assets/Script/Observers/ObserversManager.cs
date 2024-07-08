using UnityEngine;
using System.Collections.Generic;

public class ObserversManager : MonoBehaviour
{
    public ObserversData observersData;

    private void Start()
    {
        observersData.EstadoInterfaz = false;   
        observersData.EstadoSonidoAnimacion = true;
        observersData.EstadoSonidoVoz = true;
    }
}
