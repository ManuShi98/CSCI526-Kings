using UnityEngine;

public class SandstormEnemyChangeEvent : IEventData
{
    public int NumberOfEnemy { get; set; }
}

public class EnemyUnit : MonoBehaviour, IEventHandler<SeasonChangeEvent>, IEventHandler<WeatherEvent>
{
    private float speed;

    [SerializeField]
    private float startSpeed = 1;

    // Foggy paused speed
    private float pausedSpeed = 1;

    [SerializeField]
    private float health = 400;
    private float previousHealthRate = 1f;
    private int coinValue = 1;
    private Vector3 startScale;
    private Color startColor;

    [SerializeField]
    private int startCoinValue = 1;
    private Transform[] positions;
    private int index = 0;
    private Path path;
    private SpriteRenderer spriteRenderer;
    private GamingDataController gamingDataController;

    public bool canCauseSandstorm;

    // Duration of special effects
    private float arrowBulletSlowDownEffectTimer;

    void Start()
    {
        speed = startSpeed;
        coinValue = startCoinValue;
        startScale = transform.localScale;

        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        startColor = spriteRenderer.color;

        positions = path.positions;

        EventBus.register<SeasonChangeEvent>(this);
        EventBus.register<WeatherEvent>(this);

        HandleEvent(new SeasonChangeEvent() { ChangedSeason = SeasonController.GetSeason() });
        HandleEvent(new WeatherEvent() { weather = WeatherSystem.GetWeather() });

        gamingDataController = GamingDataController.GetInstance();

        if (canCauseSandstorm)
        {
            EventBus.post(new SandstormEnemyChangeEvent() { NumberOfEnemy = 1 });
        }
    }

    void Update()
    {
        Move();


        if (arrowBulletSlowDownEffectTimer > 0)
        {
            arrowBulletSlowDownEffectTimer -= Time.deltaTime;
            if(arrowBulletSlowDownEffectTimer <= 0)
            {
                speed = startSpeed;
                //Debug.Log("当前移动速度：" + speed);
            }
        }
    }

    void Move()
    {
        if (index > positions.Length - 1)
        {
            UpdateReachEndData();
            gamingDataController.ReduceHealth();
            Destroy(gameObject);
            return;
        }
        transform.Translate(speed * Time.deltaTime * (positions[index].position - transform.position).normalized);
        if (Vector3.Distance(positions[index].position, transform.position) < 0.3f)
        {
            index++;
        }
    }

    void OnDestroy()
    {
        if (canCauseSandstorm)
        {
            EventBus.post(new SandstormEnemyChangeEvent() { NumberOfEnemy = -1 });
        }
        EventBus.unregister<SeasonChangeEvent>(this);
        EventBus.unregister<WeatherEvent>(this);
    }

    public void SetPath(Path path)
    {
        this.path = path;
    }

    public float GetHealth()
    {
        return health;
    }

    // Season change handler
    public void HandleEvent(SeasonChangeEvent eventData)
    {
        if (eventData.ChangedSeason == Season.SPRING)
        {
            speed = startSpeed;
            health = health / previousHealthRate * 1f;
            previousHealthRate = 1f;
            coinValue = startCoinValue + 1;
        }
        else if (eventData.ChangedSeason == Season.SUMMER)
        {
            speed = startSpeed;
            health = health / previousHealthRate * 0.8f;
            previousHealthRate = 0.8f;
            coinValue = startCoinValue - 1;
        }
        else if (eventData.ChangedSeason == Season.AUTUMN)
        {
            speed = 0.8f * startSpeed;
            health = health / previousHealthRate * 1f;
            previousHealthRate = 1f;
            coinValue = startCoinValue + 1;
        }
        else if (eventData.ChangedSeason == Season.WINTER)
        {
            speed = 0.7f * startSpeed;
            health = health / previousHealthRate * 1f;
            previousHealthRate = 1f;
            coinValue = startCoinValue - 1;
        }
        SizeAndColorChange();
    }

    // Deal with the bullet and monster collision logic.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            GameObject bullet = collision.gameObject;
            Bullet bulletFunction = bullet.GetComponent<Bullet>();
            Debug.Log(bullet.name);

            // Special effect: slow down the enemy's moving speed.
            if ("BulletArrow(Clone)".Equals(bullet.name))
            {
                speed = (float)(startSpeed * 0.95);
                //Debug.Log("减速后移动速度：" + speed);
                arrowBulletSlowDownEffectTimer = 0.2f;
            }

            TakeDamage(bulletFunction.GetDamage());
            bulletFunction.DisplayHittingEffect();
            Destroy(bullet);
        }
    }

    private void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Singleton.Instance.numOfDiedMonster++;
            Singleton.Instance.curDiedMonster++;
            GamingDataController.GetInstance().AddCoins(coinValue);
            Destroy(gameObject);
        }
    }

    private void UpdateReachEndData()
    {
        Singleton.Instance.numOfReachEndMonster++;
        Singleton.Instance.curReachEndMonster++;
        if (SeasonController.GetSeason() == Season.SPRING)
        {
            Singleton.Instance.numOfSpringReachEndMonster++;
        }
        else if (SeasonController.GetSeason() == Season.SUMMER)
        {
            Singleton.Instance.numOfSummerReachEndMonster++;
        }
        else if (SeasonController.GetSeason() == Season.AUTUMN)
        {
            Singleton.Instance.numOfFallReachEndMonster++;
        }
        else if (SeasonController.GetSeason() == Season.WINTER)
        {
            Singleton.Instance.numOfWinterReachEndMonster++;
        }
    }

    // Enemy will grow big and turn red in summer
    private void SizeAndColorChange()
    {
        if (SeasonController.GetSeason() == Season.SUMMER)
        {
            transform.localScale = new Vector3(startScale.x * 1.5f, startScale.y * 1.5f, startScale.z);
            spriteRenderer.color = Color.red;
        }
        else
        {
            transform.localScale = startScale;
            spriteRenderer.color = startColor;
        }

    }

    // Weather change handler
    public void HandleEvent(WeatherEvent eventData)
    {
        if (eventData.weather == Weather.FOGGY)
        {
            System.Random rd = new System.Random();
            int num = rd.Next(1, 11);
            if (num <= 2)
            {
                FoggyPause();
                Invoke(nameof(FoggyResume), 2);
            }
        }
    }

    // Foggy effects
    private void FoggyPause()
    {
        pausedSpeed = speed;
        speed = 0;
    }

    private void FoggyResume()
    {
        speed = pausedSpeed;
        HandleEvent(new SeasonChangeEvent() { ChangedSeason = SeasonController.GetSeason() });
    }
}
