using System;
using UnityEngine;

namespace UEGP3.CutsceneSystem
{
	/// <summary>
	/// Generic rule that can be inherited from to create timeline skip rules.
	/// </summary>
	/// <typeparam name="TResetObject">The object that needs to be reset</typeparam>
	/// <typeparam name="TPostState">The state the object is going to be reset to</typeparam>
	[Serializable]
	public abstract class TimelineResetRule<TResetObject, TPostState>
	{
		[SerializeField] [Tooltip("The object that needs to be reset")]
		protected TResetObject _objectToReset;
		[SerializeField] [Tooltip("The state the object is going to be reset to")]
		protected TPostState _postPlaybackState;
		
		/// <summary>
		/// Executes the rule and sets the given object to the specified post-state as specified in the method.
		/// </summary>
		public abstract void ExecuteRule();
	}
}