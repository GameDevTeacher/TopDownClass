using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CompositeInteraction : MonoBehaviour, IInteractable
{
    public List<GameObject> interactableGameObjects;
    public UnityEvent onInteract;
    
    public void Interact()
    {
        foreach (var interactable in interactableGameObjects)
        {
            interactable.TryGetComponent(out IInteractable interactableObject);
            interactableObject?.Interact();
            onInteract.Invoke();
        }
    }
}
