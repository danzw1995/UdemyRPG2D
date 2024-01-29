using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryAttackState : PlayerState
{

  public int comboCounter = 0;
  private float lastTimeAttacked;
  private float comboWindow = 2;
  public PlayerPrimaryAttackState(Player player, PlayerStateMachine playerStateMachine, string animBoolName) : base(player, playerStateMachine, animBoolName)
  {
  }

  public override void Enter()
  {
    base.Enter();

    xInput = 0;

    if (comboCounter > 2 || Time.time > lastTimeAttacked + comboWindow)
    {
      comboCounter = 0;
    }

    player.anim.SetInteger("ComboCounter", comboCounter);

    float attackDirection = player.facingDirection;

    if (xInput != 0)
    {
      attackDirection = xInput;
    }

    player.SetVelocity(player.attackMovement[comboCounter].x * attackDirection, player.attackMovement[comboCounter].y);

    stateTimer = 0.1f;

  }

  public override void Update()
  {
    base.Update();

    if (stateTimer < 0)
    {
      player.SetVelocity(0, 0);
    }

    if (triggerCalled && !player.isBusy)
    {
      playerStateMachine.ChangeState(player.idleState);
    }

  }

  public override void Exit()
  {
    base.Exit();
    comboCounter++;

    lastTimeAttacked = Time.time;

    player.StartCoroutine(player.BusyFor(0.15f));
  }
}
