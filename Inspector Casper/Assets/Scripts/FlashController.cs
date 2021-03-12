using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.UI;

public class FlashController : MonoBehaviour
{
    public float flashTimelength = .2f;
    // public bool doCameraFlash = false;
    
    private float startTime;
    private bool flashing;
    private Light2D flash;
 
    void Start()
    {
        flash = GetComponent<Light2D>();
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
}
