using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.UI;

public class FlashController : MonoBehaviour
{
    public float flashDuration;
    public float freezeDuration;
    private float _startTime;
    public float cooldownTimer;
    private bool onCooldown;
    private Light2D _flash;
    

    private List<GameObject> _targets = new List<GameObject>();
    
    void Start()
    {
        _flash = GetComponent<Light2D>();
    }

    public void CameraFlash()
    {
        if (!onCooldown)
        {
            SoundManager.PlaySoundEffect("ShutterEcho");
            _startTime = Time.time;
            StartCoroutine(FlashCoroutine());
            foreach (var target in _targets)
            {
                target.GetComponent<BaseGhostAI>().RevealGhost(flashDuration);
                target.GetComponent<BaseGhostAI>().FreezeGhost(freezeDuration);
            }
            StartCoroutine(CooldownCoroutine());
        }
    }

    private IEnumerator CooldownCoroutine()
    {
        onCooldown = true;
        bool done = false;
        while(!done)
        {

            float perc;
            perc = Time.time - _startTime;
            if(perc > cooldownTimer)
            {
                done = true;
            }
            yield return null;
        }
        onCooldown = false;
    }
    
    private IEnumerator FlashCoroutine()
    {
        if (!onCooldown){
            bool done = false;
            _flash.intensity = 3;

            while(!done)
            {
                float perc;
                
                perc = Time.time - _startTime;
                if(perc > flashDuration)
                {
                    done = true;
                }
                yield return null;
            }
            _flash.intensity = 0;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Enemy")) { return; }
        if (_targets.Contains(other.gameObject)) { return; }
        if (other is BoxCollider2D)
        {
            _targets.Add(other.gameObject);
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Enemy")) { return; }
        if (other is BoxCollider2D)
        {
            _targets.Remove(other.gameObject);
        }
    }
}


