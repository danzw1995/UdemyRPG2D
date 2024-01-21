using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState
{

  protected EnemyStateMachine enemyStateMachine;

  protected Enemy enemyBase;

  protected Rigidbody2D rb;


  protected float stateTimer;

  protected bool triggerCalled;

  private string animBoolName;


  public EnemyState(Enemy enemyBase, EnemyStateMachine enemyStateMachine, string animBoolName)
  {
    this.enemyStateMachine = enemyStateMachine;
    this.enemyBase = enemyBase;
    this.animBoolName = animBoolName;
  }
  public virtual void Enter()
  {
    triggerCalled = false;
    enemyBase.anim.SetBool(animBoolName, true);
    rb = enemyBase.rb;

  }
  public virtual void Update()
  {
    stateTimer -= Time.deltaTime;
  }

  public virtual void Exit()
  {
    enemyBase.anim.SetBool(animBoolName, false);

  }

  public virtual void AnimationFinishTrigger()
  {
    triggerCalled = true;
  }
}
