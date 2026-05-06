using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathRoutine : MonoBehaviour
{
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void Routine()
    {
        Invoke(nameof(Death), 0.9f);
    }
    void Death()
    {
        anim.SetBool("die", false);
        anim.SetBool("isAlive", false);
    }
}
