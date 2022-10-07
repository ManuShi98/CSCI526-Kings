using UnityEngine;

public class Weapon : MonoBehaviour, IEventHandler<SeasonChangeEvent>, IEventHandler<SandstormStartEvent>
{
  [SerializeField]
  protected double startRadius = 100;
  protected double radius = 100; // Weapon's firing radius

  public double FiringRate = 10;  // Fire per second

  [SerializeField]
  protected float startDamage = 20f;
  protected float damage = 20f;

  public Transform firePoint;

  public GameObject enemy; // Locked enemy instance
  public GameObject bulletPrefab;

  protected double FiringIntervalTime;
  protected double timer;

  void Start()
  {
    OnStart();
  }

  protected virtual void OnStart()
  {
    damage = startDamage;
    radius = startRadius;
    FiringIntervalTime = 1.0 / FiringRate;
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
      // 旋转Fire Point
      //Vector2 dir = enemy.transform.position - transform.position;
      //float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
      //firePoint.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

      // 射击
      timer -= Time.deltaTime;
      if (timer <= 0)
      {
        Bullet newBullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation).GetComponent<Bullet>();
        newBullet.damage = damage;
        newBullet.fire(enemy);
        timer = FiringIntervalTime;
      }
    }
    else
    {
      // TODO: 临时方法。后期更换为怪物控制器 + Map(GameObject ID, Health)的方式控制目标锁定方式
      // 优先攻击距离最近的敌人
      GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
      double minDistance = double.MaxValue;
      foreach (GameObject e in enemies)
      {
        double currDistance = TwoPointDistance2D(e.transform.position, transform.position);
        if (currDistance < minDistance)
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
        radius = startRadius / 2;
    }
    else
    {
        radius = startRadius;
    }
    Debug.Log(radius);
  }

  protected virtual void SeasonChangeHandleEvent(SeasonChangeEvent eventData)
  {
    if (eventData.changedSeason == Season.SPRING)
    {
      damage *= 0.7f;
    }
    else if (eventData.changedSeason == Season.SUMMER)
    {
      damage = startDamage;
    }
    else if (eventData.changedSeason == Season.AUTUMN)
    {
      damage = startDamage;
    }
    else if (eventData.changedSeason == Season.WINTER)
    {
      damage = startDamage;
    }
  }
}