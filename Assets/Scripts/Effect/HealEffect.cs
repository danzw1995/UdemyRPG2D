using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Heal Effect", menuName = "Data/Item Effect/Heal Effect")]
public class HealEffect : ItemEffect
{
  [Range(0, 1f)]
  [SerializeField] private float healPercent = 0.1f;
  public override void ExecuteEffect(Transform target)
  {
    PlayerCharacterStats playerCharacterStats = PlayerManager.instance.player.GetComponent<PlayerCharacterStats>();

    playerCharacterStats.IncreaseHealthBy(playerCharacterStats.GetMaxHealthValue() * healPercent);
  }
}
