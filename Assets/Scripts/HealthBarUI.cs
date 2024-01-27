using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
  private Entity entity;
  private RectTransform rectTransform;
  private Slider slider;
  private CharacterStats characterStats;
  private void Awake()
  {
    rectTransform = GetComponent<RectTransform>();
    entity = GetComponentInParent<Entity>();
    characterStats = GetComponentInParent<CharacterStats>();
    slider = GetComponentInChildren<Slider>();
  }

  private void Start()
  {
    entity.OnFlipped += Flip;

    characterStats.OnHealthChange += UpdateUI;

    Debug.Log("updateUI");
    UpdateUI();


  }

  private void UpdateUI()
  {
    slider.maxValue = characterStats.GetMaxHealthValue();
    slider.value = characterStats.currentHealth;
  }

  private void Flip()
  {
    rectTransform.Rotate(0, 180, 0);
  }

  private void OnDisable()
  {
    entity.OnFlipped -= Flip;
    characterStats.OnHealthChange -= UpdateUI;
  }
}
