using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class playerController : MonoBehaviour
{
    [SerializeField]private float Speed = 3f;
    [SerializeField]private float JumpForce = 6f;

    private Rigidbody2D rb;
    private Animator anim;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask Ground;

    public bool facingRight = true;

    AudioSource jumpSound;

    public GameController gc;

    [SerializeField] public TMP_Text winText;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        jumpSound = GetComponent<AudioSource>();
        
    }

    
    void Update()
    {
        AnimationController();
        FlipController();

        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * Speed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            jumpSound.Play();
            rb.velocity = new Vector2(rb.velocity.x, JumpForce);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
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
