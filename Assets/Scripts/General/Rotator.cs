using UnityEngine;

[ExecuteInEditMode]
public class Rotator : MonoBehaviour
{
    public float Rate = .25f;
    public bool rotateVertically = true;
    public bool rotateinEditor = false;

    void Update()
    {
        if (Application.isPlaying)
        {
            ApplyRotation();
        }
    }

    void OnRenderObject()
    {
        if (!Application.isPlaying && rotateinEditor)
        {
            ApplyRotation();
        }
    }

    private void ApplyRotation()
    {
        if (rotateVertically)
        {
            gameObject.transform.RotateAround(transform.position, transform.up, Rate * Time.deltaTime * 60);
        }
        else
        {
            gameObject.transform.RotateAround(transform.position, transform.forward, Rate * Time.deltaTime * 60);
        }
    }
}
