using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObjectTrigger : MonoBehaviour
{
  private ItemObject itemObject;

  private void Awake()
  {
    itemObject = GetComponentInParent<ItemObject>();
  }


  private void OnTriggerEnter2D(Collider2D collision)
  {
    Debug.Log("123123");
    if (collision.GetComponent<Player>() != null)
    {
      if (collision.GetComponent<PlayerCharacterStats>().isDead)
      {
        return;
      }
      itemObject.PickUp();
    }
  }
}
