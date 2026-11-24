using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(PlayerMovement)), RequireComponent(typeof(InputManager))]
    public class PlayerController : MonoBehaviour
    {
        private InputManager _inputManager;
        private PlayerMovement _playerMovement;
        private PlayerAttack _playerAttack;
        private PlayerAnimation _playerAnimation;
        private PlayerPickupManager _pickupManager;
        public PlayerDataScrub playerData;

        private void Awake()
        {
            _inputManager = GetComponent<InputManager>();
            _playerMovement = GetComponent<PlayerMovement>();
            _playerAttack = GetComponent<PlayerAttack>();
            _playerAnimation = GetComponent<PlayerAnimation>();
            _pickupManager = GetComponent<PlayerPickupManager>();
        }

        private void Update()
        {
            playerData.playerTransform = transform.position;
            _playerAttack.UpdateAttack(_inputManager.AttackPressed, _inputManager.LookDirection);
            _playerAnimation.UpdateAnimation(_inputManager.MoveDirection, _inputManager.AttackPressed);
            _pickupManager.PickUp(_inputManager.InteractPressed);
            
        }

        private void FixedUpdate()
        {
            _playerMovement.UpdateMovement(_inputManager.MoveDirection);
        }
    }
}