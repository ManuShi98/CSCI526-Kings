using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SandstormEnemyChangeEvent : IEventData
{
    public int numberOfEnemy { get; set; }
}

public class EnemyUnit : MonoBehaviour, IEventHandler<SeasonChangeEvent>
{
  private float speed = 1;

  [SerializeField]
  private float startSpeed = 1;

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

  void Start()
  {
    speed = startSpeed;
    coinValue = startCoinValue;
    startScale = transform.localScale;

    spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    startColor = spriteRenderer.color;

    positions = path.positions;

    EventBus.register<SeasonChangeEvent>(this);

    HandleEvent(new SeasonChangeEvent() { changedSeason = SeasonController.GetSeason() });

    gamingDataController = GamingDataController.GetInstance();

    if (canCauseSandstorm)
    {
        EventBus.post(new SandstormEnemyChangeEvent() { numberOfEnemy = 1 });
    }
  }

  void Update()
  {
    Move();
  }

  void Move()
  {
    if (index > positions.Length - 1)
    {
      updateReachEndData();
      gamingDataController.ReduceHealth();
      Destroy(gameObject);
      return;
    }
    transform.Translate((positions[index].position - transform.position).normalized * Time.deltaTime * speed);
    if (Vector3.Distance(positions[index].position, transform.position) < 0.3f)
    {
      index++;
    }
  }

  void OnDestroy()
  {
        if (canCauseSandstorm)
        {
            EventBus.post(new SandstormEnemyChangeEvent() { numberOfEnemy = -1 });
        }
        EventBus.unregister<SeasonChangeEvent>(this);
  }

  public void SetPath(Path path)
  {
    this.path = path;
  }

  public void TakeDamage(float damage)
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

  public float GetHealth()
  {
    return health;
  }

  // Season change handler
  public void HandleEvent(SeasonChangeEvent eventData)
  {
    if (eventData.changedSeason == Season.SPRING)
    {
      speed = startSpeed;
      health = health / previousHealthRate * 1f;
      previousHealthRate = 1f;
      coinValue = startCoinValue + 1;
    }
    else if (eventData.changedSeason == Season.SUMMER)
    {
      speed = startSpeed;
      health = health / previousHealthRate * 0.8f;
      previousHealthRate = 0.8f;
      coinValue = startCoinValue - 1;
    }
    else if (eventData.changedSeason == Season.AUTUMN)
    {
      speed = 0.8f * startSpeed;
      health = health / previousHealthRate * 1f;
      previousHealthRate = 1f;
      coinValue = startCoinValue + 1;
    }
    else if (eventData.changedSeason == Season.WINTER)
    {
      speed = 0.7f * startSpeed;
      health = health / previousHealthRate * 1f;
      previousHealthRate = 1f;
      coinValue = startCoinValue - 1;
    }
    SizeAndColorChange();
  }

  private void updateReachEndData()
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
}
