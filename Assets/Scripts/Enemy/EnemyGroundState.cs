

using UnityEngine;

public class SkeletonGroundState : EnemyState
{

  protected EnemySkeleton enemy;

  protected Transform player;
  public SkeletonGroundState(Enemy enemyBase, EnemyStateMachine enemyStateMachine, string animBoolName, EnemySkeleton enemy) : base(enemyBase, enemyStateMachine, animBoolName)
  {
    this.enemy = enemy;
  }

  public override void Enter()
  {
    base.Enter();
    player = PlayerManager.instance.player.transform;
  }

  public override void Update()
  {
    base.Update();

    if (enemy.IsPlayerDetected() || Vector2.Distance(enemy.transform.position, player.position) < 2f)
    {
      enemy.stateMachine.ChangeState(enemy.battleState);
    }


  }
  public override void Exit()
  {
    base.Exit();
  }

}