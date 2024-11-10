using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class playerController : MonoBehaviour
{   //player movement speed and jump high
    [SerializeField]private float Speed = 3f;
    [SerializeField]private float JumpForce = 6f;

    //rigidbody for controlling physics and Animator for animation
    private Rigidbody2D rb;
    private Animator anim;

    //ground detection
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask Ground;

    //bool to track in which direction player is facing
    private bool facingRight = true;

    //AudioSource for jump sound
    AudioSource jumpSound;

    //reference to GameController script for tracking gem count
    public GameController gc;
    public Timerscript timerScript;

    //text to display when the player wins
    [SerializeField] public TMP_Text winText;
   

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //get Rigidbody2D component for physics
        anim = GetComponent<Animator>();  //get Animator component for animations
        jumpSound = GetComponent<AudioSource>(); //get AudioSource component for sound effect

    }

    
    void Update()
    {
        AnimationController(); //update animations based on current state
        FlipController();  //flip player sprite based on movement direction

        //horizontal movement input
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * Speed, rb.velocity.y);

        //jumping logic
        if (Input.GetButtonDown("Jump") && IsGrounded())//jump only when player is on the ground
        {
            jumpSound.Play();
            rb.velocity = new Vector2(rb.velocity.x, JumpForce);//add jump velocity
        }

        //hhort jump reduces up force if jump button released early
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        
    }
    //checks if the player is grounded 
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.1f, Ground);
    }
    //updates animator parameters 
    private void AnimationController()
    {
        anim.SetFloat("xSpeed", rb.velocity.x);
        anim.SetFloat("ySpeed", rb.velocity.y);
        anim.SetBool("isGrounded", IsGrounded());
    }
    //checks if player changed his direction
    private void FlipController()
    {
        if (rb.velocity.x < 0 && facingRight)
            Flip();
        else if (rb.velocity.x > 0 && !facingRight)
            Flip();
    }
    //flips the character based 
    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Gem"))
        {
            Destroy(collision.gameObject);
            gc.gemCount++;

            //checks if all gems collected
            if (gc.gemCount == 3)
            {
                timerScript.isLevelCompleted = true; // stop the timer
                timerScript.SaveBestTime(); // save best time
                winText.gameObject.SetActive(true); //activate winning text and button
                Time.timeScale = 0; // pause the game



            }
        }
    }
}
