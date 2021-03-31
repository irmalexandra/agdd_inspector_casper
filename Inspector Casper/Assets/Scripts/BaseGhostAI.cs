 using System;
 using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;

public class BaseGhostAI : MonoBehaviour
{
    private Transform _player;
    public Animator animator;
    public Transform originalPosition;

    private Rigidbody2D body;

    private Vector2 movement;
    private PlayerController _playerScript; 

    private bool facingLeft = true;

    public float roamSpeed = 2.5f;
    public float chaseSpeed = 5f;
    
    private bool targetVisible = false;
    private bool frozen;
    //public Vector3 originalPosition;

    private SpriteRenderer _spriteRenderer;

    // Start is called before the first frame update
  

    void Start()
    {
        _spriteRenderer = transform.gameObject.GetComponent<SpriteRenderer>();
        _player = GameManager.instance.getPlayer().GetComponent<Transform>();
        _playerScript = _player.GetComponent<PlayerController>();
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        /*Vector3 directionToPlayer = _player.position - transform.position;  
        
        directionToPlayer.Normalize();
        movement = directionToPlayer;*/
    }

    private void FixedUpdate()
    {

        if (!frozen)
        {
            if (targetVisible && _playerScript._alive)
            {
                MoveCharacter(_player.position, chaseSpeed);
            }
            else
            {
                if (transform.position.x < originalPosition.position.x + 5f && transform.position.x >
                                                                            originalPosition.position.x - 5f
                                                                            && transform.position.y >
                                                                            originalPosition.position.y - 5f &&
                                                                            transform.position.y <
                                                                            originalPosition.position.y + 5f)
                {
                    body.velocity = Vector2.zero;
                }
                else
                {
                    StartCoroutine(Wait(3f));
                }
            }
        }
    }

    private void MoveCharacter(Vector3 position, float moveSpeed)
    {
        if (transform.position == position)
        {
            return;
        }
        
        // body.MovePosition((Vector2)transform.position + (direction * (moveSpeed * Time.deltaTime)));   
        transform.position = Vector3.MoveTowards(transform.position, position,  (moveSpeed * Time.deltaTime));   

        if (position.x < 0 && !facingLeft){
            Flip();
        }
        else if (position.x > 0 && facingLeft){
            Flip();
        }	
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!_playerScript._alive)
            {
                targetVisible = false;
            }
            else
            {
                targetVisible = true;
                animator.SetBool("Chasing", true);
                if (other.gameObject.GetComponent<PlayerController>().enabled == false){
                    animator.SetBool("Chasing", false);
                }
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

    private void OnCollisionEnter2D(Collision2D other)
    {
        // if (other.gameObject.CompareTag("Player"))
        // {
        //     Collider2D[] colliders = _player.GetComponents<Collider2D>();
        //     Collider2D ghostCollider = GetComponent<BoxCollider2D>();
        //     foreach (Collider2D collider in colliders)
        //     {
        //         Physics2D.IgnoreCollision(collider, ghostCollider, true);
        //     }
        //     return;
        // }
        // Physics2D.IgnoreCollision(other.collider, gameObject.GetComponent<Collider2D>());
        
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
            if(perc > duration)
            {
                done = true;
            }
            yield return null;
        }
        frozen = false;
    }

    public void KillGhost(float duration)
    {
        // TODO start death animation
        animator.SetBool("Dead", true);
        StartCoroutine(DeathAnimationCoroutine(duration));
    }
    
    private IEnumerator DeathAnimationCoroutine(float duration)
    {
        float startTime = Time.time;
        bool done = false;
        while(!done)
        {
            float perc;
        
            perc = Time.time - startTime;
            if(perc > duration)
            {
                done = true;
            }
            yield return null;
        }
        transform.position = transform.parent.GetChild(0).position; // Index 0 is the game object "originalPosition"
        animator.SetBool("Dead", false);
        transform.gameObject.SetActive(false);

    }

    private IEnumerator Wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        MoveCharacter(originalPosition.position, roamSpeed);
    }
    
    public void Reset()
    {
        transform.position = originalPosition.position;
        targetVisible = false;
        // Collider2D[] colliders = _player.GetComponents<Collider2D>();
        // Collider2D ghostCollider = GetComponent<BoxCollider2D>();
        // foreach (Collider2D collider in colliders)
        // {
        //     Physics2D.IgnoreCollision(collider, ghostCollider, false);
        // }
    }

    public void ResetPlayerGhost()
    {
        _player = GameManager.instance.getPlayer().GetComponent<Transform>();
        if (!GameManager.instance.getPlayer().GetComponent<PlayerController>().insideSafeZone)
        {
            transform.gameObject.SetActive(true);
            transform.position = _player.position;
            targetVisible = false;
            originalPosition.position = _player.position;
        }
        else
        {
            transform.gameObject.SetActive(false);
        }
        
    }
    
}
