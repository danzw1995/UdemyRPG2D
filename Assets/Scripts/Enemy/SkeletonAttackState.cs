

using System;
using UnityEngine;

public class SkeletonAttackState : EnemyState
{

  private EnemySkeleton enemy;

  public SkeletonAttackState(Enemy enemyBase, EnemyStateMachine enemyStateMachine, string animBoolName, EnemySkeleton enemy) : base(enemyBase, enemyStateMachine, animBoolName)
  {
    this.enemy = enemy;
  }

  public override void Enter()
  {
    base.Enter();
  }

  public override void Update()
  {
    base.Update();

    enemy.SetZeroVelocity();

    if (triggerCalled)
    {
      enemyStateMachine.ChangeState(enemy.battleState);
    }


  }
  public override void Exit()
  {
    base.Exit();

    enemy.lastAttackTime = Time.time;
  }

}