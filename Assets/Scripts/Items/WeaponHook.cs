using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PC
{
    public class WeaponHook : MonoBehaviour
    {
        public GameObject[] damageCollider;

        public void OpenDamageColliders()
        {
            for (int i = 0; i < damageCollider.Length; i++)
            {
                damageCollider[i].SetActive(true);
            }
        }

        public void CloseDamageColliders()
        {
            foreach(GameObject dc in damageCollider)
            {
                dc.SetActive(false);
            }
        }
    }
}

