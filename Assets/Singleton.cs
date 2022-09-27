using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour
// public sealed class Singleton
{
  public volatile int numOfOriginalMonster = 0;
  public volatile int numOfReachEndMonster = 0;
  public volatile int numOfDiedMonster = 0;
  public System.DateTime beginTime = System.DateTime.Now;

  public volatile int numOfSpringReachEndMonster = 0;
  public volatile int numOfSummerReachEndMonster = 0;
  public volatile int numOfFallReachEndMonster = 0;
  public volatile int numOfWinterReachEndMonster = 0;

  public volatile int timeOfSpring = 0;
  public volatile int timeOfSummer = 0;
  public volatile int timeOfFall = 0;
  public volatile int timeOfWinter = 0;
  public System.DateTime lastEndTime = System.DateTime.Now;

  public volatile int numOfCoins = 0;
  public volatile int totalTime = 0;

  // Flag to indicate that whether there are any enemyies on the map
  private bool isNoEnemyOnMap;

  private int currentLevel = 1;

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
      if(currentLevel == 1)
      {
        GameObject dialog = GameObject.Find("Map/Dialog");
        dialog.SetActive(true);
        print(numOfDiedMonster + " " + numOfReachEndMonster + " " + numOfOriginalMonster);
      }
      
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

  public int GetCurrentLevel()
  {
    return this.currentLevel;
  }
}
