using System;
using UEGP3.InventorySystem;

namespace UEGP3.CutsceneSystem
{
	/// <summary>
	/// Adds an item to an inventory upon execution
	/// </summary>
	[Serializable]
	public class AddToInventoryRule : TimelineResetRule<Item, Inventory>
	{
		public override void ExecuteRule()
		{
			_postPlaybackState.TryAddItem(_objectToReset);
		}
	}
}