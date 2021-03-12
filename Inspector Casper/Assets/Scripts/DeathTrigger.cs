using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DeathTrigger : MonoBehaviour
{
    public List<string> deathTags;
    public GameObject canvas;

    public Transform player;

    private bool display = false;
    // Start is called before the first frame update
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log(other.gameObject.tag);
        if (!deathTags.Contains(other.gameObject.tag)) return;
        player.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        player.gameObject.GetComponent<PlayerController>().enabled = false;
        player.gameObject.GetComponent<PlayerMovement>().enabled = false;
        StartCoroutine(Wait());
    }
    
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
        canvas.SetActive(true);
    }
}
