using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Tomar_Foto : MonoBehaviour
{
    [SerializeField] private GameObject canvasmenu;
    void Update()
    {
    }

    public void takePhoto()
    {
        QuitarCanvas();
        StartCoroutine(TakeScreenshotAndShare());
    }

    private void QuitarCanvas()
    {
        canvasmenu.SetActive(!canvasmenu.activeSelf);
    }

    private IEnumerator TakeScreenshotAndShare()
    {
        yield return new WaitForEndOfFrame();

        Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        ss.Apply();

        string filePath = Path.Combine(Application.temporaryCachePath, "shared img.png");
        File.WriteAllBytes(filePath, ss.EncodeToPNG());
        Destroy(ss);

        new NativeShare().AddFile(filePath)
            .SetSubject("Subject goes here").SetText("Eyyy... Debes de venir al MUNAL a probar su nueva app")
            .SetCallback((result, shareTarget) => Debug.Log("Share result: " + result + ", selected app: " + shareTarget))
            .Share();
        QuitarCanvas();

    }
}
