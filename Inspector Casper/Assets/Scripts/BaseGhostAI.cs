
 using System;
 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGhostAI : MonoBehaviour
{
    public Transform player;
    public Animator animator; 

    private Rigidbody2D body;

    private Vector2 movement;

    public float moveSpeed = 5f;

    private bool targetVisible = false;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = player.position - transform.position;
        
        direction.Normalize();
        movement = direction;
    }

    private void FixedUpdate()
    {
        if (targetVisible)
        {
            MoveCharacter(movement);
            
        }
        
    }

    private void MoveCharacter(Vector2 direction)
    {
        body.MovePosition((Vector2)transform.position + (direction * (moveSpeed * Time.deltaTime)));   
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            targetVisible = true;
            animator.SetBool("Chasing", true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            targetVisible = false;
            animator.SetBool("Chasing", false);
        }
    }
}
