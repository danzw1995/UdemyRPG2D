using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimSwordState : PlayerState
{
  public PlayerAimSwordState(Player player, PlayerStateMachine playerStateMachine, string animBoolName) : base(player, playerStateMachine, animBoolName)
  {
  }

  public override void Enter()
  {
    base.Enter();

    player.skill.sword.DotsActive(true);
  }

  public override void Update()
  {
    base.Update();

    player.SetZeroVelocity();

    if (Input.GetKeyDown(KeyCode.Mouse1))
    {
      playerStateMachine.ChangeState(player.idleState);
    }

    Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    if (mousePosition.x < player.transform.position.x && player.facingDirection == 1)
    {
      player.Flip();
    }
    else if (mousePosition.x > player.transform.position.x && player.facingDirection == -1)
    {
      player.Flip();
    }
  }

  public override void Exit()
  {
    base.Exit();
    player.StartCoroutine("BusyFor", 0.1f);

  }
}
