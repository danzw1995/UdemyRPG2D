using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneSkillController : MonoBehaviour
{
  [SerializeField] private Transform attackCheck = null;
  [SerializeField] private float attackRadius = 0.8f;
  private SpriteRenderer sr;
  private Animator anim;
  private float cloneTimer;
  private float loosingSpeed;

  private Transform closestEnemy;

  private void Awake()
  {
    sr = GetComponent<SpriteRenderer>();
    anim = GetComponent<Animator>();
  }
  public void SetupClone(Vector3 position, float duration, float loosingSpeed, bool canAttack)
  {
    transform.position = position;
    cloneTimer = duration;
    this.loosingSpeed = loosingSpeed;

    if (canAttack)
    {
      anim.SetInteger("AttackNumber", Random.Range(1, 4));
      FaceClosestTarget();
    }
  }

  private void Update()
  {
    cloneTimer -= Time.deltaTime;
    if (cloneTimer < 0)
    {
      sr.color = new Color(1, 1, 1, sr.color.a - (Time.deltaTime * loosingSpeed));

      if (sr.color.a <= 0)
      {
        Destroy(gameObject);
      }
    }
  }

  private void AnimationTrigger()
  {
    cloneTimer = -1f;
  }

  private void AttackTrigger()
  {
    Collider2D[] hits = Physics2D.OverlapCircleAll(attackCheck.position, attackRadius);


    foreach (Collider2D hit in hits)
    {
      Enemy enemy = hit.GetComponent<Enemy>();
      if (enemy != null)
      {
        enemy.Damage();
      }
    }
  }

  private void FaceClosestTarget()
  {
    Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 25);
    float closestDistance = Mathf.Infinity;

    foreach (Collider2D hit in hits)
    {
      Enemy enemy = hit.GetComponent<Enemy>();
      if (enemy != null)
      {
        float distanceToEnemy = Vector2.Distance(transform.position, hit.transform.position);
        if (distanceToEnemy < closestDistance)
        {
          closestDistance = distanceToEnemy;
          closestEnemy = hit.transform;
        }
      }
    }

    if (closestEnemy != null)
    {
      if (closestEnemy.position.x < transform.position.x)
      {
        transform.Rotate(0, 180, 0);
      }
    }
  }
}
