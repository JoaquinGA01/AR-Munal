using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ajustar_Size : MonoBehaviour
{
    public SpriteRenderer targetSprite; // Referencia al SpriteRenderer que quieres imitar

    void Update()
    {
        if (targetSprite != null)
        {
            // Calcula el tamaño del sprite en el mundo
            float spriteWidth = targetSprite.sprite.bounds.size.x * targetSprite.transform.localScale.x;
            float spriteHeight = targetSprite.sprite.bounds.size.y * targetSprite.transform.localScale.y;

            // Asume que el tamaño del Plane es de 10 unidades en Unity (por defecto es 10x10)
            Vector3 newScale = transform.localScale;
            newScale.x = spriteWidth / 10.0f;
            newScale.z = spriteHeight / 10.0f;
            

            // Aplica la nueva escala al Plane
            transform.localScale = newScale;

            // Ajusta la posición para coincidir con la del SpriteRenderer
            Vector3 newPosition = targetSprite.transform.position;
            // Agrega 0.001 al eje Y de la posición

            // Opcional: ajusta la posición si es necesario
            transform.position = targetSprite.transform.position;
            transform.position += new Vector3(0, 0.01f, 0);
        }
        else
        {
            Debug.LogError("No se ha asignado un targetSprite. Por favor, asigna un SpriteRenderer de referencia.");
        }
    }
}
