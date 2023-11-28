using UnityEngine;

[ExecuteInEditMode]
public class SetSceneBuffer : MonoBehaviour
{
	void Update () 
	{
		if (!GetComponent<Camera>().targetTexture)
		{ 
			GetComponent<Camera>().targetTexture = SetRes.SceneBuffer; 
		}
	}
}
