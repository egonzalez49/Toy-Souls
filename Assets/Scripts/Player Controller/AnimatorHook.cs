using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PC
{
    public class AnimatorHook : MonoBehaviour
    {
        Animator anim;
        StateManager states;

        public float rm_multi;
        bool rolling;
        float roll_t;

        public void InitForRoll()
        {
            rolling = true;
            roll_t = 0;
        }

        public void CloseRoll()
        {
            if (rolling == false)
                return;
            rm_multi = 1;
            roll_t = 0;
            rolling = false;
        }

        public void Init(StateManager s)
        {
            states = s;
            anim = states.anim;
        }

        /**
         * Rectifies root motion offsetting of character.
         */
        void OnAnimatorMove()
        {
            if (states.canMove)
                return;

            states.rigid.drag = 0;

            if (rm_multi <= 0)
                rm_multi = 1;

            if (rolling == false)
            { 
                Vector3 del = anim.deltaPosition;
                del.y = 0;
                Vector3 v = (del * rm_multi) / states.delta;
                states.rigid.velocity = v;
            }
            else
            {
                // Sample curve, create new vector with sample z-val, generate relative velocity.
                roll_t += states.delta / 0.60f;
                if (roll_t > 1)
                {
                    roll_t = 1;
                }
                float z_val = states.roll_curve.Evaluate(roll_t);
                Vector3 v1 = Vector3.forward * z_val;
                Vector3 relative = transform.TransformDirection(v1);
                Vector3 v2 = (relative * rm_multi); // / states.delta;
                states.rigid.velocity = v2;
            }
        }

        public void OpenDamageColliders()
        {
            states.inventoryManager.currentWeapon.w_hook.OpenDamageColliders();
        }

        public void CloseDamageColliders()
        {
            states.inventoryManager.currentWeapon.w_hook.CloseDamageColliders();
        }
    }
}

