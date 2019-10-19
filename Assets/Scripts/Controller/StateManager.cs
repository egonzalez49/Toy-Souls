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
        public bool rt, rb, lt, lb, b, a, y, x, lsb, rsb;
        public bool rollInput;

        [Header("Stats")]
        public float moveSpeed;
        public float runSpeed;
        public float rotateSpeed;
        public float toGround = 0.5f;
        public float rollSpeed = 1;

        [Header("States")]
        public bool run;
        public bool onGround;
        public bool lockon;
        public bool inAttack;
        public bool canMove;
        public bool twoHanded = false;
        

        [Header("Other")]
        public EnemyTarget lockonTarget;
        public Transform lockonTransform;
        public AnimationCurve roll_curve;

        [HideInInspector]
        public Animator anim;
        [HideInInspector]
        public Rigidbody rigid;
        [HideInInspector]
        public AnimatorHook a_hook;
        [HideInInspector]
        public float delta;
        [HideInInspector]
        public LayerMask ignoreLayers;

        float _actionDelay;
       
        public void Init()
        {
            moveSpeed = 3.5f;
            runSpeed = 5.75f;
            rotateSpeed = 9;
            toGround = 0.5f;

            SetupAnimator();
            rigid = GetComponent<Rigidbody>();
            rigid.angularDrag = 999;
            rigid.drag = 4;
            rigid.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

            a_hook = activeModel.AddComponent<AnimatorHook>();
            a_hook.Init(this);

            gameObject.layer = 8;
            ignoreLayers = ~(1 << 9);

            anim.SetBool("onGround", true);
            anim.SetBool("run", false);
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
            DetectAction();

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

            //a_hook.rm_multi = 1;
            a_hook.CloseRoll();
            HandleRolls();
            
            // If we're not in an attack animation...
            anim.applyRootMotion = false;
            rigid.drag = (moveAmount > 0 || onGround==false ) ? 0 : 4;

            float targetSpeed = moveSpeed;
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

            anim.SetBool("lock_on", lockon);

            if (lockon == false)
                HandleMovementAnimations();
            else
                HandleLockOnAnimations(moveDirection);
        }
        /**
         * Detects to see if as a result of inputs, the player character 
         * is in an attack animation state.
         */
        public void DetectAction()
        {
            if (canMove == false)
                return;

            if (!rb && !rt)
                return;
            string targetAnim = null;
            if (twoHanded)
            {
                if (rb)
                {
                    targetAnim = "th_attack_1";
                }
                if (rt)
                {
                    targetAnim = "th_attack_2";
                }
            }
            else
            {
                if (rb)
                {
                    targetAnim = "oh_attack_1";
                }
                if (rt)
                {
                    targetAnim = "oh_attack_2";
                }
                if (run)
                {
                    targetAnim = "oh_attack_3";
                }
            }

            if (string.IsNullOrEmpty(targetAnim))
                return;

            canMove = false;
            inAttack = true;
            anim.CrossFade(targetAnim, 0.2f);
        }

        public void HandleTwoHanded()
        {
            anim.SetBool("two_handed", twoHanded);
        }

        void HandleRolls()
        {
            if (!rollInput)
                return;
            float v = vertical;
            float h = horizontal;
            v = (moveAmount > 0.4f) ? 1 : 0;
            h = 0;

            /*if (!lockon)
            {
                v = (moveAmount > 0.4f) ? 1 : 0;
                h = 0;
            }
            else
            {
                if(Mathf.Abs(v) < 0.4f)
                    v = 0;
                if(Mathf.Abs(h) < 0.4f)
                    h = 0;
            }*/
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