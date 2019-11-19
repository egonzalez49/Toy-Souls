using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public Collectible collectible;

    private PlayerCollectibles pc;
    private AudioSource aSource;

    void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        pc = player.GetComponent<PlayerCollectibles>();
        aSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //aSource = GetComponent<AudioSource>();
            aSource.Play();
            pc.collectItem(collectible);
            Destroy(this.gameObject, 0.2f);
            Debug.Log("Collected collectible: " + collectible);
        }
    }
}
