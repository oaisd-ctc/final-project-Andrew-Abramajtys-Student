using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractObject : MonoBehaviour
{
    public string interactionText;
    public UnityEvent OnInteract = new UnityEvent();
    public string GetInteractionText()
    {
        return interactionText;
    }
    public void Interact()
    {
        OnInteract.Invoke();
    }
}
