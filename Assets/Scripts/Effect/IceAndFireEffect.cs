using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ice And Fire Effect", menuName = "Data/Item Effect/Ice And Fire")]
public class IceAndFireEffect : ItemEffect
{
  [SerializeField] private GameObject iceAndFirePrefab = null;
  [SerializeField] private float xVelocity = 5;
  public override void ExecuteEffect(Transform target)
  {
    Player player = PlayerManager.instance.player;

    if (player.attackState.comboCounter == 2)
    {
      GameObject instance = Instantiate(iceAndFirePrefab, target.position, Quaternion.identity);
      instance.GetComponent<Rigidbody2D>().velocity = new Vector2(xVelocity * player.facingDirection, 0);


      Destroy(instance, 10f);
    }

  }
}
