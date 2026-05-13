using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Medkit : MonoBehaviour
{
    public UnityEvent Heal = new UnityEvent();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Heal.Invoke();
            Destroy(gameObject);
        }
    }
}
