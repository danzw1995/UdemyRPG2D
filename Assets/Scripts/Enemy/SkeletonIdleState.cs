

using UnityEngine;

public class SkeletonIdleState : SkeletonGroundState
{
  public SkeletonIdleState(Enemy enemyBase, EnemyStateMachine enemyStateMachine, string animBoolName, EnemySkeleton enemy) : base(enemyBase, enemyStateMachine, animBoolName, enemy)
  {
  }

  public override void Enter()
  {
    base.Enter();
    stateTimer = enemy.idleTime;
  }

  public override void Update()
  {
    base.Update();

    if (stateTimer < 0)
    {
      enemy.stateMachine.ChangeState(enemy.moveState);
    }
  }
  public override void Exit()
  {
    base.Exit();
  }

}