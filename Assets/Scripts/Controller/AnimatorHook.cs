using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PC
{
    public class AnimatorHook : MonoBehaviour
    {
        Animator anim;
        StateManager states;

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
            float multiplier = 1;

            Vector3 del = anim.deltaPosition;
            del.y = 0;
            Vector3 v = (del * multiplier) / states.delta;
            states.rigid.velocity = v;
        }
    }
}

