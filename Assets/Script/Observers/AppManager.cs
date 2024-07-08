using UnityEngine;

public class AppManager : MonoBehaviour
{
    public Voz voz;
    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            // La aplicación se ha pausado (minimizada)
            PauseApp();
        }
        else
        {
            // La aplicación ha vuelto a estar activa
            ResumeApp();
        }
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
        {
            // La aplicación ha perdido el foco (minimizada)
            PauseApp();
        }
        else
        {
            // La aplicación ha ganado el foco (vuelta a abrir)
            ResumeApp();
        }
    }

    private void PauseApp()
    {
        voz.StopSpeaking();
        // Pon tu lógica de pausa aquí
        Time.timeScale = 0;
        Debug.Log("App paused");
    }

    private void ResumeApp()
    {
        // Pon tu lógica de reanudar aquí
        Time.timeScale = 1;
        Debug.Log("App resumed");
    }

    private void OnGUI()
    {
        // Manejar el botón de retroceso en Android
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Escape)
            {
                // Cierra la aplicación cuando se presiona el botón de retroceso
                Application.Quit();
            }
        }
    }
}
