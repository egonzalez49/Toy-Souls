using UnityEngine;

namespace PC {
    public class DamageCollider : MonoBehaviour
    {
        private float damageMultiplier;
        private PlayerDamage playerDamage;

        void Awake()
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            playerDamage = player.GetComponent<PlayerDamage>();
            damageMultiplier = playerDamage.damageMultiplier;
        }

        private void Update()
        {
            damageMultiplier = playerDamage.damageMultiplier;
        }

        private void OnTriggerEnter(Collider other)
        {
            EnemyStates eStates = other.transform.GetComponentInParent<EnemyStates>();
            Enemy.BossMovement boss = other.transform.GetComponentInParent<Enemy.BossMovement>();

            // do damage
            if (eStates != null && boss == null)
            {
                eStates.TakeDamage(50 * damageMultiplier);
            }
            if (boss != null)
            {
                boss.TakeDamage(50 * damageMultiplier);
            }
        }
    }
}  
