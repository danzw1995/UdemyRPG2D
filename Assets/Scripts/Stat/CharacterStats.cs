using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatType
{
  strength,
  agility,
  intelegence,
  vitality,
  damage,
  critChance,
  critPower,
  health,
  armor,
  evasion,
  magicRes,
  fireDamage,
  iceDamage,
  lightingDamage
}

public class CharacterStats : MonoBehaviour
{
  private EntityFX entityFX;

  [Header("Major stats")]
  public Stat strength; // 1 point increase damage by 1 and crit.power by 1%
  public Stat agility;  // 1 point increase evasion by 1% and crit.chance by 1%
  public Stat intelligence; // 1 point increase magic damage by 1 and magic resistance by 3
  public Stat vitality; // 1 point incredase health by 5 points

  [Header("Offensive stats")]
  public Stat damage;
  public Stat critChance;
  public Stat critPower;

  [Header("Defensive stats")]
  public Stat armor;
  public Stat evasion;
  public Stat magicResistance;
  public Stat maxHealth;

  [Header("Magic stats")]
  public Stat fireDamage;
  public Stat iceDamage;
  public Stat lightingDamage;
  public float currentHealth;


  public bool isIgnited;   // does damage over time
  public bool isChilled;   // reduce armor by 20%
  public bool isShocked;   // reduce accuracy by 20%


  [SerializeField] private float ailmentsDuration = 4;
  private float ignitedTimer;
  private float chilledTimer;
  private float shockedTimer;

  [SerializeField]
  private GameObject shockStrikePrefab = null;
  [SerializeField] private float shockMoveSpeed = 10;
  [SerializeField] private float shockDamage = 10;

  private float igniteDamageCoodlown = .3f;
  private float igniteDamageTimer;
  private int igniteDamage;

  public bool isDead { get; private set; }

  public System.Action OnHealthChange;


  private void Awake()
  {
    entityFX = GetComponent<EntityFX>();
  }
  protected virtual void Start()
  {
    critPower.SetDefaultValue(150);
    currentHealth = maxHealth.GetValue();
  }

  protected virtual void Update()
  {
    ignitedTimer -= Time.deltaTime;
    chilledTimer -= Time.deltaTime;
    shockedTimer -= Time.deltaTime;

    igniteDamageTimer -= Time.deltaTime;


    if (ignitedTimer < 0)
      isIgnited = false;

    if (chilledTimer < 0)
      isChilled = false;

    if (shockedTimer < 0)
      isShocked = false;

    if (igniteDamageTimer < 0 && isIgnited)
    {
      DamageBy(igniteDamage);
      if (currentHealth <= 0 && !isDead)
      {
        Die();
      }
      igniteDamageTimer = igniteDamageCoodlown;
    }

  }

  public virtual void DoDamage(CharacterStats target)
  {
    if (TargetCanAvoidAttack(target))
      return;

    float totalDamage = strength.GetValue() + damage.GetValue();

    if (CanCrit())
    {
      totalDamage = CalculateCriticalDamage(totalDamage);
    }

    totalDamage = CheckTargetArmor(totalDamage, target);
    target.TakeDamage(totalDamage);

    DoMagicalDamage(target);
  }

  public void DoMagicalDamage(CharacterStats target)
  {
    float _fireDamage = fireDamage.GetValue();
    float _iceDamage = iceDamage.GetValue();
    float _lightingDamage = lightingDamage.GetValue();



    float totalMagicalDamage = _fireDamage + _iceDamage + _lightingDamage + intelligence.GetValue();

    totalMagicalDamage = CheckTargetResistance(target, totalMagicalDamage);
    target.TakeDamage(totalMagicalDamage);

    if (Mathf.Max(_fireDamage, _iceDamage, _lightingDamage) <= 0)
      return;


    AttemptyToApplyAilements(target, _fireDamage, _iceDamage, _lightingDamage);

  }

  private void AttemptyToApplyAilements(CharacterStats target, float fireDamage, float iceDamage, float lightingDamage)
  {
    bool canApplyIgnite = fireDamage > iceDamage && fireDamage > lightingDamage;
    bool canApplyChill = iceDamage > fireDamage && iceDamage > lightingDamage;
    bool canApplyShock = lightingDamage > fireDamage && lightingDamage > iceDamage;

    while (!canApplyIgnite && !canApplyChill && !canApplyShock)
    {
      if (Random.value < .3f && fireDamage > 0)
      {
        canApplyIgnite = true;
        target.ApplyAilments(canApplyIgnite, canApplyChill, canApplyShock);

        return;
      }

      if (Random.value < .5f && iceDamage > 0)
      {
        canApplyChill = true;
        target.ApplyAilments(canApplyIgnite, canApplyChill, canApplyShock);

        return;
      }

      if (Random.value < .5f && lightingDamage > 0)
      {
        canApplyShock = true;

        target.ApplyAilments(canApplyIgnite, canApplyChill, canApplyShock);
        return;

      }
    }

    if (canApplyIgnite)
    {
      target.SetupIgniteDamage(Mathf.RoundToInt(fireDamage * 0.2f));
    }


    target.ApplyAilments(canApplyIgnite, canApplyChill, canApplyShock);
  }


