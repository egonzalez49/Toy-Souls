using UnityEngine;
using UnityEngine.UI;
using PC;

public class PotionScript : MonoBehaviour
{
    public static int potionCount;
    public AudioClip error;
    public AudioClip consume;

    private Text text;
    private AudioSource audioSource;
    private Animator anim;
    private PlayerHealth pHealth;
    private StateManager stateManager;

    void Awake()
    {
        text = GetComponent<Text>();
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        pHealth = player.GetComponent<PlayerHealth>();
        stateManager = player.GetComponent<StateManager>();

        //potionCount = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GeneralManager.gamePausedOrDone && Input.GetButtonDown(StaticStrings.XButton))
        {
            Debug.Log(stateManager.anim.GetBool("interacting"));
            drinkPotion();
        }
        text.text = ": " + potionCount;
    }

    public void drinkPotion()
    {
        if (stateManager.usingItem) return;

        if (potionCount - 1 < 0)
        {
            stateManager.validItemAction = false;
            audioSource.clip = error;
            audioSource.Play();
            anim.SetTrigger("error");
        } else
        {
            stateManager.validItemAction = true;
            audioSource.clip = consume;
            audioSource.Play();

            potionCount--;
            pHealth.IncreaseHealth(40);
        }
    }
}
