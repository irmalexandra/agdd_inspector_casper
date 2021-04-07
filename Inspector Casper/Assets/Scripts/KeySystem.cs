using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeySystem : MonoBehaviour
{
    // Start is called before the first frame update
    

    private bool playerInRange;
    public string keyName;
    private void Update()
    {
        if (playerInRange)
        {
            if (Input.GetKey("e"))
            {
                gameObject.SetActive(false);
                var keySprite = gameObject.GetComponent<SpriteRenderer>().sprite;
                var keyColor = gameObject.GetComponent<SpriteRenderer>().color;
                GameManager.instance.getPlayer().GetComponent<PlayerController>().takeKey(keyName, keySprite, keyColor);
            }
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            GameManager.instance.getPlayerController().showInteractiveButton(true);
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (keyName == "FinalKey")
            {
                GameManager.instance.getPlayer().GetComponent<PlayerController>().hunted = true;
            }
            playerInRange = false;
            GameManager.instance.getPlayer().GetComponent<PlayerController>().showInteractiveButton(false);
        }
    }
}
