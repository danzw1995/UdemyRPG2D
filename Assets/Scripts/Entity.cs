using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{


  [Header("Collision info")]
  [SerializeField] protected Transform groundCheck = null;
  [SerializeField] protected float groundCheckDistance = 1f;
  [SerializeField] protected LayerMask whatIsGround;
  [SerializeField] protected Transform wallCheck = null;
  [SerializeField] protected float wallCheckDistance = 1f;
  [SerializeField] protected LayerMask whatIsWall;
  public Transform attackCheck = null;
  public float attackRadius = 0.8f;

  [Header("Knock info")]
  [SerializeField] protected Vector2 knockBackDirection;
  [SerializeField] protected float knockBackDuration;
  private bool isKnocked;

  #region Components
  public Animator anim { get; private set; }
  public Rigidbody2D rb { get; private set; }
  public EntityFX fx { get; private set; }

  public SpriteRenderer sr { get; private set; }

  public CapsuleCollider2D cd { get; private set; }
  public CharacterStats characterStats { get; private set; }


  #endregion

  public int facingDirection { get; set; } = 1;
  protected bool facingRight = true;

  public Action OnFlipped;

  protected virtual void Awake()
  {
    anim = GetComponentInChildren<Animator>();
    sr = GetComponentInChildren<SpriteRenderer>();
    rb = GetComponent<Rigidbody2D>();
    fx = GetComponent<EntityFX>();
    cd = GetComponent<CapsuleCollider2D>();
    characterStats = GetComponent<CharacterStats>();
  }

  protected virtual void Start()
  {

  }

  protected virtual void Update() { }

  public virtual void SlowEntityBy(float slowPercentage, float slowDuration)
  {

  }

  protected virtual void ReturnDefaultSpeed()
  {
    anim.speed = 1;
  }


  public bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
  public bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDirection, wallCheckDistance, whatIsGround);

  protected virtual void OnDrawGizmos()
  {
    Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
    Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
    Gizmos.DrawWireSphere(attackCheck.position, attackRadius);
  }

  public virtual void SetZeroVelocity()
  {
    SetVelocity(0, 0);
  }

  public virtual void SetVelocity(float xVelocity, float yVelocity)
  {
    if (isKnocked)
    {
      return;
    }

    rb.velocity = new Vector2(xVelocity, yVelocity);

    FlipController(xVelocity);
  }

  public virtual void Damage()
  {
    Debug.Log(gameObject.name + "  was damaged.");

    fx.StartCoroutine("FlashFX");

    StartCoroutine(HitKnockBack());
  }

  protected virtual IEnumerator HitKnockBack()
  {
    isKnocked = true;
    rb.velocity = new Vector2(knockBackDirection.x * -facingDirection, knockBackDirection.y);

    yield return new WaitForSeconds(knockBackDuration);

    isKnocked = false;
  }

  public void Flip()
  {
    facingDirection *= -1;
    facingRight = !facingRight;

    transform.Rotate(0, 180, 0);

    if (OnFlipped != null)
    {
      OnFlipped();
    }

  }

  protected virtual void FlipController(float x)
  {
    if (x > 0 && !facingRight)
    {
      Flip();
    }
    else if (x < 0 && facingRight)
    {
      Flip();
    }
  }
}
