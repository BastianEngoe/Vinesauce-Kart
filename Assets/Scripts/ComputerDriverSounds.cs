using UnityEngine;

public class ComputerDriverSounds : MonoBehaviour
{
    private ComputerDriver aiScript;
    private OpponentItemManager itemManage;
    public AudioSource kartSound;
    public AudioSource kartIdle;

    public AudioSource[] BulletSounds;

    void Start()
    {
        aiScript = GetComponent<ComputerDriver>();
        itemManage = GetComponent<OpponentItemManager>();
    }

    void Update()
    {
        kart_sounds();
    }

    void kart_sounds()
    {
        if (aiScript.current_speed < 10 && aiScript.current_speed >= -10)
        {
            if (!kartIdle.isPlaying)
            {
                kartIdle.Play();
            }
        }
        if (aiScript.current_speed < -10)
        {
            kartIdle.Stop();
        }

        //bullet stuff later
        if (aiScript.current_speed >= 5 && !aiScript.GliderFly && !itemManage.isBullet)
        {
            if (!kartSound.isPlaying)
            {
                kartSound.Play();
            }

            //speed variations affect the time the kart sound is on
            if (aiScript.current_speed > 5 && aiScript.current_speed <= 10)
            {
                kartSound.time = Mathf.Lerp(kartSound.time, 1f, 3 * Time.deltaTime);
                kartIdle.Stop();

            }
            if (aiScript.current_speed > 10 && aiScript.current_speed <= 20)
            {
                kartSound.time = Mathf.Lerp(kartSound.time, 1.5f, 4 * Time.deltaTime);
                kartIdle.Stop();

            }
            else if (aiScript.current_speed > 20 && aiScript.current_speed < 30)
            {
                kartSound.time = Mathf.Lerp(kartSound.time, 2.5f, 4 * Time.deltaTime);
                kartIdle.Stop();

            }
            else if (aiScript.current_speed >= 30 && aiScript.current_speed < 40)
            {
                kartSound.time = Mathf.Lerp(kartSound.time, 4, 4 * Time.deltaTime);
                kartIdle.Stop();

            }
            else if (aiScript.current_speed >= 40 && aiScript.current_speed < 50)
            {
                kartSound.time = Mathf.Lerp(kartSound.time, 5, 4 * Time.deltaTime);
                kartIdle.Stop();

            }
            else if (aiScript.current_speed >= 50 && aiScript.current_speed < 60)
            {
                kartSound.time = Mathf.Lerp(kartSound.time, 6, 4 * Time.deltaTime);
                kartIdle.Stop();

            }
            else if (aiScript.current_speed >= 60 && aiScript.current_speed < 70)
            {
                kartSound.time = Mathf.Lerp(kartSound.time, 7, 4 * Time.deltaTime);
                kartIdle.Stop();

            }


            //pitch
            if (!aiScript.boost)
            {
                kartSound.pitch = Mathf.Lerp(kartSound.pitch, 1f, 5f * Time.deltaTime);
            }
            else if (aiScript.boost && !aiScript.GliderFly)
            {
                kartSound.pitch = Mathf.Lerp(kartSound.pitch, 1.3f, 5f * Time.deltaTime);
            }
            else if (aiScript.boost && aiScript.GliderFly)
            {
                kartSound.pitch = Mathf.Lerp(kartSound.pitch, 1.5f, 5f * Time.deltaTime);
            }

        }
        if (aiScript.current_speed < 10 || itemManage.isBullet)
        {
            kartSound.Stop();
        }
        if (aiScript.GliderFly && !RACE_MANAGER.RACE_COMPLETED)
        {
            kartSound.volume = 0.5f;
        }
        else if (!aiScript.GliderFly && !RACE_MANAGER.RACE_COMPLETED)
        {
            kartSound.volume = 1f;
        }

        if (RACE_MANAGER.RACE_COMPLETED && kartSound.volume > 0)
        {

            kartSound.volume -= 0.01f;
        }
    }
}
