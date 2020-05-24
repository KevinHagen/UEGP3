using System;
using UnityEngine;

namespace UEGP3.PlayerSystem
{
	[RequireComponent(typeof(CharacterController))]
	public class PlayerController : MonoBehaviour
	{
		[Header("General Settings")] [Tooltip("The speed with which the player moves forward")] [SerializeField]
		private float _movementSpeed = 10f;
		[Header("General Settings")] [Tooltip("The speed with which the player moves forward when sprinting")] [SerializeField]
		private float _sprintSpeed = 10f;
		[Tooltip("The graphical represenation of the character. It is used for things like rotation")] [SerializeField]
		private Transform _graphicsObject = null;
		[Tooltip("Reference to the game camera")] [SerializeField]
		private Transform _cameraTransform = null;

		[Header("Movement")] [Tooltip("Smoothing time for turns")] [SerializeField]
		private float _turnSmoothTime = 0.15f;
		[Tooltip("Smoothing time to reach target speed")] [SerializeField]
		private float _speedSmoothTime = 0.7f;
		[Tooltip("Modifier that manipulates the gravity set in Unitys Physics settings")] [SerializeField]
		private float _gravityModifier = 1.0f;
		[Tooltip("Maximum falling velocity the player can reach")] [Range(1f, 15f)] [SerializeField]
		private float _terminalVelocity = 10f;
		[Tooltip("The height in meters the cahracter can jump")] [SerializeField]
		private float _jumpHeight;
		
		[Header("Ground Check")] [Tooltip("A transform used to detect the ground")] [SerializeField]
		private Transform _groundCheckTransform = null;
		[Tooltip("The radius around transform which is used to detect the ground")] [SerializeField]
		private float _groundCheckRadius = 0.1f;
		[Tooltip("A layermask used to exclude/include certain layers from the \"ground\"")] [SerializeField]
		private LayerMask _groundCheckLayerMask = default;

		public float CurrentYaw => _graphicsObject.rotation.eulerAngles.y;
		
		// Use formula: Mathf.Sqrt(h * (-2) * g)
		private float JumpVelocity => Mathf.Sqrt(_jumpHeight * -2 * Physics.gravity.y);
		
		private bool _isGrounded;
		private float _currentVerticalVelocity;
		private float _currentForwardVelocity;
		private float _speedSmoothVelocity;
		private CharacterController _characterController;
		private PlayerAnimationHandler _playerAnimationHandler;
		
		private void Awake()
		{
			_characterController = GetComponent<CharacterController>();
			_playerAnimationHandler = GetComponent<PlayerAnimationHandler>();
		}

		private void Update()
		{
			// Fetch inputs
			// GetAxisRaw : -1, +1 (0) 
			// GetAxis: [-1, +1]
			float horizontalInput = Input.GetAxisRaw("Horizontal");
			float verticalInput = Input.GetAxisRaw("Vertical");
			bool jumpDown = Input.GetButtonDown("Jump");
			bool isSprinting = Input.GetButton("Sprint");

			// Calculate a direction from input data 
			Vector3 direction = new Vector3(horizontalInput, 0, verticalInput).normalized;
			
			// If the player has given any input, adjust the character rotation
			if (direction != Vector3.zero)
			{
				float lookRotationAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _cameraTransform.eulerAngles.y;
				Quaternion targetRotation = Quaternion.Euler(0, lookRotationAngle, 0);
				_graphicsObject.rotation = Quaternion.Slerp(_graphicsObject.rotation, targetRotation, _turnSmoothTime);
			}

			// Calculate velocity based on gravity formula: delta-y = 1/2 * g * t^2
			// We ignore the 1/2 to safe multiplications and because it feels better.
			// Second Time.deltaTime is done in controller.Move()-call so we save one multiplication here.
			_currentVerticalVelocity += Physics.gravity.y * _gravityModifier * Time.deltaTime;
			
			// Clamp velocity to reach no more than our defined terminal velocity
			_currentVerticalVelocity = Mathf.Clamp(_currentVerticalVelocity, -_terminalVelocity, JumpVelocity);

			// Calculate velocity vector based on gravity and speed
			// (0, 0, z) -> (0, y, z)
			float targetSpeed = (isSprinting ? _sprintSpeed : _movementSpeed) * direction.magnitude;
			_currentForwardVelocity = Mathf.SmoothDamp(_currentForwardVelocity, targetSpeed, ref _speedSmoothVelocity, _speedSmoothTime);
			Vector3 velocity = _graphicsObject.forward * _currentForwardVelocity + Vector3.up * _currentVerticalVelocity;
			
			// Use the direction to move the character controller
			// direction.x * Time.deltaTime, direction.y * Time.deltaTime, ... -> resultingDirection.x * _movementSpeed
			// Time.deltaTime * _movementSpeed = res, res * direction.x, res * direction.y, ...
			_characterController.Move(velocity * Time.deltaTime);
			
			// Check if we are grounded, if so reset gravity
			_isGrounded = Physics.CheckSphere(_groundCheckTransform.position, _groundCheckRadius, _groundCheckLayerMask);
			_playerAnimationHandler.SetGrounded(_isGrounded);
			if (_isGrounded)
			{
				// Reset current vertical velocity
				_currentVerticalVelocity = 0f;
			}

			// If we are grounded and jump was pressed, jump
			if (_isGrounded && jumpDown)
			{
				_playerAnimationHandler.DoJump();
				_currentVerticalVelocity = JumpVelocity;
			}

			_playerAnimationHandler.SetSpeeds(_currentForwardVelocity, _currentVerticalVelocity);
		}
	}
}
