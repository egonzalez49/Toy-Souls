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
            Vector3 hitPoint = other.transform.position;
            // do damage
            if (eStates != null && boss == null)
            {
                //Logger.WriteToFile("Player dealt damage - " + ((playerDamage.twoHanded) ? 50 * damageMultiplier * playerDamage.twoHandedMultiplier : 50 * damageMultiplier) + ".");
                eStates.TakeDamage( (playerDamage.twoHanded)? 50*damageMultiplier*playerDamage.twoHandedMultiplier :50 * damageMultiplier, hitPoint);
            }
            if (boss != null)
            {
                //Logger.WriteToFile("Player dealt damage - " + ((playerDamage.twoHanded) ? 40 * damageMultiplier * playerDamage.twoHandedMultiplier : 40 * damageMultiplier) + ".");
                boss.TakeDamage((playerDamage.twoHanded) ? 40 * damageMultiplier * playerDamage.twoHandedMultiplier : 40 * damageMultiplier, hitPoint);
            }
        }
    }
}  
