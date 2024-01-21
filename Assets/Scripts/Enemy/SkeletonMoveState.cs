

public class SkeletonMoveState : SkeletonGroundState
{
  public SkeletonMoveState(Enemy enemyBase, EnemyStateMachine enemyStateMachine, string animBoolName, EnemySkeleton enemy) : base(enemyBase, enemyStateMachine, animBoolName, enemy)
  {
  }

  public override void Enter()
  {
    base.Enter();
  }

  public override void Update()
  {
    base.Update();
    enemy.SetVelocity(enemy.moveSpeed * enemy.facingDirection, enemy.rb.velocity.y);

    if (!enemy.IsGroundDetected() || enemy.IsWallDetected())
    {
      enemy.Flip();
      enemyStateMachine.ChangeState(enemy.idleState);
    }

  }
  public override void Exit()
  {
    base.Exit();
  }

}