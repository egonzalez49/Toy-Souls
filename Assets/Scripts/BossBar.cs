using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PC
{
    public class BossBar: MonoBehaviour
    {

        [SerializeField]
        private Image foregroundImage;
        [SerializeField]
        private float updateSpeedSeconds = 0.5f;
        public GameObject boss;
        private Canvas healthBarCanvas;

        private void Awake()
        {
            
        }

        public void HandleHealthChanged(float pct)
        {
            StartCoroutine(ChangeToPct(pct));
        }

        private IEnumerator ChangeToPct(float pct)
        {
            float preChangePct = foregroundImage.fillAmount;
            float elapsed = 0f;
            while (elapsed < updateSpeedSeconds)
            {
                elapsed += Time.deltaTime;
                foregroundImage.fillAmount = Mathf.Lerp(preChangePct, pct, elapsed / updateSpeedSeconds);
                yield return null;
            }
            foregroundImage.fillAmount = pct;
        }

        // Update is called once per frame
        void LateUpdate()
        {
            if(boss == null)
            {
                GameObject boss = GameObject.FindWithTag("Boss");
                boss.GetComponent<Enemy.BossMovement>().OnHealthPctChanged += HandleHealthChanged;
                healthBarCanvas = GetComponentInParent<Canvas>();
            }
            transform.LookAt(Camera.main.transform);
            transform.Rotate(0, 180, 0);
        }
    }
}
