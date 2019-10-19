using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class PlayerMovement : MonoBehaviour
    {
        public float speed = 6f;

        Vector3 movement;
        Animator anim;
        Rigidbody playerRigidbody;
        int floorMask;
        float camRayLength = 100f;

        // Awake is similar to start, but is executed regardless if the script is enabled or not (good for references)
        private void Awake()
        {
            floorMask = LayerMask.GetMask("Floor");
            anim = GetComponent<Animator>();
            playerRigidbody = GetComponent<Rigidbody>();
        }

        // fires every physics update -- since we're moving a physics character, FixedUpdate is better to call here
        private void FixedUpdate()
        {
            float h = Input.GetAxisRaw("Horizontal"); // only has -1, 0, 1 as values (so only goes in full speed, no slow acceleration) see edit -> project settings -> input
            float v = Input.GetAxisRaw("Vertical");
            Move(h, v);
            Turning();
            Animating(h, v);
        }

        void Move(float h, float v)
        {
            movement.Set(h, 0f, v);
            movement = movement.normalized * speed * Time.deltaTime; // we normalize so there isnt an advantage to diagonal movement compared to raw x or z movement
                                                                     // additionally, we want to move at speed and not at one, but we dont want to give that kind of movement every fixed update, so we multiply by the time between frames.

            playerRigidbody.MovePosition(transform.position + movement);
        }

        void Turning()
        {
            Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit floorHit;
            if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
            {
                Vector3 playerToMouse = floorHit.point - transform.position;
                playerToMouse.y = 0f;

                Quaternion newRotation = Quaternion.LookRotation(playerToMouse); // we need a Quaternion to store rotation (Vector3 is not enough)
                playerRigidbody.MoveRotation(newRotation);
            }
        }

        void Animating(float h, float v)
        {
            bool walking = h != 0f || v != 0f;
            anim.SetBool("IsWalking", walking);
        }
    }
}