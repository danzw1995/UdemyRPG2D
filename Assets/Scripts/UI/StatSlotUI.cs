using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class StatSlotUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
  [SerializeField] private string statName;
  [SerializeField] private StatType statType;
  [SerializeField] private TextMeshProUGUI statValueText;
  [SerializeField] private TextMeshProUGUI statNameText;

  [SerializeField] private string statDescription;

  private void OnValidate()
  {
    gameObject.name = "Stat - " + statName;

    if (statNameText != null)
    {
      statNameText.text = statName;
    }
  }


  protected UIManager uIManager;

  private void Awake()
  {
    uIManager = GetComponentInParent<UIManager>();
  }

  private void Start()
  {
    UpdateStatValueUI();
  }

  public void UpdateStatValueUI()
  {
    PlayerCharacterStats playerCharacterStats = PlayerManager.instance.player.GetComponent<PlayerCharacterStats>();
    if (playerCharacterStats != null)
    {
      statValueText.text = playerCharacterStats.GetStat(statType).GetValue().ToString();

      if (statType == StatType.health)
        statValueText.text = playerCharacterStats.GetMaxHealthValue().ToString();

      if (statType == StatType.damage)
        statValueText.text = (playerCharacterStats.damage.GetValue() + playerCharacterStats.strength.GetValue()).ToString();

      if (statType == StatType.critPower)
        statValueText.text = (playerCharacterStats.critPower.GetValue() + playerCharacterStats.strength.GetValue()).ToString();

      if (statType == StatType.critChance)
        statValueText.text = (playerCharacterStats.critChance.GetValue() + playerCharacterStats.agility.GetValue()).ToString();

      if (statType == StatType.evasion)
        statValueText.text = (playerCharacterStats.evasion.GetValue() + playerCharacterStats.agility.GetValue()).ToString();

      if (statType == StatType.magicRes)
        statValueText.text = (playerCharacterStats.magicResistance.GetValue() + (playerCharacterStats.intelligence.GetValue() * 3)).ToString();
    }
  }

  public void OnPointerEnter(PointerEventData eventData)
  {
    uIManager.statTooltip.ShowStatToolTip(statDescription);
  }

  public void OnPointerExit(PointerEventData eventData)
  {
    uIManager.statTooltip.HideStatToolTip();
  }
}
