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
    private BarScript healthAmount;
    private bool isDead;
    private bool damaged;

    private PlayerSouls playerSouls;
    private PlayerDamage playerDamage;

    ParticleSystem hitParticles;

    void Awake()
    {
        playerAudio = GetComponent<AudioSource>();
        playerSouls = GetComponent<PlayerSouls>();
        playerDamage = GetComponent<PlayerDamage>();
        healthAmount = healthBar.GetComponent<BarScript>();
        currentHealth = startingHealth;
        healthAmount.SetFillAmount(currentHealth);
        anim = GetComponentInChildren<Animator>();
        hitParticles = GetComponentInChildren<ParticleSystem>();
    }

    void Start()
    {
        GlobalControl.LoadPlayer();
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
        if (transform.position.y < -30f && !isDead)
        {
            Death();
        }
        damaged = false;
    }

    public void SetHealth(int amount)
    {
        currentHealth = amount;
        healthAmount.SetFillAmount(currentHealth);
    }

    //call this function to deal damage to the player
    public void TakeDamage(int amount, Vector3 hitPoint)
    {
        if (currentHealth <= 0) return;
        damaged = true;

        currentHealth -= amount;

        Logger.WriteToFile("Player took damage.");
        Logger.WriteToFile("Player health: " + currentHealth + ".");

        healthAmount.SetFillAmount(currentHealth);

        hitParticles.transform.position = hitPoint;
        hitParticles.Play();

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

        Logger.WriteToFile("Player healed.");
        Logger.WriteToFile("Player health: " + currentHealth + ".");
        healthAmount.SetFillAmount(currentHealth);
    }

    void Death()
    {
        isDead = true;

        Logger.WriteToFile("Player died.");

        /* play a death noise */
        playerAudio.clip = deathClip;
        playerAudio.Play();
        anim.SetTrigger("isDead");
        float time = 10.0f;

        while (time >= 0f)
        {
            time -= Time.deltaTime;
        }

        endMenu.EndScreen(false);
    }
}
