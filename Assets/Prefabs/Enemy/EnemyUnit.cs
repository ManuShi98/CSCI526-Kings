using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyUnit : MonoBehaviour
{
  public float speed = 1;
  public float startSpeed = 1;
  public float health = 400;
  private float previousHealthRate = 1f;
  private Transform[] positions;
  private int index = 0;
  private Path path;

  void Start()
  {
    positions = path.positions;
    SceneController.OnSeasonChangeHandler += ReceiveSeasonChangedValue;
    ReceiveSeasonChangedValue(gameObject, new SeasonArgs(SceneController.GetSeason().ToString().ToLower()));
  }

  void Update()
  {
    Move();
  }

  void Move()
  {
    if (index > positions.Length - 1)
    {
      Singleton.Instance.numOfReachEndMonster++;
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
      Destroy(gameObject);
    }
  }

  private void ReceiveSeasonChangedValue(object sender, System.EventArgs args)
  {
    SeasonArgs seasonArgs = (SeasonArgs)args;
    // Debug.Log(seasonArgs.CurrentSeason);
    if (seasonArgs.CurrentSeason == "spring")
    {
      speed = startSpeed;
      health = health / previousHealthRate * 1f;
      previousHealthRate = 1f;
    }
    else if (seasonArgs.CurrentSeason == "summer")
    {
      speed = startSpeed;
      health = health / previousHealthRate * 0.8f;
      previousHealthRate = 0.8f;
    }
    else if (seasonArgs.CurrentSeason == "autumn")
    {
      speed = 0.8f * startSpeed;
      health = health / previousHealthRate * 1f;
      previousHealthRate = 1f;
    }
    else if (seasonArgs.CurrentSeason == "winter")
    {
      speed = 0.7f * startSpeed;
      health = health / previousHealthRate * 1f;
      previousHealthRate = 1f;
    }
    Debug.Log("health: " + health);
    Debug.Log("speed: " + speed);
  }
}
