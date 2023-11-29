using UnityEngine;

public class RandomRotation : MonoBehaviour
{
    public float rotSpeed = 1f;
    public float driftSpeed = 1f;

    float randomSpinSpeed;
    Vector3 randomDir;

    void Start()
    {
        randomSpinSpeed = Random.Range(-10f, 10f);
        randomDir = Random.onUnitSphere;
    }

    void Update()
    {
        transform.RotateAround(transform.position, transform.forward, rotSpeed * randomSpinSpeed / 20);
        transform.position += randomDir * driftSpeed / 150;
    }
}
