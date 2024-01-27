

using UnityEngine;

public class SkeletonAnimationTrigger : MonoBehaviour
{
  private EnemySkeleton enemySkeleton;
  private void Awake()
  {
    enemySkeleton = GetComponentInParent<EnemySkeleton>();
  }

  private void AnimationTrigger()
  {
    enemySkeleton.stateMachine.currentState.AnimationFinishTrigger();
  }

  private void AttackTrigger()
  {
    Collider2D[] hits = Physics2D.OverlapCircleAll(enemySkeleton.attackCheck.position, enemySkeleton.attackRadius);
    foreach (Collider2D hit in hits)
    {
      Player player = hit.GetComponent<Player>();
      if (player != null)
      {
        PlayerCharacterStats playerCharacterStats = player.GetComponent<PlayerCharacterStats>();
        enemySkeleton.characterStats.DoDamage(playerCharacterStats);
      }
    }
  }

  private void OpenCounterAttackWindow() => enemySkeleton.OpenCounterAttackWindow();
  private void CloseCounterAttackWindow() => enemySkeleton.CloseCounterAttackWindow();
}