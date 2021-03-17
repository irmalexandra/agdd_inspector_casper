using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class DeathTrigger : MonoBehaviour
{
    public List<string> deathTags;
    public GameObject personalBlood;

    private BloodSplatter _bloodScript; 
    
    
    // Start is called before the first frame update

    private void Awake()
    {
        _bloodScript = personalBlood.GetComponent<BloodSplatter>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!deathTags.Contains(other.gameObject.tag)) return;
        _bloodScript.spawnBlood();
        
        GameManager.instance.KillPlayer();
    }
    
    
}
