using System.Collections;
using UnityEngine;

namespace GameFeel
{
    public class FlashEffect : MonoBehaviour
    {
        private static readonly int Flash1 = Shader.PropertyToID("_Flash");
        [SerializeField] private SpriteRenderer sprRenderer;

        public void Flash()
        {
            StartCoroutine(FlashRoutine());
        }

        private IEnumerator FlashRoutine()
        {
            sprRenderer.material.SetInt(Flash1, 1);
            yield return new WaitForSeconds(1);
            sprRenderer.material.SetInt(Flash1, 0);
        }
    }
}