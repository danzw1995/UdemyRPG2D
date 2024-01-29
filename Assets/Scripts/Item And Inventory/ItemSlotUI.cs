using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour, IPointerDownHandler
{
  private Image image;
  private TextMeshProUGUI itemText;
  public InventoryItem item;

  private void Awake()
  {
    image = GetComponent<Image>();
    itemText = GetComponentInChildren<TextMeshProUGUI>();
  }


  public void UpdateItemSlotUI(InventoryItem _item)
  {
    item = _item;
    if (item != null)
    {
      image.color = Color.white;
      image.sprite = item.itemData.icon;
      if (item.stackSize > 1)
      {
        itemText.text = item.stackSize.ToString();
      }
      else
      {
        itemText.text = "";
      }

    }
  }

  public void ClearItemSlotUI()
  {
    item = null;
    image.color = Color.clear;
    image.sprite = null;
    itemText.text = "";
  }

  public virtual void OnPointerDown(PointerEventData eventData)
  {
    if (Input.GetKey(KeyCode.LeftControl))
    {
      Inventory.instance.RemoveItem(item.itemData);
      return;

    }

    if (item.itemData.itemType != ItemType.Equipment)
    {
      return;
    }
    Inventory.instance.EquipItem(item.itemData);
  }
}
