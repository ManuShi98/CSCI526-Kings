using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour
// public sealed class Singleton
{

  public volatile int curOriginalMonster = 0;
  public volatile int curDiedMonster = 0;
  public volatile int curReachEndMonster = 0;
  public volatile int curLevel = 1;

  public volatile int numOfOriginalMonster = 0;
  public volatile int numOfReachEndMonster = 0;  //finished
  public volatile int numOfDiedMonster = 0; //finished
  public System.DateTime beginTime = System.DateTime.Now;

  public volatile int numOfSpringReachEndMonster = 0; //finished
  public volatile int numOfSummerReachEndMonster = 0; //finished
  public volatile int numOfFallReachEndMonster = 0; //finished
  public volatile int numOfWinterReachEndMonster = 0; //finished

  public volatile int timeOfSpring = 0; //need add the last season
  public volatile int timeOfSummer = 0;
  public volatile int timeOfFall = 0;
  public volatile int timeOfWinter = 0;
  public System.DateTime lastEndTime = System.DateTime.Now;

  public volatile int numOfCoins = 0;
  public volatile int totalTime = 0;

  // Flag to indicate that whether there are any enemyies on the map
  private bool isNoEnemyOnMap;

  private static Singleton instance;

  public static Singleton Instance
  {
    get
    {
      if (instance == null)
      {
        GameObject obj = new GameObject();
        obj.name = "Singleton";
        instance = obj.AddComponent<Singleton>();
      }
      return instance;
    }
  }

  void Start()
  {
    isNoEnemyOnMap = false;
  }

  // Update is called once per frame
  void Update()
  {
    // If num of survive monster is equal to num of reach end monster
    // then there are no monsters on the map
    if (numOfDiedMonster + numOfReachEndMonster == numOfOriginalMonster)
    {
      print(numOfDiedMonster + " " + numOfReachEndMonster + " " + numOfOriginalMonster);
      this.isNoEnemyOnMap = true;
    }
  }
  void Awake()
  {
    instance = this;
  }

  public bool GetEnemyMapStatus()
  {
    return this.isNoEnemyOnMap;
  }
}
