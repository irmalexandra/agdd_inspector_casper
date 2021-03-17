using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.UI;

public class FlashController : MonoBehaviour
{
    public float flashDuration;
    
    private float startTime;
    [HideInInspector]
    public bool flashing;
    private Light2D flash;
    
    void Start()
    {
        flash = GetComponent<Light2D>();
    }

    public void CameraFlash()
    {
        startTime = Time.time;
        StartCoroutine(FlashCoroutine());
    }
 
    IEnumerator FlashCoroutine()
    {
        bool done = false;
        flashing = true;

        while(!done)
        {
            flash.intensity = 3; 

            float perc;
            
            perc = Time.time - startTime;
            perc = perc / flashDuration;
            if(perc > flashDuration)
            {
                done = true;
            }
            yield return null;
        }
        flash.intensity = 0;
        flashing = false;
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Enemy")) { return; }
        if (flashing)
        {
            if (!other.gameObject.GetComponent<SpriteRenderer>().enabled && other is BoxCollider2D)
            {
                other.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            }
        }
        else
        {
            other.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Enemy")) { return; }
        if (other.gameObject.GetComponent<SpriteRenderer>().enabled && other is BoxCollider2D)
        {
            other.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
    
}


