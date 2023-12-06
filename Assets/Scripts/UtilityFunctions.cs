using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UtilityFunctions : MonoBehaviour
{
    private PlayerSounds playersounds;
    private Player playerscript;

    private bool drifting;
    private Vector3 offset;

    [HideInInspector]
    public bool hopEnd = false;

    private RACE_MANAGER rm;

    // Start is called before the first frame update
    void Start()
    {
        playersounds = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSounds>();
        playerscript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        //cow stuff
        if (gameObject.name.IndexOf("Cow") >= 0)
        {
            StartCoroutine(CowAnimDelay());
        }

        StartCoroutine(HeadBoneParent());
        StartCoroutine(HeadBoneParentOpponent());

        DisableCam();

        if (gameObject.name.IndexOf("PalmTree") >= 0)
        {
            StartCoroutine(PalmTreeAnim());
        }

        rm = GameObject.Find("RaceManager").GetComponent<RACE_MANAGER>();

        StartCoroutine(TrolleyAudioSource());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.V))
        {
            drifting = true;
        }
        else
        {
            drifting = false;
        }

        CamPos();
        HairAnim();
        Trail();

        PlayerAntiGravityConstantForce();

        if (gameObject.tag == "GliderPanel" || gameObject.tag == "ColliderInAir")
        {
            if (RACE_MANAGER.RACE_COMPLETED)
            {
                GetComponent<BoxCollider>().enabled = false;
            }
        }
    }

    private void LateUpdate()
    {
        MoustacheAnim();

    }

    public void CamShake()
    {
        if (transform.parent.tag == "Player")
        {

            if (!GameObject.Find("RaceManager").GetComponent<RACE_MANAGER>().FrontFPCam.activeSelf)
                Camera.main.GetComponent<Animator>().SetTrigger("Shake");
        }
    }
    public void PlayCoinSound()
    {
        GetComponent<AudioSource>().Play();
    }

    public void PlayHopSound()
    {
        if (drifting)
        {
            //removed sound from here
        }

        HopStart();
    }

    public void GliderOpenflapSound()
    {
        playersounds.effectSounds[12].Play();
    }

    public void IsDrifting()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().drifting = true;
        hopEnd = true;
    }

    public void HopStart()
    {
        hopEnd = false;
    }

    public void CamPos()
    {
        if (gameObject.name == "Main Camera Back")
        {
            transform.localPosition = new Vector3(0, -0.66f, 2.5f);
        }
    }

    public void MoustacheAnim()
    {
        if (gameObject.name == "Moustache")
        {
            if (playerscript.REALCURRENTSPEED > 40)
            {
                gameObject.GetComponent<Animator>().SetBool("moustaceMove", true);
            }
            else
            {
                gameObject.GetComponent<Animator>().SetBool("moustaceMove", false);
            }
        }
    }

    public void HairAnim()
    {
        if (gameObject.name == "Hair")
        {
            if (playerscript.REALCURRENTSPEED > 40)
            {
                gameObject.GetComponent<Animator>().SetBool("HairMove", true);
            }
            else
            {
                gameObject.GetComponent<Animator>().SetBool("HairMove", false);
            }
        }
    }

    public void CountDownNoise()
    {
        GetComponent<AudioSource>().Play();
    }

    public void GoSound()
    {
        transform.GetChild(3).GetComponent<AudioSource>().Play();
    }

    public void RaceStarted()
    {
        RACE_MANAGER.RACE_STARTED = true;
        if (rm.trolleySystem != null)
        {
            rm.trolleySystem.InstantiateFirstTram();
        }
    }

    public void FadeOut()
    {
        GameObject.Find("FadeInOut").GetComponent<Animator>().SetTrigger("FadeOut");
    }

    void Trail()
    {
        if (gameObject.name == "Trail")
        {
            //ground normal rotation
            Ray ground = new Ray(transform.position, -transform.up);
            RaycastHit hit;
            if (Physics.Raycast(ground, out hit, 4) && hit.normal.y > 0.5f)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.FromToRotation(transform.up * 2, hit.normal) * transform.rotation, 7.5f * Time.deltaTime);
                Debug.DrawRay(hit.point, hit.normal, Color.white, 20f);
            }
        }
    }

    public void StartCountDown()
    {
        StartCoroutine(GameObject.Find("RaceManager").GetComponent<RACE_MANAGER>().CountDownTImerPlay());
    }

    public void DisableThisCam()
    {
        gameObject.GetComponent<Camera>().enabled = false;
        transform.GetChild(0).gameObject.SetActive(false);
    }

    public void DisableCourseNameUI()
    {
        StartCoroutine(DisableCourseNameUIFunc());
    }

    IEnumerator DisableCourseNameUIFunc()
    {
        GameObject NameUI = GameObject.Find("RaceManager").GetComponent<RACE_MANAGER>().CourseNameUI;

        //make text transparent
        for (int i = 0; i < 20; i++)
        {
            for (int j = 0; j < NameUI.transform.GetChild(0).childCount; j++)
            {
                Transform text = NameUI.transform.GetChild(0);
                text.GetChild(j).GetComponent<Text>().color -= new Color(0, 0, 0, 0.2f);
            }
            yield return new WaitForSeconds(0.001f);

        }

        //make images transparent
        for (int i = 0; i < 200; i++)
        {
            for (int j = 0; j < NameUI.transform.GetChild(1).childCount; j++)
            {
                Transform images = NameUI.transform.GetChild(1);
                images.GetChild(j).GetComponent<Image>().color -= new Color(0, 0, 0, 0.01f);
            }
            NameUI.GetComponent<Image>().color -= new Color(0, 0, 0, 0.02f);
            yield return new WaitForSeconds(0.001f);
        }
    }

    public void Happy()
    {
        StartCoroutine(HappyFacePlayer());
    }

    public IEnumerator HappyFacePlayer()
    {
        for (int i = 0; i < 40; i++)
        {
            playerscript.MarioFace.material = playerscript.faces[3];
            yield return new WaitForSeconds(0.016f);
        }
    }

    void UpdatePositionUI()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (i + 1 != GameObject.FindGameObjectWithTag("Player").GetComponent<LapCounter>().Position)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
            else
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
        }
    }

    void Explode()
    {
        transform.parent.GetComponent<BlueShell>().isactive = false;
        GameObject clone = Instantiate(transform.parent.GetComponent<BlueShell>().blueExplosion, transform.position + new Vector3(0, 1, 0), transform.parent.GetComponent<BlueShell>().blueExplosion.transform.rotation);
        if (transform.parent.GetComponent<BlueShell>().who_threw_shell != transform.parent.GetComponent<BlueShell>().chase_opponent.name)
        {
            if (transform.parent.GetComponent<BlueShell>().who_threw_shell == "Mario")
            {
                GameObject.Find("Mario").GetComponent<Player>().Driver.SetTrigger("HitItem");
                if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSounds>().CheckIfSoundPlaying())
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSounds>().effectSounds[18].Play();
            }
        }

        try
        {
            rm.FrontCam.GetComponent<Animator>().SetTrigger("Shake2");
        }
        catch (Exception e)
        {

        }
        clone.GetComponent<AudioSource>().Play();
        Instantiate(transform.parent.GetComponent<BlueShell>().smoke, clone.transform.GetChild(0).position, transform.parent.GetComponent<BlueShell>().smoke.transform.rotation);
        Destroy(transform.parent.gameObject);
    }

    public void PlayResultSound()
    {
        GetComponent<AudioSource>().Play();
    }

    IEnumerator CowAnimDelay()
    {
        GetComponent<Animator>().enabled = false;

        int x = UnityEngine.Random.Range(0, 4);

        yield return new WaitForSeconds(x);

        GetComponent<Animator>().enabled = true;

    }

    IEnumerator HeadBoneParent()
    {
        yield return new WaitForSeconds(1);
        if (gameObject.name == "Bone_017")
        {
            Transform newParent = transform.parent.GetChild(1);

            transform.parent = newParent;
        }
    }

    IEnumerator HeadBoneParentOpponent()
    {
        yield return new WaitForSeconds(1);
        if (gameObject.name == "Head")
        {
            Transform newParent = transform.parent.GetChild(3);

            transform.parent = newParent;
        }
    }

    public void DisableSet1()
    {
        GameObject.Find("RaceManager").GetComponent<RACE_MANAGER>().DisableSet1();
    }

    public void DisableSet2()
    {
        GameObject.Find("RaceManager").GetComponent<RACE_MANAGER>().DisableSet2();
    }

    public void DisableSet3()
    {
        GameObject.Find("RaceManager").GetComponent<RACE_MANAGER>().DisableSet3();
    }

    public void EnableSets()
    {
        GameObject.Find("RaceManager").GetComponent<RACE_MANAGER>().EnableAllSets();
    }

    //to make the lens flare system detect that this camera is a camera to include in the post processing effects, but immediately disable after game starts when not using.
    public void DisableCam()
    {
        if (this.gameObject.name == "Main Camera" || gameObject.name == "Main Camera Back" || gameObject.name == "FPCAM")
            StartCoroutine(DisableMainCam());
    }

    IEnumerator DisableMainCam()
    {
        yield return new WaitForSeconds(0.2f);
        gameObject.SetActive(false);
    }

    public IEnumerator PalmTreeAnim()
    {
        float time = UnityEngine.Random.Range(0, 1.5f);
        yield return new WaitForSeconds(time);
        GetComponent<Animator>().enabled = true;
    }

    public IEnumerator TrolleyAudioSource()
    {
        if (gameObject.name == "TrolleyAudioSourceEntry")
        {
            yield return new WaitForSeconds(3);
            GetComponent<AudioSource>().enabled = false;
        }
    }

    void PlayerAntiGravityConstantForce()
    {
        if (gameObject.tag == "Player")
        {
            if (GetComponent<Player>().antiGravity)
            {
                GetComponent<ConstantForce>().enabled = true;
            }
            else
            {
                GetComponent<ConstantForce>().enabled = false;
            }
        }
    }


}
