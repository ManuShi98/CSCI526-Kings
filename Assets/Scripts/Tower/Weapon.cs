using UnityEngine;

public class Weapon : MonoBehaviour, IEventHandler<SeasonChangeEvent>, IEventHandler<SandstormStartEvent>, IEventHandler<WeatherEvent>
{
    [SerializeField]
    protected float startRadius = 5;
    protected float radius = 5; // Weapon's firing radius
    public float FiringRate = 10;  // Fire per second
    public float startFiringRate;

    [SerializeField]
    protected float startDamage = 20f;
    protected float damage = 20f;
    public float rate = 1.0f;

    public Transform firePoint;

    public GameObject enemy; // Locked enemy instance
    public GameObject bulletPrefab;

    protected float FiringIntervalTime;
    protected float timer;

    private GameObject range;

    void Start()
    {
        OnStart();
    }

    protected virtual void OnStart()
    {
        startFiringRate = FiringRate;
        damage = startDamage;
        FiringIntervalTime = 1.0f / FiringRate;
        timer = FiringIntervalTime;
        range = gameObject.transform.Find("Range").gameObject;

        EventBus.register<SeasonChangeEvent>(this);
        EventBus.register<SandstormStartEvent>(this);
        EventBus.register<WeatherEvent>(this, true);

        SeasonChangeHandleEvent(new SeasonChangeEvent() { ChangedSeason = SeasonController.GetSeason() });
    }

    void Update()
    {
        OnUpdate();
    }

    void OnDestroy()
    {
        EventBus.unregister<SeasonChangeEvent>(this);
        EventBus.unregister<SandstormStartEvent>(this);
        EventBus.unregister<WeatherEvent>(this);
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
                newBullet.damage = damage*rate;
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
                //Debug.Log(currDistance);
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
        if (eventData.ChangedSeason == Season.SPRING)
        {
            damage *= 0.7f;
            radius = startRadius;
        }
        else if (eventData.ChangedSeason == Season.SUMMER)
        {
            damage = startDamage;
            radius = startRadius;
        }
        else if (eventData.ChangedSeason == Season.AUTUMN)
        {
            damage = startDamage;
            radius = (float)(startRadius * 0.8);
        }
        else if (eventData.ChangedSeason == Season.WINTER)
        {
            damage = startDamage;
            radius = startRadius;
        }

        Debug.Log("当前武器: " + gameObject.name + "    攻击半径: " + radius);
        // 如果正在展示攻击范围，则通过开关Active状态来刷新攻击范围。
        if(range.activeSelf)
        {
            range.SetActive(false);
            range.SetActive(true);
        }
    }

    public double GetRadius()
    {
        return Mathf.Max(radius, 0);
    }

    public void HandleEvent(WeatherEvent eventData)
    {
        if (eventData.weather == Weather.CLOUDY)
        {
            FiringRate = startFiringRate / 2f;
            FiringIntervalTime = 1.0f / FiringRate;
        }
        else
        {
            FiringRate = startFiringRate;
            FiringIntervalTime = 1.0f / FiringRate;
        }
    }
}