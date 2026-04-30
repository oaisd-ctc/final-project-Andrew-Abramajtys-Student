using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float maxHp = 100f;
    public float hp = 100f;
    public Text healthText;
    public float speed = 5f;
    public float invincibilityDuration = 1f;
    private bool isInvincible = false;
    private Rigidbody2D rb;
    private Collider2D playerCollider;
    Vector2 movement;
    public float atk;
    public float def;
    public float jumpForce;
    public float checkRadius = 0.2f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public bool isGrounded = false;
    [SerializeField] private int jumpsRemaining = 2;
    private bool isAlive = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
        hp = Mathf.Clamp(hp, 0f, maxHp);
        UpdateHealthUI();
    }

    void Update()
    {
        if(!isAlive)
        {
            return; // Skip update if player is dead
        }
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !isInvincible)
        {
            TakeDamage(10f);
            StartCoroutine(InvincibilityCoroutine(collision.collider));
        }
    }

    private IEnumerator InvincibilityCoroutine(Collider2D enemyCollider)
    {
        isInvincible = true;

        if (playerCollider != null && enemyCollider != null)
        {
            Physics2D.IgnoreCollision(playerCollider, enemyCollider, true);
        }

        yield return new WaitForSeconds(invincibilityDuration);

        if (playerCollider != null && enemyCollider != null)
        {
            Physics2D.IgnoreCollision(playerCollider, enemyCollider, false);
        }

        isInvincible = false;
    }

    public void TakeDamage(float amount)
    {
        hp = Mathf.Clamp(hp - amount, 0f, maxHp);
        UpdateHealthUI();

        if (hp <= 0f)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        hp = Mathf.Clamp(hp + amount, 0f, maxHp);
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        if (healthText != null)
        {
            int currentHp = Mathf.RoundToInt(hp);
            int maxHealth = Mathf.RoundToInt(maxHp);
            healthText.text = $"HP: {currentHp} / {maxHealth}";
        }
    }

    private void Die()
    {
        isAlive = false;
        Debug.Log("Player has died.");
        // Add death handling here, such as disabling movement or playing an animation.
    }
}
