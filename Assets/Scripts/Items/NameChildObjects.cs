using UnityEngine;

public class NameChildObjects : MonoBehaviour
{
    public string TheName;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).name = TheName;
        }
    }
}
