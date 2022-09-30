using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
  public double radius = 100; // Weapon's firing radius
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
    FiringIntervalTime = 1.0 / FiringRate;
    timer = FiringIntervalTime;

    SceneController.OnSeasonChangeHandler += ReceiveSeasonChangedValue;
    ReceiveSeasonChangedValue(gameObject, new SeasonArgs(SceneController.GetSeason().ToString().ToLower()));
  }

  void Update()
  {
    OnUpdate();
  }

  protected virtual void OnUpdate()
  {
    if (enemy != null)
    {
      // 旋转Fire Point
      Vector2 dir = enemy.transform.position - transform.position;
      float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
      firePoint.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

      // 射击
      timer -= Time.deltaTime;
      if (timer <= 0)
      {
        Bullet newBullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation).GetComponent<Bullet>();
        newBullet.damage = damage;
        newBullet.fire(firePoint.rotation);
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

  private void ReceiveSeasonChangedValue(object sender, System.EventArgs args)
  {
    SeasonArgs seasonArgs = (SeasonArgs)args;
    if (seasonArgs.CurrentSeason == "spring")
    {
      damage *= 0.7f;
    }
    else if (seasonArgs.CurrentSeason == "summer")
    {
      damage = startDamage;
    }
    else if (seasonArgs.CurrentSeason == "autumn")
    {
      damage = startDamage;
    }
    else if (seasonArgs.CurrentSeason == "winter")
    {
      damage = startDamage;
    }
  }
}
