using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchUI : MonoBehaviour
{


  [SerializeField] private GameObject charcaterUI;
  [SerializeField] private GameObject skillTreeUI;
  [SerializeField] private GameObject craftUI;
  [SerializeField] private GameObject optionsUI;

  private void Awake()
  {
    SwitchTo(null);

  }
  private void Update()
  {
    if (Input.GetKeyDown(KeyCode.C))
    {
      SwitchWithKeyTo(charcaterUI);
    }


    if (Input.GetKeyDown(KeyCode.B))
    {
      SwitchWithKeyTo(craftUI);
    }



    if (Input.GetKeyDown(KeyCode.K))
    {
      SwitchWithKeyTo(skillTreeUI);
    }


    if (Input.GetKeyDown(KeyCode.O))
    {
      SwitchWithKeyTo(optionsUI);
    }

  }

  public void SwitchTo(GameObject menuTransform)
  {
    for (int i = 0; i < transform.childCount; i++)
    {
      transform.GetChild(i).gameObject.SetActive(false);
    }

    if (menuTransform != null)
    {
      menuTransform.SetActive(true);
    }
  }

  public void SwitchWithKeyTo(GameObject _menu)
  {
    if (_menu != null && _menu.activeSelf)
    {
      _menu.SetActive(false);
      return;
    }

    SwitchTo(_menu);
  }
}
