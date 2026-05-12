using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AttackHitbox : MonoBehaviour
{
    public float damage;
    public UnityEvent OnHit = new UnityEvent();


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            print("pls work");
            Alien hp = other.GetComponent<Alien>();
            OnHit.Invoke();
            if (hp != null)
            {
                hp.TakeDamage(damage);
            }
        }
    }
}
