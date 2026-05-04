using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    public float damage;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            print("pls work");
            Alien hp = other.GetComponent<Alien>();
            if (hp != null)
            {
                hp.TakeDamage(damage);
            }
        }
    }
}
