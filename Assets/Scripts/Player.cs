using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : MonoBehaviour
{
    public float hp;
    public float speed;
    private Rigidbody2D rb;
    Vector2 movement;
    public float atk;
    public float def;
    public float jumpForce;
    public float checkRadius = 0.2f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public bool isGrounded = false;
    private float horizontalInput;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }
    void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);
    }
}
