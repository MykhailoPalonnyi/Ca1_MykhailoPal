using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class playerController : MonoBehaviour
{   //player movement speed and jump high
    [SerializeField]private float Speed = 3f;
    [SerializeField]private float JumpForce = 6f;

    //Rigidbody for controlling physics and Animator for animation
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

    //text to display when the player wins
    [SerializeField] public TMP_Text winText;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //get Rigidbody2D component for physics
        anim = GetComponent<Animator>();  //get Animator component for animations
        jumpSound = GetComponent<AudioSource>(); //get AudioSource component for sound effects

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

        

        
    }
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.1f, Ground);
    }
    private void AnimationController()
    {
        anim.SetFloat("xSpeed", rb.velocity.x);
        anim.SetFloat("ySpeed", rb.velocity.y);
        anim.SetBool("isGrounded", IsGrounded());
    }

    private void FlipController()
    {
        if (rb.velocity.x < 0 && facingRight)
            Flip();
        else if (rb.velocity.x > 0 && !facingRight)
            Flip();
    }
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
            if (gc.gemCount == 3)
            {
                winText.gameObject.SetActive(true);
                Time.timeScale = 0;
                
            }
        }
    }
}
