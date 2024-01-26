using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalSkill : Skill
{
  [SerializeField] private GameObject cryStalPrefab = null;
  [SerializeField] private float crystalDuration = 1.5f;
  [SerializeField] private bool canExplode = false;
  [SerializeField] private float growSpeed = 5f;
  [SerializeField] private Vector2 growScale;
  [SerializeField] private bool canMoveToEnemy = false;
  [SerializeField] private float moveSpeed = 5f;

  [SerializeField] private bool canUseMultipleStacks;
  [SerializeField] private int amountOfStacks;
  [SerializeField] private float multipleStackCooldown;
  [SerializeField] private float useTimeWindow;
  [SerializeField] private List<GameObject> crystalLeft = new List<GameObject>();

  [SerializeField] private bool canCreateCloneInsteadCrystal;

  private bool isInit = true;


  private GameObject currentCrystal;

  public override void UseSkill()
  {
    base.UseSkill();


    if (CanUseMultiCrystal())
      return;

    if (currentCrystal == null)
    {
      CreateCrystal();
    }
    else
    {

      if (canMoveToEnemy)
      {
        return;
      }
      Player player = PlayerManager.instance.player;
      Vector2 playerPos = player.transform.position;
      player.transform.position = currentCrystal.transform.position;
      CrystalSkillController crystalSkillController = currentCrystal.GetComponent<CrystalSkillController>();

      currentCrystal.transform.position = playerPos;

      if (canCreateCloneInsteadCrystal)
      {
        SkillManager.instance.clone.CreateClone(playerPos, Vector3.zero);
        Destroy(currentCrystal);
      }
      else
      {
        crystalSkillController.FinishedCrystal();

      }

    }
  }

  public void CreateCrystal()
  {
    currentCrystal = Instantiate(cryStalPrefab, PlayerManager.instance.player.transform.position, Quaternion.identity);
    CrystalSkillController crystalSkillController = currentCrystal.GetComponent<CrystalSkillController>();
    crystalSkillController.SetupCrystal(crystalDuration, canExplode, growSpeed, growScale, canMoveToEnemy, moveSpeed, FindClosetTarget(currentCrystal.transform));
  }

  public void CurrentCrystalChooseRandom() => currentCrystal.GetComponent<CrystalSkillController>().ChooseRandomEnemy();

  private bool CanUseMultiCrystal()
  {

    if (canUseMultipleStacks)
    {
      if (isInit)
      {
        isInit = false;
        RefillCrystal();
      }

      if (crystalLeft.Count > 0)
      {
        cooldown = 0;
        if (crystalLeft.Count == amountOfStacks)
        {
          Invoke("ResetAbility", useTimeWindow);
        }
        GameObject crystalToSpawn = crystalLeft[crystalLeft.Count - 1];
        GameObject crystal = Instantiate(crystalToSpawn, PlayerManager.instance.player.transform.position, Quaternion.identity);
        crystal.GetComponent<CrystalSkillController>().SetupCrystal(crystalDuration, canExplode, growSpeed, growScale, crystal, moveSpeed, FindClosetTarget(crystal.transform));
        crystalLeft.Remove(crystalToSpawn);

        if (crystalLeft.Count <= 0)
        {
          cooldown = multipleStackCooldown;
          RefillCrystal();
        }

        return true;
      }
    }

    return false;
  }

  private void RefillCrystal()
  {
    int amountToAdd = amountOfStacks - crystalLeft.Count;
    for (int i = 0; i < amountToAdd; i++)
    {
      crystalLeft.Add(cryStalPrefab);
    }
  }

  private void ResetAbility()
  {
    if (cooldownTimer > 0)
    {
      return;
    }

    cooldownTimer = multipleStackCooldown;
    RefillCrystal();
  }
}
