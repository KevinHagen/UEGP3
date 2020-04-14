using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonFreeLookCamera : MonoBehaviour
{
	private const float FullSpinDegree = 360f;
	
	[Header("Camera Rig Settings")]
	[Tooltip("The object that the camera pivots around.")]
	[SerializeField]
	private Transform _cameraPivot;

	[Header("Camera Speed Settings")]
	[Tooltip("The speed the camera uses to rotate around the x-axis")]
	[SerializeField] private float _pitchSpeed = 100f;
	[Tooltip("The speed the camera uses to rotate around the y-axis")]
	[SerializeField] private float _yawSpeed = 100f;

	[Header("Clamp Values")] [Tooltip("Smallest amount yaw allowed. -360 allows a full-spin.")] [SerializeField]
	private float _minimumYaw = -FullSpinDegree;
	[Tooltip("Biggest amount yaw allowed. 360 allows a full-spin.")]
	[SerializeField] private float _maximumYaw = FullSpinDegree;
	[Tooltip("Smallest amount pitch allowed. -360 allows a full-spin.")] [SerializeField]
	private float _minimumPitch = -20f;
	[Tooltip("Biggest amount pitch allowed. 360 allows a full-spin.")]
	[SerializeField] private float _maximumPitch = 80f;
	
	// Default offset as configured in the scene view
	private Vector3 _defaultPosition;
	
	// Current yaw and pitch stored in member variables
	private float _currentPitch;
	private float _currentYaw;
	
	private void Awake()
	{
		// Subtract our position from the observed object position to receive a directional vector from our position to the objects position
		_defaultPosition = _cameraPivot.position - transform.position;
	}

	private void Update()
	{
		// Fetch inputs from the mouse, multiply by Time.deltaTime so its frame-rate independent
		float deltaYaw = Input.GetAxis("Mouse X") * Time.deltaTime * _yawSpeed;
		float deltaPitch = Input.GetAxis("Mouse Y") * Time.deltaTime * _pitchSpeed;

		_currentYaw += deltaYaw;
		_currentPitch += deltaPitch;

		_currentYaw = ClampRotation(_currentYaw, _minimumYaw, _maximumYaw);
		_currentPitch = ClampRotation(_currentPitch, _minimumPitch, _maximumPitch);
	}

	// Camera movement (positioning, rotation, ...) is handled LateUpdate so we can be sure the observed object has already moved this frame.
	// This helps avoiding jittery movement.
	private void LateUpdate()
	{
		Quaternion currentRotation = Quaternion.Euler(_currentPitch, _currentYaw, 0);
		transform.position = _cameraPivot.position + currentRotation * new Vector3(0, 0, -4.5f);
		transform.LookAt(_cameraPivot.position);
	}

	private float ClampRotation(float currentAngle, float minimumAngle, float maximumAngle)
	{
		// Add a full turn, in case we made a full left-turn. This means after -360° we start -1° rotation again
		if (currentAngle < -FullSpinDegree)
		{
			currentAngle += FullSpinDegree;
		}
		
		// Subtract a full turn, in case we made a full right-turn. This means after 360° we start 1° rotation again
		if (currentAngle > FullSpinDegree)
		{
			currentAngle -= FullSpinDegree;
		}
		
		// Clamp the angle between minimum and maximum allowed rotation
		return Mathf.Clamp(currentAngle, minimumAngle, maximumAngle);
	}
}
