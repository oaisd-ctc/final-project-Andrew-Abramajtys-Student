using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAttack : MonoBehaviour
{
    public GameObject attackArea;
    private bool attacking = false;
    private float timeToAttack = 0.25f;
    private float timer = 0f;
    private Animator anim;
    public UnityEvent OnAttack = new UnityEvent();
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && !attacking)
        {
            Attack();
            anim.SetTrigger("attack");
        }
        if (attacking)
        {
            timer += Time.deltaTime;
            if (timer >= timeToAttack)
            {
                timer = 0;
                attacking = false;
                attackArea.SetActive(false);
            }
        }
    }
    private void Attack()
    {
        attacking = true;
        attackArea.SetActive(true);
        print("swing");
        OnAttack.Invoke();
    }
}
