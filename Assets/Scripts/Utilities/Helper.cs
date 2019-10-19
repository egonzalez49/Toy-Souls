using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PC
{
    public class Helper : MonoBehaviour
    {
        [Range(-1, 1)]
        public float vertical;
        [Range(-1, 1)]
        public float horizontal;

        public string[] oh_attacks;
        public string[] th_attacks;
        public bool playAnim;

        public bool twoHanded;
        public bool enableRootMotion;
        public bool useItem;
        public bool interacting;
        public bool lock_on;

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

            interacting = anim.GetBool("interacting");

            if (!lock_on)
            {
                horizontal = 0;
                vertical = Mathf.Clamp01(vertical);
            }

            anim.SetBool("lock_on", lock_on);

            if (enableRootMotion)
            {
                return;
            }

            if (useItem)
            {
                anim.Play("use_item");
                useItem = false;
            }

            if (interacting)
            {
                playAnim = false;
                vertical = Mathf.Clamp(vertical, 0, 0.5f);
            }

            anim.SetBool("two_handed", twoHanded);
            if (playAnim)
            {
                string targetAnim;
                string[] attacks = (twoHanded) ? th_attacks : oh_attacks;
                int r = Random.Range(0, attacks.Length);
                targetAnim = attacks[r];
                if (vertical > 0.5f )
                {
                    targetAnim = "oh_attack_3";
                }
                vertical = 0;
                anim.CrossFade(targetAnim, 0.2f);
                //anim.SetBool("can_move", false);
                //enableRootMotion = true;
                playAnim = false;
            }
            anim.SetFloat("vertical", vertical);
            anim.SetFloat("horizontal", horizontal);

            

        }
    }
}
