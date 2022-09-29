using UnityEngine;
using System.Collections;

public enum TowerType
{
  NORMAL,
  SPRING_TOWER,
}

public class Weapon : MonoBehaviour
{
  public double radius = 100; // Weapon's firing radius
  public double FiringRate = 10;  // Fire per second

  [SerializeField]
  private float startDamage = 20f;
  private float damage = 20f;

  [SerializeField]
  private TowerType towerType;

  public Transform firePoint;

  public GameObject enemy; // Locked enemy instance
  public GameObject bulletPrefab;

  private double FiringIntervalTime;
  private double timer;

  void Start()
  {
    damage = startDamage;
    FiringIntervalTime = 1.0 / FiringRate;
    timer = FiringIntervalTime;

    SceneController.OnSeasonChangeHandler += ReceiveSeasonChangedValue;
    ReceiveSeasonChangedValue(gameObject, new SeasonArgs(SceneController.GetSeason().ToString().ToLower()));
  }

  void Update()
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

    if (towerType == TowerType.SPRING_TOWER)
    {
      SpringTowerFunc(seasonArgs.CurrentSeason);
    }
  }

  // Spring tower special
  private void SpringTowerFunc(string currentSeason)
  {
    if (currentSeason == "spring")
    {
      // Current season is spring
      // Increase damage with coroutine
      StartCoroutine("StartSpringTowerCoroutine");
    }
    else
    {
      // Current season is not spring
      // Reset damage and stop coroutine
      StopCoroutine("StartSpringTowerCoroutine");
      damage = startDamage;
      Debug.Log("spring down " + damage);
    }
  }

  private IEnumerator StartSpringTowerCoroutine()
  {
    for (int i = 0; i < 10; ++i)
    {
      yield return new WaitForSeconds(5f);
      damage += 2f;
      Debug.Log("spring up " + damage);
    }
  }
}
