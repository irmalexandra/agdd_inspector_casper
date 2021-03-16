using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.UI;

public class FlashController : MonoBehaviour
{
    public float flashTimelength = .2f;

    public float freezeEnemy = 5.0f;
    // public bool doCameraFlash = false;
    
    private float startTime;
    private bool flashing;
    private Light2D flash;
    private Collider2D flashCone;
 
    void Start()
    {
        flash = GetComponent<Light2D>();
        flashCone = GetComponent<Collider2D>();
    }
    
    public void CameraFlash()
    {
        
        startTime = Time.time;

        flashing = true;
        StartCoroutine(FlashCoroutine());
        flashing = false;
    }
 
    IEnumerator FlashCoroutine()
    {
        bool done = false;
 
        while(!done)
        {
            
            flash.intensity = 3; 

            float perc;


            perc = Time.time - startTime;
            perc = perc / flashTimelength;

            if(perc > 1.0f)
            {
                perc = 1.0f;
                done = true;
            }
            flashing = true;

            yield return null;
        }

        flash.intensity = 0;


    }
    
    IEnumerator FreezeCoroutine(Rigidbody2D target)
    {
        var originalVelocity = target.velocity;

        bool done = false;
        while (!done)
        {
            float perc;
            target.velocity = new Vector2(0, 0);

            perc = Time.time - startTime;
            perc = perc / flashTimelength;

            if(perc > 1.0f)
            {
                perc = 1.0f;
                done = true;
            }
            yield return null;
        }

        target.velocity = originalVelocity;

    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.layer == 7) // Enemies layer
        {
            Debug.Log("Successfull detection");
            StartCoroutine(FreezeCoroutine(other.rigidbody));
        }
    }
}


