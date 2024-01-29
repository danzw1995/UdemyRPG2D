

using UnityEngine;

public class EnemyCharacterStats : CharacterStats
{
  private Enemy enemy;

  [SerializeField] private int level = 1;
  [SerializeField] private float percantageModifier = 0.4f;

  private ItemDrop itemDrop;


  protected override void Start()
  {
    ApplyLevelModifiers();
    base.Start();
    enemy = GetComponent<Enemy>();
    itemDrop = GetComponent<ItemDrop>();
  }

  private void ApplyLevelModifiers()
  {
    Modify(strength);
    Modify(agility);
    Modify(intelligence);
    Modify(vitality);

    Modify(damage);
    Modify(critChance);
    Modify(critPower);

    Modify(maxHealth);
    Modify(armor);
    Modify(evasion);
    Modify(magicResistance);

    Modify(fireDamage);
    Modify(iceDamage);
    Modify(lightingDamage);

  }

  private void Modify(Stat _stat)
  {
    for (int i = 1; i < level; i++)
    {
      float modifier = _stat.GetValue() * percantageModifier;

      _stat.AddModifier(Mathf.RoundToInt(modifier));
    }
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
    itemDrop.GenerateDrop();
  }
}