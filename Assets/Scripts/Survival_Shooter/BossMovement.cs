using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

namespace Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class BossMovement : MonoBehaviour
    {
        Transform player;
        GameObject playerObject;
        NavMeshAgent agent;
        Animator anim;
        SceneData InfoSaver;
        private VelocityReporter playerVelocityReporter;
        public GameObject leftLegCollider;
        public GameObject rightLegCollider;
        public GameObject leftArmCollider;
        public GameObject rightArmCollider;
        public GameObject forceAttackCollider;
        public bool isInvincible;
        public bool canMove;
        public bool isDead;
        public float attackDistance = 2.0f;
        public float dot;
        public PlayerHealth playerHealth;
        public float health;
        public event Action<float> OnHealthPctChanged = delegate { };
        private float maxHealth = 1000;
        public bool ableToDealDamage = true;
        public AudioClip hitClip;
        public AudioClip deathClip;
        public AudioClip powerUp;
        public AudioClip powerDown;
        public AudioClip stomp;
        public AudioClip explosion;
        AudioSource enemyAudio;
        private Quaternion previousRotation;
        public EndGameManager endMenu;
        public float delta;
        private bool strongAttack = false;
        public int numAttacks;
        private int randomAnimSelector = 0;
        public GameObject[] floorArray;
        private int floorPointer = 0;
        public GameObject forceAttackRenderer;
        ParticleSystem hitParticles;
        private bool forceDamagePossible = false;

        public float dist_2;
        public float radius;
        private TransitionController transitionController;

        //private int num_moves_start = 1;
        //private int num_moves_end = 2;
        //private bool phase_two = false;

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
            forceAttackCollider.SetActive(false);
            player = GameObject.FindGameObjectWithTag("Player").transform;
            playerObject = GameObject.FindGameObjectWithTag("Player");
            InfoSaver = GameObject.FindGameObjectWithTag("InfoSaver").GetComponent<SceneData>();
            health = this.maxHealth;
            agent = GetComponent<NavMeshAgent>();
            anim = GetComponentInChildren<Animator>();
            playerVelocityReporter = player.GetComponent<VelocityReporter>();
            aiState = AIState.chasePlayer;
            dist = Vector3.Distance(player.position, transform.position);
            playerHealth = player.GetComponent<PlayerHealth>();
            enemyAudio = GetComponent<AudioSource>();
            agent.speed = InfoSaver.getAgentSpeed();
            hitParticles = GetComponentInChildren<ParticleSystem>();
            transitionController = GameObject.FindGameObjectWithTag("TransitionController").GetComponent<TransitionController>();
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
            if (health <= 0.8*this.maxHealth && canMove && !InfoSaver.getPhase_two())
            {
                anim.SetBool("IsIdle", true);
                anim.SetBool("can_move", false);
                anim.Play("Luigi_PhaseTwo");
                agent.isStopped = true;
                anim.applyRootMotion = true;
                aiState = AIState.idle;
                idle();
            }
            if (!canMove && !isDead && randomAnimSelector != 2)
            {
                Quaternion rotationAngle = Quaternion.LookRotation(player.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotationAngle, delta / InfoSaver.getSlepSpeed());
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
            dist_2 = Vector3.Distance(player.position, transform.position);
            if (forceDamagePossible && ableToDealDamage && (dist_2 <= 4.7))
            {
                playerHealth.TakeDamage(25, player.position);
                ableToDealDamage = false;
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
            randomAnimSelector = (int) UnityEngine.Random.Range(1, InfoSaver.getNumBossMovesEnd() + 1);
            anim.Play("Luigi_Attack" + randomAnimSelector);
            agent.isStopped = true;
            anim.applyRootMotion = true;
            aiState = AIState.idle;
            idle();
        }

        public void TakeDamage(float v, Vector3 hitPoint)
        {
            if (isInvincible || isDead)
                return;
            health -= v;
            float currentHealthPct = this.health / this.maxHealth;
            OnHealthPctChanged(currentHealthPct);
            hitParticles.transform.position = hitPoint;
            hitParticles.Play();
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

        public void OpenBossForceAttackCollider()
        {

            forceDamagePossible = true;
            ableToDealDamage = true;
        }

        public void CloseBossLeftLegDamageCollider()
        {
            leftLegCollider.SetActive(false);
        }

        public void CloseBossForceAttackCollider()
        {
            forceDamagePossible = false;
        }

        public void CloseBossRightLegDamageCollider()
        {
            rightLegCollider.SetActive(false);
        }

        public void GrowOneSize()
        {
            transform.localScale += new Vector3(0.65f, 0.65f, 0.65f);
        }

        public void playPowerUp()
        {
            enemyAudio.clip = powerUp;
            enemyAudio.Play();
        }

        public void playExplosion()
        {
            enemyAudio.clip = explosion;
            enemyAudio.Play();
        }

        public void ShrinkOneSize()
        {
            transform.localScale -= new Vector3(0.65f, 0.65f, 0.65f);
        }

        public void playPowerDown()
        {
            enemyAudio.clip = powerDown;
            enemyAudio.Play();
        }

        public void playStomp()
        {
            enemyAudio.clip = stomp;
            enemyAudio.Play();
        }

        public void begin_phase_two()
        {
            InfoSaver.SavePlayer();
            InfoSaver.setPhase_two(true);
            playerObject.GetComponent<CapsuleCollider>().enabled = false;
            playerObject.GetComponent<Rigidbody>().AddRelativeForce(Vector3.up * 50, ForceMode.Impulse);
            transitionController.fadeOut();
            InfoSaver.setNumBossMovesEnd(4);
            InfoSaver.setAgentSpeed(4.0f);
            InfoSaver.setSlepSpeed(1.5f);
            // commented out knockbacking
            //InfoSaver.setBossKnockbackBool(true);
            InfoSaver.setEnemyBossDamage(20);
            agent.speed = InfoSaver.getAgentSpeed();
            Invoke("load_final_battle", 2.5f);
        }

        public void load_final_battle()
        {
            SceneManager.LoadScene("Final_Battle");
        }

        public void destroyFloor()
        {
            if (floorPointer < floorArray.Length && floorArray[floorPointer] != null)
            {
                int i = 0;
                GameObject destructibleFloor = floorArray[floorPointer];
                //GameObject[] childrenObjects = new GameObject[4];
                foreach (Transform child in destructibleFloor.transform)
                {
                    //childrenObjects[i] = child.gameObject;
                    child.gameObject.GetComponent<FloorDestruction>().flashing = true;
                    ++i;
                }
                //StartCoroutine(flashCoroutine(childrenObjects, 0.2f));
                ++floorPointer;
            }
        }

        /*
        // should not be used anymore
        IEnumerator flashCoroutine(GameObject[] childrenObjects, float intervalTime)
        {
            float duration = 0.35f;
            bool stop = false;
            int index = 0;
            Color startColor = childrenObjects[0].GetComponent<MeshRenderer>().material.color;
            Color[] colorArray = { startColor, Color.red };
            while (!stop)
            {
                for (int i = 0; i < childrenObjects.Length; ++i)
                {
                    childrenObjects[i].GetComponent<MeshRenderer>().material.color = colorArray[index % 2];
                }
                duration -= Time.deltaTime;
                if (duration < 0)
                {
                    stop = true;
                    for (int i = 0; i < childrenObjects.Length; ++i)
                    {
                        childrenObjects[i].GetComponent<FloorDestruction>().destroy = true;
                    }
                    yield break;
                }
                ++index;
                yield return new WaitForSeconds(intervalTime);
            }
        }
        */

        IEnumerator flashCollider(float intervalTime)
        {   
            float duration = 0.3f;
            bool stop = false;
            int index = 0;

            forceAttackCollider.SetActive(true);
            forceAttackCollider.GetComponent<SphereCollider>().enabled = false;
            
            while (!stop)
            {
                if (index % 5 == 0)
                {
                    forceAttackRenderer.GetComponent<MeshRenderer>().enabled = true;
                }
                else
                {
                    forceAttackRenderer.GetComponent<MeshRenderer>().enabled = false;
                }
                duration -= Time.deltaTime;
                if (duration < 0)
                {
                    stop = true;
                    forceAttackCollider.GetComponent<SphereCollider>().enabled = true;
                    forceAttackRenderer.GetComponent<MeshRenderer>().enabled = false;
                    forceAttackCollider.SetActive(false);
                    yield break;
                }
                ++index;
                yield return new WaitForSeconds(intervalTime);
            }
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