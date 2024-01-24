using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackholeSkill : Skill
{
  [SerializeField] private BlackholeSkillController blackholePrefab = null;
  [SerializeField] private float maxSize = 15;
  [SerializeField] private float growSpeed = 5;
  [SerializeField] private float shrinkSpeed = 3;
  [SerializeField] private int amountOfAttacks = 4;
  [SerializeField] private float cloneAttackCooldown = 0.3f;
  [SerializeField] private float blackholeDuration = 3f;

  private BlackholeSkillController currentBlackhole;
  protected override void Update()
  {
    base.Update();
  }

  public override bool CanUseSkill()
  {
    return base.CanUseSkill();
  }

  public override void UseSkill()
  {
    base.UseSkill();

    currentBlackhole = Instantiate(blackholePrefab, PlayerManager.instance.player.transform.position, Quaternion.identity);
    currentBlackhole.SetupBlackhole(maxSize, growSpeed, shrinkSpeed, amountOfAttacks, cloneAttackCooldown, blackholeDuration);
  }

  public bool SkillCompleted()
  {
    if (!currentBlackhole)
    {
      return false;
    }
    if (currentBlackhole.playerCanExitState)
    {
      currentBlackhole = null;
      return true;
    }

    return false;
  }
}
