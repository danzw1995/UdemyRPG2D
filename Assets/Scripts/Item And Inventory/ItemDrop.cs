using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
  [SerializeField] private int possibleItemDrop;
  [SerializeField] private ItemData[] possibleDrop;

  private List<ItemData> dropList = new List<ItemData>();
  [SerializeField] private GameObject dropPrefab = null;
  public virtual void GenerateDrop()
  {
    dropList.Clear();
    for (int i = 0; i < possibleDrop.Length; i++)
    {
      if (Random.Range(0, 100) < possibleDrop[i].dropChance)
      {
        dropList.Add(possibleDrop[i]);
      }
    }
    if (dropList.Count <= possibleItemDrop)
    {
      foreach (ItemData itemToDrop in dropList)
      {
        DropItem(itemToDrop);
      }
      return;
    }

    for (int i = 0; i < possibleItemDrop; i++)
    {
      ItemData itemToDrop = dropList[Random.Range(0, dropList.Count - 1)];
      DropItem(itemToDrop);
      dropList.Remove(itemToDrop);
    }
  }

  public void DropItem(ItemData itemToDrop)
  {
    GameObject dropItem = Instantiate(dropPrefab, transform.position, Quaternion.identity);
    Vector2 randomVelocity = new Vector2(Random.Range(-5, 5), Random.Range(12, 15));
    dropItem.GetComponent<ItemObject>().SetUpItem(itemToDrop, randomVelocity);
  }
}
