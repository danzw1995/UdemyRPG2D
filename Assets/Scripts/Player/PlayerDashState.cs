using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
  public PlayerDashState(Player player, PlayerStateMachine playerStateMachine, string animBoolName) : base(player, playerStateMachine, animBoolName)
  {
  }

  public override void Enter()
  {
    base.Enter();

    player.skill.clone.CreateClone(player.transform.position);

  }

  public override void Update()
  {
    base.Update();
    if (!player.IsGroundDetected() && player.IsWallDetected())
    {
      playerStateMachine.ChangeState(player.wallSlideState);
    }

    player.SetVelocity(player.dashSpeed * player.dashDirection, 0);


    if (stateTimer < 0)
    {
      playerStateMachine.ChangeState(player.idleState);
    }

  }

  public override void Exit()
  {
    base.Exit();

    player.SetVelocity(0, rb.velocity.y);
  }
}
