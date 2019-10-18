using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OOB
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

        [Header("Stats")]
        public float moveSpeed;
        public float runSpeed;
        public float rotateSpeed;

        [Header("States")]
        public bool run;

        [HideInInspector]
        public Animator anim;
        [HideInInspector]
        public Rigidbody rigid;
        [HideInInspector]
        public float delta;
       
        public void Init()
        {
            moveSpeed = 3;
            runSpeed = 4.0f;
            rotateSpeed = 5;
            SetupAnimator();
            rigid = GetComponent<Rigidbody>();
            rigid.angularDrag = 999;
            rigid.drag = 4;
            rigid.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }

        

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

        public void FixedTick(float d)
        {
            delta = d;

            rigid.drag = (moveAmount > 0) ? 0 : 4;

            float targetSpeed = moveSpeed;
            if (run)
            {
                targetSpeed = runSpeed;
            }

            rigid.velocity = moveDirection * (targetSpeed * moveAmount);

            Vector3 targetDirection = moveDirection;
            targetDirection.y = 0;
            if (targetDirection == Vector3.zero)
                targetDirection = transform.forward;
            Quaternion tempTR = Quaternion.LookRotation(targetDirection);
            Quaternion targetRotation = Quaternion.Slerp(transform.rotation, tempTR, delta * moveAmount * rotateSpeed);
            transform.rotation = targetRotation;

            HandleMovementAnimations();
        }

        void HandleMovementAnimations()
        {
            anim.SetFloat("vertical", moveAmount, 0.4f, delta);

        }
    }
}