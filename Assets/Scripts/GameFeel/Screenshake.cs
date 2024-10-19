using System.Collections;
using UnityEngine;

namespace GameFeel
{
    public class ScreenShake : MonoBehaviour
    {
        [Header("Configuration")]
        public Transform cameraTransform; 
        public float shakeDuration = 0.5f; 
        public float shakeMagnitude = 0.1f; 
        public float dampingSpeed = 1.0f; 

        private Vector3 initialPosition;

        private void Start()
        {
            if (cameraTransform == null)
            {
                if (Camera.main != null) cameraTransform = Camera.main.transform;
            }
            initialPosition = cameraTransform.localPosition;
        }

        public void TriggerShake()
        {
            StartCoroutine(Shake());
        }

        private IEnumerator Shake()
        {
            var elapsed = 0.0f;

            while (elapsed < shakeDuration)
            {
                var randomOffset = Random.insideUnitSphere * shakeMagnitude;
                cameraTransform.localPosition = initialPosition + randomOffset;

                elapsed += Time.deltaTime;
                yield return null; 
            }

            while (Vector3.Distance(cameraTransform.localPosition, initialPosition) > 0.01f)
            {
                cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, initialPosition, Time.deltaTime * dampingSpeed);
                yield return null;
            }

            cameraTransform.localPosition = initialPosition; 
        }
    }
}
