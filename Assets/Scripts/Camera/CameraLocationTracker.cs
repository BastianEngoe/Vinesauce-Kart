using UnityEngine;
#if UNITY_EDITOR_WIN
using UnityEditor;
#endif

[ExecuteInEditMode]
public class CameraLocationTracker : MonoBehaviour
{
	public string RuntimeCamTag = "RenderCamera";
	public bool DebugReport = false;

	public static Vector3 cameraLoc;
	private Vector3 lastCamLoc;
	public static Transform cameraTransform;

	public static int frameCount = 0;
	public static float Aspect;
	public static float FOV;
	public static GameObject CameraToTrack;

	void Start()
	{
		if(Application.isPlaying)
		{
			CameraToTrack = GameObject.FindGameObjectWithTag(RuntimeCamTag);
			FOV = CameraToTrack.GetComponent<Camera>().fieldOfView;
			Aspect = CameraToTrack.GetComponent<Camera>().aspect;
		}
		if (CameraToTrack is not null && Aspect == 0)
		{
			Aspect = CameraToTrack.GetComponent<Camera>().aspect;
		}
	}

	void OnRenderObject() 
	{
		#if UNITY_EDITOR_WIN
		if(!Application.isPlaying) 
		{
			if(SceneView.currentDrawingSceneView)
			{
				cameraLoc =  SceneView.currentDrawingSceneView.camera.transform.position;
				lastCamLoc = cameraLoc;
				FOV =  SceneView.currentDrawingSceneView.camera.fieldOfView;
				cameraTransform = SceneView.currentDrawingSceneView.camera.transform;
				Aspect = SceneView.currentDrawingSceneView.camera.aspect;
			}
			else 
			{
				cameraLoc = lastCamLoc;
			}
			if (DebugReport)
			{
				Debug.Log("The currently tracked Camera is located at: " + cameraLoc);
			}
		}
		#endif
		if(Application.isPlaying)
		{
			if(CameraToTrack is null)
			{
				CameraToTrack = GameObject.FindGameObjectWithTag(RuntimeCamTag);
				if (CameraToTrack && CameraToTrack.TryGetComponent(out Camera cameraComponent))
				{
					FOV = cameraComponent.fieldOfView;
					Aspect = cameraComponent.aspect;
				}
			}

			if (CameraToTrack is not null)
			{
				cameraLoc = CameraToTrack.transform.position;
				if (!cameraTransform)
				{
					cameraTransform = CameraToTrack.transform;
				}
				if (Time.timeSinceLevelLoad < 2)
				{ 
					Aspect = CameraToTrack.GetComponent<Camera>().aspect;
				}
			}
			else
			{
				cameraLoc = Vector3.zero; 
			}

			if (DebugReport)
			{
				Debug.Log("The currently tracked Camera is located at: " + cameraLoc);
			}
		}

		frameCount ++;
	}
}
