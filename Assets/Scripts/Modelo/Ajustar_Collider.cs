using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider))] // Usa BoxCollider en lugar de BoxCollider2D
public class Ajustar_Collider : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private BoxCollider boxCollider; // Cambia a BoxCollider

    void Awake()
    {
        // Obtiene los componentes
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider>(); // Cambia a BoxCollider
    }

    void Update()
    {
        // Llama a este método si esperas que el tamaño del sprite cambie durante el juego.
        AdjustColliderSize();
    }

    void AdjustColliderSize()
    {
        // Ajusta el tamaño del boxCollider para que coincida con el tamaño del sprite
        // Aquí necesitamos asegurarnos de que el tamaño sea un Vector3, ya que estamos trabajando con 3D
        boxCollider.size = new Vector3(spriteRenderer.sprite.bounds.size.x, spriteRenderer.sprite.bounds.size.y, 1); // Asegúrate de ajustar la profundidad (z) según sea necesario
        
        // Ajusta la posición del collider si es necesario
        // Al igual que con el tamaño, ahora usamos Vector3
        boxCollider.center = Vector3.zero; // Usa 'center' en lugar de 'offset' para BoxCollider
    }
}
