using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{   
    public class EnemyDamageCollider : MonoBehaviour
    {
        public BossMovement boss;
        public PC.EnemyStates parentObject;

        private void OnTriggerEnter(Collider other)
        {
            // do damage
            if (boss != null)
            {
                if (other.gameObject.tag == "Player" && boss.ableToDealDamage == true)
                {
                    PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
                    playerHealth.TakeDamage(15);
                    boss.ableToDealDamage = false;
                }
            }
            if (parentObject != null)
            {
                if (other.gameObject.tag == "Player" && parentObject.ableToDealDamage == true)
                {
                    PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
                    playerHealth.TakeDamage(15);
                    parentObject.ableToDealDamage = false;
                }
            }
        }
    }
}

