using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
  public PlayerGroundedState(Player player, PlayerStateMachine playerStateMachine, string animBoolName) : base(player, playerStateMachine, animBoolName)
  {
  }

  public override void Enter()
  {
    base.Enter();
  }

  public override void Update()
  {
    base.Update();

    if (Input.GetKeyDown(KeyCode.Mouse1) && HasNoSword())
    {
      playerStateMachine.ChangeState(player.aimSwordState);
    }

    if (Input.GetKeyDown(KeyCode.Q))
    {
      playerStateMachine.ChangeState(player.counterAttackState);
    }

    if (Input.GetKeyDown(KeyCode.Mouse0))
    {
      playerStateMachine.ChangeState(player.attackState);
    }

    if (!player.IsGroundDetected())
    {
      playerStateMachine.ChangeState(player.airState);
    }

    if (Input.GetKeyDown(KeyCode.Space) && player.IsGroundDetected())
    {
      playerStateMachine.ChangeState(player.jumpState);
    }
  }

  public override void Exit()
  {
    base.Exit();
  }

  private bool HasNoSword()
  {
    if (!player.sword)
    {
      return true;
    }
    SwordSkillController swordSkillController = player.sword.GetComponent<SwordSkillController>();
    swordSkillController.ReturnSword();
    return false;
  }
}
