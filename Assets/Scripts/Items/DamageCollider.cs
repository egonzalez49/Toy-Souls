using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PC { 
    public class DamageCollider : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            EnemyStates eStates = other.transform.GetComponentInParent<EnemyStates>();

            if (eStates == null)
                return;

            // do damage
            eStates.TakeDamage(50);
            
        }
    }
}  
