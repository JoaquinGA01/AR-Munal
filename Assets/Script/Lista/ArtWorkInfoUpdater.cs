using UnityEngine;
using TMPro;

public class ArtWorkInfoUpdater : MonoBehaviour
{
    public TextMeshProUGUI nombreObraText;  // Referencia al componente TextMeshProUGUI para Nombre_Obra
    public TextMeshProUGUI autorObraText;   // Referencia al componente TextMeshProUGUI para Autor_Obra
    public TextMeshProUGUI ubicacionObraText;   // Referencia al componente TextMeshProUGUI para Ubicacion

    private ArtWorkDataStorage artWorkDataStorage;  // Referencia al ScriptableObject
    private ObserversData observersData;

    void Start()
    {
        observersData = FindObjectOfType<ObserversData>();
        if (observersData == null)
        {
            return;
        }

        // Cargar el ScriptableObject desde la ruta especificada
        artWorkDataStorage = Resources.Load<ArtWorkDataStorage>("ArtWorkDataStorage");
        if (artWorkDataStorage == null)
        {
            return;
        }

        observersData.OnNombreObraChanged += UpdateArtWorkInfo;
        UpdateArtWorkInfo();  // Llama inicialmente para configurar los textos
    }

    private void UpdateArtWorkInfo()
    {
        // Obtén el nombre del target desde observersData
        string nombreTarget = observersData.Nombre_Obra;

        // Busca el objeto ArtWorkData en la lista basado en Nombre_Target
        ArtWorkData foundArtWork = artWorkDataStorage.ArtWorks.Find(artWork => artWork.Nombre_Target == nombreTarget);

        if (foundArtWork != null)
        {
            // Asigna los valores a los TextMeshProUGUI
            nombreObraText.text = "Titulo: " + foundArtWork.Nombre_Obra;
            autorObraText.text = "Autor: " + foundArtWork.Autor_Obra;
            ubicacionObraText.text = "Ubicacion: " + foundArtWork.Ubicacion_Obra;
        }
        else
        {
            nombreObraText.text = "Obra no encontrada";
            autorObraText.text = "";
            ubicacionObraText.text = "";
        }
    }
}
