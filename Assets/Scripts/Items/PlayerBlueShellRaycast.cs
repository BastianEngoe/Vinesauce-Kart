using UnityEngine;

public class PlayerBlueShellRaycast : MonoBehaviour
{
    public Ray upRay = new Ray();

    void Update()
    {
        upRay = new Ray(transform.GetChild(0).position, transform.up);
    }
}

