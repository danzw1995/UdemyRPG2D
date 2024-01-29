using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemDrop : ItemDrop
{
  [Header("Player's drop")]
  [Range(0, 100)]
  [SerializeField] private float chanceLoseItem;
  [Range(0, 100)]
  [SerializeField] private float chanceLoseMaterial;

  public override void GenerateDrop()
  {
    LoseEquipment();
    LoseMaterial();
  }

  private void LoseEquipment()
  {
    Inventory inventory = Inventory.instance;

    List<InventoryItem> itemsToUnEquip = new List<InventoryItem>();
    foreach (InventoryItem item in inventory.GetCurrentEquipment())
    {
      if (Random.Range(0, 100) < chanceLoseItem)
      {
        DropItem(item.itemData);
        itemsToUnEquip.Add(item);
      }
    }



    for (int i = 0; i < itemsToUnEquip.Count; i++)
    {
      inventory.UnEquipItem(itemsToUnEquip[i].itemData as EquipmentItemData);
    }

  }

  private void LoseMaterial()
  {
    Inventory inventory = Inventory.instance;

    List<InventoryItem> itemsToLose = new List<InventoryItem>();
    foreach (InventoryItem item in inventory.GetCurrentMaterial())
    {
      if (Random.Range(0, 100) < chanceLoseMaterial)
      {
        DropItem(item.itemData);
        itemsToLose.Add(item);
      }
    }



    for (int i = 0; i < itemsToLose.Count; i++)
    {
      inventory.RemoveItem(itemsToLose[i].itemData);
    }
  }
}
