using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CraftListUI : MonoBehaviour, IPointerDownHandler
{
  [SerializeField] private Transform craftSlotParent;
  [SerializeField] private GameObject craftSlotPrefab;
  [SerializeField] private List<EquipmentItemData> craftEquipments;
  [SerializeField] private List<CraftSlotUI> craftSlots;

  private void Start()
  {
    AssignCraftSlot();
  }

  private void AssignCraftSlot()
  {
    for (int i = 0; i < craftSlotParent.childCount; i++)
    {
      craftSlots.Add(craftSlotParent.GetChild(i).GetComponent<CraftSlotUI>());
    }
  }

  public void SetupCraftList()
  {
    for (int i = 0; i < craftSlots.Count; i++)
    {
      Destroy(craftSlots[i].gameObject);
    }

    craftSlots = new List<CraftSlotUI>();

    for (int i = 0; i < craftEquipments.Count; i++)
    {
      GameObject instance = Instantiate(craftSlotPrefab, craftSlotParent);
      instance.GetComponent<CraftSlotUI>().SetupCraftSlot(craftEquipments[i]);
    }
  }

  public void OnPointerDown(PointerEventData eventData)
  {
    SetupCraftList();
  }
}
