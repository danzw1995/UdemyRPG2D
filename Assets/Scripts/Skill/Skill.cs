using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
  [SerializeField] protected float cooldown;
  protected float cooldownTimer;

  protected virtual void Update()
  {
    cooldownTimer -= Time.deltaTime;
  }

  public virtual bool CanUseSkill()
  {
    if (cooldownTimer < 0)
    {
      UseSkill();
      cooldownTimer = cooldown;
      return true;
    }

    return false;
  }

  public virtual void UseSkill()
  {
    Debug.Log("Use Skill");
  }

  public virtual Transform FindClosetTarget(Transform checkTransform)
  {
    Collider2D[] hits = Physics2D.OverlapCircleAll(checkTransform.position, 25);
    float closestDistance = Mathf.Infinity;

    Transform closestEnemy = null;

    foreach (Collider2D hit in hits)
    {
      Enemy enemy = hit.GetComponent<Enemy>();
      if (enemy != null)
      {
        float distanceToEnemy = Vector2.Distance(checkTransform.position, hit.transform.position);
        if (distanceToEnemy < closestDistance)
        {
          closestDistance = distanceToEnemy;
          closestEnemy = hit.transform;
        }
      }
    }

    return closestEnemy;
  }
}
