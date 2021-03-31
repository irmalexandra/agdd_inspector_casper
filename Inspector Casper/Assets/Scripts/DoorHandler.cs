using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public int sceneIndex;
    public string doorName;
    private bool _inRange = false;
    


    private void FixedUpdate()
    {
        if (_inRange)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                PlayerPrefs.SetString("door", doorName);
                SceneManager.LoadScene(sceneIndex);
            }
        }
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.getPlayerController().showInteractiveButton(true);
            _inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.getPlayerController().showInteractiveButton(false);
            _inRange = false;
        }
    }
}
