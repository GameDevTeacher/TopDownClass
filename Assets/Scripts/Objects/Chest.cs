using UnityEngine;

public class Chest : MonoBehaviour, IDamageable<float>, IInteractable
{
    float health;
    public void Damage(float damage)
    {
        health -= damage;
    }

    public void Interact()
    {
        // OPEN CHEST
    }
}
