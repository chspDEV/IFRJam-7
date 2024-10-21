using GameFeel;
using Interface;
using Manager;
using UnityEngine;

namespace Entitys
{
    public class DestroyableEntity : MonoBehaviour, IDestroyable
    {
        public float timeToDestroy;
        private bool isPlayer;

        public void DestroyObject()
        {
            if (!TryGetComponent<FlashEffect>(out var flash)) { Debug.Log("Objeto não possui flashEffect!"); } else { flash.Flash(); }

            //Se for um objeto com movimento entao é o PET ou o PLAYER
            if (!TryGetComponent<EntityMainLogic>(out var grd)) { Debug.Log("Objeto não possui gridMovement!"); }
            else { grd.enabled = false; isPlayer = true; grd.isMoving = false; }

            if (isPlayer)
            {
                SoundManager.Instance.StopAllSounds();
                SoundManager.Instance.PlaySound("Morte", SoundManager.SoundMixer.MASTER);
            }
            
            if (!TryGetComponent<ScreenShake>(out var scr)) { Debug.Log("Objeto não possui screenShake!"); } else { scr.TriggerShake(); }

            Destroy(gameObject, timeToDestroy);
        }

        private void OnDestroy()
        {
            
            if(isPlayer) GameManager.Instance.SetState(GameState.DEFEAT);
        }

    }
}