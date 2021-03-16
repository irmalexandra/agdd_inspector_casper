using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public int sceneIndex;
    private PlayerController _playerController;
    private void Awake()
    {
        _playerController = gameObject.GetComponentInChildren<PlayerController>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("what?");
            _playerController.showInteractiveButton(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _playerController.showInteractiveButton(false);
        }
    }
}
