using UnityEngine;
using UnityEditor; // Necesario para el EditorGUILayout y el CustomEditor

public class InterfazManager : MonoBehaviour
{
    private ObserversData observersData;
    public GameObject Canvas_Principal;
    public GameObject Canvas_Proyeccion;

    private void Start()
    {
        observersData = FindObjectOfType<ObserversData>();
        if(observersData != null){
            observersData.OnEstadoInterfazChanged += HandleEstadoInterfazChanged;
        }
        HandleEstadoInterfazChanged();
    }

    private void HandleEstadoInterfazChanged()
    {
        Canvas_Principal.SetActive(!Canvas_Principal.activeSelf);
        Canvas_Proyeccion.SetActive(!Canvas_Proyeccion.activeSelf);
    }

    private void OnDestroy()
    {
        if(observersData != null){
            observersData.OnEstadoInterfazChanged -= HandleEstadoInterfazChanged;
        }
    }
}
