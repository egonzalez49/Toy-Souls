using UnityEngine;
using UnityEngine.UI;

public class PotionScript : MonoBehaviour
{
    public static int potionCount;
    public AudioClip error;
    public AudioClip consume;

    private Text text;
    private PlayerHealth pHealth;
    private AudioSource audioSource;
    private Animator anim;

    void Awake()
    {
        text = GetComponent<Text>();
        audioSource = GetComponent<AudioSource>();
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
            audioSource.clip = error;
            audioSource.Play();
            anim.SetTrigger("error");
        } else
        {
            audioSource.clip = consume;
            audioSource.Play();

            potionCount--;
            pHealth.IncreaseHealth(10);
        }
    }
}
