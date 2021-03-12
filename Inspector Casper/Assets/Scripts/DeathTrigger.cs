using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class DeathTrigger : MonoBehaviour
{
    public List<string> deathTags;
    public GameObject canvas;

    public Transform player;
    
    // Start is called before the first frame update
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log(other.gameObject.tag);
        if (!deathTags.Contains(other.gameObject.tag)) return;
        player.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        player.gameObject.GetComponent<PlayerController>().enabled = false;
        player.gameObject.GetComponent<PlayerMovement>().enabled = false;
        player.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
        player.gameObject.GetComponent<Light2D>().enabled = false;
        StartCoroutine(Wait());
    }
    
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
        canvas.SetActive(true);
    }
}
