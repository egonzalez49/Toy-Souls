using Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace PC { 
    public class EnemyStates : MonoBehaviour
    {
        public float health;
        public bool isInvincible;
        public bool canMove;
        public float delta;
        public bool isDead;
        public bool isSinking;
        public float sinkSpeed = 2.5f;
        public float idleTimer = 0.0f;
        public bool ableToDealDamage = true;

        public Animator anim;
        public Rigidbody rigid;
        SoulScript soulScript;
        EnemyManager enemyManager;
        EnemyTarget enemyTarget;
        AnimatorHook a_hook;
        CapsuleCollider capsuleCollider;
        GameObject playerObject;
        PlayerHealth playerHealth;

        Transform player;
        NavMeshAgent agent;
        private float attackTimer = 1.0f;
        private bool isPlayerColliding = false;
        AudioSource enemyAudio;
        public AudioClip deathClip;
        public bool enemyDamagePossible = false;
        private SphereCollider enemyAttackCollider;

        public enum AIState
        {
            wander,
            chasePlayer,
            attack,
            idle
        }

        public AIState aiState;
        public float dist;
        public float random_radius = 45.0f;
        public float wanderTimer = 28.0f;
        public float chaseDistance = 7.0f;
        public float attackDistance = 1.5f;
        public float chaseSpeed = 2.0f;
        public float wanderSpeed = 1.0f;
        public float attackingSpeed = 0.2f;
        public float timer;

        /*
        List<Rigidbody> ragdollRigids = new List<Rigidbody>();
        List<Collider> ragdollColliders = new List<Collider>();
        */

        void Start()
        {
            enemyAudio = GetComponent<AudioSource>();
            health = 100;
            anim = GetComponentInChildren<Animator>();
            enemyTarget = GetComponent<EnemyTarget>();
            enemyTarget.Init(this);
            capsuleCollider = GetComponent<CapsuleCollider>();
            isSinking = false;
            player = GameObject.FindGameObjectWithTag("Player").transform;
            playerObject = GameObject.FindGameObjectWithTag("Player");
            agent = GetComponent<NavMeshAgent>();
            playerHealth = player.GetComponent<PlayerHealth>();
            enemyAttackCollider = GetComponent<SphereCollider>();
            enemyAttackCollider.enabled = false;

            rigid = GetComponent<Rigidbody>();
            aiState = AIState.wander;
            dist = Vector3.Distance(player.position, transform.position);
            agent.destination = RandomNavMeshLocation(random_radius);

            a_hook = anim.GetComponent<AnimatorHook>();
            if (a_hook == null)
                a_hook = anim.gameObject.AddComponent<AnimatorHook>();
            a_hook.Init(null, this);

            GameObject SoulsText = GameObject.FindGameObjectWithTag("Souls");
            soulScript = SoulsText.GetComponent<SoulScript>();

            GameObject enemySpawner = GameObject.FindGameObjectWithTag("Enemy Manager");
            enemyManager = enemySpawner.GetComponent<EnemyManager>();

        }

        void Update()
        {
            delta = Time.deltaTime;

            if (isSinking)
            {
                // ... move the enemy down by the sinkSpeed per second.
                transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
            }

            if (isPlayerColliding)
            {
                dealDamage();
            }

            if (isInvincible)
            {
                isInvincible = !canMove;
            }
            canMove = anim.GetBool("can_move");
            if (canMove && !isDead)
            {
                timer += delta;
                agent.enabled = true;
                agent.isStopped = false;
                anim.applyRootMotion = false;
                dist = Vector3.Distance(player.position, transform.position);
                if (dist >= chaseDistance)
                {
                    aiState = AIState.wander;
                    wander();
                }
                if (dist <= attackDistance)
                {
                    aiState = AIState.attack;
                    attack();
                }
                if (dist <= chaseDistance && dist > attackDistance)
                {
                    aiState = AIState.chasePlayer;
                    chasePlayer();
                }

                if (aiState == AIState.attack)
                {
                    attackTimer -= Time.deltaTime;
                }

                if (aiState == AIState.idle)
                {
                    idleTimer += delta;
                }
            }
        }

        void idle()
        {
            anim.SetBool("IsIdle", true);
            anim.SetBool("IsRunning", false);
            anim.SetBool("IsWalking", false);
            timer = wanderTimer - 2.0f;
            wander();
        }

        void death()
        {
            anim.SetBool("IsIdle", true);
            anim.SetBool("IsRunning", false);
            anim.SetBool("IsWalking", false);
            anim.SetTrigger("IsDead");
            isDead = true;
            capsuleCollider.isTrigger = true;
            enemyAudio.clip = deathClip;
            enemyAudio.Play();

            soulScript.AddSouls(10);
            enemyManager.currentEnemies--;
        }

        void wander()
        {
            agent.speed = wanderSpeed;
            if (timer >= wanderTimer)
            {
                anim.SetBool("IsIdle", false);
                anim.SetBool("IsRunning", false);
                anim.SetBool("IsWalking", true);
                timer = 0f;
                Vector3 random_direction = RandomNavMeshLocation(random_radius);
                agent.destination = random_direction;
            }

            else if (!agent.pathPending)
            {
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                    {
                        anim.SetBool("IsIdle", true);
                        anim.SetBool("IsRunning", false);
                        anim.SetBool("IsWalking", false);
                    }
                }
            }
        }

        void chasePlayer()
        {
            anim.SetBool("IsIdle", false);
            anim.SetBool("IsWalking", false);
            anim.SetBool("IsRunning", true);
            agent.speed = chaseSpeed;
            agent.destination = player.position;
        }
    
        void attack()
        {
            anim.SetBool("IsIdle", false);
            anim.SetBool("IsWalking", true);
            anim.SetBool("IsRunning", false);
            anim.SetBool("can_move", false);
            anim.Play("Enemy_Attack");
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
                anim.Play("ted_react");
                idle();
            }
            
        }

        public void StartSinking()
        {
            // Find and disable the Nav Mesh Agent.
            GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;

            // Find the rigidbody component and make it kinematic (since we use Translate to sink the enemy).
            GetComponent<Rigidbody>().isKinematic = true;

            // The enemy should no sink.
            isSinking = true;

            // Increase the score by the enemy's score value.
            //ScoreManager.score += scoreValue;

            // After 2 seconds destroy the enemy.
            Destroy(gameObject, 2f);
        }

        IEnumerator CloseAnimator()
        {
            yield return new WaitForEndOfFrame();
            anim.enabled = false;
            this.enabled = false;
        }

        private void dealDamage()
        {
            if (ableToDealDamage)
            {
                ableToDealDamage = false;
                playerHealth.TakeDamage(15);
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
           if(other.gameObject.tag == "Player")
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

        public Vector3 RandomNavMeshLocation(float radius)
        {
            Vector3 randomDirection = player.position + Random.insideUnitSphere * radius;
            NavMeshHit hit;
            NavMesh.SamplePosition(randomDirection, out hit, radius, -1);
            return hit.position;
        }
    }
}
