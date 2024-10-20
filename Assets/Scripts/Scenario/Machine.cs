using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scenario
{
    public class Machine : MonoBehaviour
    {
        public float timeToShoot;
        public float currentTime;
        public GameObject laser;
        public List<GameObject> laserPreview;
        public float timeToReturn = 4;
        
        private List<SpriteRenderer> sprLaserPreview;

        private void Start()
        {
            foreach (GameObject go in laserPreview)
            {
                sprLaserPreview.Add(go.GetComponent<SpriteRenderer>());
            }
        }

        void Update()
        {
            HandleShootLogic();
            HandleShowPreview();
        }

        private void HandleShootLogic()
        {
            if (currentTime > timeToShoot)
            {
                laser.SetActive(true);
                StartCoroutine(DeactiveBullet());
            }
            else 
            {
                currentTime += Time.deltaTime;
            }
        }

        private void HandleShowPreview()
        {

            foreach (SpriteRenderer spr in sprLaserPreview)
            {
                spr.color = new Color(spr.color.r, spr.color.g, spr.color.b,
                    Mathf.Clamp01(currentTime / timeToShoot));
            }
            
        }

        IEnumerator DeactiveBullet()
        { 
            //SoundManager.Instance.PlaySound("Lazer 2", SoundManager.SoundMixer.SFX);
            yield return new WaitForSeconds(timeToReturn); 
            laser.SetActive(false);
            currentTime = 0;
        }
    }
}
