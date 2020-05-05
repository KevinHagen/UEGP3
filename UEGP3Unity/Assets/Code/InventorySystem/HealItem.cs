﻿using UnityEngine;

namespace UEGP3.InventorySystem
{
	[CreateAssetMenu(fileName = "New HealItem", menuName = "UEGP3/Inventory System/New Heal Item")]
	public class HealItem : Item
	{
		[SerializeField] private float _restoredHealth = 5.0f;
		
		public override void UseItem()
		{
			Debug.Log($"Healing by {_restoredHealth}");
		}
	}
}