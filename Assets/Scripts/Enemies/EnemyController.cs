using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    #region VARIABLES
    
    [Header("Targets")]
    public Transform _target; // MUST have a trigger box that covers its centre
    
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

    private Vector2 _playerDirection;
    private float _distance;
    private Vector2 _trueDir;
    
    private RaycastHit2D _hit;
    
    
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
        
        _playerDirection = _target.position - transform.position;
        _distance = Vector2.Distance(transform.position, _target.position);
        _trueDir = _playerDirection / _distance;
        // ReSharper disable once Unity.PreferNonAllocApi
        
    }

    public bool hasLineOfSight;
    private void FixedUpdate()
    {
        _hit = Physics2D.Raycast(transform.position, _trueDir, Mathf.Infinity);
        if (_hit.collider == null) return;
        
        hasLineOfSight = _hit.collider.CompareTag("Player");
    }


    #region STATE-FUNCTIONS
    private void FollowState()
    {
        SetMovement(_target.position);

        #region SET-STATE
       
        if (_hit && (Vector2.Distance(_target.position, transform.position) < attackRange && hasLineOfSight))
        {
            _agent.enabled = false;
            _state = States.Attack;
        }
        else if (Vector2.Distance(_target.position, transform.position) < fleeRange)
        {
            _agent.enabled = true;
            _state = States.Flee;
        }

        #endregion
    }

    private void AttackState()
    {
        if (_attackIntervalCounter < Time.time)
        {
            _attackIntervalCounter = Time.time + attackInterval;
        }
        
        #region SET-STATE
       if (Vector2.Distance(_target.position, transform.position) > followRange || !hasLineOfSight)
        {
            _agent.enabled = true;
            _state = States.Follow;
        }
        else if (Vector2.Distance(_target.position, transform.position) < fleeRange)
        {
            _agent.enabled = true;
            _state = States.Flee;
        }
       
        #endregion
        
    }

    private void FleeState()
    {
        Vector3 direction = transform.position - _target.position;
        Vector3 newPosition = transform.position + direction;
        
        SetMovement(newPosition);

        #region SET-STATE
        
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
        transform.position = Vector2.SmoothDamp(transform.position,_agent.nextPosition, ref _velocity, 0.1f);
    }
    private void ResetAgent(bool setValue)
    {
        _agent.enabled = setValue;
        _canMove = setValue;
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, _trueDir, hasLineOfSight ? Color.green : Color.red);
    }
}