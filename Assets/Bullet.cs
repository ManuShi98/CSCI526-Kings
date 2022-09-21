using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
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

        Health enemyHealth = collision.GetComponent<Health>();
        
        if(enemyHealth != null)
        {
            enemyHealth.TakeDamage(20);
        }
        Destroy(gameObject);    // gameObject指代自身
    }
}
