using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public enum ItemType
{
  Material,
  Equipment
}

[CreateAssetMenu(fileName = "itemData", menuName = "Data/Item")]
public class ItemData : ScriptableObject
{
  public ItemType itemType = ItemType.Material;
  public string itemName;
  public Sprite icon;
  [Range(0, 100)]
  public float dropChance;
}
