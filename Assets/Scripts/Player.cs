using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : MonoBehaviour
{
    public float hp;
    public float speed = 5f;
    private Rigidbody2D rb;
    Vector2 movement;
    public float atk;
    public float def;
    public float jumpForce;
    public float checkRadius = 0.2f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public bool isGrounded = false;
    [SerializeField]private int jumpsRemaining = 2;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 10f;
        }
        else
        {
            speed = 5f;
        }
        float moveX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float moveZ = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        transform.Translate(moveX, 0, moveZ);
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);
        if (isGrounded)
        {
            jumpsRemaining = 2;
        }
        if (Input.GetButtonDown("Jump") && jumpsRemaining >= 1)
        {
            if (!isGrounded)
            {
                jumpsRemaining--;
            }
            if (jumpsRemaining <= 1)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce * 1.5f);
            }
            else
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
            jumpsRemaining--;
        }
    }
}
