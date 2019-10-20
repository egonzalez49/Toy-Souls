using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PC
{
    public class EnemyTarget : MonoBehaviour
    {
        public List<Transform> targets = new List<Transform>();
        public List<HumanBodyBones> humanoidBones = new List<HumanBodyBones>();
        public int index;
        Animator anim;

        public EnemyStates enemyStates;

        public void Init(EnemyStates es)
        {
            enemyStates = es;
            anim = es.anim;
            if (anim.isHuman == false)
                return;

            for (int i = 0; i < humanoidBones.Count; i++)
            {
                targets.Add(anim.GetBoneTransform(humanoidBones[i]));
            }
        }

        public Transform GetTarget(bool negative = false)
        {
            if (targets.Count == 0)
                return transform;

            int targetIndex = index;

            if (negative == false)
            {
                if (index < targets.Count - 1)
                    index++;
                else
                    index = 0;
            }
            else
            {
                if (index <= 0)
                    index = targets.Count - 1;
                else
                    index--;
            }
            index = Mathf.Clamp(index, 0, targets.Count);
            return targets[index];

        }

    }
}

