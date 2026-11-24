using System;
using UnityEngine;

public class Animal : MonoBehaviour
{
    public float health;

    public void Death()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}