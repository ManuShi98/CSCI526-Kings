using UnityEngine;

public class SandstormEnemyChangeEvent : IEventData
{
    public int NumberOfEnemy { get; set; }
}

public class EnemyUnit : MonoBehaviour, IEventHandler<SeasonChangeEvent>, IEventHandler<WeatherEvent>
{
    [SerializeField]
    private float startSpeed;
    private float speed;

    [SerializeField]
    private float health;
    private float previousHealthRate = 1f;

    [SerializeField]
    private int startCoinValue;
    private int coinValue;

    // Effect ratio
    private float speedRatio = 1f;
    private float coinRatio = 1f;
    private float damageRatio = 1f;

    // Record current season
    private Season currSeason;

    // Foggy paused speed
    private float pausedSpeed;

    private Vector3 startScale;
    private Color startColor;
    private Transform[] positions;
    private int index = 0;
    private Path path;
    private SpriteRenderer spriteRenderer;
    private GamingDataController gamingDataController;

    // HP bar
    [SerializeField]
    private HPBar bar;

    public bool canCauseSandstorm;

    // Duration of special effects
    private float arrowBulletSlowDownEffectTimer = 0f;

    void Start()
    {
        speed = startSpeed;
        coinValue = startCoinValue;
        startScale = transform.localScale;

        currSeason = SeasonController.GetSeason();

        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        startColor = spriteRenderer.color;

        bar.SetMaxHP(health);

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
            if (arrowBulletSlowDownEffectTimer <= 0)
            {
                speedRatio += 0.1f;
                speed = startSpeed * speedRatio;
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

    /// <summary>
    /// Season change handler
    /// 1. Spring: Normal & Baseline
    /// 2. Summer: Increase damageRatio which can make tower kill the enemies faster.
    ///            Increase enemies' moving speed.
    ///            Decrease gold income.
    /// 3. Autumn: Decrease damageRatio to make the enemies look like have more health.
    ///            Clear debuff.
    /// 4. Winter: Decrease enemies' moving speed.
    ///            Decrease gold income.
    /// </summary>
    /// <param name="eventData"></param>
    public void HandleEvent(SeasonChangeEvent eventData)
    {
        SizeAndColorChange();
        if (currSeason == eventData.ChangedSeason)
            return;

        // Reset all the properties to normal(Spring)
        if (currSeason != Season.SPRING)
        {
            if (currSeason == Season.SUMMER)
            {
                //damageRatio -= 0.1f;
                speedRatio -= 0.2f;
                coinValue += 1;
            }
            else if (currSeason == Season.AUTUMN)
            {
                //damageRatio += 0.1f;
            }
            else if (currSeason == Season.WINTER)
            {
                speedRatio += 0.2f;
                coinValue += 1;
            }

            coinValue = startCoinValue;
            health = health / previousHealthRate * 1f;
            previousHealthRate = 1f;
        }

        currSeason = eventData.ChangedSeason;

        if (currSeason != Season.SPRING)
        {
            if (currSeason == Season.SUMMER)
            {
                //damageRatio += 0.1f;
                speedRatio += 0.2f;
                coinValue -= 1;
                previousHealthRate = 0.8f;
            }
            else if (currSeason == Season.AUTUMN)
            {
                //damageRatio -= 0.1f;
                previousHealthRate = 1.1f;
            }
            else if (currSeason == Season.WINTER)
            {
                speedRatio -= 0.2f;
                coinValue -= 1;
            }
        }

        health *= previousHealthRate;
        bar.HPRateEffect(previousHealthRate);
        speed = startSpeed * speedRatio;



        //if (eventData.ChangedSeason == Season.SPRING)
        //{
        //    speed = startSpeed;
        //    health = health / previousHealthRate * 1f;
        //    previousHealthRate = 1f;
        //    coinValue = startCoinValue + 1;
        //}
        //else if (eventData.ChangedSeason == Season.SUMMER)
        //{
        //    speed = startSpeed;
        //    health = health / previousHealthRate * 0.8f;
        //    previousHealthRate = 0.8f;
        //    coinValue = startCoinValue - 1;
        //}
        //else if (eventData.ChangedSeason == Season.AUTUMN)
        //{
        //    speed = 0.8f * startSpeed;
        //    health = health / previousHealthRate * 1f;
        //    previousHealthRate = 1f;
        //    coinValue = startCoinValue + 1;
        //}
        //else if (eventData.ChangedSeason == Season.WINTER)
        //{
        //    speed = 0.7f * startSpeed;
        //    health = health / previousHealthRate * 1f;
        //    previousHealthRate = 1f;
        //    coinValue = startCoinValue - 1;
        //}
    }

    // Deal with the bullet and monster collision logic.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            GameObject bullet = collision.gameObject;
            Bullet bulletFunction = bullet.GetComponent<Bullet>();
            //Debug.Log(bullet.name);

            // Special effect: slow down the enemy's moving speed.
            if ("BulletArrow(Clone)".Equals(bullet.name) && arrowBulletSlowDownEffectTimer <= 0)
            {
                speedRatio -= 0.1f;
                speed = startSpeed * speedRatio;
                Debug.Log("减速后移动速度：" + speed);
                arrowBulletSlowDownEffectTimer = 0.2f;
            }

            TakeDamage(bulletFunction.GetDamage());
            bulletFunction.DisplayHittingEffect();
            Destroy(bullet);
        }
    }

    private void TakeDamage(float damage)
    {
        health -= damage * damageRatio;
        bar.SetHP(health);

        if (health <= 0)
        {
            Singleton.Instance.numOfDiedMonster++;
            Singleton.Instance.curDiedMonster++;
            GamingDataController.GetInstance().AddCoins(coinValue);
            Destroy(gameObject);
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

    // Data Collection Part
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
}
