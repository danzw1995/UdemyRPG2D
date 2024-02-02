using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CraftSlotUI : ItemSlotUI
{
  protected override void Awake()
  {
    base.Awake();
  }
  private void OnEnable()
  {
    UpdateItemSlotUI(item);
  }

  public void SetupCraftSlot(EquipmentItemData _data)
  {
    if (_data == null)
      return;

    item.itemData = _data;

    image.sprite = _data.icon;
    itemText.text = _data.itemName;
  }

  public override void OnPointerDown(PointerEventData eventData)
  {
    EquipmentItemData craftData = item.itemData as EquipmentItemData;
    Inventory.instance.CanCraft(craftData, craftData.craftingMaterials);
  }
}
