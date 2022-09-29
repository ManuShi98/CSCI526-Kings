using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyUnit : MonoBehaviour
{
  private float speed = 1;

  [SerializeField]
  private float startSpeed = 1;

  [SerializeField]
  private float health = 400;
  private float previousHealthRate = 1f;
  private int coinValue = 1;

  [SerializeField]
  private int startCoinValue = 1;
  private Transform[] positions;
  private int index = 0;
  private Path path;
  private GamingDataController gamingDataController;

  void Start()
  {
    speed = startSpeed;
    coinValue = startCoinValue;

    positions = path.positions;
    SceneController.OnSeasonChangeHandler += ReceiveSeasonChangedValue;
    ReceiveSeasonChangedValue(gameObject, new SeasonArgs(SceneController.GetSeason().ToString().ToLower()));

    gamingDataController = GamingDataController.getInstance();
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
      gamingDataController.reduceHealth();
      Destroy(gameObject);
      return;
    }
    transform.Translate((positions[index].position - transform.position).normalized * Time.deltaTime * speed);
    if (Vector3.Distance(positions[index].position, transform.position) < 0.3f)
    {
      index++;
    }
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
      GamingDataController.getInstance().addCoins(coinValue);
      Destroy(gameObject);
    }
  }

  public float GetHealth()
  {
    return health;
  }

  private void ReceiveSeasonChangedValue(object sender, System.EventArgs args)
  {
    SeasonArgs seasonArgs = (SeasonArgs)args;
    if (seasonArgs.CurrentSeason == "spring")
    {
      speed = startSpeed;
      health = health / previousHealthRate * 1f;
      previousHealthRate = 1f;
      coinValue += 2;
    }
    else if (seasonArgs.CurrentSeason == "summer")
    {
      speed = startSpeed;
      health = health / previousHealthRate * 0.8f;
      previousHealthRate = 0.8f;
      coinValue = startCoinValue;
    }
    else if (seasonArgs.CurrentSeason == "autumn")
    {
      speed = 0.8f * startSpeed;
      health = health / previousHealthRate * 1f;
      previousHealthRate = 1f;
      coinValue += 1;
    }
    else if (seasonArgs.CurrentSeason == "winter")
    {
      speed = 0.7f * startSpeed;
      health = health / previousHealthRate * 1f;
      previousHealthRate = 1f;
      coinValue = startCoinValue;
    }
  }

  private void updateReachEndData()
  {
    Singleton.Instance.numOfReachEndMonster++;
    Singleton.Instance.curReachEndMonster++;
    if (SceneController.GetSeason() == SceneController.Season.SPRING)
    {
      Singleton.Instance.numOfSpringReachEndMonster++;
    }
    else if (SceneController.GetSeason() == SceneController.Season.SUMMER)
    {
      Singleton.Instance.numOfSummerReachEndMonster++;
    }
    else if (SceneController.GetSeason() == SceneController.Season.AUTUMN)
    {
      Singleton.Instance.numOfFallReachEndMonster++;
    }
    else if (SceneController.GetSeason() == SceneController.Season.WINTER)
    {
      Singleton.Instance.numOfWinterReachEndMonster++;
    }
  }
}
