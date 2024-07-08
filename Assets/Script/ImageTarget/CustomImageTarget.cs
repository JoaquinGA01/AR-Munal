using UnityEngine;
using Vuforia;

public class CustomImageTarget : MonoBehaviour
{
    private DefaultObserverEventHandler observerEventHandler;
    private ObserversData observersData;
    private GameObject instantiatedPrefab;

    void Start()
    {
        observerEventHandler = GetComponent<DefaultObserverEventHandler>();
        observersData = FindObjectOfType<ObserversData>();

        if (observerEventHandler != null)
        {
            observerEventHandler.OnTargetFound.AddListener(OnTargetFound);
            observerEventHandler.OnTargetLost.AddListener(OnTargetLost);
        }
    }

    private void OnTargetFound()
    {
        if (observersData != null)
        {
            observersData.EstadoInterfaz = !observersData.EstadoInterfaz;
            observersData.Nombre_Obra = gameObject.name;
            observersData.EstadoObra = true;
            observersData.EstadoVideoObra = false;
        }

        // Instanciar el prefab como hijo de este objeto
        if (instantiatedPrefab == null)
        {
            GameObject prefabToInstantiate = Resources.Load<GameObject>("Prefabs/ComponentesImageTarget");
            if (prefabToInstantiate != null)
            {
                // Desplazamiento en el eje Y
                Vector3 offsetPosition = new Vector3(transform.position.x, transform.position.y + 0.02f, transform.position.z);
                instantiatedPrefab = Instantiate(prefabToInstantiate, offsetPosition, transform.rotation, transform);
            }
            else
            {
                Debug.LogError("Prefab 'ComponentesImageTarget' no encontrado en la carpeta 'Assets/Resources/Prefabs'.");
            }
        }
    }

    private void OnTargetLost()
    {
        if (observersData != null)
        {
            observersData.EstadoInterfaz = !observersData.EstadoInterfaz;
            observersData.Nombre_Obra = string.Empty;
            observersData.EstadoObra = false;
            observersData.EstadoVideoObra = false;
        }

        // Eliminar el prefab instanciado
        if (instantiatedPrefab != null)
        {
            Destroy(instantiatedPrefab);
            instantiatedPrefab = null;
        }
    }

    void OnDestroy()
    {
        if (observerEventHandler != null)
        {
            observerEventHandler.OnTargetFound.RemoveListener(OnTargetFound);
            observerEventHandler.OnTargetLost.RemoveListener(OnTargetLost);
        }

        // Asegurarse de eliminar el prefab si el objeto se destruye
        if (instantiatedPrefab != null)
        {
            Destroy(instantiatedPrefab);
        }
    }
}
