using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCollider : MonoBehaviour
{
    public GameObject currentInteractObject;
    
    private void Update()
    {
        if (Keyboard.current.oKey.wasPressedThisFrame && currentInteractObject != null)
        {
            if (currentInteractObject.TryGetComponent(out IInteractable interactable))
            {
                interactable.Interact();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        currentInteractObject = other.gameObject;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        currentInteractObject = null;
    }
}
