using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : MonoBehaviour
{
  public float speed = 1;
  public int health = 400;
  private Transform[] positions;
  private int index = 0;
  private Path path;

  void Start()
  {
    positions = path.positions;
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

  public void TakeDamage(int damage)
  {
    health -= damage;

    if (health <= 0)
    {
      Singleton.Instance.numOfSurviveMonster--;
      Singleton.Instance.numOfDiedMonster++;
      Destroy(gameObject);
    }
  }
}
