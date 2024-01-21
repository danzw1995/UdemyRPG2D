using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{

  protected PlayerStateMachine playerStateMachine;
  protected Player player;

  protected float xInput;

  protected float yInput;

  protected float stateTimer;

  protected bool triggerCalled;
  protected Rigidbody2D rb;

  private string animBoolName;

  public PlayerState(Player player, PlayerStateMachine playerStateMachine, string animBoolName)
  {
    this.player = player;
    this.playerStateMachine = playerStateMachine;
    this.animBoolName = animBoolName;
  }

  public virtual void Enter()
  {
    player.anim.SetBool(animBoolName, true);
    rb = player.rb;

    stateTimer = player.dashDuration;
    triggerCalled = false;

  }

  public virtual void Update()
  {
    xInput = Input.GetAxisRaw("Horizontal");
    yInput = Input.GetAxisRaw("Vertical");

    player.anim.SetFloat("yVelocity", rb.velocity.y);

    stateTimer -= Time.deltaTime;

  }

  public virtual void Exit()
  {
    player.anim.SetBool(animBoolName, false);

  }

  public virtual void AnimationFinishTrigger()
  {
    triggerCalled = true;
  }
}
