using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float damage = 20f;
    public Rigidbody2D bullet;

    /// <summary>
    /// Open fire. The initial rotation refers to the fire point's rotation.
    /// And bullets always fly from the right direction,
    /// so the bullet prefabs need to guarantee their head is to the right side.
    /// </summary>
    /// <param name="rotation"></param>
    public void fire(Quaternion rotation)
    {
        transform.rotation = rotation;
        bullet.velocity = transform.right * speed;
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
        Destroy(gameObject);
    }
}
