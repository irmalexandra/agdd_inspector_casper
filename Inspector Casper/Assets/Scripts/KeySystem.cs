using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeySystem : MonoBehaviour
{
    // Start is called before the first frame update
    

    private bool playerInRange = false;
    public string name;
    
    private void Update()
    {
        if (playerInRange)
        {
            GameManager.instance.getPlayerController().showInteractiveButton(true);
            if (Input.GetKey("e"))
            {
                gameObject.SetActive(false);
                var keySprite = gameObject.GetComponent<SpriteRenderer>().sprite;
                var keyColor = gameObject.GetComponent<SpriteRenderer>().color;
                GameManager.instance.getPlayerController().takeKey(name, keySprite, keyColor);
                GameManager.instance.getPlayerController().showInteractiveButton(false);
            }
        }
        else
        {
            GameManager.instance.getPlayerController().showInteractiveButton(false);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
