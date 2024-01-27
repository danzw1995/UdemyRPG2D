

public class PlayerCharacterStats : CharacterStats
{
  Player player;

  protected override void Start()
  {
    base.Start();
    player = GetComponent<Player>();
  }

  public override void DoDamage(CharacterStats target)
  {
    base.DoDamage(target);
  }

  public override void TakeDamage(float damage)
  {
    base.TakeDamage(damage);
  }
  protected override void Die()
  {
    base.Die();
    player.stateMachine.ChangeState(player.deadState);
  }
}