

using System;
using UnityEngine;

public class SkeletonStunnedState : EnemyState
{

  private EnemySkeleton enemy;

  public SkeletonStunnedState(Enemy enemyBase, EnemyStateMachine enemyStateMachine, string animBoolName, EnemySkeleton enemy) : base(enemyBase, enemyStateMachine, animBoolName)
  {
    this.enemy = enemy;
  }

  public override void Enter()
  {
    base.Enter();
    stateTimer = enemy.stunDuration;

    enemy.fx.InvokeRepeating("RedColorBlink", 0, 0.1f);

    enemy.rb.velocity = new Vector2(enemy.stunDirection.x * -enemy.facingDirection, enemy.stunDirection.y);


  }

  public override void Update()
  {
    base.Update();


    if (stateTimer < 0)
    {
      enemyStateMachine.ChangeState(enemy.idleState);
    }

  }
  public override void Exit()
  {
    base.Exit();
    enemy.fx.Invoke("CancelColorChange", 0);

  }

}