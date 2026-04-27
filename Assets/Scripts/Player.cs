using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float hp;
    public float speed;
    public Rigidbody2D rb;
    Vector2 movement;
    public float atk;
    public float def;
    public float jumpForce;
    public float checkRadius = 0.2f;
    public Transform groundCheck;
    public bool isGrounded;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
