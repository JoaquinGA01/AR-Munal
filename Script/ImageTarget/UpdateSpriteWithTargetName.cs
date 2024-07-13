using UnityEngine;
using Vuforia;
using System.Linq;

public class UpdateSpriteWithTargetName : MonoBehaviour
{    
    public delegate void SpriteSizeChanged(Vector3 spriteSize, Vector3 spritePosition);
    public static event SpriteSizeChanged OnSpriteSizeChanged;

    private ObserversData observersData;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // Buscar el ObserversData en toda la escena
        observersData = FindObjectOfType<ObserversData>();

        if (observersData != null)
        {
            // Obtener el nombre de la obra
            string obraName = observersData.Nombre_Obra;

            // Cargar todos los sprites desde Resources/Marcadores y subcarpetas
            Sprite[] allSprites = Resources.LoadAll<Sprite>("Marcadores");

            // Buscar el sprite que coincida con el nombre de la obra
            Sprite newSprite = allSprites.FirstOrDefault(sprite => sprite.name == obraName);

            // Obtener el SpriteRenderer en este objeto
            spriteRenderer = GetComponent<SpriteRenderer>();

            if (spriteRenderer != null && newSprite != null)
            {
                // Asignar el nuevo sprite
                spriteRenderer.sprite = newSprite;

                // Ajustar el tamaño del SpriteRenderer para que coincida con el tamaño del ImageTarget
                Transform grandParent = transform.parent?.parent;
                if (grandParent != null)
                {
                    ImageTargetBehaviour imageTargetBehaviour = grandParent.GetComponent<ImageTargetBehaviour>();
                    if (imageTargetBehaviour != null)
                    {
                        Vector2 targetSize = imageTargetBehaviour.GetSize();
                        spriteRenderer.transform.localScale = new Vector3(targetSize.x / spriteRenderer.sprite.bounds.size.x, targetSize.y / spriteRenderer.sprite.bounds.size.y, 1);

                        // Emitir evento de cambio de tamaño
                        OnSpriteSizeChanged?.Invoke(spriteRenderer.bounds.size, spriteRenderer.transform.position);

                        // Añadir un BoxCollider si no existe
                        BoxCollider boxCollider = GetComponent<BoxCollider>();
                        if (boxCollider == null)
                        {
                            boxCollider = gameObject.AddComponent<BoxCollider>();
                        }

                        // Ajustar el tamaño del BoxCollider
                        if (boxCollider != null)
                        {
                            boxCollider.size = new Vector3(boxCollider.size.x, boxCollider.size.y, 0);
                        }
                    }
                    else
                    {
                        Debug.LogWarning("ImageTargetBehaviour no encontrado en el objeto abuelo");
                    }
                }
                else
                {
                    Debug.LogWarning("Objeto abuelo no encontrado");
                }
            }
            else
            {
                Debug.LogWarning("SpriteRenderer o Sprite no encontrado");
            }
        }
        else
        {
            Debug.LogWarning("ObserversData no encontrado en la escena");
        }
    }
}
