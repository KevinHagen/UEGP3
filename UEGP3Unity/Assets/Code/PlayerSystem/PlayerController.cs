using System;
using UnityEngine;

namespace UEGP3.PlayerSystem
{
	[RequireComponent(typeof(CharacterController))]
	public class PlayerController : MonoBehaviour
	{
		[Header("General Settings")] [Tooltip("The speed with which the player moves forward")] [SerializeField]
		private float _movementSpeed = 10f;
		[Tooltip("The graphical represenation of the character. It is used for things like rotation")] [SerializeField]
		private Transform _graphicsObject = null;
		
		
		private CharacterController _characterController;
		
		private void Awake()
		{
			_characterController = GetComponent<CharacterController>();
		}

		private void Update()
		{
			// Fetch inputs
			// GetAxisRaw : -1, +1 (0) 
			// GetAxis: [-1, +1]
			float horizontalInput = Input.GetAxisRaw("Horizontal");
			float verticalInput = Input.GetAxisRaw("Vertical");

			// Calculate a direction from input data 
			Vector3 direction = new Vector3(horizontalInput, 0, verticalInput).normalized;
			
			// If the player has given any input, adjust the character rotation
			if (direction != Vector3.zero)
			{
				float lookRotationAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
				_graphicsObject.rotation = Quaternion.Euler(0, lookRotationAngle, 0);
			}

			// Use the direction to move the character controller
			// direction.x * Time.deltaTime, direction.y * Time.deltaTime, ... -> resultingDirection.x * _movementSpeed
			// Time.deltaTime * _movementSpeed = res, res * direction.x, res * direction.y, ...
			_characterController.Move(_graphicsObject.forward * (Time.deltaTime * _movementSpeed * direction.magnitude));
		}
	}
}
