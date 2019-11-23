using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PC
{
    public class HealthBar : MonoBehaviour
    {

        [SerializeField]
        private Image foregroundImage;
        [SerializeField]
        private float updateSpeedSeconds = 0.5f;

        private Canvas healthBarCanvas;

        private void Awake()
        {
            GetComponentInParent<EnemyStates>().OnHealthPctChanged += HandleHealthChanged;
            healthBarCanvas = GetComponentInParent<Canvas>();
            healthBarCanvas.enabled = false;
        }

        public void HandleHealthChanged(float pct)
        {
            healthBarCanvas.enabled = true;
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
            if(pct <= 0)
            {
                healthBarCanvas.enabled = false;
            }
        }

        // Update is called once per frame
        void LateUpdate()
        {
            transform.LookAt(Camera.main.transform);
            transform.Rotate(0, 180, 0);
        }
    }
}
