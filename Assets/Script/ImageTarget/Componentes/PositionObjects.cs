using UnityEngine;
using Vuforia;
public class PositionObjects : MonoBehaviour
{
    // Arrastra los objetos en el inspector
    public GameObject objectBelow;
    public GameObject objectAbove;
    public GameObject objectRight;
    public GameObject objectLeft;
    void Start()
    {
        AdjustWidth(objectBelow);
        AdjustHeightProportionally(objectRight);
        AdjustHeightProportionally(objectLeft);
        PositionAllObjects();
    }

    void OnEnable()
    {
        ZoomModel.OnZoomChangedPerfect += HandleZoomChanged;
    }

    void OnDisable()
    {
        ZoomModel.OnZoomChangedPerfect -= HandleZoomChanged;
    }

    void HandleZoomChanged()
    {
        PositionAllObjects();
    }

    void PositionAllObjects()
    {
        if (objectBelow != null)
        {
            PositionObject(objectBelow, new Vector3(0, 0, -1), "Below", false);
        }
        if (objectAbove != null)
        {
            PositionObject(objectAbove, new Vector3(0, 0, 1), "Above", false);
        }
        if (objectRight != null)
        {
            PositionObject(objectRight, new Vector3(1, 0, 0), "Right", true);
        }
        if (objectLeft != null)
        {
            PositionObject(objectLeft, new Vector3(-1, 0, 0), "Left", true);
        }
    }

    void PositionObject(GameObject obj, Vector3 direction, string positionName, bool isSide)
    {
        Renderer mainRenderer = GetComponent<Renderer>();
        Renderer objRenderer = obj.GetComponent<Renderer>();

        if (mainRenderer != null && objRenderer != null)
        {
            Vector3 mainObjectSize = mainRenderer.bounds.size;
            Vector3 objSize = objRenderer.bounds.size;
            float distanceFactor = isSide ? 1.1f : 1.1f; // 10% más de distancia solo para objetos no laterales
            if (positionName == "Above")
            {
                distanceFactor = 1.2f;
            }
            Vector3 offset = new Vector3(
                direction.x * (mainObjectSize.x / 2 + objSize.x / 2) * distanceFactor,
                0,
                direction.z * (mainObjectSize.z / 2 + objSize.z / 2) * distanceFactor
            );
            Vector3 newPosition = transform.position + offset;
            obj.transform.position = newPosition;
        }
        else
        {
            // Debug.LogError($"Both {positionName} objects must have a Renderer component.");
        }
    }

    void AdjustWidth(GameObject obj)
    {
        Renderer mainRenderer = GetComponent<Renderer>();
        Renderer objRenderer = obj.GetComponent<Renderer>();
        if (mainRenderer != null && objRenderer != null)
        {
            Vector3 mainObjectSize = mainRenderer.bounds.size;
            Vector3 objSize = objRenderer.bounds.size;
            float widthScale = mainObjectSize.x / objSize.x;
            obj.transform.localScale = new Vector3(
                obj.transform.localScale.x * widthScale,
                obj.transform.localScale.y * widthScale,
                obj.transform.localScale.z * widthScale
            );
        }
    }

    void AdjustHeightProportionally(GameObject obj)
    {
        Renderer mainRenderer = GetComponent<Renderer>();
        Renderer objRenderer = obj.GetComponent<Renderer>();
        if (mainRenderer != null && objRenderer != null)
        {
            Vector3 mainObjectSize = mainRenderer.bounds.size;
            Vector3 objSize = objRenderer.bounds.size;
            float heightScale = mainObjectSize.z / objSize.z;
            obj.transform.localScale = new Vector3(
                obj.transform.localScale.x * heightScale,
                obj.transform.localScale.y * heightScale,
                obj.transform.localScale.z * heightScale
            );
        }
    }
}
