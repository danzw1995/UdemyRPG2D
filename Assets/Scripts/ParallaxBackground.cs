using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
  private Camera cam;

  [SerializeField] private float parallaxEffect;

  private float xPosition;

  private float length;

  private void Start()
  {
    cam = Camera.main;

    xPosition = transform.position.x;

    length = GetComponent<SpriteRenderer>().bounds.size.x;

  }

  private void Update()
  {

    float distanceMove = cam.transform.position.x * (1 - parallaxEffect);
    float distanceToMove = cam.transform.position.x * parallaxEffect;

    transform.position = new Vector3(xPosition + distanceToMove, transform.position.y);

    if (distanceMove > xPosition + length)
    {
      xPosition = xPosition + length;
    }
    else if (distanceMove < xPosition - length)
    {
      xPosition = xPosition - length;
    }
  }
}
