using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class DoorSystem : MonoBehaviour
{
    public GameObject door;
    private bool playerInRange;
    public string keyName;
    private bool unlocked = false;

    private void Update()
    {
        if (playerInRange)
        {
            GameManager.instance.getPlayerController().showInteractiveButton(true);
            if (Input.GetKey("e"))
            {
                Debug.Log("Pressed E");
                var keys = GameManager.instance.getPlayerController().GetKeys();
                if (keys.ContainsKey(keyName))
                {
                    Debug.Log("Player has key");
                    if (keys[keyName])
                    {
                        Debug.Log("Door Unlocked");
                        door.gameObject.GetComponent<Collider2D>().enabled = false;
                        unlocked = true;
                    }
                }
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
