using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadState : PlayerState
{
  public PlayerDeadState(Player player, PlayerStateMachine playerStateMachine, string animBoolName) : base(player, playerStateMachine, animBoolName)
  {
  }

  public override void Enter()
  {
    base.Enter();

    player.SetZeroVelocity();
  }

  public override void Update()
  {
    base.Update();
  }

  public override void Exit()
  {
    base.Exit();
  }
}