  public void ApplyAilments(bool _ignite, bool _chill, bool _shock)
  {
    bool canApplyIgnite = !isIgnited && !isChilled && !isShocked;
    bool canApplyChill = !isIgnited && !isChilled && !isShocked;
    bool canApplyShock = !isIgnited && !isChilled;

    if (_ignite && canApplyIgnite)
    {
      isIgnited = _ignite;
      ignitedTimer = ailmentsDuration;
      entityFX.IgniteColorFor(ailmentsDuration);
    }

    if (_chill && canApplyChill)
    {
      isChilled = _chill;
      chilledTimer = ailmentsDuration;
      entityFX.ChillColorFor(ailmentsDuration);
      float slowPercentage = 0.2f;
      entityFX.GetComponent<Entity>().SlowEntityBy(slowPercentage, ailmentsDuration);
    }

    if (_shock && canApplyShock)
    {
      if (!isShocked)
      {
        ApplyShock(_shock);

      }
      else
      {
        if (GetComponent<Player>() != null)
        {
          return;
        }

        HitNearestTargetWithShockStrike();
      }
    }

  }

  private void HitNearestTargetWithShockStrike()
  {
    Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 25);

    float closestDistance = Mathf.Infinity;
    Transform closestEnemy = null;

    foreach (var hit in colliders)
    {
      if (hit.GetComponent<Enemy>() != null && Vector2.Distance(transform.position, hit.transform.position) > 1)
      {
        float distanceToEnemy = Vector2.Distance(transform.position, hit.transform.position);

        if (distanceToEnemy < closestDistance)
        {
          closestDistance = distanceToEnemy;
          closestEnemy = hit.transform;
        }
      }

      if (closestEnemy == null)            // delete if you don't want shocked target to be hit by shock strike
        closestEnemy = transform;
    }


    if (closestEnemy != null)
    {
      GameObject newShockStrike = Instantiate(shockStrikePrefab, transform.position, Quaternion.identity);
      newShockStrike.GetComponent<ShockStrikeController>().Setup(closestEnemy.GetComponent<CharacterStats>(), shockDamage, shockMoveSpeed);
    }
  }

  private void ApplyShock(bool shock)
  {
    if (isShocked)
    {
      return;
    }


    shockedTimer = ailmentsDuration;
    isShocked = shock;

    entityFX.ShockColorFor(ailmentsDuration);
  }

  private float CheckTargetResistance(CharacterStats target, float totalMagicalDamage)
  {
    totalMagicalDamage -= target.magicResistance.GetValue() + (target.intelligence.GetValue() * 3);
    totalMagicalDamage = Mathf.Clamp(totalMagicalDamage, 0, int.MaxValue);
    return totalMagicalDamage;
  }

  private float CalculateCriticalDamage(float totalDamage)
  {
    float totalCritPower = (critPower.GetValue() + strength.GetValue()) * 0.1f;

    float critDamage = totalDamage * totalCritPower;

    return Mathf.RoundToInt(critDamage);
  }

  private bool CanCrit()
  {
    float totalChance = critChance.GetValue() + agility.GetValue();
    if (Random.Range(0, 100) < totalChance)
    {
      return true;
    }
    return false;

  }

  private float CheckTargetArmor(float totalDamage, CharacterStats target)
  {
    totalDamage -= target.armor.GetValue();
    totalDamage = Mathf.Max(0, totalDamage);
    return totalDamage;
  }

  private bool TargetCanAvoidAttack(CharacterStats target)
  {
    float totalEvasion = target.evasion.GetValue() + target.agility.GetValue();
    if (Random.Range(0, 100) < totalEvasion)
    {
      return true;
    }

    return false;
  }

  public virtual void TakeDamage(float damage)
  {
    DamageBy(damage);
    if (currentHealth <= 0 && !isDead)
    {
      Die();
    }
  }

  private void DamageBy(float damage)
  {
    currentHealth -= damage;
    if (OnHealthChange != null)
    {
      OnHealthChange();

    }
  }

  protected virtual void Die()
  {
    isDead = true;
  }
  public void SetupIgniteDamage(int _damage) => igniteDamage = _damage;

  public float GetMaxHealthValue()
  {
    return maxHealth.GetValue() + vitality.GetValue() * 5;
  }

  public void IncreaseHealthBy(float health)
  {
    currentHealth += health;
    if (currentHealth > GetMaxHealthValue())
    {
      currentHealth = GetMaxHealthValue();
    }
    if (OnHealthChange != null)
    {
      OnHealthChange();
    }
  }
  public Stat GetStat(StatType _statType)
  {
    if (_statType == StatType.strength) return strength;
    else if (_statType == StatType.agility) return agility;
    else if (_statType == StatType.intelegence) return intelligence;
    else if (_statType == StatType.vitality) return vitality;
    else if (_statType == StatType.damage) return damage;
    else if (_statType == StatType.critChance) return critChance;
    else if (_statType == StatType.critPower) return critPower;
    else if (_statType == StatType.health) return maxHealth;
    else if (_statType == StatType.armor) return armor;
    else if (_statType == StatType.evasion) return evasion;
    else if (_statType == StatType.magicRes) return magicResistance;
    else if (_statType == StatType.fireDamage) return fireDamage;
    else if (_statType == StatType.iceDamage) return iceDamage;
    else if (_statType == StatType.lightingDamage) return lightingDamage;

    return null;
  }
}
