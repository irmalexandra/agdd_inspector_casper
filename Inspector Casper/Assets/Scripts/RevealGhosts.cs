using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevealGhosts : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Enemy")) { return; }
        if (!other.gameObject.GetComponent<SpriteRenderer>().enabled && other is BoxCollider2D)
        {
            other.gameObject.GetComponent<SpriteRenderer>().enabled = true;
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
