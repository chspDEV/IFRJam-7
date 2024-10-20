using System.Collections;
using System.Collections.Generic;
using Manager;
using UnityEngine;

namespace Scenario
{
    public class Machine : MonoBehaviour
    {
        public float timeToShoot;
        public float currentTime;
        public GameObject laser;
        public List<GameObject> laserPreview;
        private float timeToReturn = 4;

        // Update is called once per frame
        void Update()
        {
            laser.transform.localScale = new Vector3(laser.transform.localScale.x, laser.transform.localScale.y + currentTime / 100, laser.transform.localScale.z);
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

        IEnumerator DeactiveBullet()
        { 
            SoundManager.Instance.PlaySound("Laser 2", SoundManager.SoundMixer.SFX);
            yield return new WaitForSeconds(timeToReturn); 
            laser.SetActive(false);
            currentTime = 0;
        }
    }
}
