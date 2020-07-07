using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UEGP3.InventorySystem;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class TheAddItemToInventoryMethod
    {
        [Test]
        public void AddANewItemToANewInventory()
        {
            // Arrange 
            Inventory theNewInventory = ScriptableObject.CreateInstance<Inventory>();
            ItemType someItemType = ScriptableObject.CreateInstance<ItemType>();
            Item someItem = ScriptableObject.CreateInstance<HealItem>();
            ItemBag someBag = ScriptableObject.CreateInstance<ItemBag>();

            someItem.ItemType = someItemType;
            someBag.SupportedItemTypes.Add(someItemType);
            theNewInventory.Bags = new List<ItemBag>
                                   {
                                       someBag
                                   };

            // Act
            theNewInventory.TryAddItem(someItem);
            
            // Assert
            Assert.IsTrue(theNewInventory.Bags[0].InventoryItems.ContainsKey(someItem));
        }
    }
}
