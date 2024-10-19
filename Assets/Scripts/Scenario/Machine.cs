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
        public float timeToReturn;

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
            yield return new WaitForSeconds(timeToReturn); 
            laser.SetActive(false);
            currentTime = 0;
        }
    }
}
