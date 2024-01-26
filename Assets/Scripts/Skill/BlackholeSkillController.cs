using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackholeSkillController : MonoBehaviour
{
  [SerializeField] private GameObject hotkeyPrefab = null;
  [SerializeField] private List<KeyCode> keycodeList = null;
  private bool canShrink;
  private bool canGrow = true;
  private bool canCreateHotkeys = true;
  private bool cloneAttackReleased;
  private float maxSize;
  private float growSpeed;
  private float shrinkSpeed;
  private int amountOfAttacks;
  private float cloneAttackCooldown;
  private float cloneAttackTimer;
  private float blackholeDuration;
  private bool playerCanDisapear = true;


  public List<Transform> targets = new List<Transform>();

  public List<GameObject> createdHotkeys;

  public bool playerCanExitState;

  public void SetupBlackhole(float maxSize, float growSpeed, float shrinkSpeed, int amountOfAttacks, float cloneAttackCooldown, float blackholeDuration)
  {
    this.maxSize = maxSize;
    this.growSpeed = growSpeed;
    this.shrinkSpeed = shrinkSpeed;
    this.amountOfAttacks = amountOfAttacks;
    this.cloneAttackCooldown = cloneAttackCooldown;
    this.blackholeDuration = blackholeDuration;
    playerCanExitState = false;
  }

  private void Update()
  {
    cloneAttackTimer -= Time.deltaTime;
    blackholeDuration -= Time.deltaTime;

    if (blackholeDuration < 0)
    {
      blackholeDuration = Mathf.Infinity;
      HandleBlackhole();
    }

    if (Input.GetKeyDown(KeyCode.R))
    {
      HandleBlackhole();
    }

    CloneAttackLogic();

    if (canGrow && !canShrink)
    {
      transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(maxSize, maxSize), growSpeed * Time.deltaTime);
    }

    if (canShrink)
    {
      transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(-1, -1), shrinkSpeed * Time.deltaTime);
      if (transform.localScale.x < 0)
      {
        Destroy(gameObject);
      }
    }
  }

  private void HandleBlackhole()
  {
    if (targets.Count > 0)
    {
      ReleaseCloneAttack();
    }
    else
    {
      FinishBlackHoleAbility();
    }
  }

  private void CloneAttackLogic()
  {
    if (cloneAttackTimer < 0 && cloneAttackReleased)
    {
      cloneAttackTimer = cloneAttackCooldown;
      int randomIndex = Random.Range(0, targets.Count);

      int xOffset = Random.Range(0, 100) > 50 ? 2 : -2;

      if (SkillManager.instance.clone.canCrystalInsteadClone)
      {
        SkillManager.instance.crystal.CreateCrystal();
        SkillManager.instance.crystal.CurrentCrystalChooseRandom();
      }
      else
      {
        SkillManager.instance.clone.CreateClone(targets[randomIndex].position, new Vector3(xOffset, 0));

      }


      amountOfAttacks--;
      if (amountOfAttacks <= 0)
      {
        Invoke("FinishBlackHoleAbility", 1f);
      }
    }
  }

  private void FinishBlackHoleAbility()
  {
    DestroyHotkeys();
    canShrink = true;
    cloneAttackReleased = false;
    playerCanExitState = true;
  }

  private void ReleaseCloneAttack()
  {
    if (targets.Count < 0)
    {
      return;
    }
    DestroyHotkeys();
    cloneAttackReleased = true;
    canCreateHotkeys = false;

    if (playerCanDisapear)
    {
      playerCanDisapear = false;
      PlayerManager.instance.player.MakeTransparent(true);

    }

  }

  private void OnTriggerEnter2D(Collider2D collision)
  {

    Enemy enemy = collision.GetComponent<Enemy>();

    if (enemy != null)
    {
      enemy.FreezeTime(true);
      CreateHotkey(collision, enemy);
    }
  }

  private void OnTriggerExit2D(Collider2D collision)
  {

    Enemy enemy = collision.GetComponent<Enemy>();

    if (enemy != null)
    {
      enemy.FreezeTime(false);
    }
  }

  private void CreateHotkey(Collider2D collision, Enemy enemy)
  {
    if (keycodeList.Count <= 0)
    {
      return;
    }
    if (!canCreateHotkeys)
    {
      return;
    }

    GameObject hotKeyInstance = Instantiate(hotkeyPrefab, collision.transform.position + new Vector3(0, 2), Quaternion.identity);

    createdHotkeys.Add(hotKeyInstance);

    KeyCode chooseKey = keycodeList[Random.Range(0, keycodeList.Count)];
    keycodeList.Remove(chooseKey);

    BlackholeHotKeyController blackholeHotKeyController = hotKeyInstance.GetComponent<BlackholeHotKeyController>();
    blackholeHotKeyController.SetupHotkey(chooseKey, enemy.transform, this);
  }

  private void DestroyHotkeys()
  {
    foreach (GameObject hotkey in createdHotkeys)
    {
      Destroy(hotkey);
    }
  }

  public void AddEnemy(Transform enemy) => targets.Add(enemy);
}
