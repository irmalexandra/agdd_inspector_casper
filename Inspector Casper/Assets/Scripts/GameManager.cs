using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using System.Runtime.CompilerServices;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject enemyNumberTwo;
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
        Physics2D.IgnoreLayerCollision(10, 10);
        Physics.IgnoreLayerCollision(7,10);
        Physics2D.IgnoreLayerCollision(6, 7);
        Physics2D.IgnoreLayerCollision(0, 7);
        Physics2D.IgnoreLayerCollision(10,10);

        _playerRigidBody = player.gameObject.GetComponent<Rigidbody2D>();
        _playerSpriteRenderer = player.gameObject.GetComponent<SpriteRenderer>();
        _playerController = player.gameObject.GetComponent<PlayerController>();
        _playerMovement = player.gameObject.GetComponent<PlayerMovement>();
        //enemies = GameObject.FindGameObjectsWithTag("Enemy");
        //enemies = enemyNumberTwo.GetComponentsInChildren<GameObject>();
        Debug.Log(enemies);
    }

    public PlayerController getPlayerController()
    {
        return _playerController;
    }

    public GameObject getPlayer()
    {
        return player;
    }



    // Update is called once per frame
    void Update()
    {
        if (!_isDead) return;
        /*if (Input.GetKeyDown(KeyCode.R) && !_playerController._alive)
        {
            Debug.Log("what the shit");
            _playerController.Revive();
            //Debug.Log(enemies.Length);
            
            Transform thing = enemyNumberTwo.transform;
            
            foreach (Transform child in thing)
            {
                Debug.Log(child);
            }
            Debug.Log("lol");
            /*foreach (GameObject enemy in enemies)
            {
                enemy.GetComponent<BaseGhostAI>().Reset();
                Debug.Log(enemy);
            }#1#

        }*/
    }

    public void Reset()
    {
        Debug.Log("what the shit");
        _playerController.Revive();
        //Debug.Log(enemies.Length);
            
        Transform thing = enemyNumberTwo.transform;
            
        foreach (Transform child in thing)
        {
            Debug.Log("yes");
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
