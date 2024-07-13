using UnityEngine;
using Vuforia;
using UnityEngine.Video;
using System.Collections;

public class MatchImageTargetSize : MonoBehaviour
{
    public float zoomSpeed = 0.1f;
    public float zoomDuration = 0.5f; // Duración del zoom en segundos
    private Vector3 initialScale;
    private Vector3 minScale;
    private Vector3 maxScale;

    void Start()
    {
        // Obtener el componente ImageTargetBehaviour del abuelo
        ImageTargetBehaviour imageTarget = GetComponentInParent<Transform>().parent.GetComponentInParent<ImageTargetBehaviour>();

        if (imageTarget != null)
        {
            // Obtener las dimensiones del ImageTarget
            Vector3 targetSize = imageTarget.GetSize();

            // Ajustar las dimensiones del objeto actual
            transform.localScale = new Vector3(targetSize.x / 10, 1, targetSize.y / 10);
            initialScale = transform.localScale;
            minScale = initialScale;
            maxScale = initialScale * 2f;
            // Añadir el componente VideoPlayer si no está presente
            VideoPlayer videoPlayer = gameObject.GetComponent<VideoPlayer>();
            if (videoPlayer == null)
            {
                videoPlayer = gameObject.AddComponent<VideoPlayer>();
                Debug.Log("VideoPlayer component added to the Plane.");
            }

            // Suscribirse al evento de zoom
            ZoomModel.OnZoomChanged += HandleZoomChanged;
        }
        else
        {
            Debug.LogWarning("No se encontró un ImageTargetBehaviour en el abuelo del abuelo.");
        }
    }

    void OnDestroy()
    {
        // Desuscribirse del evento de zoom
        ZoomModel.OnZoomChanged -= HandleZoomChanged;
    }

    private void HandleZoomChanged(bool isZoomingIn)
    {
        if (isZoomingIn)
        {
            StartCoroutine(ZoomIn());
        }
        else
        {
            StartCoroutine(ZoomOut());
        }
    }

    private IEnumerator ZoomIn()
    {
        float elapsedTime = 0f;

        while (elapsedTime < zoomDuration)
        {
            transform.localScale += Vector3.one * zoomSpeed * Time.deltaTime;

            // Limitar el tamaño del objeto al tamaño máximo
            if (transform.localScale.x >= maxScale.x || transform.localScale.y >= maxScale.y || transform.localScale.z >= maxScale.z)
            {
                transform.localScale = maxScale;
                break;
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator ZoomOut()
    {
        float elapsedTime = 0f;

        while (elapsedTime < zoomDuration)
        {
            transform.localScale -= Vector3.one * zoomSpeed * Time.deltaTime;

            // Limitar el tamaño del objeto al tamaño mínimo
            if (transform.localScale.x <= minScale.x || transform.localScale.y <= minScale.y || transform.localScale.z <= minScale.z)
            {
                transform.localScale = minScale;
                break;
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
