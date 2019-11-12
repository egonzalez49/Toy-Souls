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
        public GameObject leftLegCollider;
        public GameObject rightLegCollider;
        public GameObject leftArmCollider;
        public GameObject rightArmCollider;
        public bool isInvincible;
        public bool canMove;
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
        public EndGameManager endMenu;
        public float delta;
        private bool strongAttack = false;
        public int numAttacks;
        private int randomAnimSelector = 0;

        public enum AIState
        {
            chasePlayer,
            attack,
            idle,
            throwProjectile
        };

        public AIState aiState;
        public float dist;

        private void Awake()
        {
            leftLegCollider.SetActive(false);
            rightLegCollider.SetActive(false);
            leftArmCollider.SetActive(false);
            rightArmCollider.SetActive(false);
            player = GameObject.FindGameObjectWithTag("Player").transform;
            health = 250;
            agent = GetComponent<NavMeshAgent>();
            anim = GetComponentInChildren<Animator>();
            playerVelocityReporter = player.GetComponent<VelocityReporter>();
            aiState = AIState.chasePlayer;
            dist = Vector3.Distance(player.position, transform.position);
            playerHealth = player.GetComponent<PlayerHealth>();
            enemyAudio = GetComponent<AudioSource>();
        }

        // Update is called once per frame
        void Update()
        {
            delta = Time.deltaTime;
            if (isInvincible)
            {
                isInvincible = !canMove;
            }
            canMove = anim.GetBool("can_move");
            if (!canMove && !isDead && randomAnimSelector != 2)
            {
                Quaternion rotationAngle = Quaternion.LookRotation(player.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotationAngle, delta / 2f);
            }
            if (canMove && !isDead)
            {
                agent.enabled = true;
                agent.isStopped = false;
                anim.applyRootMotion = false;
                dist = Vector3.Distance(player.position, transform.position);
                dot = Vector3.Dot((player.position - transform.position).normalized, transform.forward.normalized);
                Quaternion rotationAngle = Quaternion.LookRotation(player.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotationAngle, delta);
                if (dist <= attackDistance && dot >= 0.75)
                {
                    anim.applyRootMotion = false;
                    aiState = AIState.attack;
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
            agent.destination = player.position;
        }

        /*
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
        */
        
        void attack()
        {
            anim.SetBool("IsIdle", true);
            anim.SetBool("can_move", false);
            randomAnimSelector = (int)Random.Range(1, numAttacks + 1);
            anim.Play("Luigi_Attack" + randomAnimSelector);
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
            endMenu.EndScreen(true);
        }

        public void OpenBossLeftLegDamageCollider()
        {
            leftLegCollider.SetActive(true);
            ableToDealDamage = true;
        }

        public void OpenBossRightLegDamageCollider()
        {
            rightLegCollider.SetActive(true);
            ableToDealDamage = true;
        }

        public void CloseBossLeftLegDamageCollider()
        {
            leftLegCollider.SetActive(false);
        }

        public void CloseBossRightLegDamageCollider()
        {
            rightLegCollider.SetActive(false);
        }

        /*
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
        */
    }
}