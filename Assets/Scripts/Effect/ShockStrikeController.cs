using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockStrikeController : MonoBehaviour
{
  private Animator anim;
  private CharacterStats target;
  private float moveSpeed;

  private float damage;

  private bool triggered = false;
  private void Awake()
  {
    anim = GetComponentInChildren<Animator>();
  }

  public void Setup(CharacterStats target, float damage, float moveSpeed)
  {
    this.damage = damage;
    this.target = target;
    this.moveSpeed = moveSpeed;
    triggered = false;
  }

  private void Update()
  {
    if (triggered)
    {
      return;
    }

    transform.position = Vector2.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
    if (Vector2.Distance(transform.position, target.transform.position) < 0.1f)
    {
      triggered = true;

      anim.transform.localPosition = new Vector3(0, 0.5f);
      anim.transform.localRotation = Quaternion.identity;


      transform.localRotation = Quaternion.identity;
      transform.localScale = new Vector3(3, 3);

      Invoke("DamageAndSelfDestroy", 0.2f);

      anim.SetBool("Hit", true);
    }

  }

  private void DamageAndSelfDestroy()
  {
    target.TakeDamage(damage);
    Destroy(gameObject, 0.4f);
  }
}
