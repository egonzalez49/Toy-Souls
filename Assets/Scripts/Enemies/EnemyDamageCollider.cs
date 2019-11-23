using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{   
    public class EnemyDamageCollider : MonoBehaviour
    {
        public BossMovement boss;
        public PC.EnemyStates parentObject;
        private SceneData InfoSaver;
        private GameObject bossObject;

        private void Awake()
        {
            InfoSaver = GameObject.FindGameObjectWithTag("InfoSaver").GetComponent<SceneData>();
            bossObject = GameObject.FindGameObjectWithTag("Boss");
        }

        private void OnTriggerEnter(Collider other)
        {
            // do damage
            Vector3 hitPoint = other.transform.position;
            if (boss != null)
            {
                if (other.gameObject.tag == "Player" && boss.ableToDealDamage == true)
                {
                    PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
                    playerHealth.TakeDamage(InfoSaver.getEnemyBossDamage(), hitPoint);

                    if (InfoSaver.getBossKnockbackBool())
                    {
                        other.GetComponent<Rigidbody>().AddForce(bossObject.transform.forward.normalized * 50, ForceMode.VelocityChange);
                    }
                    boss.ableToDealDamage = false;
                }
            }
            if (parentObject != null)
            {
                if (other.gameObject.tag == "Player" && parentObject.ableToDealDamage == true)
                {
                    PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
                    playerHealth.TakeDamage(15, hitPoint);
                    parentObject.ableToDealDamage = false;
                }
            }
        }
    }
}

