
 using System;
 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGhostAI : MonoBehaviour
{
    private Transform _player;
    public Animator animator; 

    private Rigidbody2D body;

    private Vector2 movement;

    private bool facingLeft = true;

    public float roamSpeed = 2.5f;
    public float chaseSpeed = 5f;
    
    private bool targetVisible = false;
    private bool frozen;
    public Vector3 originalPosition;

    private SpriteRenderer _spriteRenderer;

    // Start is called before the first frame update
    private void Start()
    {
        _spriteRenderer = transform.gameObject.GetComponent<SpriteRenderer>();
    }

    void Awake()
    {
        _player = GameManager.instance.getPlayer().GetComponent<Transform>();
        body = GetComponent<Rigidbody2D>();
        originalPosition = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = _player.position - transform.position;
        
        direction.Normalize();
        movement = direction;
    }

    private void FixedUpdate()
    {
        if (!frozen)
        {
            if (targetVisible)
            {
                MoveCharacter(movement, chaseSpeed);
            
            }
            else
            {
                if (transform.position == originalPosition) return;
                StartCoroutine(Wait(3f));
            }
        }

        
    }

    private void MoveCharacter(Vector2 direction, float moveSpeed)
    {   
        body.MovePosition((Vector2)transform.position + (direction * (moveSpeed * Time.deltaTime)));   
        
        if (direction.x < 0 && !facingLeft){
            Flip();
        }
        else if (direction.x > 0 && facingLeft){
            Flip();
        }	
    }

    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            targetVisible = true;
            animator.SetBool("Chasing", true);
            if (other.gameObject.GetComponent<PlayerController>().enabled == false){
                animator.SetBool("Chasing", false);
            }
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

    private void Flip()
	{
		// Switch the way the player is labelled as facing.
		facingLeft = !facingLeft;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

    
    public void RevealGhost(float duration)
    {
        StartCoroutine(RevealGhostCoroutine(duration));
    }

    private IEnumerator RevealGhostCoroutine(float duration)
    {
        _spriteRenderer.enabled = true;
        float startTime = Time.time;
        bool done = false;
        while(!done)
        {

            float perc;
        
            perc = Time.time - startTime;
            perc = perc / duration;
            if(perc > duration)
            {
                done = true;
            }
            yield return null;
        }
        _spriteRenderer.enabled = false;
    }    
    public void FreezeGhost(float duration)
    {
        StartCoroutine(FreezeGhostCoroutine(duration));
    }

    IEnumerator FreezeGhostCoroutine(float duration)
    {
        frozen = true;
        float startTime = Time.time;
        bool done = false;
        while(!done)
        {

            float perc;
        
            perc = Time.time - startTime;
            perc = perc / duration;
            if(perc > duration)
            {
                done = true;
            }
            yield return null;
        }
        frozen = false;
    }
    
    
    private IEnumerator Wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        MoveCharacter((originalPosition - transform.position).normalized, roamSpeed);
    }

    public void Reset()
    {
        transform.position = originalPosition;
    }
}
