using UnityEngine;

public class SwipeDetector : MonoBehaviour
{
    private Vector2 startTouchPosition;
    private Vector2 currentTouchPosition;
    private bool isSwiping = false;

    public GameObject informacion_Lista;
    public GameObject color_dominante;
    public Animator Informacion;
    public Animator Imagen;

    void Start(){

    }
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    isSwiping = true;
                    startTouchPosition = touch.position;
                    break;
                case TouchPhase.Moved:
                    if (isSwiping)
                    {
                        currentTouchPosition = touch.position;
                        DetectSwipeDirection();
                    }
                    break;
                case TouchPhase.Ended:
                    isSwiping = false;
                    break;
            }
        }

        // For mouse input
        if (Input.GetMouseButtonDown(0))
        {
            isSwiping = true;
            startTouchPosition = Input.mousePosition;
        }
        if (Input.GetMouseButton(0) && isSwiping)
        {
            currentTouchPosition = Input.mousePosition;
            DetectSwipeDirection();
        }
        if (Input.GetMouseButtonUp(0))
        {
            isSwiping = false;
        }
    }

    private void DetectSwipeDirection()
    {
        if (Mathf.Abs(currentTouchPosition.x - startTouchPosition.x) > 20f) // Swipe distance
        {
            if (currentTouchPosition.x < startTouchPosition.x)
            {
                informacion_Lista.GetComponent<Controlador_Informacion_Lista>().SiguienteObra();
            }
            else if (currentTouchPosition.x > startTouchPosition.x)
            {
                informacion_Lista.GetComponent<Controlador_Informacion_Lista>().ObraAnterior();
            }
            Informacion.Play("Aparicion Informacion");
            Imagen.Play("Aparicion");
            color_dominante.GetComponent<ColorDominante>().AplicarColorDominante();
            isSwiping = false; // Reset isSwiping to detect new swipe after this one
        }
    }
}
