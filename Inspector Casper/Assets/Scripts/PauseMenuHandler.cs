using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuHandler : MonoBehaviour
{
// Start is called before the first frame update    

    public GameObject pauseMenu;
    private bool _onScreen = false;
    
private void Update()
{
    if (Input.GetKeyDown(KeyCode.P))
    {
        onPauseToggle();
    }
}


public void onPauseToggle()
{
    if (_onScreen)
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        _onScreen = false;

    }
    else
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        _onScreen = true;
    }
}

public void onExit()
{
        SceneManager.LoadScene(0);
    }
}
