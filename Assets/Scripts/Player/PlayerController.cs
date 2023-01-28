using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof(SpriteRenderer))]

public class PlayerController : MonoBehaviour
{
    // Components
    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer sr;

    // Movement variable
    public float speed;
    public float jumpForce;

    // Ground checking
    public bool isGrounded;
    public Transform groundCheck;
    public LayerMask isGroundLayer;
    public float groundCheckRadius;
    public bool isAttacking;
    public bool isFiring;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        if (speed <= 0)
        {
            speed = 6.0f;
            Debug.Log("Speed was set incorrectly, defaulting to " + speed.ToString());
        }

        if (jumpForce <= 0)
        {
            jumpForce = 300;
            Debug.Log("jumpForce was set incorrectly, defaulting to " + jumpForce.ToString());
        }

        if (groundCheckRadius <= 0)
        {
            groundCheckRadius = 0.2f;
            Debug.Log("groundCheckRadius was set incorrectly, defaulting to " + groundCheckRadius.ToString());
        }

        if (!groundCheck)
        {
            groundCheck = GameObject.FindGameObjectWithTag("GroundCheck").transform;
            Debug.Log("groundCheck not set, finding it manually");
        }

    }

    // Update is called once per frame
    void Update()
    {
        float hInput = Input.GetAxisRaw("Horizontal");

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);

        // Jumping
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * jumpForce);
        }

        // Walking
        Vector2 moveDirection = new Vector2(hInput * speed, rb.velocity.y);
        rb.velocity = moveDirection;

        anim.SetFloat("hInput", Mathf.Abs(hInput));
        anim.SetBool("isGrounded", isGrounded);

        Debug.Log(hInput);

        // Grounded
        if (isGrounded)
        {
            anim.SetBool("isAttacking", false);
        }

        // Fire
        if (Input.GetButtonDown("Fire1"))
        {
            anim.SetBool("isFiring", true);
        }

        // Jump Attack
        if (isGrounded == false && Input.GetButtonDown("Fire2"))
        {
            anim.SetBool("isAttacking", true);
            rb.AddForce(Vector2.down * jumpForce);
        }
    }
}
