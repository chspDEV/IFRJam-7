using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MoveOnHover : MonoBehaviour
    {
        public Vector3 hoverOffset = new Vector3(50f, 0f, 0f); 
        public float transitionSpeed = 5f;
        public float normalAlpha = 0.1f; 
        public float hoverAlpha = 1f; 
        public TextMeshProUGUI textMesh;

        private Vector3 originalPosition;
        private CanvasGroup canvasGroup;

        void Start()
        {
            originalPosition = transform.localPosition;
            canvasGroup = GetComponent<CanvasGroup>();

            if (canvasGroup == null)
            {
                canvasGroup = gameObject.AddComponent<CanvasGroup>();
            }
            
            textMesh.enabled = false;
            canvasGroup.alpha = normalAlpha;
        }

        public void MouseEnter()
        {
            StopAllCoroutines();
            textMesh.enabled = true;
            StartCoroutine(MoveAndFade(originalPosition + hoverOffset, hoverAlpha));
        }

        public void MouseExit()
        {
            StopAllCoroutines();
            textMesh.enabled = false;
            StartCoroutine(MoveAndFade(originalPosition, normalAlpha));
        }

        private System.Collections.IEnumerator MoveAndFade(Vector3 targetPosition, float targetAlpha)
        {
            while (Vector3.Distance(transform.localPosition, targetPosition) > 0.1f || Mathf.Abs(canvasGroup.alpha - targetAlpha) > 0.01f)
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, Time.deltaTime * transitionSpeed);
                canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, targetAlpha, Time.deltaTime * transitionSpeed);
                yield return null;
            }

            transform.localPosition = targetPosition;
            canvasGroup.alpha = targetAlpha;
        }
    }
}