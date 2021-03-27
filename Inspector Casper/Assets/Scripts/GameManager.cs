using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject deathCanvas;
    
    public GameObject player;
    public GameObject[] enemies;
    
    private Vector3 _checkPointPosition;
    
    private SpriteRenderer _playerSpriteRenderer;
    private PlayerController _playerController;
    private PlayerMovement _playerMovement;
    private Rigidbody2D _playerRigidBody;
    
    private  bool _isDead = false;


    // Start is called before the first frame update

    private void Awake()
    {
        instance = this;
        _checkPointPosition = player.transform.position;
    }
    

    void Start()
    {
        Physics2D.IgnoreLayerCollision(6, 7); // Ceiling check layer and Enemy layer
        Physics2D.IgnoreLayerCollision(9, 7); // Grid layer and Enemy layer
        Physics2D.IgnoreLayerCollision(10, 10); // Ignore self layer and Ignore self layer
        Physics2D.IgnoreLayerCollision(7,10); // Enemy layer and Ignore self layer
        Physics2D.IgnoreLayerCollision(0, 7); // Default layer and Enemy layer

        _playerRigidBody = player.gameObject.GetComponent<Rigidbody2D>();
        _playerSpriteRenderer = player.gameObject.GetComponent<SpriteRenderer>();
        _playerController = player.gameObject.GetComponent<PlayerController>();
        _playerMovement = player.gameObject.GetComponent<PlayerMovement>();

        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    public PlayerController getPlayerController()
    {
        return _playerController;
    }

    public GameObject getPlayer()
    {
        return player;
    }

    
    public void Reset()
    {
        _playerController.Revive();
        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<BaseGhostAI>().Reset();
        }
    }

    public void DisplayDeathCanvas(bool show)
    {
        if (show)
        {
            deathCanvas.SetActive(true);
        }
        else
        {
            deathCanvas.SetActive(false);
        }
       
    }

    public void setCheckpoint(Vector3 newPos)
    {
        _checkPointPosition = newPos;
    }

    public Vector3 getCheckpointPosition()
    {
        return _checkPointPosition;
    }
}
