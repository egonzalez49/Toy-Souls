using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    public float damageMultiplier;

    void Awake()
    {
        damageMultiplier = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void increaseDamage(float f)
    {
        damageMultiplier += f;
    }
}
