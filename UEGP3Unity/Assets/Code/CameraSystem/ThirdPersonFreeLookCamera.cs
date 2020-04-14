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
		//  Most basic use-case: A follower camera where the camera maintains the initial position each frame.
		transform.position = _cameraPivot.position - _defaultPosition;
	}
}
