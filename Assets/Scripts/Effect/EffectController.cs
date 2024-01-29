using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectController : MonoBehaviour
{
  protected virtual void OnTriggerEnter2D(Collider2D collision)
  {
    Enemy enemy = collision.GetComponent<Enemy>();
    if (enemy != null)
    {
      PlayerCharacterStats playerCharacterStats = PlayerManager.instance.player.GetComponent<PlayerCharacterStats>();
      playerCharacterStats.DoMagicalDamage(enemy.GetComponent<EnemyCharacterStats>());
    }
  }
}
