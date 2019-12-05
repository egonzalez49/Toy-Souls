using UnityEngine;
using PC;

public class ShopManager : MonoBehaviour
{
    public bool playerInRange;

    private PlayerSouls playerSouls;
    private PlayerDamage playerDamage;
    private int souls;

    //public DialogueManager popup;

    public AudioClip errorSound;
    public AudioClip purchaseSound;

    private GameObject menu;
    private CanvasGroup canvasGroup;
    private AudioSource audioSource;
    private bool allowAction;
    private bool shopOpen;

    void Awake()
    {
        allowAction = false;
        playerInRange = false;
        menu = gameObject.transform.GetChild(0).gameObject;
        canvasGroup = menu.GetComponent<CanvasGroup>();
        audioSource = GetComponent<AudioSource>();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerSouls = player.GetComponent<PlayerSouls>();
        playerDamage = player.GetComponent<PlayerDamage>();
        souls = playerSouls.souls;

        shopOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        souls = playerSouls.souls;

        if ((!GeneralManager.gamePausedOrDone || allowAction) && playerInRange && (Input.GetKeyUp(KeyCode.H) || Input.GetButtonUp(StaticStrings.AButton)))
        {
            // If the menu is open, close it.
            if (canvasGroup.interactable)
            {
                allowAction = false;
                GeneralManager.UpdateGameState(false);
                shopOpen = false;
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
                canvasGroup.alpha = 0f;
                Time.timeScale = 1f;
            }
            else
            {
                allowAction = true;
                GeneralManager.UpdateGameState(true);
                Debug.Log("Game is paused: " + GeneralManager.gamePausedOrDone);
                shopOpen = true;
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
                canvasGroup.alpha = 1f;
                Time.timeScale = 0f;
                Debug.Log("Time scale: " + Time.timeScale);
            }
        }

        if (shopOpen && Input.GetButtonDown(StaticStrings.XButton))
        {
            Debug.Log("Pressed X");
            buyDamage();
        }

        if (shopOpen && Input.GetButtonDown(StaticStrings.BButton))
        {
            Debug.Log("Pressed B");
            buyPotion();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = true;
            //popup.generatePopupMessage("Hi, I'm Piglet! \n Press A/Z to see my wares.");
            //Debug.Log("Player entered shop zone.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
            //popup.hideMessage();
            //Debug.Log("Player left shop zone.");
        }
    }

    public void buyDamage()
    {
        if (souls < 50)
        {
            playSound(errorSound);
        } else
        {
            playerDamage.increaseDamage(0.2f);
            Debug.Log("Damage multiplier is " + playerDamage.damageMultiplier);
            playerSouls.AddSouls(-50);
            playSound(purchaseSound);
        }
    }

    public void buyPotion()
    {
        if (souls < 30)
        {
            playSound(errorSound);
        }
        else
        {
            PotionScript.potionCount++;
            playerSouls.AddSouls(-30);
            playSound(purchaseSound);
        }
    }

    private void playSound(AudioClip aClip)
    {
        audioSource.clip = aClip;
        audioSource.Play();
    }
}
