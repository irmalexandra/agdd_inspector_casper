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

    public GameObject test;
    
    private Vector3 _checkPointPosition;
    
    private SpriteRenderer playerSpriteRenderer;
    private PlayerController playerController;
    private PlayerMovement playerMovement;
    private Rigidbody2D playerRigidBody;

    private  bool _isDead = false;


    // Start is called before the first frame update

    private void Awake()
    {
        instance = this;
        _checkPointPosition = player.transform.position;
    }

    void Start()
    {
        Physics2D.IgnoreLayerCollision(6, 7);
        Physics2D.IgnoreLayerCollision(0, 7);

        playerRigidBody = player.gameObject.GetComponent<Rigidbody2D>();
        playerSpriteRenderer = player.gameObject.GetComponent<SpriteRenderer>();
        playerController = player.gameObject.GetComponent<PlayerController>();
        playerMovement = player.gameObject.GetComponent<PlayerMovement>();
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        
    }

    public void KillPlayer()
    {
        playerSpriteRenderer.enabled = false;
        playerMovement.enabled = false;
        playerController.enabled = false;
        playerRigidBody.velocity = new Vector2(0,0);
        _isDead = true;
        StartCoroutine(Wait());
    }

    private void RevivePlayer()
    {
        playerSpriteRenderer.enabled = true;
        playerMovement.enabled = true;
        playerController.enabled = true;
        playerRigidBody.velocity = new Vector2(0,0);
        player.transform.position = _checkPointPosition;
        foreach (var enemy in enemies)
        {
            var aiComponent = enemy.GetComponent<BaseGhostAI>();
            if (aiComponent != null)
            {
                enemy.transform.position = aiComponent.originalPosition;
            }
        }
        deathCanvas.SetActive(false);
        _isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isDead) return;
        if (Input.GetKeyDown(KeyCode.R))
        {
            RevivePlayer();
        }

    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
        deathCanvas.SetActive(true);
    }

    public void setCheckpoint(Vector3 newPos)
    {
        _checkPointPosition = newPos;
    }
}
