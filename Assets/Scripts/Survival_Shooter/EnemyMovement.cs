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
        Rigidbody rb;
        public float timer;

        public enum AIState
        {
            wander,
            chasePlayer,
            attack,
        };

        public AIState aiState;
        private float dist;
        public float random_radius = 45.0f;
        public float wanderTimer = 28.0f;
        public float chaseDistance = 7.0f;
        public float attackDistance = 2.0f;
        public float attackThrust = 3.0f;
        public float chaseSpeed = 2.5f;
        public float wanderSpeed = 0.8f;
        public bool isHit;

        private void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            agent = GetComponent<NavMeshAgent>();
            anim = GetComponent<Animator>();
            rb = GetComponent<Rigidbody>();
            aiState = AIState.wander;
            dist = Vector3.Distance(player.position, transform.position);
            agent.destination = RandomNavMeshLocation(random_radius);
        }

        // Update is called once per frame
        void Update()
        {
            timer += Time.deltaTime;
            dist = Vector3.Distance(player.position, transform.position);

            if (dist >= chaseDistance)
            {
                aiState = AIState.wander;
                wander();
            }
            else if (dist < chaseDistance && aiState != AIState.attack)
            {
                aiState = AIState.chasePlayer;
                chasePlayer();
            }
            /*
            else if (dist < attackDistance)
            {
                aiState = AIState.attack;
                attack();
            }
            */
        }

        private void FixedUpdate()
        {
            if (isHit)
            {
                rb.AddForce(transform.forward * -1 * attackThrust);
            }
        }

        void wander()
        {
            agent.speed = wanderSpeed;
            if (timer > wanderTimer)
            {
                anim.SetBool("IsMoving", true);
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
                        anim.SetBool("IsMoving", false);
                    }
                } 
            }
        }

        void chasePlayer()
        {
            agent.speed = chaseSpeed;
            agent.destination = player.position;
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