﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PC
{
    public class AnimatorHook : MonoBehaviour
    {
        Animator anim;
        StateManager states;

        public float rm_multi;

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

            if(rm_multi <= 0)
                rm_multi = 1;

            Vector3 del = anim.deltaPosition;
            del.y = 0;
            Vector3 v = (del * rm_multi) / states.delta;
            states.rigid.velocity = v;
        }
    }
}

