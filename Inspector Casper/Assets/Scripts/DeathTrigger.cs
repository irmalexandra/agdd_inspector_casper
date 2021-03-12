using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DeathTrigger : MonoBehaviour
{
    public List<string> deathTags;

    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log(other.gameObject.tag);
        if (deathTags.Contains(other.gameObject.tag))
        {
            player.gameObject.SetActive(false);
        }
    }
}
