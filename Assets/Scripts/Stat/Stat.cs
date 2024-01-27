


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

  public void AddModify(float modify)
  {
    modifies.Add(modify);
  }

  public void RemoveModify(int position)
  {
    modifies.RemoveAt(position);
  }
}