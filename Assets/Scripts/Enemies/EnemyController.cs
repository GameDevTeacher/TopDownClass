using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    #region VARIABLES
    
    [Header("Targets")]
    public Transform _target;
    public Transform _currentTarget;
    
    [Header("Movement")]
    private Vector2 _velocity = Vector2.zero;
    public bool _canMove = true;
    
    [Header("Components")]
    private NavMeshAgent _agent;

    [Header("Ranges")] 
    [SerializeField] private float followRange = 8f;
    [SerializeField] private float attackRange = 6f;
    [SerializeField] private float fleeRange = 4f;

    [Header("Attack")] 
    [SerializeField] private float attackInterval;
    private float _attackIntervalCounter;
    private bool _isDead;
    #endregion

    public enum States
    {
        Follow,
        Attack,
        Flee
    }
    public States _state;
    
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _state = States.Follow;
    }
    private void OnEnable()
    {
        _agent.updateRotation = false;
        _agent.updatePosition = false;
        _agent.updateUpAxis = false;
        _isDead = false;
    }


    private void Update()
    {
        if (!_canMove) return;
        switch (_state)
        {
            case States.Follow:
                FollowState();
                break;
            case States.Flee:
                FleeState();
                break;
            case States.Attack:
                AttackState();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    #region STATE-FUNCTIONS
    private void FollowState()
    {
        SetMovement(_currentTarget.position);

        #region SET-STATE
        var hit2D = Physics2D.Raycast(transform.position, _target.position - transform.position, Mathf.Infinity);

        if (hit2D.transform.CompareTag("Player"))
        {
            if (SetStateIfLess(attackRange, false, States.Attack));
        }
        else if (SetStateIfLess(fleeRange, true, States.Flee));
        
       
        /*if (Vector2.Distance(_target.position, transform.position) < attackRange && hit2D.transform.CompareTag("Player"))
        {
            _agent.enabled = false;
            _state = States.Attack;
        }
        else if (Vector2.Distance(_target.position,
                transform.position) < fleeRange)
        {
            _agent.enabled = true;
            _state = States.Flee;
        }*/

        #endregion
    }

    private void AttackState()
    {
        if (_attackIntervalCounter < Time.time)
        {
            print("Attack");
            _attackIntervalCounter = Time.time + attackInterval;
        }
        
        #region SET-STATE
        var hit2D = Physics2D.Raycast(transform.position, _target.position - transform.position, Mathf.Infinity);
        
        print(hit2D.transform.name);
        
        if (!hit2D.transform.CompareTag("Player"))
        {
            _agent.enabled = true;
            _state = States.Follow;
        }
        
        else if (SetStateIfGreater(followRange, true, States.Follow) || SetStateIfLess(fleeRange, true, States.Flee)) ;
        /*
        else if (Vector2.Distance(_target.position, 
                transform.position) > followRange)
        {
            _agent.enabled = true;
            _state = States.Follow;
        }
        else if (Vector2.Distance(_target.position,
                     transform.position) < fleeRange)
        {
            _agent.enabled = true;
            _state = States.Flee;
        }*/
        
        #endregion
        
    }

    private void FleeState()
    {
        Vector3 direction = transform.position - _target.position;
        Vector3 newPosition = transform.position + direction;
        
        SetMovement(newPosition);

        #region SET-STATE

        //if (SetStateIfGreater(attackRange, false, States.Attack) || SetStateIfGreater(followRange, true, States.Follow)) ;
        
        
        if (Vector2.Distance(_target.position, transform.position) > attackRange)
        {
            _agent.enabled = false;
            _state = States.Attack;
        }
        else if (Vector2.Distance(_target.position, transform.position) > followRange)
        {
            _agent.enabled = true;
            _state = States.Follow;
        }
        #endregion
    }
    
    #endregion
    private void SetMovement(Vector3 target)
    {
        // Set end goal for agent movement
        _agent.SetDestination(target);
        
        // Smoothly move towards next position in chain towards end goal
        transform.position = Vector2.SmoothDamp(
            transform.position, // Where we are
            _agent.nextPosition, // Where we go
            ref _velocity, // Reference to Velocity
            0.1f // Max Speed
            );
    }
    private void ResetAgent(bool setValue)
    {
        _agent.enabled = setValue;
        _canMove = setValue;
    }
    public void SetTarget(Transform target)
    {
        _currentTarget = target;
        _target = target;
    }
    
    private bool SetStateIfGreater(float distance, bool agentEnable, States state)
    {
        var Dist = Vector2.Distance(_target.position, transform.position) > distance;
        if (Dist)
        {
            _agent.enabled = agentEnable;
            _state = state;
        }

        return Dist;
    } 
    private bool SetStateIfLess(float distance, bool agentEnable, States state)
    {
        var Dist = Vector2.Distance(_target.position, transform.position) < distance;
        if (Dist)
        {
            _agent.enabled = agentEnable;
            _state = state;
        }
        return Dist;
    }

    public void OnDrawGizmosSelected()
    {
        Debug.DrawRay(transform.position, _target.position-transform.position, Color.red);
    }
}