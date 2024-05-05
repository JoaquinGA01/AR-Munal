using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoom_Modelo : MonoBehaviour
{
    public GameObject secondObject; // Referencia al segundo objeto
    public float zoomSpeed = 0.5f;
    private Vector3 originalScale;
    private Vector3 originalScaleSecondObject;
    private Vector3 maxScale;

    void Start()
    {
        // Guarda la escala original del objeto principal
        originalScale = transform.localScale;
        maxScale = originalScale * 3;

        // Guarda la escala original del segundo objeto
        if (secondObject != null)
        {
            originalScaleSecondObject = secondObject.transform.localScale;
        }
    }

    void Update()
    {
        // Zoom con la rueda del mouse
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Zoom(scroll, zoomSpeed);

        // Zoom con gestos táctiles para móviles
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            Zoom(deltaMagnitudeDiff, zoomSpeed * 0.1f);
        }
    }

    void Zoom(float deltaMagnitudeDiff, float speed)
    {
        Vector3 newScale = transform.localScale - Vector3.one * deltaMagnitudeDiff * speed;
        newScale = new Vector3(
            Mathf.Clamp(newScale.x, originalScale.x, maxScale.x),
            Mathf.Clamp(newScale.y, originalScale.y, maxScale.y),
            Mathf.Clamp(newScale.z, originalScale.z, maxScale.z)
        );

        // Aplica la nueva escala al objeto principal
        transform.localScale = newScale;

        // Calcula y aplica la nueva escala al segundo objeto manteniendo sus proporciones
        if (secondObject != null)
        {
            float scaleFactor = newScale.x / originalScale.x;
            Vector3 newScaleSecondObject = originalScaleSecondObject * scaleFactor;
            secondObject.transform.localScale = newScaleSecondObject;
        }
    }

    public void Restaurar(){
        transform.localScale = originalScale;
        secondObject.transform.localScale = originalScaleSecondObject;
    }
}
