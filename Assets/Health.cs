using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
  public int health = 100;

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
