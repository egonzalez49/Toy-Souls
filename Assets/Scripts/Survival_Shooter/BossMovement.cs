using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class BossMovement : MonoBehaviour
    {
        Transform player;
        NavMeshAgent agent;
        Animator anim;
        private VelocityReporter playerVelocityReporter;
        private SphereCollider enemyAttackCollider;
        public bool isInvincible;
        public bool canMove;
        private bool isPlayerColliding;
        public bool isDead;
        public float attackDistance = 2.0f;
        public float dot;
        public PlayerHealth playerHealth;
        public float health;
        public bool ableToDealDamage = true;
        public AudioClip hitClip;
        public AudioClip deathClip;
        AudioSource enemyAudio;
        private Quaternion previousRotation;
        public float timeSinceLastAttack;

        public enum AIState
        {
            chasePlayer,
            attack,
            idle
        };

        public AIState aiState;
        public float dist;
        
        private void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            health = 250;
            agent = GetComponent<NavMeshAgent>();
            anim = GetComponentInChildren<Animator>();
            playerVelocityReporter = player.GetComponent<VelocityReporter>();
            aiState = AIState.chasePlayer;
            dist = Vector3.Distance(player.position, transform.position);
            enemyAttackCollider = GetComponent<SphereCollider>();
            enemyAttackCollider.enabled = false;
            playerHealth = player.GetComponent<PlayerHealth>();
            enemyAudio = GetComponent<AudioSource>();
        }

        // Update is called once per frame
        void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            if (isPlayerColliding)
            {
                dealDamage();
            }
            if (isInvincible)
            {
                isInvincible = !canMove;
            }
            canMove = anim.GetBool("can_move");
            if (!canMove && !isDead)
            {
                transform.LookAt(player.position);
            }
            if (canMove && !isDead)
            {
                agent.enabled = true;
                agent.isStopped = false;
                anim.applyRootMotion = false;
                dist = Vector3.Distance(player.position, transform.position);
                if (dist <= attackDistance && timeSinceLastAttack >= 2.5f)
                {
                    previousRotation = transform.rotation;
                    anim.applyRootMotion = false;
                    aiState = AIState.attack;
                    timeSinceLastAttack = 0.0f;
                    attack();
                }
                if (dist > attackDistance)
                {
                    anim.applyRootMotion = false;
                    aiState = AIState.chasePlayer;
                    chasePlayer();
                }
            }
        }

        void idle()
        {
            anim.SetBool("IsIdle", true);
            chasePlayer();
        }

        void chasePlayer()
        {
            anim.SetBool("IsIdle", false);
            dot = Vector3.Dot((transform.position- player.position).normalized, (playerVelocityReporter.velocity).normalized);
            if (dot >= 0.7f)
            {
                agent.destination = player.position;
            }
            else
            {
                SetDestinationMoving();
            }
        }
        
        void attack()
        {
            anim.SetBool("IsIdle", true);
            anim.SetBool("can_move", false);
            anim.Play("Luigi_Kick");
            agent.isStopped = true;
            anim.applyRootMotion = true;
            aiState = AIState.idle;
            idle();
        }

        public void TakeDamage(float v)
        {
            if (isInvincible || isDead)
                return;
            health -= v;
            isInvincible = true;
            // Play damage animation
            enemyAudio.clip = hitClip;
            enemyAudio.Play();
            anim.applyRootMotion = true;
            anim.SetBool("can_move", false);
            aiState = AIState.idle;
            agent.isStopped = true;
            if (health <= 0)
            {
                death();
            }
            else
            {
                //anim.Play("ted_react");
                idle();
            }
        }

        void death()
        {
            anim.SetBool("IsIdle", true);
            anim.SetTrigger("IsDead");
            isDead = true;
            enemyAudio.clip = deathClip;
            enemyAudio.Play();
        }

        private void dealDamage()
        {
            if (ableToDealDamage)
            {
                ableToDealDamage = false;
                playerHealth.TakeDamage(25);
            }
        }

        public void OpenDamageColliders()
        {
            enemyAttackCollider.enabled = true;
            ableToDealDamage = true;
        }

        public void CloseDamageColliders()
        {
            enemyAttackCollider.enabled = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                isPlayerColliding = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                isPlayerColliding = false;
            }
        }

        private void SetDestinationMoving()
        {
            float dist = (player.position - agent.transform.position).magnitude;
            float lookAheadT = dist / agent.speed;
            Vector3 targetVelocity = playerVelocityReporter.velocity;
            NavMeshHit hit;
            Vector3 targetPoint = player.position + lookAheadT * targetVelocity;
            NavMesh.SamplePosition(targetPoint, out hit, 8.0f, -1);
            agent.destination = hit.position;
        }
    }
}

