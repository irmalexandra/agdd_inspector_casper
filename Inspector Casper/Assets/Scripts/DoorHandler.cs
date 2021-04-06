using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.UI;
using UnityEngine.SceneManagement;

public class DoorHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public string doorName;
    private bool _inRange = false;
    private bool _exiting;
    public GameObject exit;
    private bool _canTeleport = true;


    private void FixedUpdate()
    {
        if (_inRange)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (_canTeleport)
                {
                    
                    if (doorName == "KeyChamber" && GameManager.instance.getPlayer().GetComponent<PlayerController>().hunted)
                    {
                        MusicManager.Instance.PlayPart2();
                    }
                    GameManager.instance.getPlayer().transform.position = exit.transform.position;
                    StartCoroutine(TeleportCooldownCoroutine());
                }
            }
        }
    }
    
    private IEnumerator TeleportCooldownCoroutine()
    {
        _canTeleport = false;
        yield return new WaitForSeconds(0.5f);
        _canTeleport = true;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        StartCoroutine(TeleportCooldownCoroutine());
        if (other.CompareTag("Player"))
        {
            var playerScript = other.GetComponent<PlayerController>();
            
            if (PlayerPrefs.GetString("hunted") == "true" && doorName == "KeyChamber")
            {
                GameManager.instance.flipGhostType();
            }
            playerScript.showInteractiveButton(true);
            _inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var playerScript = other.GetComponent<PlayerController>();
            playerScript.showInteractiveButton(false);
            _inRange = false;
        }
    }
}
