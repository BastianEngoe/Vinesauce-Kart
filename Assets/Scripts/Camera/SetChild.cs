using UnityEngine;

[ExecuteInEditMode]
public class SetChild : MonoBehaviour
{
	void OnEnable()
	{
		GetComponent<Camera>().targetTexture = SetRes.SceneBuffer;
	}

	void Update()
	{
		if (!GetComponent<Camera>().targetTexture)
		{
			GetComponent<Camera>().targetTexture = SetRes.SceneBuffer; 
		}
	}
}
