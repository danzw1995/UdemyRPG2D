using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonEnemyDeadState : EnemyState
{
  private EnemySkeleton enemy;
  public SkeletonEnemyDeadState(Enemy enemyBase, EnemyStateMachine enemyStateMachine, string animBoolName, EnemySkeleton enemy) : base(enemyBase, enemyStateMachine, animBoolName)
  {
    this.enemy = enemy;
  }

  public override void Enter()
  {
    base.Enter();

    enemy.anim.SetBool(enemy.lastAnimBoolName, true);
    enemy.anim.speed = 0;
    enemy.cd.enabled = false;

    stateTimer = -1;
  }

  public override void Update()
  {
    base.Update();
    if (stateTimer > 0)
    {
      rb.velocity = new Vector2(0, 10);
    }
  }

  public override void Exit()
  {
    base.Exit();
  }
}
