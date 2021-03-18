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


    public PlayerController getPlayerController()
    {
        return _playerController;
    }

    public GameObject getPlayer()
    {
        return player;
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
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    /*public void KillPlayer()
    {
        
        /*
        _playerSpriteRenderer.enabled = false;
        _playerRigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
        #1#
      
        /*GameObject[] colliders = player.GetComponentsInChildren<GameObject>();
        foreach (GameObject found_collider in colliders)
        {
            Debug.Log(found_collider.name);
            found_collider.SetActive(false);
        }#1#
        StartCoroutine(Wait());
    }*/

    private void RevivePlayer()
    {
        player.transform.position = _checkPointPosition;
        player.SetActive(true);
        /*_playerSpriteRenderer.enabled = true;
        _playerRigidBody.velocity = new Vector2(0,0);
        player.transform.position = _checkPointPosition;*/
        _isDead = false;
        foreach (var enemy in enemies)
        {
            var aiComponent = enemy.GetComponent<BaseGhostAI>();
            if (aiComponent != null)
            {
                enemy.transform.position = aiComponent.originalPosition;
            }
        }
        deathCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isDead) return;
        if (Input.GetKeyDown(KeyCode.R))
        {
            _playerController.Revive();
            
        }
    }

    public void DisplayDeathCanvas(bool show)
    {
        if (show)
        {
            // yield return new WaitForSeconds(1);
            deathCanvas.SetActive(true);
            // _isDead = true;
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
