using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
  public PlayerMoveState(Player player, PlayerStateMachine playerStateMachine, string animBoolName) : base(player, playerStateMachine, animBoolName)
  {
  }

  public override void Enter()
  {
    base.Enter();

  }

  public override void Update()
  {
    base.Update();

    player.SetVelocity(xInput * player.moveSpeed, rb.velocity.y);
    if (xInput == 0 || player.IsWallDetected())
    {
      playerStateMachine.ChangeState(player.idleState);
    }
  }

  public override void Exit()
  {
    base.Exit();

  }
}