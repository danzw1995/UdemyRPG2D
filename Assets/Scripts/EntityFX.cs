using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityFX : MonoBehaviour
{
  [Header("FX info")]
  [SerializeField] private Material hitMaterial;
  [SerializeField] private float flashDuration = 0.2f;
  private Material originalMaterial;
  private SpriteRenderer spriteRenderer;

  private void Awake()
  {
    spriteRenderer = GetComponentInChildren<SpriteRenderer>();

  }

  private void Start()
  {
    originalMaterial = spriteRenderer.material;
  }

  private IEnumerator FlashFX()
  {
    spriteRenderer.material = hitMaterial;

    yield return new WaitForSeconds(flashDuration);

    spriteRenderer.material = originalMaterial;
  }

  private void RedColorBlink()
  {
    if (spriteRenderer.color != Color.white)
    {
      spriteRenderer.color = Color.white;
    }
    else
    {
      spriteRenderer.color = Color.red;
    }
  }

  private void CancelRedBlink()
  {
    CancelInvoke();
    spriteRenderer.color = Color.white;
  }
}
