using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour
{

    public static volatile int numOfOriginalMonster = 0; //finished
    public static volatile int numOfReachEndMonster = 0;  //finished
    public static volatile int numOfDiedMonster = 0; //finished

    public static volatile int numOfSpringReachEndMonster = 0; //finished
    public static volatile int numOfSummerReachEndMonster = 0; //finished
    public static volatile int numOfFallReachEndMonster = 0; //finished
    public static volatile int numOfWinterReachEndMonster = 0; //finished

    public static volatile int timeOfSpring = 0; //finished
    public static volatile int timeOfSummer = 0; //finished
    public static volatile int timeOfFall = 0; //finished
    public static volatile int timeOfWinter = 0; //finished

    public static volatile int totalTime = 0; // finished
    public static volatile int numOfCoins = 0;

    public static volatile string currentLevelIndex = "";
    public static volatile bool isPass = false;

    public static volatile string endLevel = "";
    public static volatile int level1Time = 0;
    public static volatile int level2Time = 0;
    public static volatile int level3Time = 0;
    public static volatile int level4Time = 0;
    public static volatile int level5Time = 0;
    public static volatile int level6Time = 0;
    public static volatile int level7Time = 0;

    public static volatile int buttonClickLevel1Season = 0;
    public static volatile int buttonClickLevel2Season = 0;
    public static volatile int buttonClickLevel3Season = 0;
    public static volatile int buttonClickLevel4Season = 0;
    public static volatile int buttonClickLevel5Season = 0;
    public static volatile int buttonClickLevel6Season = 0;
    public static volatile int buttonClickLevel7Season = 0;

    public static volatile int buttonClickLevel1Weather = 0;
    public static volatile int buttonClickLevel2Weather = 0;
    public static volatile int buttonClickLevel3Weather = 0;
    public static volatile int buttonClickLevel4Weather = 0;
    public static volatile int buttonClickLevel5Weather = 0;
    public static volatile int buttonClickLevel6Weather = 0;
    public static volatile int buttonClickLevel7Weather = 0;

    public static volatile int numOfTower1 = 0;
    public static volatile int numOfTower2 = 0;
    public static volatile int numOfTower3 = 0;
    public static volatile int numOfDestroyTower = 0;



    public static int level1Index = 11;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public static void SumCurrentLevelData()
    {
        DataManager.numOfOriginalMonster += Singleton.Instance.numOfOriginalMonster;
        DataManager.numOfReachEndMonster += Singleton.Instance.numOfReachEndMonster;
        DataManager.numOfDiedMonster += Singleton.Instance.numOfDiedMonster;
        DataManager.numOfSpringReachEndMonster += Singleton.Instance.numOfSpringReachEndMonster;
        DataManager.numOfSummerReachEndMonster += Singleton.Instance.numOfSummerReachEndMonster;
        DataManager.numOfFallReachEndMonster += Singleton.Instance.numOfFallReachEndMonster;
        DataManager.numOfWinterReachEndMonster += Singleton.Instance.numOfWinterReachEndMonster;
        DataManager.timeOfSpring += Singleton.Instance.timeOfSpring;
        DataManager.timeOfSummer += Singleton.Instance.timeOfSummer;
        DataManager.timeOfFall += Singleton.Instance.timeOfFall;
        DataManager.timeOfWinter += Singleton.Instance.timeOfWinter;
        DataManager.numOfCoins += Singleton.Instance.numOfCoins;
        DataManager.totalTime += Singleton.Instance.totalTime;
        
    }

    public static void Init()
    {
        DataManager.numOfOriginalMonster = 0;
        DataManager.numOfReachEndMonster = 0;
        DataManager.numOfDiedMonster = 0;
        DataManager.numOfSpringReachEndMonster = 0;
        DataManager.numOfSummerReachEndMonster = 0;
        DataManager.numOfFallReachEndMonster = 0;
        DataManager.numOfWinterReachEndMonster = 0;
        DataManager.timeOfSpring = 0;
        DataManager.timeOfSummer = 0;
        DataManager.timeOfFall = 0;
        DataManager.timeOfWinter = 0;
        DataManager.numOfCoins = 0;
        DataManager.totalTime = 0;
        DataManager.endLevel = "";
        DataManager.level1Time = 0;
        DataManager.level2Time = 0;
        DataManager.level3Time = 0;
        DataManager.level4Time = 0;
        DataManager.level5Time = 0;
        DataManager.level6Time = 0;
        DataManager.level7Time = 0;
    }

   
}
