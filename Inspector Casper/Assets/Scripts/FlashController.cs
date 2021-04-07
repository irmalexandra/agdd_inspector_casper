using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.UI;

public class FlashController : MonoBehaviour
{
    public float flashDuration;
    // public float freezeDuration;
    private float _startTime;
    public float cooldownTimer;
    private bool onCooldown;
    private Light2D _flash;
    private PolygonCollider2D flashCone;
    

    private List<GameObject> _targets = new List<GameObject>();
    
    void Start()
    {
        flashCone = GetComponent<PolygonCollider2D>();
        flashCone.enabled = false;
        _flash = GetComponent<Light2D>();
    }

    public void CameraFlash()
    {
        if (!onCooldown)
        {
            SoundManager.PlaySoundEffect("ShutterEcho");
            _startTime = Time.time;
            StartCoroutine(FlashCoroutine());
            StartCoroutine(CooldownCoroutine());
        }
    }

    private IEnumerator CooldownCoroutine()
    {
        onCooldown = true;
        yield return new WaitForSeconds(cooldownTimer);
        onCooldown = false;
    }
    
    private IEnumerator FlashCoroutine()
    {
        
        if (!onCooldown)
        {
            flashCone.enabled = true;
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
            flashCone.enabled = false;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Enemy")) { return; }
        if (_targets.Contains(other.gameObject)) { return; }
        
        BaseGhostAI targetScript = other.gameObject.GetComponent<BaseGhostAI>();
        targetScript.KillGhost(flashDuration);
        GameManager.instance.respawnGhost(targetScript.ghostRespawnTimer, other.gameObject);

    }
}


