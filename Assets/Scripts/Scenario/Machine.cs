using System;
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
        private bool canRun = false;

        private void Start()
        {
            HandleShowPreview();
            StartCoroutine(Wait());
        }

        private IEnumerator Wait()
        {
            yield return new WaitForSeconds(6);
            canRun = true;
        }

        void Update()
        {
            if (canRun)
            {
                HandleShootLogic();
                HandleShowPreview();
            }
        }

        private void HandleShootLogic()
        {
            if (currentTime >= timeToShoot)
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

            foreach (GameObject go in laserPreview)
            {
                var spr = go.GetComponent<SpriteRenderer>();
                spr.color = new Color(spr.color.r, spr.color.g, spr.color.b,
                    currentTime / timeToShoot);
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
