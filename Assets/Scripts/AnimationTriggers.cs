

using UnityEngine;

public class AnimationTriggers : MonoBehaviour
{
  private Player player;
  private void Awake()
  {
    player = GetComponentInParent<Player>();
  }

  private void AnimationTrigger()
  {
    player.stateMachine.currentState.AnimationFinishTrigger();
  }

  private void AttackTrigger()
  {
    Collider2D[] hits = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackRadius);

    foreach (Collider2D hit in hits)
    {
      Enemy enemy = hit.GetComponent<Enemy>();
      if (enemy != null)
      {
        enemy.Damage();
      }
    }
  }

  private void ThrowSword()
  {
    SkillManager.instance.sword.CreateSword();
  }
}