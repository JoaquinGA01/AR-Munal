using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Cambiar_Escena : MonoBehaviour
{
    public string sceneName; // El nombre de la nueva escena que deseas cargar

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
