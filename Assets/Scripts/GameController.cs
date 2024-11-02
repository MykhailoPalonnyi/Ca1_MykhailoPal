using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    //variables to store the player's start position, SpriteRenderer, and Rigidbody2D component
    Vector2 startPosition;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;

    public int gemCount;
    [SerializeField] public TMP_Text gemText;

    
    void Start()
    {
        startPosition = transform.position;//initialize starting position of the player
        spriteRenderer = GetComponent<SpriteRenderer>();//bet the SpriteRenderer component for controlling visibility
        rb = GetComponent<Rigidbody2D>();//get Rigidbody2D component for physics
    }
    void Update()
    {
        //update the gem count to show the current gem count
        gemText.text = gemCount.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //check if the player collides with an object tagged "Obstacle"
        if (collision.CompareTag("Obstacle"))
        {
            StartCoroutine(Respawn(0.5f));//calls Respawn() with 0.5 as value for the delay
        }
    }
    //coroutine to respawn a player
    IEnumerator Respawn(float duration)
    {

        rb.simulated = false; //turn off Rigidbody simulation
        spriteRenderer.enabled = false; //turn off spriteRenderer simulation
        yield return new WaitForSeconds(duration); //wait for the specified duration before respawning
        transform.position = startPosition; //move player to start position
        spriteRenderer.enabled = true; //turn on Rigidbody simulation
        rb.simulated = true; //turn on spriteRenderer simulation
    }
    
}
