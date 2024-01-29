using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EquipmentType
{
  Weapon,
  Armor,
  Amulet
}
[CreateAssetMenu(fileName = "Equipment Item", menuName = "Data/Equipment")]
public class EquipmentItemData : ItemData
{
  public EquipmentType equipmentType = EquipmentType.Weapon;

  [Header("Major stats")]
  public int strength;
  public int agility;
  public int intelligence;
  public int vitality;

  [Header("Offensive stats")]
  public int damage;
  public int critChance;
  public int critPower;

  [Header("Defensive stats")]
  public int health;
  public int armor;
  public int evasion;
  public int magicResistance;

  [Header("Magic stats")]
  public int fireDamage;
  public int iceDamage;
  public int lightingDamage;

  [Header("Craft requirements")]
  public List<InventoryItem> craftingMaterials;

  public ItemEffect[] itemEffects;



  public void AddModifiers()
  {
    PlayerCharacterStats playerStats = PlayerManager.instance.player.GetComponent<PlayerCharacterStats>();

    playerStats.strength.AddModifier(strength);
    playerStats.agility.AddModifier(agility);
    playerStats.intelligence.AddModifier(intelligence);
    playerStats.vitality.AddModifier(vitality);

    playerStats.damage.AddModifier(damage);
    playerStats.critChance.AddModifier(critChance);
    playerStats.critPower.AddModifier(critPower);

    playerStats.maxHealth.AddModifier(health);
    playerStats.armor.AddModifier(armor);
    playerStats.evasion.AddModifier(evasion);
    playerStats.magicResistance.AddModifier(magicResistance);

    playerStats.fireDamage.AddModifier(fireDamage);
    playerStats.iceDamage.AddModifier(iceDamage);
    playerStats.lightingDamage.AddModifier(lightingDamage);
  }

  public void RemoveModifiers()
  {
    PlayerCharacterStats playerStats = PlayerManager.instance.player.GetComponent<PlayerCharacterStats>();

    playerStats.strength.RemoveModifier(strength);
    playerStats.agility.RemoveModifier(agility);
    playerStats.intelligence.RemoveModifier(intelligence);
    playerStats.vitality.RemoveModifier(vitality);


    playerStats.damage.RemoveModifier(damage);
    playerStats.critChance.RemoveModifier(critChance);
    playerStats.critPower.RemoveModifier(critPower);


    playerStats.maxHealth.RemoveModifier(health);
    playerStats.armor.RemoveModifier(armor);
    playerStats.evasion.RemoveModifier(evasion);
    playerStats.magicResistance.RemoveModifier(magicResistance);


    playerStats.fireDamage.RemoveModifier(fireDamage);
    playerStats.iceDamage.RemoveModifier(iceDamage);
    playerStats.lightingDamage.RemoveModifier(lightingDamage);
  }

  public void ExecuteEffect(Transform target)
  {
    foreach (ItemEffect itemEffect in itemEffects)
    {
      itemEffect.ExecuteEffect(target);
    }
  }
}
