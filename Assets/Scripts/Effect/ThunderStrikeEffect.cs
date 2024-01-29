using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Thunder Strike Effect", menuName = "Data/Item Effect/Thunder Strike")]
public class ThunderStrikeEffect : ItemEffect
{
  [SerializeField] private GameObject thunderStrikePrefab = null;
  public override void ExecuteEffect(Transform target)
  {
    GameObject instance = Instantiate(thunderStrikePrefab, target.position, Quaternion.identity);

    Destroy(instance, 1f);
  }
}
