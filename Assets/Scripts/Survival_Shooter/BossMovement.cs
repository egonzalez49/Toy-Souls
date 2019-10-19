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

        public enum AIState
        {
            chasePlayer,
            attack,
        };

        public AIState aiState;
        private float dist;
        public float attackDistance = 2.0f;
        public float chaseSpeed = 1.5f;

        private void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            agent = GetComponent<NavMeshAgent>();
            anim = GetComponent<Animator>();
            playerVelocityReporter = player.GetComponent<VelocityReporter>();
            aiState = AIState.chasePlayer;
            agent.speed = chaseSpeed;
            dist = Vector3.Distance(player.position, transform.position);
        }

        // Update is called once per frame
        void Update()
        {
            dist = Vector3.Distance(player.position, transform.position);
            chasePlayer();
            /*
            if (dist < attackDistance)
            {
                aiState = AIState.attack;
                attack();
            }
            */
        }

        void chasePlayer()
        {
            //SetDestinationMoving();
            agent.destination = player.position;
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

