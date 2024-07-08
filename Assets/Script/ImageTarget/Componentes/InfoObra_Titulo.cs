using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class InfoObra_Titulo : MonoBehaviour
{
    public TextMeshPro nombreObraText;
    public TextMeshPro autorObraText;
    public TextMeshPro fehcaObra;

    private void Start()
    {
        ObserversData observersData = FindObjectOfType<ObserversData>();

        if (observersData == null)
        {
            Debug.LogError("No se encontró un objeto ObserversData en la escena.");
            return;
        }

        string nombreObra = observersData.Nombre_Obra;

        if (!string.IsNullOrEmpty(nombreObra))
        {
            ArtWorkData artWork = observersData.ArtWorks.Find(obra => obra.Nombre_Target == nombreObra);

            if (artWork != null)
            {
                nombreObraText.text = "Titulo: " + artWork.Nombre_Obra;
                autorObraText.text = "Autor: "+artWork.Autor_Obra;
                fehcaObra.text = "Fecha: " + artWork.Fecha;
            }
            else
            {
                Debug.LogError($"No se encontró ninguna obra con el Nombre_Target: {nombreObra}");
            }
        }
        else
        {
            Debug.LogError("El nombre de la obra está vacío o es nulo.");
        }
    }

}
