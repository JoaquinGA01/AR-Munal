using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class ZoomModel_2 : MonoBehaviour, IPointerDownHandler
{
public float zoomSpeed = 0.1f;
    public float zoomDuration = 0.5f; // Duración del zoom en segundos
    public float doubleClickTime = 0.3f; // Tiempo para detectar doble clic

    private bool isZoomingIn = true;
    private Vector3 initialScale;
    private Vector3 minScale;
    private Vector3 maxScale;
    private float lastClickTime;
    private bool isDoubleClick = false;

    void Start()
    {
        initialScale = transform.localScale;
        minScale = initialScale;
        maxScale = initialScale * 2f;
        lastClickTime = -doubleClickTime; // Inicializar para permitir el primer clic
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        float timeSinceLastClick = Time.time - lastClickTime;

        if (timeSinceLastClick <= doubleClickTime)
        {
            // Doble clic detectado
            isDoubleClick = true;
            return; // No hacer nada en caso de doble clic
        }
        else
        {
            isDoubleClick = false;
            StartCoroutine(HandleSingleClick());
        }

        lastClickTime = Time.time;
    }

    private IEnumerator HandleSingleClick()
    {
        yield return new WaitForSeconds(doubleClickTime);

        if (!isDoubleClick)
        {
            if (isZoomingIn)
            {
                StartCoroutine(ZoomIn());
            }
            else
            {
                StartCoroutine(ZoomOut());
            }

            isZoomingIn = !isZoomingIn;
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
                transform.position += new Vector3(0,0.05f,0);
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
                transform.position -= new Vector3(0,0.05f,0);
                break;
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
