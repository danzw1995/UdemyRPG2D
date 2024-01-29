

using UnityEngine;

public class SkeletonGroundState : EnemyState
{

  protected EnemySkeleton enemy;

  protected Transform player;
  private PlayerCharacterStats playerCharacterStats;
  public SkeletonGroundState(Enemy enemyBase, EnemyStateMachine enemyStateMachine, string animBoolName, EnemySkeleton enemy) : base(enemyBase, enemyStateMachine, animBoolName)
  {
    this.enemy = enemy;
  }

  public override void Enter()
  {
    base.Enter();
    player = PlayerManager.instance.player.transform;
    playerCharacterStats = player.GetComponent<PlayerCharacterStats>();
  }

  public override void Update()
  {
    base.Update();

    if (playerCharacterStats.isDead)
    {
      return;
    }

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