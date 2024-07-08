using UnityEditor;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

public class InstallPackages : EditorWindow
{
    private static ListRequest listRequest;
    private static AddRequest addRequest;
    private static bool isAssetBundleBrowserInstalled;
    private static bool isVuforiaInstalled;
    private const string VuforiaPackageName = "com.ptc.vuforia.engine";

    [MenuItem("Tools/Install/Install Required Packages")]
    public static void ShowWindow()
    {
        GetWindow<InstallPackages>("Install Required Packages");
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Check and Install Packages"))
        {
            CheckAndInstallPackages();
        }
    }

    private static void CheckAndInstallPackages()
    {
        listRequest = Client.List(); // List installed packages
        EditorApplication.update += ListProgress;
    }

    private static void ListProgress()
    {
        if (listRequest.IsCompleted)
        {
            if (listRequest.Status == StatusCode.Success)
            {
                foreach (var package in listRequest.Result)
                {
                    if (package.name == VuforiaPackageName)
                    {
                        isVuforiaInstalled = true;
                    }
                }
            }
            else if (listRequest.Status >= StatusCode.Failure)
            {
                Debug.LogError(listRequest.Error.message);
            }

            EditorApplication.update -= ListProgress;

            if (!isVuforiaInstalled)
            {
                addRequest = Client.Add(VuforiaPackageName);
                EditorApplication.update += AddProgress;
            }
            else
            {
                Debug.Log("Vuforia Engine is already installed.");
            }
        }
    }

    private static void AddProgress()
    {
        if (addRequest.IsCompleted)
        {
            if (addRequest.Status == StatusCode.Success)
            {
                Debug.Log($"{addRequest.Result.displayName} installed successfully.");
            }
            else if (addRequest.Status >= StatusCode.Failure)
            {
                Debug.LogError(addRequest.Error.message);
            }

            EditorApplication.update -= AddProgress;
        }
    }
}
