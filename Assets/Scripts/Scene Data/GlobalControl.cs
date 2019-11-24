using PC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GlobalControl : MonoBehaviour
{
    public static GlobalControl Instance;

    public int health = 100;
    public int swordIndex = 0;
    public float damageMultiplier = 1f;
    public int potionCount = 3;
    public int souls = 0;
    public bool canRoll = false;

    private static SwordScript swordScript;
    private static PlayerHealth playerHealth;
    private static PlayerSouls playerSouls;
    private static PlayerDamage playerDamage;
    private static StateManager stateManager;

    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        swordScript = GameObject.FindGameObjectWithTag("Sword").GetComponent<SwordScript>();
        playerHealth = player.GetComponent<PlayerHealth>();
        playerSouls = player.GetComponent<PlayerSouls>();
        playerDamage = player.GetComponent<PlayerDamage>();
        stateManager = player.GetComponent<StateManager>();
        GlobalControl.LoadPlayer();
    }

    //Save data to global control   
    public static void SavePlayer()
    {
        Instance.swordIndex = swordScript.swordIndex;
        Instance.health = playerHealth.currentHealth;
        Instance.souls = playerSouls.souls;
        Instance.damageMultiplier = playerDamage.damageMultiplier;
        Instance.potionCount = PotionScript.potionCount;
        Instance.canRoll = stateManager.canRoll;
    }

    //Load necessary data on new scene
    public static void LoadPlayer()
    {
        //swordScript.SetSword(Instance.swordIndex);
        swordScript.swordIndex = Instance.swordIndex;
        playerHealth.SetHealth(Instance.health);
        playerSouls.SetSouls(Instance.souls);
        playerDamage.damageMultiplier = Instance.damageMultiplier;
        PotionScript.potionCount = Instance.potionCount;
        stateManager.canRoll = Instance.canRoll;
    }

    public static void ResetPlayer()
    {
        //swordScript.SetSword(0);
        swordScript.swordIndex = 0;
        playerHealth.SetHealth(100);
        playerSouls.SetSouls(0);
        playerDamage.damageMultiplier = 1f;
        PotionScript.potionCount = 3;
        stateManager.canRoll = false;

        SavePlayer();     
    }
}
