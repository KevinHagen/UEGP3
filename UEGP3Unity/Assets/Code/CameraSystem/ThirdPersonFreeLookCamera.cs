using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonFreeLookCamera : MonoBehaviour
{
	[Header("Camera Rig Settings")]
	[Tooltip("The object that the camera pivots around.")]
	[SerializeField]
	private Transform _cameraPivot;

	[Header("Camera Speed Settings")]
	[Tooltip("The speed the camera uses to rotate around the x-axis")]
	[SerializeField] private float _pitchSpeed = 100f;
	[Tooltip("The speed the camera uses to rotate around the y-axis")]
	[SerializeField] private float _yawSpeed = 100f;
	
	// Default offset as configured in the scene view
	private Vector3 _defaultPosition;
	
	private void Awake()
	{
		// Subtract our position from the observed object position to receive a directional vector from our position to the objects position
		_defaultPosition = _cameraPivot.position - transform.position;
	}
	
	// Camera movement (positioning, rotation, ...) is handled LateUpdate so we can be sure the observed object has already moved this frame.
	// This helps avoiding jittery movement.
	private void LateUpdate()
	{
		// Fetch inputs from the mouse, multiply by Time.deltaTime so its frame-rate independent
		float deltaYaw = Input.GetAxis("Mouse X") * Time.deltaTime;
		float deltaPitch = Input.GetAxis("Mouse Y") * Time.deltaTime;
		
		// Rotate by the movement delta * speed to receive camera angle
		transform.eulerAngles += new Vector3(deltaPitch * _pitchSpeed, deltaYaw * _yawSpeed, 0);
		//  Most basic use-case: A follower camera where the camera maintains the initial position each frame.
		transform.position = _cameraPivot.position - transform.forward * 4.5f;
	}
}
