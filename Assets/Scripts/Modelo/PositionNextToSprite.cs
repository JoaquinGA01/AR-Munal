using UnityEngine;

public class PositionNextToSprite : MonoBehaviour
{
    public GameObject Panel_Derecho; // Objeto a posicionar a la derecha
    public GameObject Panel_Izquierdo; // Objeto a posicionar a la izquierda
    public GameObject Panel_Titulo; // Objeto a posicionar debajo
    public GameObject Panel_Interacciones; // Objeto a posicionar arriba
    public SpriteRenderer referenceSpriteRenderer; // El SpriteRenderer del objeto de referencia

    public float Distancia_lados;
    public float Distancia_alto;
    public float Distancia_alto_2;

    private float Distancia_alto_2_1 = 0.1f;

    void Update()
    {
        if (referenceSpriteRenderer != null)
        {
            PositionToTheRightOfSprite();
            PositionToTheLeftOfSprite();
            PositionBelowSprite();
            PositionAboveSprite();
        }
    }

    void PositionToTheRightOfSprite()
    {
        if (Panel_Derecho != null)
        {
            float spriteWidth = referenceSpriteRenderer.bounds.size.x;
            float rightEdgeOfSprite = referenceSpriteRenderer.transform.position.x + (spriteWidth / 2);
            Vector3 newPosition = new Vector3(rightEdgeOfSprite + Distancia_lados, Panel_Derecho.transform.position.y, Panel_Derecho.transform.position.z);
            Panel_Derecho.transform.position = newPosition;
        }
    }

    void PositionToTheLeftOfSprite()
    {
        if (Panel_Izquierdo != null)
        {
            float spriteWidth = referenceSpriteRenderer.bounds.size.x;
            float leftEdgeOfSprite = referenceSpriteRenderer.transform.position.x - (spriteWidth / 2);
            Vector3 newPosition = new Vector3(leftEdgeOfSprite - Distancia_lados, Panel_Izquierdo.transform.position.y, Panel_Izquierdo.transform.position.z);
            Panel_Izquierdo.transform.position = newPosition;
        }
    }

    void PositionBelowSprite()
    {
        if (Panel_Titulo != null)
        {
            float spriteHeight = referenceSpriteRenderer.bounds.size.y;
            float bottomEdgeOfSprite = referenceSpriteRenderer.transform.position.y - (spriteHeight / 2);
            Vector3 newPosition = new Vector3(Panel_Titulo.transform.position.x, Panel_Titulo.transform.position.y, bottomEdgeOfSprite - Distancia_alto);
            Panel_Titulo.transform.position = newPosition;
        }
    }
    
    void PositionAboveSprite()
    {
        if (Panel_Interacciones != null)
        {
            float spriteHeight = referenceSpriteRenderer.bounds.size.y;
            float topEdgeOfSprite = referenceSpriteRenderer.transform.position.y + (spriteHeight / 2);
            Vector3 newPosition = new Vector3(Panel_Interacciones.transform.position.x, Panel_Interacciones.transform.position.y,topEdgeOfSprite + Distancia_alto_2);
            Panel_Interacciones.transform.position = newPosition;
        }
    }
}
