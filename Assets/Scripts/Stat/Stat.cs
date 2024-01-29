


using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
  [SerializeField] private float baseValue;

  public List<float> modifies = new List<float>();
  public float GetValue()
  {

    float finalValue = baseValue;

    foreach (int modify in modifies)
    {
      finalValue += modify;
    }
    return finalValue;
  }

  public void SetDefaultValue(float value)
  {
    baseValue = value;
  }

  public void AddModifier(float modifier)
  {
    modifies.Add(modifier);
  }

  public void RemoveModifier(float modifier)
  {
    modifies.Remove(modifier);
  }
}