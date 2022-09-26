using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
  public float speed = 10f;
  public float damage = 20f;
  public Rigidbody2D bullet;

  void Start()
  {
    bullet.velocity = transform.up * speed;
  }

  public void OnBecameInvisible()
  {
    Destroy(gameObject);
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (!collision.CompareTag("Enemy"))
      return;

    EnemyUnit enemy = collision.GetComponent<EnemyUnit>();

    if (enemy.health > 0)
    {
      enemy.TakeDamage(damage);
    }
    Destroy(gameObject);    // gameObject指代自身
  }
}
