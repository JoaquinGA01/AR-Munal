using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public class AutoAddCustomImageTargetHandler
{
    static AutoAddCustomImageTargetHandler()
    {
        EditorApplication.hierarchyChanged += OnHierarchyChanged;
    }

    private static void OnHierarchyChanged()
    {
        // Get all objects in the scene
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            // Check if the object has the DefaultObserverEventHandler component
            DefaultObserverEventHandler observer = obj.GetComponent<DefaultObserverEventHandler>();

            if (observer != null)
            {
                // Check if the object already has the CustomImageTarget component
                CustomImageTarget customImageTarget = obj.GetComponent<CustomImageTarget>();

                if (customImageTarget == null)
                {
                    // Add the CustomImageTarget component if it doesn't already have it
                    obj.AddComponent<CustomImageTarget>();
                }
            }
        }
    }
}
