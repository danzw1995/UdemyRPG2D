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

  private bool canCloneDuplicate;
  private int chanceDuplicate;
  private int facingDirection = 1;

  private Transform closestEnemy;

  private void Awake()
  {
    sr = GetComponent<SpriteRenderer>();
    anim = GetComponent<Animator>();
  }
  public void SetupClone(Vector3 position, float duration, float loosingSpeed, bool canAttack, Vector3 offset, Transform closestEnemy, bool canCloneDuplicate, int chanceDuplicate)
  {
    transform.position = position + offset;
    cloneTimer = duration;
    this.loosingSpeed = loosingSpeed;
    this.closestEnemy = closestEnemy;
    this.canCloneDuplicate = canCloneDuplicate;
    this.chanceDuplicate = chanceDuplicate;

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
        EnemyCharacterStats enemyCharacterStats = enemy.GetComponent<EnemyCharacterStats>();
        PlayerManager.instance.player.characterStats.DoDamage(enemyCharacterStats);

        if (canCloneDuplicate)
        {
          if (Random.Range(0, 100) < chanceDuplicate)
          {
            SkillManager.instance.clone.CreateClone(hit.transform.position, new Vector3(0.5f * facingDirection, 0));
          }
        }
      }
    }
  }

  private void FaceClosestTarget()
  {


    if (closestEnemy != null)
    {
      if (closestEnemy.position.x < transform.position.x)
      {
        facingDirection = -1;
        transform.Rotate(0, 180, 0);
      }
    }
  }
}
