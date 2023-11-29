using System.Collections;
using UnityEngine;

public class RotatePlayerJumpPanel : MonoBehaviour
{
    public float rotateY;
    public float rotateZ;

    private IEnumerator OnTriggerEnter(Collider other)
    {
        if((other.gameObject.tag == "Player" && other.gameObject.GetComponent<Player>().JUMP_PANEL) || (other.gameObject.tag == "Opponent" && other.GetComponent<ComputerDriver>().JUMP_PANEL))
        {
            for(int i = 0; i < 60; i++)
            {
                other.transform.rotation = Quaternion.Lerp(other.transform.rotation, Quaternion.Euler(transform.eulerAngles.x, rotateY, rotateZ), 1 * Time.deltaTime);
                yield return new WaitForSeconds(0.01f);
            }
        }
    }
}
