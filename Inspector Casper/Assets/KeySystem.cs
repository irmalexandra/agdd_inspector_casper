using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeySystem : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject button_display;

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
                GameManager.instance.getPlayerController().takeKey(name);
            }
        }
        else
        {
            button_display.SetActive(false);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        playerInRange = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        playerInRange = false;
    }
    
    
}
