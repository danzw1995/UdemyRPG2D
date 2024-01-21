using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCounterAttackState : PlayerState
{

  public PlayerCounterAttackState(Player player, PlayerStateMachine playerStateMachine, string animBoolName) : base(player, playerStateMachine, animBoolName)
  {
  }

  public override void Enter()
  {
    base.Enter();

    stateTimer = player.counterAttackDuration;

    player.anim.SetBool("SuccessfulCounterAttack", false);

  }

  public override void Update()
  {
    base.Update();
    Collider2D[] hits = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackRadius);

    foreach (Collider2D hit in hits)
    {
      Enemy enemy = hit.GetComponent<Enemy>();
      if (enemy != null && enemy.CanBeStunned())
      {
        stateTimer = 10;

        player.anim.SetBool("SuccessfulCounterAttack", true);
      }
    }

    if (stateTimer < 0 || triggerCalled)
    {
      playerStateMachine.ChangeState(player.idleState);
    }

  }

  public override void Exit()
  {
    base.Exit();
  }
}
