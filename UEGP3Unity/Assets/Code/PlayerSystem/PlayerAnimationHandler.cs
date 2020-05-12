using System;
using UnityEngine;

namespace UEGP3.PlayerSystem
{
	public class PlayerAnimationHandler : MonoBehaviour
	{
		private static readonly int MovementSpeed = Animator.StringToHash("MovementSpeed");
		
		[SerializeField] private Animator _animator;
		private float _movementSpeed;

		private void Update()
		{
			_animator.SetFloat(MovementSpeed, _movementSpeed);
		}

		public void SetMovementSpeed(float movementSpeed)
		{
			_movementSpeed = movementSpeed;
		}
	}
}