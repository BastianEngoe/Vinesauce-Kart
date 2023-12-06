using UnityEngine;

public class colliderInAir : MonoBehaviour
{
    public float force = 15000;
    public float groundForce = 0;
    public bool isForAir = true;
    public bool isForRaceEnd = true;
    public bool isONLYforRaceEnd = false;
    public bool relativeForce = true;
}
