using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemTooltipUI : MonoBehaviour
{
  [SerializeField] private TextMeshProUGUI itemNameText;
  [SerializeField] private TextMeshProUGUI itemTypeText;
  [SerializeField] private TextMeshProUGUI itemDescriptionText;
  [SerializeField] private int defaultFontSize = 32;

  public void ShowToolTip(EquipmentItemData item)
  {
    if (item == null)
    {
      return;
    }

    itemNameText.text = item.itemName;
    itemTypeText.text = item.equipmentType.ToString();
    itemDescriptionText.text = item.GetDescription();
    AdjustFontSize(itemNameText);


    gameObject.SetActive(true);
  }

  public void AdjustFontSize(TextMeshProUGUI _text)
  {
    if (_text.text.Length > 8)
      _text.fontSize = _text.fontSize * .8f;
  }

  public void HideToolTip()
  {
    itemNameText.fontSize = defaultFontSize;

    gameObject.SetActive(false);
  }
}
