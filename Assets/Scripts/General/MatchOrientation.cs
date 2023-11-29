using UnityEngine;

[ExecuteInEditMode]
public class MatchOrientation : MonoBehaviour
{
    public bool TrackInEditor = false;
    public GameObject ReferenceObj;

    void OnRenderObject()
    {
        if (Application.isPlaying)
        {
            transform.position = CameraLocationTracker.cameraLoc;
        }
#if UNITY_EDITOR_WIN
        if (TrackInEditor)
        {
            if (CameraLocationTracker.cameraTransform)
            {
                transform.position = CameraLocationTracker.cameraLoc; 
            }
        }
#endif
    }

}
