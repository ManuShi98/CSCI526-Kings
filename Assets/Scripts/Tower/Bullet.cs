using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float damage = 20f;
    public Rigidbody2D bullet;
    public GameObject effect;

    private GameObject target;

    // Set speed and direction to the enemy
    public void Fire(GameObject enemy)
    {
        target = enemy;

        Vector2 dir = target.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        bullet.velocity = transform.right * speed;
    }

    public void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    public float GetDamage()
    {
        return damage;
    }

    public void DisplayHittingEffect()
    {
        Instantiate(effect, transform.position, transform.rotation);
    }
}