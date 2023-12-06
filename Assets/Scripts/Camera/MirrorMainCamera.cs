using UnityEngine;

[ExecuteInEditMode]
public class MirrorMainCamera : MonoBehaviour
{
    public static Texture2D Lighting;

    void OnRenderObject()
    {
        if (CameraLocationTracker.cameraTransform != null)
        {
            gameObject.transform.rotation = CameraLocationTracker.cameraTransform.rotation;
        }
    }
}
