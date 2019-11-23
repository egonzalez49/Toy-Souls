using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PC
{
    public class AnimatorHook : MonoBehaviour
    {
        Animator anim;
        StateManager states;
        EnemyStates eStates;
        Rigidbody rigid;


        public float rm_multi;
        bool rolling;
        float roll_t;
        float delta;
        AnimationCurve roll_curve;
        public AudioClip[] clips = new AudioClip[3];
        private AudioSource audioSource;

        private void Awake()
        {
            audioSource = GetComponentInParent<AudioSource>();
        }

        public void PlaySwingSound()
        {
            PlayRandomSoundEffect(clips, 0.5f);
        }

        // Input an array of audio clips and volume to randomly select and play a clip.
        private void PlayRandomSoundEffect(AudioClip[] audioClips, float volume)
        {
            int randomValue = Random.Range(0, audioClips.Length - 1);
            audioSource.PlayOneShot(audioClips[randomValue], volume);
        }

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

        public void Init(StateManager s, EnemyStates es)
        {
            states = s;
            eStates = es;
            if(s != null)
            {
                anim = s.anim;
                rigid = s.rigid;
                roll_curve = states.roll_curve;
                delta = states.delta;
            }
                
            if (es != null)
            {
                anim = es.anim;
                rigid = es.rigid;
                delta = es.delta;
            }
                
        }

        /**
         * Rectifies root motion offsetting of character.
         */
        void OnAnimatorMove()
        {
            if (states == null && eStates == null)
                return;

            if(rigid == null)
                return;

            if (states != null)
            {
                if (states.canMove)
                    return;
                delta = states.delta;
            }

            if (eStates != null)
            {
                if (eStates.canMove)
                    return;
                delta = eStates.delta;
            }

            rigid.drag = 0;

            if (rm_multi <= 0)
                rm_multi = 1;

            if (rolling == false)
            {
                Vector3 del = anim.deltaPosition;
                del.y = 0;
                Vector3 v = (del * rm_multi) / delta;
                if (float.IsNaN(v.x))
                    v.x = 0.0f;
                if (float.IsNaN(v.y))
                    v.y = 0.0f;
                if (float.IsNaN(v.z))
                    v.z = 0.0f;
                rigid.velocity = v;
            }
            else
            {
                // Sample curve, create new vector with sample z-val, generate relative velocity.
                roll_t += delta / 0.60f;
                if (roll_t > 1)
                {
                    roll_t = 1;
                }

                if (states == null)
                    return;

                float z_val = roll_curve.Evaluate(roll_t);
                Vector3 v1 = Vector3.forward * z_val;
                Vector3 relative = transform.TransformDirection(v1);
                Vector3 v2 = (relative * rm_multi); // / states.delta;
                rigid.velocity = v2;
            }
        }

        public void OpenDamageColliders()
        {
            if (states == null)
                return;
            states.inventoryManager.currentWeapon.w_hook.OpenDamageColliders();
        }

        public void CloseDamageColliders()
        {
            if (states == null)
                return;
            states.inventoryManager.currentWeapon.w_hook.CloseDamageColliders();
        }
    }
}

