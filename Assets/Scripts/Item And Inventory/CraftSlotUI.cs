using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CraftSlotUI : ItemSlotUI
{
  private void OnEnable()
  {
    UpdateItemSlotUI(item);
  }

  public override void OnPointerDown(PointerEventData eventData)
  {
    EquipmentItemData craftData = item.itemData as EquipmentItemData;
    Inventory.instance.Cancraft(craftData, craftData.craftingMaterials);
  }
}
