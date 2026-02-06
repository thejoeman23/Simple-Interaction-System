using System.Collections;
using UnityEngine;

namespace SimpleInteractionSystem2DSample
{
    public class ShakeItem : MonoBehaviour
    {
        [Header("Shake Duration (seconds)")]
        public float shakeDuration = 0.5f;
        [Header("Shake Strength (degrees)")]
        public float shakeStrength = 10f;
        [Header("Shake Speed (oscillations/second)")]
        public float shakeSpeed = 10f;
        [Header("Scale Amount (e.g. 1.2 = 20% larger at peak)")]
        public float scaleAmount = 1.2f;

        // Call this to shake the transform
        public void Shake()
        {
            StartCoroutine(ShakeCoroutine());
        }

        IEnumerator ShakeCoroutine()
        {
            float elapsed = 0f;
            Quaternion originalRotation = transform.localRotation;
            Vector3 originalScale = transform.localScale;

            while (elapsed < shakeDuration)
            {
                elapsed += Time.deltaTime;
                float damper = 1f - Mathf.Clamp01(elapsed / shakeDuration); // fade out

                // Shake rotation
                float shakeOffset = Mathf.Sin(elapsed * shakeSpeed * Mathf.PI * 2) * shakeStrength * damper;
                Vector3 euler = originalRotation.eulerAngles;
                euler.z += shakeOffset;
                transform.localRotation = Quaternion.Euler(euler);

                // Scale up and down
                float scaleFactor = Mathf.Lerp(1f, scaleAmount, Mathf.Abs(Mathf.Sin(elapsed * shakeSpeed * Mathf.PI * 2))) * damper + 1f - damper;
                transform.localScale = originalScale * scaleFactor;

                yield return null;
            }

            // Reset rotation and scale
            transform.localRotation = originalRotation;
            transform.localScale = originalScale;
        }
    }
}