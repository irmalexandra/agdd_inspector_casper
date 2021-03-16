using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class DeathTrigger : MonoBehaviour
{
    public List<string> deathTags;

    // Start is called before the first frame update
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!deathTags.Contains(other.gameObject.tag)) return;
        GameManager.instance.KillPlayer();
    }
    
    
}
