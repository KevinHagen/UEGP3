using System;
using UnityEngine;

namespace UEGP3.CutsceneSystem
{
	/// <summary>
	/// Sets a specified transform to a target transform upon execution
	/// </summary>
	[Serializable]
	public class ResetTransformRule : TimelineResetRule<Transform, Transform>
	{
		public override void ExecuteRule()
		{
			_objectToReset.position = _postPlaybackState.position;
			_objectToReset.rotation = _postPlaybackState.rotation;
		}
	}
}