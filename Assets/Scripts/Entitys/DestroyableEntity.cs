﻿using GameFeel;
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
            else { grd.enabled = false; isPlayer = true; }

            if (!TryGetComponent<ScreenShake>(out var scr)) { Debug.Log("Objeto não possui screenShake!"); } else { scr.TriggerShake(); }

            Destroy(gameObject, timeToDestroy);
        }

        private void OnDestroy()
        {
            SoundManager.Instance.PlaySound("Morte", SoundManager.SoundMixer.SFX);
            if(isPlayer) GameManager.Instance.SetState(GameState.DEFEAT);
        }

    }
}