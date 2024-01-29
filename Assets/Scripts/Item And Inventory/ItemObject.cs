using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
  [SerializeField] private ItemData itemData;
  [SerializeField] private Rigidbody2D rb;
  [SerializeField] private Vector2 velocity;

  private SpriteRenderer spriteRenderer;

  private void Awake()
  {
    spriteRenderer = GetComponent<SpriteRenderer>();
    rb = GetComponent<Rigidbody2D>();
  }

  private void Update()
  {
    if (Input.GetKeyDown(KeyCode.M))
    {
      rb.velocity = velocity;
    }
  }


  private void SetName()
  {
    spriteRenderer.sprite = itemData.icon;
    gameObject.name = "item object -" + itemData.name;
    rb.velocity = velocity;
  }

  public void PickUp()
  {
    Inventory.instance.AddItem(itemData);
    Destroy(gameObject);
  }

  public void SetUpItem(ItemData itemData, Vector2 velocity)
  {
    this.itemData = itemData;
    this.velocity = velocity;
    SetName();
  }
}
