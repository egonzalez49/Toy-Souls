using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PC
{
    public class StateManager : MonoBehaviour
    {
        [Header("Init")]
        public GameObject activeModel;

        [Header("Inputs")]
        public float vertical;
        public float horizontal;
        public float moveAmount;
        public Vector3 moveDirection;
        public bool rt, rb, lt, lb, b, a, y, x, lsb, rsb, startBtn;
        public bool rollInput;
        public bool itemInput;

        [Header("Stats")]
        public float moveSpeed = 4f;
        public float runSpeed = 5.75f;
        public float rotateSpeed = 9;
        public float toGround = 0.5f;
        public float rollSpeed = 1;

        [Header("States")]
        public bool run;
        public bool onGround;
        public bool lockon;
        public bool inAttack;
        public bool canMove;
        public bool twoHanded = false;
        public bool usingItem;
        public bool canRoll = false;
        

        [Header("Other")]
        public EnemyTarget lockonTarget;
        public Transform lockonTransform;
        public AnimationCurve roll_curve;
        //public AudioClip[] clips = new AudioClip[3];

        [HideInInspector]
        public Animator anim;
        [HideInInspector]
        public Rigidbody rigid;
        [HideInInspector]
        public AnimatorHook a_hook;
        [HideInInspector]
        public ActionManager actionManager;
        [HideInInspector]
        public InventoryManager inventoryManager;
        [HideInInspector]
        public float delta;
        [HideInInspector]
        public LayerMask ignoreLayers;

        //private AudioSource audioSource;

        float _actionDelay;
       
        public void Init()
        {
            SetupAnimator();
            rigid = GetComponent<Rigidbody>();
            rigid.angularDrag = 999;
            rigid.drag = 4;
            rigid.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

            inventoryManager = GetComponent<InventoryManager>();
            inventoryManager.Init();

            actionManager = GetComponent<ActionManager>();
            actionManager.Init(this);

            a_hook = activeModel.GetComponent<AnimatorHook>();
            if (a_hook == null)
                a_hook = activeModel.AddComponent<AnimatorHook>();
            a_hook.Init(this, null);

            gameObject.layer = 8;
            ignoreLayers = ~(1 << 9);

            anim.SetBool("onGround", true);
            anim.SetBool("run", false);

            //audioSource = GetComponent<AudioSource>();
        }

        
        /**
         * Setups the animator for the State Manager.  This depends on
         * whether an animator exists, either in the active model or in
         * a child of the current GameObject.
         */
        void SetupAnimator()
        {
            if (activeModel == null)
            {
                anim = GetComponentInChildren<Animator>();
                if (anim == null)
                {
                    Debug.Log("No model found");
                }
                else
                {
                    activeModel = anim.gameObject;
                }
            }
            if (anim == null)
                anim = activeModel.GetComponent<Animator>();
            anim.applyRootMotion = false;
        }

        /**
         * One tick of the physics for the State Manager.
         * Checks to see if PC should be in an attack animation.   If so, follows
         * through with that attack with root motion enabled.
         * If not, then proceeds to check to see if we should be moving.
         */
        public void FixedTick(float d)
        {
            delta = d;

            usingItem = anim.GetBool("interacting");
            DetectItemAction();
            DetectAction();
            inventoryManager.currentWeapon.weaponModel.SetActive(!usingItem);

            // If already in an attack animation...
            if (inAttack)
            {
                // Delays translation to prevent slipping.
                anim.applyRootMotion = true;
                _actionDelay += delta;
                if(_actionDelay > 0.21f)
                {
                    inAttack = false;
                    _actionDelay = 0;
                }
                else
                    return;
            }
            // Secondary check to see if we can now move again.
            canMove = anim.GetBool("can_move");
            if (!canMove)
                return;

                
            a_hook.CloseRoll();
            HandleRolls();
            
            // If we're not in an attack animation...
            // Determine player orientation and physics attributes.
            anim.applyRootMotion = false;
            rigid.drag = (moveAmount > 0 || onGround==false ) ? 0 : 4;

            float targetSpeed = moveSpeed;
            if (usingItem)
            {
                run = false;
                moveAmount = Mathf.Clamp(moveAmount, 0, 0.4f);
            }
            if (run)
            {
                targetSpeed = runSpeed;
                lockon = false;
            }



            if (onGround)
                rigid.velocity = moveDirection * (targetSpeed * moveAmount);
            Vector3 targetDirection = (lockon == false) ? moveDirection
                : (lockonTransform != null) ?
                    lockonTransform.transform.position - transform.position 
                    : moveDirection;
            targetDirection.y = 0;
            if (targetDirection == Vector3.zero)
                targetDirection = transform.forward;
            Quaternion tempTR = Quaternion.LookRotation(targetDirection);
            Quaternion targetRotation = Quaternion.Slerp(transform.rotation, tempTR, delta * moveAmount * rotateSpeed);
            transform.rotation = targetRotation;

            // Either handle lock-on translational motion or normal translational motion.
            anim.SetBool("lock_on", lockon);
            if (lockon == false)
                HandleMovementAnimations();
            else
                HandleLockOnAnimations(moveDirection);
        }
        
        public void DetectItemAction()
        {
            if (canMove == false || itemInput==false || usingItem)
                return;

            ItemAction slot = actionManager.consumableItem;
            string targetAnim = slot.targetAnim;
            if (string.IsNullOrEmpty(targetAnim))
                return;
            usingItem = true;
            anim.Play(targetAnim);
        }

        /**
         * Detects to see if as a result of inputs, the player character 
         * is in an attack animation state.
         */
        public void DetectAction()
        {
            // Check if we are in a state to be able to do an action.
            if (canMove == false || usingItem || (!rb && !rt && !lt && !lb))
                return;

            // Pull action animation from Action Manager
            string targetAnim = null;
            Action slot = actionManager.GetActionSlots(this);
            if (slot == null)
                return;
            targetAnim = slot.targetAnimation;
            if (string.IsNullOrEmpty(targetAnim))
                return;

            // Cross-fade into new animation
            //PlayRandomSoundEffect(clips, 0.5f);
            canMove = false;
            inAttack = true;
            anim.CrossFade(targetAnim, 0.2f);
        }

        //public void PlaySwingSound()
        //{
        //    PlayRandomSoundEffect(clips, 0.5f);
        //}

        //// Input an array of audio clips and volume to randomly select and play a clip.
        //private void PlayRandomSoundEffect(AudioClip[] audioClips, float volume)
        //{
        //    int randomValue = Random.Range(0, audioClips.Length - 1);
        //    audioSource.PlayOneShot(audioClips[randomValue], volume);
        //}

        public void HandleTwoHanded()
        {
            anim.SetBool("two_handed", twoHanded);
            if (twoHanded)
                actionManager.UpdateActionsTwoHanded();
            else
                actionManager.UpdateActionsOneHanded();
        }

        void HandleRolls()
        {
            if (!rollInput || usingItem || !canRoll)
                return;
            float v = vertical;
            float h = horizontal;
            v = (moveAmount > 0.4f) ? 1 : 0;
            h = 0;

            if (v != 0)
            {
                if (moveDirection == Vector3.zero)
                    moveDirection = transform.forward;
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                transform.rotation = targetRotation;
                a_hook.InitForRoll();
                a_hook.rm_multi = rollSpeed;
            }
            else
            {
                a_hook.rm_multi = Mathf.Max(rollSpeed/6, 1.5f);
            }

            anim.SetFloat("vertical", v);
            anim.SetFloat("horizontal", h);

            canMove = false;
            inAttack = true;
            anim.CrossFade("Rolls", 0.2f); 
        }

        public void Tick(float d)
        {
            delta = d;
            onGround = OnGround();
            anim.SetBool("onGround", onGround);
        }

        void HandleMovementAnimations()
        {
            anim.SetBool("run", run);
            anim.SetFloat("vertical", moveAmount, 0.4f, delta);
        }

        void HandleLockOnAnimations(Vector3 moveDir)
        {
            Vector3 relativeDir = transform.InverseTransformDirection(moveDir);
            float h = relativeDir.x;
            float v = relativeDir.z;
            anim.SetFloat("vertical", v, 0.2f, delta);
            anim.SetFloat("horizontal", h, 0.2f, delta);
        }

        public bool OnGround()
        {
            bool r = false;

            Vector3 origin = transform.position + (Vector3.up * toGround);
            Vector3 dir = -Vector3.up;
            float dis = toGround + 0.3f;
            RaycastHit hit;
            Debug.DrawRay(origin, dir * dis);
            if (Physics.Raycast(origin, dir, out hit, dis, ignoreLayers))
            {
                r = true;
                Vector3 targetPos = hit.point;
                transform.position = targetPos;
            }
            return r;
        }
    }
}