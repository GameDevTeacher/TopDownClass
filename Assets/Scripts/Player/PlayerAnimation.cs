using System;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;
    private void Awake() => _animator = GetComponent<Animator>();
    
    private static readonly int Idle = Animator.StringToHash("Idle");
    private static readonly int Move = Animator.StringToHash("Move");
    private static readonly int Attack = Animator.StringToHash("Attack");
    private static readonly int X = Animator.StringToHash("X");
    private static readonly int Y = Animator.StringToHash("Y");

    [SerializeField] private float attackTime; // 0.4333f
    private float _lockedTill;
    private int _currentState;
    
    public void UpdateAnimation(Vector2 moveDirection, bool attackPressed)
    {
        if (moveDirection != Vector2.zero)
        {
            _animator.SetFloat(X, moveDirection.x);
            _animator.SetFloat(Y, moveDirection.y);
        }
        var state = GetState();
        
        if (state == _currentState) return;
        
        _animator.Play(state);
        _currentState = state;
        
        return;
            
        int GetState()
        {
            if (Time.time < _lockedTill) return _currentState;

            if (attackPressed) return LockState(Attack, attackTime);
            return moveDirection != Vector2.zero ? Move : Idle;
            
            int LockState(int s, float t)
            {
                _lockedTill = Time.time + t;
                return s;
            }
        }
    }
}