using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private Rigidbody2D _rigidbody2D;
    private Transform _transform;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _transform = GetComponent<Transform>();
    }

    public void UpdateMovement(Vector2 moveDirection)
    {
        
        _rigidbody2D.linearVelocity = moveDirection * moveSpeed;

        if (moveDirection.x != 0)
        {
            _transform.localScale = new Vector2(Mathf.Sign(moveDirection.x), _transform.localScale.y);
        }
    }
}
