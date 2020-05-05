using System.Collections.Generic;
using UnityEngine;

namespace UEGP3.InventorySystem
{
	[CreateAssetMenu(menuName = "UEGP3/Inventory System/New Inventory", fileName = "New Inventory")]
	public class Inventory : ScriptableObject
	{
		private Dictionary<Item, int> _inventoryItems = new Dictionary<Item, int>();

		/// <summary>
		/// Prints the inventory to the console.
		/// </summary>
		public void ShowInventory()
		{
			Debug.Log(this);
		}
		
		public bool TryAddItem(Item item)
		{
			bool success = false;

			// Item is not yet in inventory, add it
			if (!_inventoryItems.ContainsKey(item))
			{
				_inventoryItems.Add(item, 1);
				success = true;
			}
			// Item is already in inventory, stack it up if possible
			else
			{
				// Only items that are not unique can be stacked
				if (!item.IsUnique)
				{
					_inventoryItems[item]++;
					success = true;
				}
			}
			return success;
		}

		public override string ToString()
		{
			// "String-Interpolation": $ before a string "" allows us to use variables in {} 
			// inventory = "Inventory " + name + " contains:\r\n" is the same as the line below, but nicer! :) 
			string inventory = $"Inventory {name} contains:\r\n";

			foreach (KeyValuePair<Item,int> inventoryItem in _inventoryItems)
			{
				inventory += $"[{inventoryItem.Key.ItemName} - {inventoryItem.Value}]\r\n";
			}
			
			return inventory;
		}
	}
}