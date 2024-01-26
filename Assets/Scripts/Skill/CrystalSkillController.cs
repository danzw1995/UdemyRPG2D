using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalSkillController : MonoBehaviour
{
  private Animator anim;
  private CircleCollider2D circleCollider;
  private float crystalDestroyTimer;

  private bool canExplode;
  private bool canGrow;
  private float growSpeed;
  private Vector2 growScale;

  private bool canMove;
  private float moveSpeed;

  private Transform closestEnemy;

  private void Awake()
  {
    anim = GetComponent<Animator>();
    circleCollider = GetComponent<CircleCollider2D>();
  }
  private void Update()
  {
    crystalDestroyTimer -= Time.deltaTime;
    if (crystalDestroyTimer < 0)
    {
      FinishedCrystal();
    }

    if (canMove)
    {
      transform.position = Vector2.MoveTowards(transform.position, closestEnemy.position, moveSpeed * Time.deltaTime);
      if (Vector2.Distance(transform.position, closestEnemy.position) < 1f)
      {
        canMove = false;
        FinishedCrystal();
      }
    }

    if (canGrow)
    {
      transform.localScale = Vector2.Lerp(transform.localScale, growScale, growSpeed * Time.deltaTime);
      if (transform.localScale.x >= growScale.x)
      {
        canGrow = false;
      }
    }
  }


  public void SetupCrystal(float crystalDuration, bool canExplode, float growSpeed, Vector2 growScale, bool canMove, float moveSpeed, Transform closestEnemy)
  {
    crystalDestroyTimer = crystalDuration;
    this.canExplode = canExplode;
    this.growSpeed = growSpeed;
    this.growScale = growScale;
    this.canMove = canMove;
    this.moveSpeed = moveSpeed;
    this.closestEnemy = closestEnemy;
  }

  public void FinishedCrystal()
  {
    if (canExplode)
    {
      canGrow = true;
      anim.SetBool("Explode", true);
    }
    else
    {
      DestroyCrystal();
    }
  }

  public void ChooseRandomEnemy()
  {
    float radius = SkillManager.instance.blackhole.GetRadius();
    Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);
    if (colliders.Length > 0)
    {
      closestEnemy = colliders[Random.Range(0, colliders.Length)].transform;
    }

  }

  private void DestroyCrystal() => Destroy(gameObject);

  private void AnimationExplodeTrigger()
  {
    Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, circleCollider.radius);


    foreach (Collider2D hit in hits)
    {
      Enemy enemy = hit.GetComponent<Enemy>();
      if (enemy != null)
      {
        enemy.Damage();
      }
    }
  }
}
