using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerState
{
  public PlayerJumpState(Player player, PlayerStateMachine playerStateMachine, string animBoolName) : base(player, playerStateMachine, animBoolName)
  {
  }

  public override void Enter()
  {
    base.Enter();

    rb.velocity = new Vector2(rb.velocity.x, player.jumpForce);
  }

  public override void Update()
  {
    base.Update();

    if (rb.velocity.y < 0)
    {
      playerStateMachine.ChangeState(player.airState);
    }
  }

  public override void Exit()
  {
    base.Exit();
  }
}
