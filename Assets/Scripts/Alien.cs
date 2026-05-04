using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour
{
    public Transform player;
    public GameObject thisThing;
    public float speed = 3f;
    public float chaseDistance = 5f;
    public float jumpForce = 5f;
    private Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float checkRadius = 0.2f;
    private bool isGrounded;
    public float hp;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() 
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);
        
        float distance = Vector2.Distance(transform.position, player.position);
        if (distance < chaseDistance) 
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            
            // Jump if player is higher than the alien and alien is grounded
            if (player.position.y > transform.position.y && isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
        }
    }
    public void TakeDamage(float damage)
    {
        hp = hp - damage;
        print("slice");
        if (hp <= 0)
        {
            Die();
        }
        
    }
    void Die()
    {
        Destroy(thisThing);
    }
}
