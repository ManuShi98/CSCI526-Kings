using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public static void sumCurrentLevelData()
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
}