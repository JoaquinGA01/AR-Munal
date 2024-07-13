using TMPro;
using UnityEngine;
using System.Collections;

public class TextTyper : MonoBehaviour
{
    public TextMeshPro textMeshPro;  // Asegúrate de asignar el TextMeshPro desde el editor
    public string Atributo;
    private ObserversData observersData;

    void OnEnable()
    {
        observersData = FindObjectOfType<ObserversData>();
        if (observersData == null)
        {
            return;
        }
        observersData.OnEstadoVideoObraChanged += CambioDeObra;
        CambioDeObra();
    }

    void OnDisable(){
        if (observersData != null)
        {
            observersData.OnEstadoVideoObraChanged -= CambioDeObra;
        }
    }

    void CambioDeObra()
    {
        Debug.Log(observersData.EstadoVideoObra);
        if(!observersData.EstadoVideoObra){
            return;
        }
        string nombreObra = observersData.Nombre_Obra;
        if (!string.IsNullOrEmpty(nombreObra))
        {
            ArtWorkData artWork = observersData.ArtWorks.Find(obra => obra.Nombre_Target == nombreObra);

            if (artWork != null)
            {
                if (Atributo == "Panel_Informacion_Audio")
                {
                    TypeText(artWork.Informacion_Audio);
                }
                else
                {
                    TypeText(artWork.Informacion_Extra);
                }
            }
        }
    }
    public void TypeText(string textToType, float delay = 0.05f)
    {
        StartCoroutine(TypeTextCoroutine(textToType, delay));
    }

    private IEnumerator TypeTextCoroutine(string textToType, float delay)
    {
        textMeshPro.text = "";  // Limpiar el texto antes de empezar
        foreach (char letter in textToType.ToCharArray())
        {
            textMeshPro.text += letter;
            yield return new WaitForSeconds(delay);
        }
    }
}
