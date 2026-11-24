using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.Rendering;

namespace Player
{
    public class InputManager : MonoBehaviour
    {
        public Vector2 MoveDirection { get; private set; }
        public Vector2 LookDirection { get; private set; }
        public bool InteractPressed { get; private set; }
        public bool AttackPressed { get; private set; }

        private void Start() => LookDirection = Vector2.right;

        private void Update()
        {
            MoveDirection = SetMoveDirection();
            if (MoveDirection != Vector2.zero) LookDirection = MoveDirection;

            InteractPressed = _inputSystemActions.Player.Interact.WasPressedThisFrame();
            AttackPressed = _inputSystemActions.Player.Attack.WasPressedThisFrame();

        }

        private InputSystem_Actions _inputSystemActions;

        private void Awake() => _inputSystemActions = new InputSystem_Actions();
        private void OnEnable() => _inputSystemActions.Enable();
        private void OnDisable() => _inputSystemActions.Disable();

        #region 4DIR

        private bool[] _4Dir = {false, false, false, false};
        private readonly Vector2[] _moveDirection = new [] {Vector2.left, Vector2.right, Vector2.up, Vector2.down};
        
        [SerializeField] private List<Vector2> directions;
        
        private readonly float _stickDeadZone = 0.625f;
    
        private Vector2 SetMoveDirection()
        {
            _4Dir[0] = _inputSystemActions.Player.Move.ReadValue<Vector2>().x < -_stickDeadZone || _inputSystemActions.Player.Left.IsPressed();
            _4Dir[1] = _inputSystemActions.Player.Move.ReadValue<Vector2>().x > _stickDeadZone || _inputSystemActions.Player.Right.IsPressed();
            _4Dir[2] = _inputSystemActions.Player.Move.ReadValue<Vector2>().y > _stickDeadZone || _inputSystemActions.Player.Up.IsPressed();
            _4Dir[3] = _inputSystemActions.Player.Move.ReadValue<Vector2>().y < -_stickDeadZone || _inputSystemActions.Player.Down.IsPressed();

            for (int i = 0; i < _4Dir.Length; i++)
            {
                if (_4Dir[i] && !directions.Contains(_moveDirection[i]))
                {
                    directions.Add(_moveDirection[i]);
                }
                else if (_4Dir[i] == false)
                {
                    directions.Remove(_moveDirection[i]);
                }
            }
            return directions.Count > 0 ? directions.Last() : Vector2.zero;
        }
        #endregion
    
    }
}