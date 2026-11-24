using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable<float>
{
    private float health;
    private float armour;
    
    public void Damage(float damage)
    {
        damage -= armour;
        health -= damage;
    }
}