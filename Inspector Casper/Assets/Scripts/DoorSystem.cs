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
    private bool unlocked;
    public Sprite openDoorSprite;



    private void Update()
    {
        if (playerInRange)
        {
            
            if (Input.GetKey("e"))
            {
                var keys = GameManager.instance.getPlayerController().GetKeys();
                if (keys.ContainsKey(keyName))
                {
                    if (keys[keyName])
                    {
                        door.gameObject.GetComponent<Collider2D>().enabled = false;
                        unlocked = true;
                        if (!unlocked)
                        {
                            SoundManager.PlaySoundEffect("DoorCreak");
                        }
                        door.GetComponent<SpriteRenderer>().sprite = openDoorSprite;
                    }
                }
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
            playerInRange = false;
            GameManager.instance.getPlayerController().showInteractiveButton(false);
        }
    }
}
