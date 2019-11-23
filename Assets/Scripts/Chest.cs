using UnityEngine;
using PC;

public enum ChestItem
{
    Potion
}

public class Chest : MonoBehaviour
{
    public Mesh openMesh;
    public PopupManager popupManager;
    public ChestItem item;


    private bool playerInRange;
    private bool openedChest;
    private AudioSource audioSource;
    private MeshFilter meshFilter;
    //private PotionScript potionScript;

    void Awake()
    {
        playerInRange = false;
        openedChest = false;
        audioSource = GetComponent<AudioSource>();
        meshFilter = GetComponent<MeshFilter>();

        //GameObject player = GameObject.FindGameObjectWithTag("Player");
        //potionScript = player.GetComponent<PotionScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange && Input.GetButtonDown(StaticStrings.AButton) && !openedChest)
        {
            openedChest = true;
            audioSource.Play();
            meshFilter.mesh = openMesh;
            popupManager.generateTimedPopupMessage("Obtained " + item + ".", 3f);
            GetItem();
        }
    }

    private void GetItem()
    {
        if (item == ChestItem.Potion)
        {
            PotionScript.potionCount++;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = true;
            if (!openedChest)
                popupManager.generateTimedPopupMessage("Press A/Z to open the chest.", 3f);
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
