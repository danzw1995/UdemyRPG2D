using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneSkill : Skill
{
  [Header("Clone info")]
  [SerializeField] private CloneSkillController clonePrefab = null;
  [SerializeField] private float cloneDuration = 1.5f;
  [SerializeField] private float cloneLosingSpeed = 20f;
  [SerializeField] private bool canAttack = true;
  [SerializeField] private bool canCreateOnEnter = false;
  [SerializeField] private bool canCreateOnExit = false;
  [SerializeField] private bool canCreateOnCounterAttack = false;

  [Header("Clone duplicate info")]
  [SerializeField] private bool canCloneDuplicate = false;
  [Range(0, 100)]
  [SerializeField] private int chanceDuplicate;

  [Header("Crystal instead clone")]
  public bool canCrystalInsteadClone;

  protected override void Update()
  {
    base.Update();
  }

  public void CreateClone(Vector3 position, Vector3 offset)
  {
    if (canCrystalInsteadClone)
    {
      SkillManager.instance.crystal.CreateCrystal();
      return;
    }


    CloneSkillController cloneSkillController = Instantiate(clonePrefab);

    cloneSkillController.SetupClone(position, cloneDuration, cloneLosingSpeed, canAttack, offset, FindClosetTarget(cloneSkillController.transform), canCloneDuplicate, chanceDuplicate);
  }

  public void CreateCloneOnEnter(Vector3 position, Vector3 offset)
  {
    if (canCreateOnEnter)
    {
      CreateClone(position, offset);
    }
  }

  public void CreateCloneOnExit(Vector3 position, Vector3 offset)
  {
    if (canCreateOnExit)
    {
      CreateClone(position, offset);
    }
  }

  public void CreateCloneOnCounterAttack(Vector3 position, Vector3 offset)
  {
    if (canCreateOnCounterAttack)
    {
      StartCoroutine(CreateCloneWithDelay(position, offset));
    }
  }

  private IEnumerator CreateCloneWithDelay(Vector3 position, Vector3 offset)
  {
    yield return new WaitForSeconds(0.4f);
    CreateClone(position, offset);
  }
}
