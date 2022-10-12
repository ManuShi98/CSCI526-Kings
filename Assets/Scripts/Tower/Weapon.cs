using UnityEngine;

public class Weapon : MonoBehaviour, IEventHandler<SeasonChangeEvent>, IEventHandler<SandstormStartEvent>
{
    [SerializeField]
    protected float startRadius = 5;
    protected float radius = 5; // Weapon's firing radius
    public float FiringRate = 10;  // Fire per second

    [SerializeField]
    protected float startDamage = 20f;
    protected float damage = 20f;

    public Transform firePoint;

    public GameObject enemy; // Locked enemy instance
    public GameObject bulletPrefab;

    protected float FiringIntervalTime;
    protected float timer;

    void Start()
    {
        OnStart();
    }

    protected virtual void OnStart()
    {
        damage = startDamage;
        FiringIntervalTime = 1.0f / FiringRate;
        timer = FiringIntervalTime;

        EventBus.register<SeasonChangeEvent>(this);
        EventBus.register<SandstormStartEvent>(this);

        SeasonChangeHandleEvent(new SeasonChangeEvent() { changedSeason = SeasonController.GetSeason() });
    }

    void Update()
    {
        OnUpdate();
    }

    void OnDestroy()
    {
        EventBus.unregister<SeasonChangeEvent>(this);
        EventBus.unregister<SandstormStartEvent>(this);
    }

    protected virtual void OnUpdate()
    {
        if (enemy != null)
        {
            if (TwoPointDistance2D(transform.position, enemy.transform.position) > radius)
            {
                enemy = null;
            }

            // 射击
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                Bullet newBullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation).GetComponent<Bullet>();
                newBullet.damage = damage;
                newBullet.Fire(enemy);
                timer = FiringIntervalTime;
            }
        }
        else
        {
            // 在有效范围内，优先攻击距离最近的敌人
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            double minDistance = double.MaxValue;
            foreach (GameObject e in enemies)
            {
                double currDistance = TwoPointDistance2D(e.transform.position, transform.position);
                Debug.Log(currDistance);
                if (currDistance <= radius && currDistance < minDistance)
                {
                    minDistance = currDistance;
                    enemy = e;
                }
            }
        }
    }

    /// <summary>
    /// Calculate the distance between two points in 2D game
    /// </summary>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <returns></returns>
    private float TwoPointDistance2D(Vector2 p1, Vector2 p2)
    {
        return Mathf.Sqrt((p1.x - p2.x) * (p1.x - p2.x) + (p1.y - p2.y) * (p1.y - p2.y));
    }

    // Season change handler
    public void HandleEvent(SeasonChangeEvent eventData)
    {
        SeasonChangeHandleEvent(eventData);
    }

    // Sandstorm start handler
    public void HandleEvent(SandstormStartEvent eventData)
    {
        if (eventData.isSandstormStart)
        {
            radius = (float)(startRadius * 0.8);
        }
        else
        {
            radius = startRadius;
        }
    }

    protected virtual void SeasonChangeHandleEvent(SeasonChangeEvent eventData)
    {
        if (eventData.changedSeason == Season.SPRING)
        {
            damage *= 0.7f;
            radius = startRadius;
        }
        else if (eventData.changedSeason == Season.SUMMER)
        {
            damage = startDamage;
            radius = startRadius;
        }
        else if (eventData.changedSeason == Season.AUTUMN)
        {
            damage = startDamage;
            radius = (float)(startRadius * 0.8);
        }
        else if (eventData.changedSeason == Season.WINTER)
        {
            damage = startDamage;
            radius = startRadius;
        }

        Debug.Log("当前武器: " + gameObject.name + "    攻击半径: " + radius);
    }

    public double GetRadius()
    {
        return Mathf.Max(radius, 0);
    }
}