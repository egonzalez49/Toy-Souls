using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    public float damageMultiplier;

    private SwordScript swordScript;

    void Awake()
    {
        damageMultiplier = 1f;
        swordScript = GameObject.FindGameObjectWithTag("Sword").GetComponent<SwordScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void increaseDamage(float f)
    {
        damageMultiplier += f;
        swordScript.UpgradeSword();
    }
}
