using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ArtWorkData
{
    public string Nombre_Obra;
    public string Autor_Obra;
    public string Informacion_Audio;
    public string Informacion_Extra;
    public string Ubicacion_Obra;
    public string Nombre_Target;
    public string Fecha;
    public string Obra_Instanciada;
}

public class ObserversData : MonoBehaviour
{
    [SerializeField]
    private ArtWorkDataStorage artWorkDataStorage; // Referencia al ScriptableObject
    private bool estadoInterfaz;
    private string nombreObra;
    private bool estadoObra;
    private bool estadoVideoObra;
    private bool estadoSonidoAnimacion;
    private bool estadoSonidoVoz;

    public event Action<List<ArtWorkData>> OnListChanged;
    public event Action OnEstadoInterfazChanged;
    public event Action OnNombreObraChanged;
    public event Action OnEstadoObraChanged;
    public event Action OnEstadoVideoObraChanged;
    public event Action OnEstadoSonidoAnimacionChanged;
    public event Action OnEstadoSonidoVozChanged;

    public List<ArtWorkData> ArtWorks
    {
        get => artWorkDataStorage.ArtWorks;
        set
        {
            artWorkDataStorage.ArtWorks = value;
            OnListChanged?.Invoke(artWorkDataStorage.ArtWorks);
        }
    }

    public bool EstadoInterfaz
    {
        get => estadoInterfaz;
        set
        {
            if (estadoInterfaz != value)
            {
                estadoInterfaz = value;
                OnEstadoInterfazChanged?.Invoke();
            }
        }
    }

    public string Nombre_Obra
    {
        get => nombreObra;
        set
        {
            if (nombreObra != value)
            {
                nombreObra = value;
                OnNombreObraChanged?.Invoke();
            }
        }
    }

    public bool EstadoSonidoAnimacion
    {
        get => estadoSonidoAnimacion;
        set
        {
            if (estadoSonidoAnimacion != value)
            {
                estadoSonidoAnimacion = value;
                OnEstadoSonidoAnimacionChanged?.Invoke();
            }
        }
    }

    public bool EstadoSonidoVoz
    {
        get => estadoSonidoVoz;
        set
        {
            if (estadoSonidoVoz != value)
            {
                estadoSonidoVoz = value;
                OnEstadoSonidoVozChanged?.Invoke();
            }
        }
    }
    public bool EstadoObra
    {
        get => estadoObra;
        set
        {
            if (estadoObra != value)
            {
                estadoObra = value;
                OnEstadoObraChanged?.Invoke();
            }
        }
    }

    public bool EstadoVideoObra{
        get => estadoVideoObra;
        set{
            if(estadoVideoObra != value){
                estadoVideoObra = value;
                OnEstadoVideoObraChanged?.Invoke();
            }
        }
    }

    public void Add(ArtWorkData item)
    {
        artWorkDataStorage.ArtWorks.Add(item);
        OnListChanged?.Invoke(artWorkDataStorage.ArtWorks);
    }

    public void Remove(ArtWorkData item)
    {
        artWorkDataStorage.ArtWorks.Remove(item);
        OnListChanged?.Invoke(artWorkDataStorage.ArtWorks);
    }

    public void Clear()
    {
        artWorkDataStorage.ArtWorks.Clear();
        OnListChanged?.Invoke(artWorkDataStorage.ArtWorks);
    }
}
