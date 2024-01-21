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

  protected override void Update()
  {
    base.Update();
  }

  public void CreateClone(Vector3 position)
  {
    CloneSkillController cloneSkillController = Instantiate(clonePrefab);

    cloneSkillController.SetupClone(position, cloneDuration, cloneLosingSpeed, canAttack);
  }
}
