using UnityEngine;
using UnityEngine.UI;

public class PotionScript : MonoBehaviour
{
    public static int potionCount;
    public AudioClip error;
    public AudioClip consume;

    private Text text;
    private PlayerHealth pHealth;
    private AudioSource audio;
    private Animator anim;

    void Awake()
    {
        text = GetComponent<Text>();
        audio = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        pHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        potionCount = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GeneralManager.gamePausedOrDone && Input.GetKeyUp("f"))
        {
            drinkPotion();
        }
        text.text = ": " + potionCount;
    }

    public void drinkPotion()
    {
        if (potionCount - 1 < 0)
        {
            audio.clip = error;
            audio.Play();
            anim.SetTrigger("error");
        } else
        {
            audio.clip = consume;
            audio.Play();

            potionCount--;
            pHealth.IncreaseHealth(10);
        }
    }
}
