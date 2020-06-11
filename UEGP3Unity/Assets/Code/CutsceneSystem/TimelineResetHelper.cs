using System.Collections.Generic;
using UnityEngine;

namespace UEGP3.CutsceneSystem
{
	/// <summary>
	/// Helper class that takes a number of inputs and sets them to a defined state once a timeline is skipped.
	/// </summary>
	public class TimelineResetHelper : MonoBehaviour
	{
		[SerializeField] [Tooltip("All GameObjects that get either moved/rotated need to be configured in here")]
		private List<ResetTransformRule> _transformRules = new List<ResetTransformRule>();
		[SerializeField] [Tooltip("If we collect an item in a cutscene it has to be added here")]
		private List<AddToInventoryRule> _itemRules = new List<AddToInventoryRule>();
		
		/// <summary>
		/// Executes all available rules that are configured to trigger once a timeline is skipped.
		/// </summary>
		public void CleanupForPostPlaybackState()
		{
			// execute all transform related rules
			foreach (ResetTransformRule resetTransformRule in _transformRules)
			{
				resetTransformRule.ExecuteRule();
			}

			// execute all item related rules
			foreach (AddToInventoryRule resetItemRule in _itemRules)
			{
				resetItemRule.ExecuteRule();
			}
		}
	}
}