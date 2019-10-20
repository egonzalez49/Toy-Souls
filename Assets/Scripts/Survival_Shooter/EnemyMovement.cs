using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyMovement : MonoBehaviour
    {
        Transform player;
        NavMeshAgent agent;
        Animator anim;
        private float attackTimer = 1.0f;
        private bool isPlayerColliding;
        public bool isAttacking;
        public float _actionDelay;
        public float attackDuration;


        public enum AIState
        {
            wander,
            chasePlayer,
            attack,
        };

        public AIState aiState;
        public float dist;
        public float random_radius = 45.0f;
        public float wanderTimer = 28.0f;
        public float chaseDistance = 7.0f;
        public float attackDistance = 1.5f;
        public float chaseSpeed = 2.0f;
        public float wanderSpeed = 1.0f;
        public float timer;


        private void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            agent = GetComponent<NavMeshAgent>();
            anim = GetComponent<Animator>();
            aiState = AIState.wander;
            dist = Vector3.Distance(player.position, transform.position);
            agent.destination = RandomNavMeshLocation(random_radius);
            UpdateAnimClipTimes();
        }

        // Update is called once per frame
        void Update()
        {
            timer += Time.deltaTime;
            dist = Vector3.Distance(player.position, transform.position);
            if (dist >= chaseDistance && (!isAttacking || _actionDelay >= attackDuration))
            {
                aiState = AIState.wander;
                anim.applyRootMotion = false;
                wander();
            }
            if (dist <= attackDistance && (!isAttacking || _actionDelay >= attackDuration))
            {
                anim.applyRootMotion = false;
                _actionDelay = 0f;
                attackTimer = 1f;
                aiState = AIState.attack;
                attack();
            }
            if (dist <= chaseDistance && dist >= attackDistance && (!isAttacking || _actionDelay >= attackDuration))
            {
                aiState = AIState.chasePlayer;
                anim.applyRootMotion = false;
                chasePlayer();
                timer = wanderTimer;
            }
            if (isAttacking)
            {
                _actionDelay += Time.deltaTime;
                attackTimer -= Time.deltaTime;
                transform.LookAt(player.position);
            }
        }

        void wander()
        {
            agent.speed = wanderSpeed;
            if (timer > wanderTimer)
            {
                anim.SetBool("IsIdle", false);
                anim.SetBool("IsRunning", false);
                anim.SetBool("IsWalking", true);
                anim.SetBool("Attack", false);
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
                        anim.SetBool("Attack", false);
                    }
                } 
            }
        }

        void chasePlayer()
        {
            anim.SetBool("IsIdle", false);
            anim.SetBool("IsWalking", false);
            anim.SetBool("IsRunning", true);
            anim.SetBool("Attack", false);
            agent.speed = chaseSpeed;
            agent.destination = player.position;
        }

        void attack()
        {
            agent.destination = agent.transform.position;
            isAttacking = true;
            anim.applyRootMotion = true;
            anim.SetBool("IsIdle", false);
            anim.SetBool("IsWalking", false);
            anim.SetBool("IsRunning", false);
            anim.SetBool("Attack", true);
        }

        /*
        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.tag == "Player" && aiState == AIState.attack)
            {
                isPlayerColliding = true;
            }
        }
        */

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.tag == "Player" && attackTimer <= 0.15f && attackTimer >= -0.6f)
            {
                Debug.Log("Damage Done to Player");
            }
        }

        /*
        private void OnTriggerExit(Collider other)
        {
            if (_actionDelay > attackDuration)
            {
                attackTimer = 1.0f;
                isPlayerColliding = false;
                aiState = AIState.chasePlayer;
                agent.destination = player.position;
                anim.applyRootMotion = false;
                isAttacking = false;
            }
        }
        */

        public Vector3 RandomNavMeshLocation(float radius)
        {
            Vector3 randomDirection = player.position + Random.insideUnitSphere * radius;
            NavMeshHit hit;
            NavMesh.SamplePosition(randomDirection, out hit, radius, -1);
            return hit.position;
        }

        public void UpdateAnimClipTimes()
        {
            AnimationClip[] clips = anim.runtimeAnimatorController.animationClips;
            foreach(AnimationClip clip in clips)
            {
                if (clip.name == "Ted_Attack")
                {
                    attackDuration = clip.length;
                }
            }
        }
    }
}