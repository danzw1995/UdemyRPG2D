using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerState
{
  public PlayerAirState(Player player, PlayerStateMachine playerStateMachine, string animBoolName) : base(player, playerStateMachine, animBoolName)
  {
  }

  public override void Enter()
  {
    base.Enter();
  }

  public override void Update()
  {
    base.Update();

    if (player.IsWallDetected())
    {
      playerStateMachine.ChangeState(player.wallSlideState);
    }

    if (player.IsGroundDetected())
    {
      playerStateMachine.ChangeState(player.idleState);
    }

    if (xInput != 0)
    {
      player.SetVelocity(player.moveSpeed * 0.8f * xInput, rb.velocity.y);
    }
  }

  public override void Exit()
  {
    base.Exit();
  }
}
