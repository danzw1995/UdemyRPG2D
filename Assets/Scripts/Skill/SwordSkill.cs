using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SwordType
{
  Regular,
  Bounce,
  Pierce,
  Spin
}

public class SwordSkill : Skill
{

  [SerializeField] private SwordType swordType = SwordType.Regular;

  [Header("Bouncing info")]
  [SerializeField] private int amountOfBounce = 4;
  [SerializeField] private float bounceSpeed = 20f;
  [SerializeField] private float bounceCheckRadius = 10f;
  [SerializeField] private float bounceGravity = 1f;

  [Header("Pierce info")]
  [SerializeField] private int pierceAmount = 2;
  [SerializeField] private float pierceGravity = 1f;

  [Header("Spin info")]
  [SerializeField] private float maxTravelDistance = 7;
  [SerializeField] private float spinDuration = 2;
  [SerializeField] private float spinGravity = 1f;
  [SerializeField] private float hitCooldown = 0.4f;


  [Header("Skill info")]
  [SerializeField] private SwordSkillController swordPrefab;
  [SerializeField] private Vector2 launchForce;
  [SerializeField] private float swordGravity;
  [SerializeField] private float freezeTimeDuration = 1.5f;

  [SerializeField] private float returnSpeed = 16f;

  private Vector2 finalDirection;

  [Header("Aim dots")]
  [SerializeField] private int numberOfDots;
  [SerializeField] private float spaceBetweenDots;
  [SerializeField] private GameObject dotPrefab;
  [SerializeField] private Transform dotsParent;
  private GameObject[] dots;

  private void Start()
  {
    GenerateDots();

    SetGravity();
  }

  protected override void Update()
  {
    base.Update();

    if (Input.GetKeyUp(KeyCode.Mouse1))
    {
      Vector2 direction = AimDirection();
      finalDirection = new Vector2(direction.normalized.x * launchForce.x, direction.normalized.y * launchForce.y);
    }
    if (Input.GetKey(KeyCode.Mouse1))
    {
      for (int i = 0; i < numberOfDots; i++)
      {
        dots[i].transform.position = DotsPosition(i * spaceBetweenDots);
      }
    }
  }

  public void CreateSword()
  {
    SwordSkillController swordSkillController = Instantiate(swordPrefab, PlayerManager.instance.player.transform.position, transform.rotation);
    swordSkillController.SetupSword(finalDirection, swordGravity, PlayerManager.instance.player, freezeTimeDuration, returnSpeed);
    if (swordType == SwordType.Bounce)
    {
      swordSkillController.SetupBounce(true, amountOfBounce, bounceSpeed, bounceCheckRadius);
    }
    else if (swordType == SwordType.Pierce)
    {
      swordSkillController.SetupPierce(pierceAmount);
    }
    else if (swordType == SwordType.Spin)
    {
      swordSkillController.SetupSpin(true, maxTravelDistance, spinDuration, hitCooldown);
    }


    DotsActive(false);
    PlayerManager.instance.player.AssignSword(swordSkillController.gameObject);
  }

  public Vector2 AimDirection()
  {
    Vector2 playerPosition = PlayerManager.instance.player.transform.position;
    Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

    return mousePosition - playerPosition;

  }

  private void GenerateDots()
  {
    dots = new GameObject[numberOfDots];
    for (int i = 0; i < numberOfDots; i++)
    {
      dots[i] = Instantiate(dotPrefab, PlayerManager.instance.player.transform.position, Quaternion.identity, dotsParent);
      dots[i].SetActive(false);
    }
  }
  private void SetGravity()
  {
    if (swordType == SwordType.Bounce)
    {
      swordGravity = bounceGravity;
    }
    else if (swordType == SwordType.Pierce)
    {
      swordGravity = pierceGravity;
    }
    else if (swordType == SwordType.Spin)
    {
      swordGravity = spinGravity;
    }
  }

  public void DotsActive(bool isActive)
  {
    for (int i = 0; i < numberOfDots; i++)
    {
      dots[i].SetActive(isActive);
    }
  }

  private Vector2 DotsPosition(float t)
  {
    Vector2 direction = AimDirection();

    Vector2 position = (Vector2)PlayerManager.instance.player.transform.position + new Vector2(
      direction.normalized.x * launchForce.x,
      direction.normalized.y * launchForce.y
    ) * t + 0.5f * (Physics2D.gravity * swordGravity) * (t * t);

    return position;
  }
}
