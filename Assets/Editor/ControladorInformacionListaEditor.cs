using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
[CustomEditor(typeof(Controlador_Informacion_Lista))]
public class ControladorInformacionListaEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Pegar Lista de Obras"))
        {
            Controlador_Informacion_Lista script = (Controlador_Informacion_Lista)target;
            if (InformacionEditorUtilities.CopiedInformacionObras != null)
            {
                script.setLista(new List<Obra_Informarcion>(InformacionEditorUtilities.CopiedInformacionObras));
                Debug.Log("Lista de obras pegada");
            }
            else
            {
                Debug.LogWarning("No hay una lista copiada para pegar");
            }
        }
    }
}