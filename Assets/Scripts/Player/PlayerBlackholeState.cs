using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlackholeState : PlayerState
{
  private float flyTime = 0.4f;
  private bool skillUsed;

  private float defaultGravity;
  public PlayerBlackholeState(Player player, PlayerStateMachine playerStateMachine, string animBoolName) : base(player, playerStateMachine, animBoolName)
  {
  }

  public override void Enter()
  {
    base.Enter();

    stateTimer = flyTime;
    skillUsed = false;
    defaultGravity = rb.gravityScale;
    rb.gravityScale = 0;
  }

  public override void Update()
  {
    base.Update();

    if (stateTimer > 0)
    {
      rb.velocity = new Vector2(0, 15);
    }

    if (stateTimer < 0)
    {
      rb.velocity = new Vector2(0, -0.1f);

      if (!skillUsed)
      {
        if (player.skill.blackhole.CanUseSkill())
        {
          skillUsed = true;

        }

      }
    }

    if (player.skill.blackhole.SkillCompleted())
    {
      playerStateMachine.ChangeState(player.airState);
    }
  }
  public override void AnimationFinishTrigger()
  {
    base.AnimationFinishTrigger();
  }

  public override void Exit()
  {
    base.Exit();
    PlayerManager.instance.player.MakeTransparent(false);
    rb.gravityScale = defaultGravity;

  }
}
