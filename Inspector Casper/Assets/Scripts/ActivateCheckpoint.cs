using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateCheckpoint : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite activeSprite;
    public Sprite unActiveSprite;

    public void Activate()
    {
        spriteRenderer.sprite = activeSprite;
    }

    public void Deactivate()
    {
        spriteRenderer.sprite = unActiveSprite;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        GameManager.instance.setCheckpoint(transform.position);
        Activate();
    }

}
