using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCatchSwordState : PlayerState
{
  private Transform sword;
  public PlayerCatchSwordState(Player player, PlayerStateMachine playerStateMachine, string animBoolName) : base(player, playerStateMachine, animBoolName)
  {
  }

  public override void Enter()
  {
    base.Enter();

    sword = player.sword.transform;
    if (sword.position.x < player.transform.position.x && player.facingDirection == 1)
    {
      player.Flip();
    }
    else if (sword.position.x > player.transform.position.x && player.facingDirection == -1)
    {
      player.Flip();
    }

    rb.velocity = new Vector2(player.returnImpact * -player.facingDirection, rb.velocity.y);
  }

  public override void Update()
  {
    base.Update();
    if (triggerCalled)
    {
      playerStateMachine.ChangeState(player.idleState);
    }
  }

  public override void Exit()
  {
    base.Exit();

    player.StartCoroutine("BusyFor", 0.15f);
  }
}
