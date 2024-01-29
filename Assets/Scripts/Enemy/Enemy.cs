using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
  [Header("Stunned info")]
  public float stunDuration;
  public Vector2 stunDirection;
  protected bool canBeStunned;
  [SerializeField] protected GameObject counterImage;

  [Header("Move info")]
  public float moveSpeed = 6f;

  public int moveDirection = 1;
  public float idleTime = 2f;

  private float defaultSpeed;


  [Header("Attack info")]
  public float attackDistance = 1.5f;

  public float playerCheckDistance = 50f;

  public float attackCooldown = 1f;

  public float battleTime = 4f;

  [HideInInspector] public float lastAttackTime;

  public LayerMask whatIsPlayer;

  public EnemyStateMachine stateMachine { get; private set; }

  public string lastAnimBoolName;

  private float defaultMoveSpeed;
  protected override void Awake()
  {
    base.Awake();

    stateMachine = new EnemyStateMachine();
    defaultSpeed = moveSpeed;
  }

  protected override void Start()
  {
    base.Start();
    defaultMoveSpeed = moveSpeed;
  }

  protected override void Update()
  {
    base.Update();
    stateMachine.currentState.Update();
  }

  public RaycastHit2D IsPlayerDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDirection, playerCheckDistance, whatIsPlayer);

  protected override void OnDrawGizmos()
  {
    base.OnDrawGizmos();

    Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + attackDistance * facingDirection, wallCheck.position.y));
  }

  public virtual void FreezeTime(bool timeFrozen)
  {
    if (timeFrozen)
    {
      moveSpeed = 0;
      anim.speed = 0;
    }
    else
    {
      moveSpeed = defaultSpeed;
      anim.speed = 1;
    }
  }

  public virtual IEnumerator FreezeTimeFor(float seconds)
  {
    FreezeTime(true);
    yield return new WaitForSeconds(seconds);
    FreezeTime(false);
  }

  public virtual void OpenCounterAttackWindow()
  {
    canBeStunned = true;
    counterImage.SetActive(true);
  }

  public virtual void CloseCounterAttackWindow()
  {
    canBeStunned = false;
    counterImage.SetActive(false);
  }

  public virtual bool CanBeStunned()
  {
    if (canBeStunned)
    {
      CloseCounterAttackWindow();
      return true;
    }

    return false;
  }

  public void AssignLastAnimBoolName(string animBoolName)
  {
    lastAnimBoolName = animBoolName;
  }

  public virtual void Die()
  {

  }

  public override void SlowEntityBy(float slowPercentage, float slowDuration)
  {
    moveSpeed = moveSpeed * (1 - slowPercentage);
    anim.speed = anim.speed * (1 - slowPercentage);
    Invoke("ReturnDefaultSpeed", slowDuration);

  }

  protected override void ReturnDefaultSpeed()
  {
    anim.speed = 1;
  }
}
