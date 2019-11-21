using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public Image damageImage;
    public Image healthBar;
    public AudioClip deathClip;
    public AudioClip playerHurt;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);
    public EndGameManager endMenu;

    private Animator anim;
    private AudioSource playerAudio;
    //private PlayerMovement playerMovement; //TODO: creates script for player movement
    private BarScript healthAmount;
    private bool isDead;
    private bool damaged;

    private PlayerSouls playerSouls;
    private SwordScript swordScript;
    private PlayerDamage playerDamage;

    void Awake()
    {
        //anim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        playerSouls = GetComponent<PlayerSouls>();
        playerDamage = GetComponent<PlayerDamage>();
        swordScript = GameObject.FindGameObjectWithTag("Sword").GetComponent<SwordScript>();
        //playerMovement = GetComponent<PlayerMovement>();
        healthAmount = healthBar.GetComponent<BarScript>();
        currentHealth = startingHealth;
        healthAmount.SetFillAmount(currentHealth);
    }

    void Start()
    {
        currentHealth = GlobalControl.Instance.health;
        healthAmount.SetFillAmount(currentHealth);
        swordScript.swordIndex = GlobalControl.Instance.swordIndex;
        playerDamage.damageMultiplier = GlobalControl.Instance.damageMultiplier;
        PotionScript.potionCount = GlobalControl.Instance.potionCount;
        playerSouls.souls = GlobalControl.Instance.souls;
    }

    void Update()
    {
        if (damaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        if (transform.position.y < -40f && !isDead)
        {
            Death();
        }
        damaged = false;
    }

    //call this function to deal damage to the player
    public void TakeDamage(int amount)
    {
        if (currentHealth <= 0) return;
        damaged = true;

        currentHealth -= amount;
        Debug.Log("Current health: " + currentHealth);
        healthAmount.SetFillAmount(currentHealth);

        /* play a hurt noise */
        playerAudio.clip = playerHurt;
        playerAudio.Play();

        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    public void IncreaseHealth(int amount)
    {
        if (currentHealth + amount > 100)
        {
            currentHealth = 100;
        } else
        {
            currentHealth += amount;
        }
        healthAmount.SetFillAmount(currentHealth);
    }

    void Death()
    {
        isDead = true;

        /* play a death animation */
        //anim.SetTrigger("Die");

        /* play a death noise */
        playerAudio.clip = deathClip;
        playerAudio.Play();

        float time = 10.0f;

        while (time >= 0f)
        {
            time -= Time.deltaTime;
        }

        endMenu.EndScreen(false);

        /* disable player movement */
        //playerMovement.enabled = false;
    }

    //Save data to global control   
    public void SavePlayer()
    {
        GlobalControl.Instance.health = currentHealth;
        GlobalControl.Instance.swordIndex = swordScript.swordIndex;
        GlobalControl.Instance.damageMultiplier = playerDamage.damageMultiplier;
        GlobalControl.Instance.potionCount = PotionScript.potionCount;
        GlobalControl.Instance.souls = playerSouls.souls;
    }
}
