using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour
// public sealed class Singleton
{

  public volatile int curOriginalMonster = 0;
  public volatile int curMonsterNum = 0;
  public volatile int curDiedMonster = 0;
  public volatile int curReachEndMonster = 0;
  public volatile int curLevel = 1;
  public volatile bool isGameOver = false;

  public volatile int numOfOriginalMonster = 0; //finished
  public volatile int numOfReachEndMonster = 0;  //finished
  public volatile int numOfDiedMonster = 0; //finished
  public System.DateTime beginTime = System.DateTime.Now;

  public volatile int numOfSpringReachEndMonster = 0; //finished
  public volatile int numOfSummerReachEndMonster = 0; //finished
  public volatile int numOfFallReachEndMonster = 0; //finished
  public volatile int numOfWinterReachEndMonster = 0; //finished

  public volatile int timeOfSpring = 0; //finished
  public volatile int timeOfSummer = 0; //finished
  public volatile int timeOfFall = 0; //finished
  public volatile int timeOfWinter = 0; //finished
  public System.DateTime lastEndTime = System.DateTime.Now;

  public volatile int numOfCoins = 0;
  public volatile int totalTime = 0; // finished



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
    
    }

    // Update is called once per frame
    void Update()
    {
    
    }
    void Awake()
    {
        instance = this;
    }

 

    public void updateTime()
    {
        int gapTime = (int)(System.DateTime.Now - Singleton.Instance.lastEndTime).TotalSeconds;
        if (SceneController.GetSeason() == SceneController.Season.SPRING)
        {
            Singleton.Instance.timeOfSpring += gapTime;
        }
        else if (SceneController.GetSeason() == SceneController.Season.SUMMER)
        {
            Singleton.Instance.timeOfSummer += gapTime;
        }
        else if (SceneController.GetSeason() == SceneController.Season.AUTUMN)
        {
            Singleton.Instance.timeOfFall += gapTime;
        }
        else if (SceneController.GetSeason() == SceneController.Season.WINTER)
        {
            Singleton.Instance.timeOfWinter += gapTime;
        }
        Singleton.Instance.totalTime += gapTime;

        Singleton.Instance.lastEndTime = System.DateTime.Now;
    }

}
