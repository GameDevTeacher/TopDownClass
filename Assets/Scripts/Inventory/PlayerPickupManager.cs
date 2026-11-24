using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerPickupManager : MonoBehaviour
{
    
    public static SerializedDictionary<GameObject, int> InventoryDictionary 
    = new SerializedDictionary<GameObject, int>();

    [SerializeField] 
    public SerializedDictionary<GameObject, int> visibleInventory;

    public bool canPickUp = false;
    public IPickupable currentPickup;

    private void Update()
    {
        // Each 30th Frame - Run this Statement
        if (Time.frameCount % 30 == 0)
        {
            visibleInventory = InventoryDictionary;
        }

        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            InventoryDictionary.Clear();
        }
    }

    public void PickUp(bool interact)
    {
        if (interact && canPickUp)
        {
            currentPickup?.OnPickup();
        }
        
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IPickupable pickupable))
        {
            canPickUp = true;
            currentPickup = pickupable;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out IPickupable pickupable))
        {
            canPickUp = false;
            currentPickup = null;
        }
    }
}
