using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Importa el namespace para UI.Image
using TMPro; // Importa el namespace para TextMeshPro

public class Controlador_Informacion_Lista : MonoBehaviour
{
    public GameObject color_dominante;
    [SerializeField]
    private GameObject Imagen;
    [SerializeField]
    private TMP_Text Titulo;
    [SerializeField]
    private TMP_Text Autor;
    [SerializeField]
    private TMP_Text Ubicacion;

    public List<Obra_Informarcion> Informacion_Obras = new List<Obra_Informarcion>();
    private int indiceActual = 0; // Indice para llevar el control de la obra actual

    public void setLista(List<Obra_Informarcion> l){
        Informacion_Obras = l;
        MostrarObraActual(); // Muestra la primera obra al inicializar la lista
    }

    void Start(){
        MostrarObraActual();
        color_dominante.GetComponent<ColorDominante>().AplicarColorDominante();
    }

    public void SiguienteObra() {
        if (Informacion_Obras.Count > 0) {
            indiceActual = (indiceActual + 1) % Informacion_Obras.Count; // Avanza y vuelve al inicio si es necesario
            MostrarObraActual();
        }
    }

    public void ObraAnterior() {
        if (Informacion_Obras.Count > 0) {
            if (indiceActual == 0) { // Si es la primera obra, va a la última
                indiceActual = Informacion_Obras.Count - 1;
            } else {
                indiceActual--;
            }
            MostrarObraActual();
        }
    }

    private void MostrarObraActual() {
        Obra_Informarcion obraActual = Informacion_Obras[indiceActual];

        Titulo.text = obraActual.Nombre_Obra;
        Autor.text = obraActual.Autor;
        Ubicacion.text = obraActual.Ubicacion;

        // Carga la imagen desde Resources/NombreObra/NombreObra.png
        Sprite imagenObra = Resources.Load<Sprite>($"{obraActual.Nombre_Obra}/{obraActual.Nombre_Obra}");
        if (imagenObra != null && Imagen.GetComponent<Image>() != null) {
            Imagen.GetComponent<Image>().sprite = imagenObra;
        } else {
            Debug.LogError("No se pudo cargar la imagen para la obra: " + obraActual.Nombre_Obra);
        }
    }
}
