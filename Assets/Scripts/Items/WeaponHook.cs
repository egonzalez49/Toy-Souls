using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PC
{
    public class WeaponHook : MonoBehaviour
    {
        private MeshCollider damageCollider; //GameObject[] damageCollider;

        private void Awake()
        {
            damageCollider = GetComponent<MeshCollider>();
        }

        public void OpenDamageColliders()
        {
            //for (int i = 0; i < damageCollider.Length; i++)
            //{
            //    damageCollider[i].SetActive(true);
            //}

            damageCollider.enabled = true;
        }

        public void CloseDamageColliders()
        {
            //foreach(GameObject dc in damageCollider)
            //{
            //    dc.SetActive(false);
            //}

            damageCollider.enabled = false;
        }
    }
}

