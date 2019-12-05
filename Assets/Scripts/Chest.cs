using UnityEngine;
using PC;
using Enemy;

public enum ChestItem
{
    Potion,
    Souls,
    Trick
}

public class Chest : MonoBehaviour
{
    public Mesh openMesh;
    public PopupManager popupManager;
    public ChestItem item;
    public AudioClip trickClip;


    private bool playerInRange;
    private bool openedChest;
    private AudioSource audioSource;
    private MeshFilter meshFilter;
    private PlayerSouls pSouls;
    private EnemyManager enemyManager;
    //private PotionScript potionScript;

    void Awake()
    {
        playerInRange = false;
        openedChest = false;
        audioSource = GetComponent<AudioSource>();
        meshFilter = GetComponent<MeshFilter>();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        pSouls = player.GetComponent<PlayerSouls>();

        enemyManager = GameObject.FindGameObjectWithTag("Enemy Manager").GetComponent<EnemyManager>();
        //potionScript = player.GetComponent<PotionScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange && Input.GetButtonDown(StaticStrings.AButton) && !openedChest)
        {
            openedChest = true;
            meshFilter.mesh = openMesh;
            if (item != ChestItem.Souls || item != ChestItem.Trick)
            {
                audioSource.Play();
                popupManager.generateTimedPopupMessage("Obtained " + item + ".", 3f);
            }
            if (item == ChestItem.Souls)
            {
                audioSource.Play();
                popupManager.generateTimedPopupMessage("Obtained 20 " + item + ".", 3f);
            }
            if (item == ChestItem.Trick)
                popupManager.generateTimedPopupMessage("Oh no, Billy tricked you!.", 3f);
            GetItem();
        }
    }

    private void GetItem()
    {
        if (item == ChestItem.Potion)
        {
            PotionScript.potionCount++;
        } else if (item == ChestItem.Souls) {
            pSouls.AddSouls(20);
        } else if (item == ChestItem.Trick)
        {
            enemyManager.TrickSpawn();
            audioSource.PlayOneShot(trickClip);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = true;
            if (!openedChest)
                popupManager.generateTimedPopupMessage("Press Button A/Key Z to open the chest.", 3f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
            //popupManager.hideMessage();
        }
    }
}
