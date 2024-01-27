

public class EnemyCharacterStats : CharacterStats
{
  Enemy enemy;

  protected override void Start()
  {
    base.Start();
    enemy = GetComponent<Enemy>();
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
    enemy.Die();
  }
}