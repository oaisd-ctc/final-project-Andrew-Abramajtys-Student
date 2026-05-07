using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DeathRoutine : MonoBehaviour
{
    private Animator anim;
    private float deathTimer = 0.9f;
    private bool started = false;
    public UnityEvent DelayedDeath = new UnityEvent();
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        if (started == true)
        {
            deathTimer -= Time.deltaTime;
            if (deathTimer <= 0)
            {
                Routine();
                started = false;
            }
        }
    }
    public void Routine()
    {
        Death();
        DelayedDeath.Invoke();
    }
    void Death()
    {
        anim.SetBool("die", false);
        anim.SetBool("isAlive", false);
    }
    public void StartDeath()
    {
        started = true;
    }
}
