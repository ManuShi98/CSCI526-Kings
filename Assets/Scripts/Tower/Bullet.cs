using UnityEngine;

//todo: 【性能优化】将子弹以对象池的方式实现
public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float damage = 20f;
    public Rigidbody2D bullet;
    public GameObject effect;

    private GameObject target;

    /// <summary>
    /// Open fire. The initial rotation refers to the fire point's rotation.
    /// And bullets always fly from the right direction,
    /// so the bullet prefabs need to guarantee their head is to the right side.
    /// </summary>
    /// <param name="rotation"></param>
    public void fire(GameObject enemy)
    {
        target = enemy;

        //transform.rotation = rotation;
        //bullet.velocity = transform.right * speed;
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

        if (enemy.GetHealth() > 0)
        {
            enemy.TakeDamage(damage);
        }
        Instantiate(effect, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    private void Update()
    {
        if(target != null)
        {
            Vector2 dir = target.transform.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            bullet.velocity = transform.right * speed;
        }
    }
}