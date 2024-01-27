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

  [Header("Ailment colors")]
  [SerializeField] private Color[] chillColors;
  [SerializeField] private Color[] igniteColors;
  [SerializeField] private Color[] shockColors;

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

    Color currentColor = spriteRenderer.color;

    spriteRenderer.color = Color.white;
    yield return new WaitForSeconds(flashDuration);
    spriteRenderer.color = currentColor;

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

  private void CancelColorChange()
  {
    CancelInvoke();
    spriteRenderer.color = Color.white;
  }

  public void IgniteColorFor(float seconds)
  {
    InvokeRepeating("IgniteColorFX", 0, 0.3f);
    Invoke("CancelColorChange", seconds);
  }

  public void ChillColorFor(float seconds)
  {
    InvokeRepeating("ChillColorFX", 0, 0.3f);
    Invoke("CancelColorChange", seconds);
  }

  public void ShockColorFor(float seconds)
  {
    InvokeRepeating("ShockColorFX", 0, 0.3f);
    Invoke("CancelColorChange", seconds);
  }

  private void IgniteColorFX()
  {
    if (spriteRenderer.color != igniteColors[0])
    {
      spriteRenderer.color = igniteColors[0];
    }
    else
    {
      spriteRenderer.color = igniteColors[1];
    }
  }

  private void ChillColorFX()
  {
    if (spriteRenderer.color != chillColors[0])
    {
      spriteRenderer.color = chillColors[0];
    }
    else
    {
      spriteRenderer.color = chillColors[1];
    }
  }

  private void ShockColorFX()
  {
    if (spriteRenderer.color != shockColors[0])
    {
      spriteRenderer.color = shockColors[0];
    }
    else
    {
      spriteRenderer.color = shockColors[1];
    }
  }
}
