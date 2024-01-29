using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
  public static Inventory instance;

  public List<InventoryItem> inventoryItems;
  public Dictionary<ItemData, InventoryItem> inventoryItemDict;

  public List<InventoryItem> stashItems;
  public Dictionary<ItemData, InventoryItem> stashItemDict;

  public List<InventoryItem> equippedItems;
  public Dictionary<EquipmentItemData, InventoryItem> equippedItemDict;

  public List<ItemData> startingItems;

  [Header("Inventory UI")]
  [SerializeField] private Transform itemSlotParent;
  private ItemSlotUI[] itemSlots;
  [SerializeField] private Transform stashItemSlotParent;
  private ItemSlotUI[] stashItemSlots;

  [SerializeField] private Transform equippedItemSlotParent;
  private EquipItemSlotUI[] equippedItemSlots;

  private void Awake()
  {
    if (instance == null)
    {
      instance = this;
    }
    else
    {
      Destroy(gameObject);
    }
  }

  private void Start()
  {
    inventoryItemDict = new Dictionary<ItemData, InventoryItem>();
    inventoryItems = new List<InventoryItem>();

    stashItemDict = new Dictionary<ItemData, InventoryItem>();
    stashItems = new List<InventoryItem>();

    equippedItemDict = new Dictionary<EquipmentItemData, InventoryItem>();
    equippedItems = new List<InventoryItem>();

    itemSlots = itemSlotParent.GetComponentsInChildren<ItemSlotUI>();

    stashItemSlots = stashItemSlotParent.GetComponentsInChildren<ItemSlotUI>();

    equippedItemSlots = equippedItemSlotParent.GetComponentsInChildren<EquipItemSlotUI>();

    AddStartingItem();
  }

  private void AddStartingItem()
  {
    foreach (ItemData item in startingItems)
    {
      AddItem(item);
    }
  }

  public void UpdateItemSlotUI()
  {
    for (int i = 0; i < itemSlots.Length; i++)
    {
      itemSlots[i].ClearItemSlotUI();
    }
    for (int i = 0; i < stashItemSlots.Length; i++)
    {
      stashItemSlots[i].ClearItemSlotUI();
    }

    for (int i = 0; i < equippedItemSlots.Length; i++)
    {
      equippedItemSlots[i].ClearItemSlotUI();
    }



    for (int i = 0; i < inventoryItems.Count; i++)
    {
      itemSlots[i].UpdateItemSlotUI(inventoryItems[i]);
    }

    for (int i = 0; i < stashItems.Count; i++)
    {
      stashItemSlots[i].UpdateItemSlotUI(stashItems[i]);
    }

    for (int i = 0; i < equippedItems.Count; i++)
    {
      equippedItemSlots[i].UpdateItemSlotUI(equippedItems[i]);
    }
  }

  public void EquipItem(ItemData itemData)
  {

    EquipmentItemData data = itemData as EquipmentItemData;

    UnEquipItem(data);
    RemoveStashItem(itemData);

    InventoryItem equipItem = new InventoryItem(data);
    data.AddModifiers();
    equippedItems.Add(equipItem);
    equippedItemDict.Add(data, equipItem);

    UpdateItemSlotUI();
  }

  public void UnEquipItem(EquipmentItemData equipmentItem)
  {
    if (equippedItemDict.TryGetValue(equipmentItem, out InventoryItem item))
    {
      equippedItemDict.Remove(equipmentItem);
      equippedItems.Remove(item);
      (item.itemData as EquipmentItemData)?.RemoveModifiers();

      AddStashItem(item.itemData);
    }
  }

  public void AddItem(ItemData itemData)
  {
    if (itemData.itemType == ItemType.Material)
    {
      AddInventoryItem(itemData);
    }
    else if (itemData.itemType == ItemType.Equipment)
    {
      AddStashItem(itemData);
    }

    UpdateItemSlotUI();
  }

  public void AddInventoryItem(ItemData itemData)
  {
    if (inventoryItemDict.TryGetValue(itemData, out InventoryItem inventoryItem))
    {
      inventoryItem.AddStack();
    }
    else
    {
      InventoryItem item = new InventoryItem(itemData);
      inventoryItems.Add(item);
      inventoryItemDict.Add(itemData, item);
    }
  }

  public void AddStashItem(ItemData itemData)
  {
    if (stashItemDict.TryGetValue(itemData, out InventoryItem stashItem))
    {
      stashItem.AddStack();
    }
    else
    {
      InventoryItem item = new InventoryItem(itemData);
      stashItems.Add(item);
      stashItemDict.Add(itemData, item);
    }

  }

  public void RemoveItem(ItemData itemData)
  {
    if (itemData.itemType == ItemType.Material)
    {
      RemoveInventoryItem(itemData);
    }
    else if (itemData.itemType == ItemType.Equipment)
    {
      RemoveStashItem(itemData);
    }
    UpdateItemSlotUI();

  }

  public void RemoveInventoryItem(ItemData itemData)
  {
    if (inventoryItemDict.TryGetValue(itemData, out InventoryItem inventoryItem))
    {
      if (inventoryItem.stackSize <= 1)
      {
        inventoryItemDict.Remove(itemData);
        inventoryItems.Remove(inventoryItem);
      }
      else
      {
        inventoryItem.RemoveStack();
      }
    }
  }

  public void RemoveStashItem(ItemData itemData)
  {
    if (stashItemDict.TryGetValue(itemData, out InventoryItem stashItem))
    {
      if (stashItem.stackSize <= 1)
      {
        stashItemDict.Remove(itemData);
        stashItems.Remove(stashItem);
      }
      else
      {
        stashItem.RemoveStack();
      }
    }
  }

  public bool Cancraft(EquipmentItemData itemToCraft, List<InventoryItem> requiredMaterials)
  {
    List<InventoryItem> materialsToRemove = new List<InventoryItem>();

    for (int i = 0; i < requiredMaterials.Count; i++)
    {
      if (stashItemDict.TryGetValue(requiredMaterials[i].itemData, out InventoryItem stashItem))
      {
        if (stashItem.stackSize < requiredMaterials[i].stackSize)
        {
          return false;
        }
        else
        {
          materialsToRemove.Add(stashItem);
        }
      }
    }

    for (int i = 0; i < materialsToRemove.Count; i++)
    {
      RemoveItem(materialsToRemove[i].itemData);
    }
    AddItem(itemToCraft);
    return true;
  }

  public List<InventoryItem> GetCurrentEquipment()
  {
    return equippedItems;
  }

  public List<InventoryItem> GetCurrentMaterial()
  {
    return stashItems;
  }

  public EquipmentItemData GetEquipment(EquipmentType type)
  {
    foreach (EquipmentItemData equipmentItem in equippedItemDict.Keys)
    {
      if (equipmentItem.equipmentType == type)
      {
        return equipmentItem;
      }
    }
    return null;
  }
}
