using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditorInternal;
using UnityEngine;

public class Player : Entity
{
  [Header("Attack info")]
  public Vector2[] attackMovement;
  public bool isBusy { get; private set; }

  public float counterAttackDuration = 0.2f;
  [Header("Move info")]
  public float moveSpeed = 10f;
  public float jumpForce = 12f;

  [Header("Dash info")]

  [SerializeField] private float dashCoolDown = 0.5f;
  private float dashUsageTimer = 0;
  public float dashSpeed = 20f;
  public float dashDuration = 0.3f;

  public float dashDirection { get; private set; }


  #region State
  public PlayerStateMachine stateMachine { get; private set; }

  public PlayerIdleState idleState { get; private set; }
  public PlayerMoveState moveState { get; private set; }
  public PlayerGroundedState groundedState { get; private set; }
  public PlayerJumpState jumpState { get; private set; }
  public PlayerAirState airState { get; private set; }
  public PlayerDashState dashState { get; private set; }
  public PlayerWallSlideState wallSlideState { get; private set; }
  public PlayerWallJumpState wallJumpState { get; private set; }
  public PlayerPrimaryAttackState attackState { get; private set; }
  public PlayerCounterAttackState counterAttackState { get; private set; }
  #endregion

  protected override void Awake()
  {
    base.Awake();
    stateMachine = new PlayerStateMachine();
    idleState = new PlayerIdleState(this, stateMachine, "Idle");
    moveState = new PlayerMoveState(this, stateMachine, "Move");
    jumpState = new PlayerJumpState(this, stateMachine, "Jump");
    airState = new PlayerAirState(this, stateMachine, "Jump");
    dashState = new PlayerDashState(this, stateMachine, "Dash");
    wallSlideState = new PlayerWallSlideState(this, stateMachine, "WallSlide");
    wallJumpState = new PlayerWallJumpState(this, stateMachine, "Jump");
    attackState = new PlayerPrimaryAttackState(this, stateMachine, "Attack");
    counterAttackState = new PlayerCounterAttackState(this, stateMachine, "CounterAttack");

  }

  protected override void Start()
  {

    base.Start();
    stateMachine.Initialize(idleState);

  }

  protected override void Update()
  {
    base.Update();
    stateMachine.currentState.Update();

    CheckDashInputs();
  }

  private void CheckDashInputs()
  {

    if (IsWallDetected())
    {
      return;
    }

    dashUsageTimer -= Time.deltaTime;

    if (Input.GetKeyDown(KeyCode.LeftShift) && dashUsageTimer < 0)
    {
      dashDirection = Input.GetAxisRaw("Horizontal");

      if (dashDirection == 0)
      {
        dashDirection = facingDirection;
      }

      dashUsageTimer = dashCoolDown;

      stateMachine.ChangeState(dashState);
    }
  }

  public IEnumerator BusyFor(float seconds)
  {
    isBusy = true;

    yield return new WaitForSeconds(seconds);

    isBusy = false;
  }

}