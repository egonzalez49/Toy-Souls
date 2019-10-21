using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PC {
    public class DamageCollider : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            EnemyStates eStates = other.transform.GetComponentInParent<EnemyStates>();
            Enemy.BossMovement boss = other.transform.GetComponentInParent<Enemy.BossMovement>();

            // do damage
            if (eStates != null && boss == null)
            {
                eStates.TakeDamage(50);
            }
            if (boss != null)
            {
                boss.TakeDamage(50);
            }
        }
    }
}  
