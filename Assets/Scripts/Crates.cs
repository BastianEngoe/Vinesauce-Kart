using System.Collections;
using UnityEngine;

public class Crates : MonoBehaviour
{
    private Rigidbody rb;
    private Collider bc;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        bc = GetComponent<Collider>();
    }

    public IEnumerator Destroy()
    {
        if (gameObject.name.IndexOf("Crash") >= 0)
        {
            rb.isKinematic = true;
            bc.enabled = false;
            transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().enabled = false;
            transform.GetChild(2).GetComponent<ParticleSystem>().Play();
            transform.GetChild(3).GetComponent<ParticleSystem>().Play();
            GetComponent<AudioSource>().Play();
            transform.GetChild(4).GetComponent<AudioSource>().Play();
        }
        else if (gameObject.name.IndexOf("DK") >= 0)
        {
            rb.isKinematic = true;
            bc.enabled = false;
            transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().enabled = false;
            transform.GetChild(2).GetComponent<SkinnedMeshRenderer>().enabled = false;
            transform.GetChild(3).GetComponent<ParticleSystem>().Play();
            transform.GetChild(4).GetComponent<ParticleSystem>().Play();
            GetComponent<AudioSource>().Play();
            transform.GetChild(5).GetComponent<AudioSource>().Play();
        }

        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
