using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float projectileDamage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IDamageable<float> damageable))
        {
           damageable.Damage(projectileDamage);
           Destroy(gameObject);
        }
    }
}