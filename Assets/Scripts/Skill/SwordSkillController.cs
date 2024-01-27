using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.U2D.IK;

public class SwordSkillController : MonoBehaviour
{

  private float returnSpeed;
  private Animator anim;
  private Rigidbody2D rb;
  private CircleCollider2D cb;
  private Player player;

  private bool canRotate = true;
  private bool isReturning;

  private float freezeTimeDuration;


  private bool isBouncing;
  private int bounceAmount;
  private float bounceSpeed;
  private float bounceCheckRadius;
  private List<Transform> enemyTargets;
  private int targetIndex;


  private float pierceAmount;

  #region spin state
  private bool isSpining;

  private float maxTravelDistance;
  private float spinDuration;
  private float spinTimer;
  private bool wasStopped;

  private float hitTimer;
  private float hitCooldown;

  private float spinDirection;
  #endregion

  private void Awake()
  {
    anim = GetComponentInChildren<Animator>();
    rb = GetComponent<Rigidbody2D>();
    cb = GetComponent<CircleCollider2D>();

    Invoke("DestroyMe", 7f);
  }

  private void DestroyMe()
  {
    Destroy(gameObject);
  }

  public void SetupSword(Vector2 direction, float gravity, Player player, float freezeTimeDuration, float returnSpeed)
  {
    rb.velocity = direction;
    rb.gravityScale = gravity;
    this.player = player;
    this.freezeTimeDuration = freezeTimeDuration;
    this.returnSpeed = returnSpeed;
    if (pierceAmount <= 0)
    {
      anim.SetBool("Rotation", true);
    }

    spinDirection = Mathf.Clamp(rb.velocity.x, -1, 1);
  }

  public void SetupBounce(bool isBouncing, int bounceAmount, float bounceSpeed, float bounceCheckRadius)
  {
    this.isBouncing = isBouncing;
    this.bounceAmount = bounceAmount;
    this.bounceSpeed = bounceSpeed;
    this.bounceCheckRadius = bounceCheckRadius;

    enemyTargets = new List<Transform>();
    targetIndex = 0;
  }

  public void SetupPierce(float pierceAmount)
  {
    this.pierceAmount = pierceAmount;
  }

  public void SetupSpin(bool isSpining, float maxTravelDistance, float spinDuration, float hitCooldown)
  {
    this.isSpining = isSpining;
    this.maxTravelDistance = maxTravelDistance;
    this.spinDuration = spinDuration;
    this.hitCooldown = hitCooldown;
  }

  private void Update()
  {
    if (canRotate)
    {
      transform.right = rb.velocity;
    }

    if (isReturning)
    {
      transform.position = Vector2.MoveTowards(transform.position, player.transform.position, returnSpeed * Time.deltaTime);

      if (Vector2.Distance(transform.position, player.transform.position) < 1)
      {
        player.CatchSword();
        isReturning = false;
      }
    }

    BounceLogic();

    SpinLogic();
  }

  private void SpinLogic()
  {
    if (isSpining)
    {
      if (Vector2.Distance(player.transform.position, transform.position) > maxTravelDistance && !wasStopped)
      {
        StopWhenSpining();
      }

      if (wasStopped)
      {
        spinTimer -= Time.deltaTime;

        transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x + spinDirection, transform.position.y), 1.5f * Time.deltaTime);
        if (spinTimer < 0)
        {
          isSpining = false;
          isReturning = true;
        }

        hitTimer -= Time.deltaTime;
        if (hitTimer < 0)
        {
          hitTimer = hitCooldown;

          Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, bounceCheckRadius);
          foreach (Collider2D collider in colliders)
          {
            Enemy enemy = collider.GetComponent<Enemy>();
            if (enemy != null)
            {
              DamageAndFreezeTime(enemy);
            }
          }
        }
      }
    }
  }

  private void StopWhenSpining()
  {
    wasStopped = true;
    rb.constraints = RigidbodyConstraints2D.FreezePosition;
    spinTimer = spinDuration;
  }

  private void BounceLogic()
  {
    if (isBouncing && enemyTargets.Count > 0)
    {
      transform.position = Vector2.MoveTowards(transform.position, enemyTargets[targetIndex].position, bounceSpeed * Time.deltaTime);

      if (Vector2.Distance(transform.position, enemyTargets[targetIndex].position) < 0.1f)
      {
        targetIndex++;
        bounceAmount--;
        if (bounceAmount <= 0)
        {
          isBouncing = false;
          isReturning = true;
        }


        if (targetIndex >= enemyTargets.Count)
        {
          targetIndex = 0;
        }
      }
    }
  }

  public void ReturnSword()
  {
    rb.constraints = RigidbodyConstraints2D.FreezeAll;
    transform.parent = null;
    isReturning = true;
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (isReturning)
    {
      return;
    }

    Enemy enemy = collision.GetComponent<Enemy>();
    if (enemy != null)
    {
      DamageAndFreezeTime(enemy);

    }


    if (isBouncing && enemy != null)
    {
      Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, bounceCheckRadius);
      foreach (Collider2D collider in colliders)
      {
        Enemy innerEnemy = collider.GetComponent<Enemy>();
        if (innerEnemy != null)
        {
          enemyTargets.Add(innerEnemy.transform);
        }
      }
    }

    StunInto(collision);
  }

  private void DamageAndFreezeTime(Enemy enemy)
  {
    EnemyCharacterStats enemyCharacterStats = enemy.GetComponent<EnemyCharacterStats>();
    PlayerManager.instance.player.characterStats.DoDamage(enemyCharacterStats);

    enemy.StartCoroutine("FreezeTimeFor", freezeTimeDuration);
  }

  private void StunInto(Collider2D collision)
  {

    if (pierceAmount > 0 && collision.GetComponent<Enemy>() != null)
    {
      pierceAmount--;
      return;
    }
    if (isSpining)
    {
      StopWhenSpining();
      return;
    }


    canRotate = false;
    cb.enabled = false;

    rb.isKinematic = true;
    rb.constraints = RigidbodyConstraints2D.FreezeAll;

    if (isBouncing && enemyTargets.Count > 0)
    {
      return;
    }

    anim.SetBool("Rotation", false);
    transform.parent = collision.transform;
  }
}
