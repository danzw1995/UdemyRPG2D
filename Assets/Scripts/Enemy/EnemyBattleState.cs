

using System;
using UnityEngine;

public class SkeletonBattleState : EnemyState
{

  private EnemySkeleton enemy;

  private Transform player;
  private PlayerCharacterStats playerCharacterStats;


  private int moveDirection;
  public SkeletonBattleState(Enemy enemyBase, EnemyStateMachine enemyStateMachine, string animBoolName, EnemySkeleton enemy) : base(enemyBase, enemyStateMachine, animBoolName)
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
      enemyStateMachine.ChangeState(enemy.idleState);
    }

    if (enemy.IsPlayerDetected())
    {
      stateTimer = enemy.battleTime;
      if (enemy.IsPlayerDetected().distance < enemy.attackDistance)
      {
        if (CanAttack())
        {
          enemyStateMachine.ChangeState(enemy.attackState);

        }
      }
    }
    else
    {
      if (stateTimer < 0 || Vector2.Distance(enemy.transform.position, player.position) > 10)
      {
        enemyStateMachine.ChangeState(enemy.idleState);
      }
    }

    if (player.position.x < enemy.transform.position.x)
    {
      moveDirection = -1;
    }
    else if (player.position.x > enemy.transform.position.x)
    {
      moveDirection = 1;
    }

    enemy.SetVelocity(enemy.moveSpeed * moveDirection, enemy.rb.velocity.y);


  }
  public override void Exit()
  {
    base.Exit();
  }

  public bool CanAttack()
  {
    if (Time.time > enemy.lastAttackTime + enemy.attackCooldown)
    {
      enemy.lastAttackTime = Time.time;
      return true;
    }
    return false;
  }

}