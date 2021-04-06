using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverHandler : MonoBehaviour
{
    public GameObject platform = null;
    private MovingPlatform movingPlatform;
    private bool playerInRange;
    private bool interactable = true;

    private void Start()
    {
        movingPlatform = platform.gameObject.GetComponentInChildren<MovingPlatform>();
    }

    private void Update()
    {
        if (platform == null) { return; }
        if (!interactable) { return; }
        if (playerInRange)
        {
            if (Input.GetKey("e"))
            {
                Debug.Log(movingPlatform);
                movingPlatform.Toggle();
                StartCoroutine(ToggleCooldown());
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

    private IEnumerator ToggleCooldown()
    {
        interactable = false;
        yield return new WaitForSeconds(0.5f);
        interactable = true;
    }
}
