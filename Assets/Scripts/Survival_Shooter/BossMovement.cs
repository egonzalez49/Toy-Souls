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
        private float attackTimer = 1.0f;
        private bool isPlayerColliding;
        public bool isAttacking;
        public float _actionDelay;
        public float attackDuration;
        public float attackDistance = 2.0f;
        public float dot;
        public PlayerHealth playerHealth;

        public enum AIState
        {
            chasePlayer,
            attack,
        };

        public AIState aiState;
        public float dist;
        

        private void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            agent = GetComponent<NavMeshAgent>();
            anim = GetComponent<Animator>();
            playerVelocityReporter = player.GetComponent<VelocityReporter>();
            aiState = AIState.chasePlayer;
            dist = Vector3.Distance(player.position, transform.position);
            UpdateAnimClipTimes();
        }

        // Update is called once per frame
        void Update()
        {
            dist = Vector3.Distance(player.position, transform.position);
            if (dist <= attackDistance && (!isAttacking || _actionDelay >= attackDuration))
            {
                anim.applyRootMotion = false;
                _actionDelay = 0f;
                attackTimer = 1f;
                aiState = AIState.attack;
                attack();
            }
            if (dist > attackDistance && (!isAttacking || _actionDelay >= attackDuration))
            {
                anim.applyRootMotion = false;
                aiState = AIState.chasePlayer;
                chasePlayer();
            }
            if (isAttacking)
            {
                _actionDelay += Time.deltaTime;
                attackTimer -= Time.deltaTime;
                transform.LookAt(player.position);
            }
        }

        void chasePlayer()
        {
            anim.SetBool("IsAttacking", false);
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
            agent.destination = agent.transform.position;
            isAttacking = true;
            anim.applyRootMotion = true;
            anim.SetBool("IsAttacking", true);
        }

        private void OnTriggerEnter(Collider other)
        {
            attackTimer = 1.5f;
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.tag == "Player" && attackTimer <= 0.1f && attackTimer >= -0.55f)
            {
                playerHealth.TakeDamage(15);
                attackTimer = 1.5f;
                //Debug.Log("Damage Done to Player");
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

        public void UpdateAnimClipTimes()
        {
            AnimationClip[] clips = anim.runtimeAnimatorController.animationClips;
            foreach (AnimationClip clip in clips)
            {
                if (clip.name == "Luigi_Kick")
                {
                    attackDuration = clip.length;
                }
            }
        }
    }
}

