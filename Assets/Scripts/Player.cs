using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public float maxHp = 100f;
    public float hp = 100f;
    public Text healthText;
    public float baseSpeed;
    private float speed;
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
    public Sprite deathSprite;
    private SpriteRenderer spriteRenderer;
    private bool facingRight = true;
    public Collider2D attackCollider;
    private Animator anim;
    public UnityEvent OnDie = new UnityEvent();
    public UnityEvent OnHit = new UnityEvent();

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        hp = Mathf.Clamp(hp, 0f, maxHp);
        UpdateHealthUI();
        spriteRenderer = GetComponent<SpriteRenderer>();
        // Get the attack collider from child objects or this object
        attackCollider = GetComponentInChildren<Collider2D>();
    }

    void Update()
    {
        if(!isAlive)
        {
            return; // Skip update if player is dead
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = baseSpeed * 2;
        }
        else
        {
            speed = baseSpeed;
        }

        float moveX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float moveZ = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        transform.Translate(moveX, 0, moveZ);
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }

        // Flip player based on movement direction
        if (Input.GetAxis("Horizontal") > 0 && !facingRight)
        {
            Flip();
        }
        else if (Input.GetAxis("Horizontal") < 0 && facingRight)
        {
            Flip();
        }

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
        if (collision.gameObject.CompareTag("Kill"))
        {
            TakeDamage(999f);
            print("ded");
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
        OnHit.Invoke();
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
        anim.SetBool("die", true);
        isAlive = false;
        OnDie.Invoke();
        print("u r ded. not big soup rice.");
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}