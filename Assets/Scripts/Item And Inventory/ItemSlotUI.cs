using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
  [SerializeField] public Image image;
  [SerializeField] public TextMeshProUGUI itemText;
  public InventoryItem item;

  protected UIManager uIManager;

  protected virtual void Awake()
  {
    uIManager = GetComponentInParent<UIManager>();
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
    if (item == null || item.itemData == null)
    {
      return;
    }
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

  public virtual void OnPointerEnter(PointerEventData eventData)
  {
    if (item == null)
    {
      return;
    }

    uIManager.itemTooltip.ShowToolTip(item.itemData as EquipmentItemData);

  }

  public virtual void OnPointerExit(PointerEventData eventData)
  {
    if (item == null)
    {
      return;
    }

    uIManager.itemTooltip.HideToolTip();
  }
}
