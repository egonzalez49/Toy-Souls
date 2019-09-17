using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    public class Helper : MonoBehaviour
    {
        [Range(0, 1)]
        public float vertical;

        public string[] oh_attacks;
        public string[] th_attacks;
        public bool playAnim;

        public bool twoHanded;
        public bool enableRootMotion;

        Animator anim;

        // Start is called before the first frame update
        void Start()
        {
            anim = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            enableRootMotion = !anim.GetBool("can_move");
            anim.applyRootMotion = enableRootMotion;

            if (enableRootMotion)
            {
                return;
            }

            anim.SetBool("two_handed", twoHanded);
            if (playAnim)
            {
                string targetAnim;
                string[] attacks = (twoHanded) ? th_attacks : oh_attacks;
                int r = Random.Range(0, attacks.Length);
                targetAnim = attacks[r];
                vertical = 0;
                anim.CrossFade(targetAnim, 0.2f);
                playAnim = false;
            }
            anim.SetFloat("vertical", vertical);
            
        }
    }
}
