﻿using System.Collections;
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
        public float attackDistance = 1.5f;
        public float chaseSpeed = 2.0f;
        public float wanderSpeed = 1.0f;
        public float attackingSpeed = 0.2f;
        public float timer;


        private void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            agent = GetComponent<NavMeshAgent>();
            anim = GetComponent<Animator>();
            aiState = AIState.wander;
            dist = Vector3.Distance(player.position, transform.position);
            agent.destination = RandomNavMeshLocation(random_radius);
        }

        // Update is called once per frame
        void Update()
        {
            timer += Time.deltaTime;
            dist = Vector3.Distance(player.position, transform.position);
            if (dist >= chaseDistance && aiState != AIState.attack)
            {
                aiState = AIState.wander;
                wander();
            }
            else if (dist < chaseDistance && aiState != AIState.attack)
            {
                aiState = AIState.chasePlayer;
                chasePlayer();
            }
            else if (dist < attackDistance && aiState != AIState.attack)
            {
                aiState = AIState.attack;
                attack();
            }
            if (aiState == AIState.attack)
            {
                attackTimer -= Time.deltaTime;
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
            agent.speed = attackingSpeed;
            anim.SetTrigger("Attack");
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.tag == "Player" && aiState == AIState.attack)
            {
                isPlayerColliding = true;
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.tag == "Player" && isPlayerColliding == true)
            {
                if (attackTimer <= 0.0f)
                {
                    Debug.Log("Damage Done to Player");
                    aiState = AIState.chasePlayer;
                    isPlayerColliding = false;
                    attackTimer = 1.0f;
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            attackTimer = 1.0f;
            isPlayerColliding = false;
            aiState = AIState.chasePlayer;
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