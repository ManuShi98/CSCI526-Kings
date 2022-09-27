using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour
{
  private SpriteRenderer spriteRenderer;
  private static Hover instance;

  public static Hover Instance
  {
    get
    {
      if (instance == null)
      {
        GameObject obj = new GameObject();
        obj.name = "Singleton";
        instance = obj.AddComponent<Hover>();
      }
      return instance;
    }
  }

  void Awake()
  {
    instance = this;
  }

  // Start is called before the first frame update
  void Start()
  {
    this.spriteRenderer = GetComponent<SpriteRenderer>();
  }

  // Update is called once per frame
  void Update()
  {
    FollowMouse();
  }

  private void FollowMouse()
  {
    if (spriteRenderer.enabled)
    {
      transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
      transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }

  }

  public void Activate(Sprite sprite)
  {
    this.spriteRenderer.sprite = sprite;
    spriteRenderer.enabled = true;
  }

  public void Deactivate()
  {
    spriteRenderer.enabled = false;
  }
}
