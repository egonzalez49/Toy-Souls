using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public bool playerInRange;
    public string[] dialogue;
    public DialogueManager sdManager;
    public bool isPuzzleGateKeeper;

    private GameObject fence;
    private PlayerCollectibles pc;

    // Start is called before the first frame update
    void Awake()
    {
        playerInRange = false;
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCollectibles>();
        fence = GameObject.FindGameObjectWithTag("Fence");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (pc.hasItem(Collectible.Bucket) && this.CompareTag("Sheep"))
            {
                sdManager.generatePopupMessage(dialogue, 1);
                removeFence();
            } else
            {
                sdManager.generatePopupMessage(dialogue, 0);
            }
            playerInRange = true;
            Debug.Log("Player entered shop zone.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
            sdManager.hideMessage();
            Debug.Log("Player left shop zone.");
        }
    }

    private void removeFence()
    {
        fence.SetActive(false);
    }
}
