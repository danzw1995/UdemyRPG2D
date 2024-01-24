using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BlackholeHotKeyController : MonoBehaviour
{
  private KeyCode myHotKey;
  private TextMeshProUGUI myText;

  private Transform enemyTransform;
  private BlackholeSkillController blackholeSkillController;

  private SpriteRenderer sr;

  public void SetupHotkey(KeyCode hotkey, Transform enemyTransform, BlackholeSkillController blackholeSkillController)
  {
    myText = GetComponentInChildren<TextMeshProUGUI>();
    sr = GetComponent<SpriteRenderer>();
    myHotKey = hotkey;
    myText.text = myHotKey.ToString();

    this.enemyTransform = enemyTransform;
    this.blackholeSkillController = blackholeSkillController;
  }

  private void Update()
  {
    if (Input.GetKeyDown(myHotKey))
    {
      blackholeSkillController.AddEnemy(enemyTransform);

      myText.color = Color.clear;
      sr.color = Color.clear;
    }
  }
}
