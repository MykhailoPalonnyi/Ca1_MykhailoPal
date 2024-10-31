using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    Vector2 startPosition;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    void Start()
    {
        startPosition = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            Die();
        }
            
           
    }
    private void Die()
    {
        StartCoroutine(Respawn(0.5f));
    }
    IEnumerator Respawn(float duration)
    {
        rb.simulated = false;
        spriteRenderer.enabled = false;
        yield return new WaitForSeconds(duration);
        transform.position = startPosition;
        spriteRenderer.enabled = true;
        rb.simulated = true;
    }
}
