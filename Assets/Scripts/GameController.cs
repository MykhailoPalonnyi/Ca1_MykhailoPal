using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    //variables to store the player's checkPoint position, SpriteRenderer, and Rigidbody2D component
    Vector2 checkPointPosition;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;

    public int gemCount;
    [SerializeField] public TMP_Text gemText;

    [SerializeField] private ParticleSystem deathParticles;
    private ParticleSystem deathParticleInstance;

    void Start()
    {

        checkPointPosition = transform.position;//initialize starting position of the player
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
            Die();
        }
    }
    public void UpdateCheckPoint(Vector2 pos)
    {
        checkPointPosition = pos;
    }
    //coroutine to respawn a player
    void Die()
    {
        SpawnDeathParticles();
        StartCoroutine(Respawn(0.5f));//calls Respawn() with 0.5 as value for the delay
    }
    IEnumerator Respawn(float duration)
    {

        rb.simulated = false; //turn off Rigidbody simulation
        spriteRenderer.enabled = false; //turn off spriteRenderer simulation
        yield return new WaitForSeconds(duration); //wait for the specified duration before respawning
        transform.position = checkPointPosition; //move player to start position
        spriteRenderer.enabled = true; //turn on Rigidbody simulation
        rb.simulated = true; //turn on spriteRenderer simulation
    }
    private void SpawnDeathParticles()
    {
        ParticleSystem deathParticleInstance = Instantiate(deathParticles, transform.position, Quaternion.identity);
        //play the particle
        deathParticleInstance.Play();

        // destroy the particle system instance after its duration
        Destroy(deathParticleInstance.gameObject, deathParticleInstance.main.duration);
    }

}