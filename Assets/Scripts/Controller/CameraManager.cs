﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PC
{
    public class CameraManager : MonoBehaviour
    {
        public bool lockon;
        public float followSpeed = 3;
        public float mouseSpeed = 2;
        public float controllerSpeed = 5;

        public Transform target;
        public Transform lockonTarget;

        [HideInInspector]
        public Transform pivot;
        [HideInInspector]
        public Transform camTrans;

        float turnSmoothing = .1f;
        public float minAngle = -35;
        public float maxAngle = 35;

        float smoothX;
        float smoothY;
        float smoothXvelocity;
        float smoothYvelocity;
        public float lookAngle;
        public float tiltAngle;

        public void Init(Transform t)
        {
            target = t;
            camTrans = Camera.main.transform;
            pivot = camTrans.parent;
        }

        public void Tick(float d)
        {
            float h = Input.GetAxis("Mouse X");
            float v = Input.GetAxis("Mouse Y");
            
            float c_h = Input.GetAxis("RightAxis X");
            float c_v = Input.GetAxis("RightAxis Y");

            float targetSpeed = mouseSpeed;

            if(c_h != 0 || c_v != 0)
            {
                h = c_h;
                v = c_v;
                targetSpeed = controllerSpeed;
            }

            FollowTarget(d);
            HandleRotations(d, v, h, targetSpeed);
        }


        void FollowTarget(float d)
        {
            float speed = d * this.followSpeed;
            Vector3 targetPosition = Vector3.Lerp(transform.position, target.position, speed);
            transform.position = targetPosition;
        }

        void HandleRotations(float d, float v, float h, float targetSpeed)
        {
            if(turnSmoothing > 0)
            {
                smoothX = Mathf.SmoothDamp(smoothX, h, ref smoothXvelocity, this.turnSmoothing);
                smoothY = Mathf.SmoothDamp(smoothY, v, ref smoothYvelocity, this.turnSmoothing);
            }
            else
            {
                smoothX = h;
                smoothY = v;
            }

            tiltAngle -= smoothY * targetSpeed;
            tiltAngle = Mathf.Clamp(tiltAngle, minAngle, maxAngle);
            pivot.localRotation = Quaternion.Euler(tiltAngle, 0, 0);
            lookAngle += smoothX * targetSpeed;

            if (lockon && lockonTarget != null)
            {
                Vector3 targetDir = lockonTarget.position - transform.position;
                targetDir.Normalize();
                //targetDir.y = 0;

                if(targetDir == Vector3.zero)
                    targetDir = transform.forward;
                Quaternion targetRotation = Quaternion.LookRotation(targetDir);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, d * 9);
                tiltAngle = transform.eulerAngles.z+12.5f;
                lookAngle = transform.eulerAngles.y;
                return;
            }

            
            transform.rotation = Quaternion.Euler(0, lookAngle, 0);

            
        }

        public static CameraManager singleton;
        void Awake()
        {
            singleton = this;
        }
    }
}
