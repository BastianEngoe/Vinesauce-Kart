using UnityEngine;

public class JumpToAnimationTime : MonoBehaviour
{
    Animator anim;
    public float normalizedTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        JumpToTime(CurrentAnimationName(), normalizedTime);
    }

    void JumpToTime(string name, float nTime)
    {
        anim.Play(name, 0, nTime);
    }

    string CurrentAnimationName()
    {
        var currAnimName = "";
        foreach (AnimationClip clip in anim.runtimeAnimatorController.animationClips)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName(clip.name))
            {
                currAnimName = clip.name.ToString();
            }
        }

        return currAnimName;
    }
}
