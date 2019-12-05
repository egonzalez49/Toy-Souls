using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    public float damageMultiplier;
    public bool twoHanded;
    public float twoHandedMultiplier;
    private PC.StateManager playerStateManager;
    private SwordScript swordScript;

    void Awake()
    {
        damageMultiplier = 1f;
        twoHandedMultiplier = 1.5f;
        playerStateManager = GetComponentInParent<PC.StateManager>();
        twoHanded = playerStateManager.twoHanded;
        swordScript = GameObject.FindGameObjectWithTag("Sword").GetComponent<SwordScript>();
    }

    // Update is called once per frame
    void Update()
    {
        twoHanded = playerStateManager.twoHanded;
    }

    public void increaseDamage(float f)
    {
        damageMultiplier += f;
        twoHandedMultiplier += f;
        swordScript.UpgradeSword();
    }
}
