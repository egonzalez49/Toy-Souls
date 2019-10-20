using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PC { 
    public class EnemyStates : MonoBehaviour
    {
        public float health;
        public bool isInvincible;

        Animator anim;
        EnemyTarget enemyTarget;

        void Start()
        {
            anim = GetComponentInChildren<Animator>();
            enemyTarget = GetComponent<EnemyTarget>();
            enemyTarget.Init(anim);
        }

        private void Update()
        {
            if (isInvincible)
            {
                isInvincible = anim.GetBool("canMove");
            }
        }

        public void DoDamage(float v)
        {
            if (isInvincible)
                return;
            health -= v;
            isInvincible = true;
            // Play damage animation
            anim.Play("damage_1");
        }
    }
}
