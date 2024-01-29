using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipItemSlotUI : ItemSlotUI
{
  public EquipmentType slotType;

  private void OnValidate()
  {
    gameObject.name = "EquipType " + slotType.ToString();
  }

  public override void OnPointerDown(PointerEventData eventData)
  {
    Inventory.instance.UnEquipItem(item.itemData as EquipmentItemData);
    Inventory.instance.UpdateItemSlotUI();
  }
}
