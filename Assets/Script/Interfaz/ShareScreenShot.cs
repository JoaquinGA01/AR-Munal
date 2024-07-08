using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ShareScreenShot : MonoBehaviour
{
    [SerializeField]
    private GameObject Canvas_Proyeccion;

    public void TakeScreenShot()
    {
        TurnOffARContents();
        StartCoroutine(TakeScreenshotAndShare());
    }

    private void TurnOffARContents(){
        Canvas_Proyeccion.SetActive(Canvas_Proyeccion.activeSelf);
    }

    private IEnumerator TakeScreenshotAndShare()
    {
        yield return new WaitForEndOfFrame();

        Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        ss.Apply();

        string filePath = Path.Combine(Application.temporaryCachePath, "shared img.png");
        File.WriteAllBytes(filePath, ss.EncodeToPNG());

        // To avoid memory leaks
        Destroy(ss);
// SetUrl("https://github.com/yasirkula/UnityNativeShare")
        new NativeShare().AddFile(filePath)
            .SetSubject("Subject goes here").SetText("Eyyy Deberias de venir a probar la nueva aplicacion del MUNAL!")
            .SetCallback((result, shareTarget) => Debug.Log("Share result: " + result + ", selected app: " + shareTarget))
            .Share();
        // Share on WhatsApp only, if installed (Android only)
        //if( NativeShare.TargetExists( "com.whatsapp" ) )
        //	new NativeShare().AddFile( filePath ).AddTarget( "com.whatsapp" ).Share();
        TurnOffARContents();
    }
}
