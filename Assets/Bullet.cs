using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public Rigidbody2D bullet;

    // Start is called before the first frame update
    void Start()
    {
        bullet.velocity = -transform.right * speed;
    }

    // Update is called once per frame
    //void Update()
    //{

    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Health enemyHealth = collision.GetComponent<Health>();
        
        if(enemyHealth != null)
        {
            enemyHealth.TakeDamage(50);
        }
        Destroy(gameObject);    // gameObject指代自身
    }
}
