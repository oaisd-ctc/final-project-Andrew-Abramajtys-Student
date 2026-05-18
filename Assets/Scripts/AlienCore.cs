using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AlienCore : MonoBehaviour
{
    public UnityEvent OnHit = new UnityEvent();
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerAttack"))
        {
            OnHit.Invoke();
            Destroy(this.gameObject);
        }
    }
}
