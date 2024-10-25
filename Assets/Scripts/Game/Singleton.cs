using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Singleton : MonoBehaviour, IEventHandler<SeasonChangeEvent>, IEventHandler<WeatherEvent>, IEventHandler<TowerBuildEvent>
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



    private volatile static Singleton instance;

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
        EventBus.register<SeasonChangeEvent>(this);
        EventBus.register<WeatherEvent>(this);
        EventBus.register<TowerBuildEvent>(this);
        HandleEvent(new SeasonChangeEvent() { ChangedSeason = SeasonController.GetSeason() });
    }


    void Awake()
    {
        instance = this;
    }


    void OnDestroy()
    {
        EventBus.unregister<SeasonChangeEvent>(this);
        EventBus.unregister<WeatherEvent>(this);
        EventBus.unregister<TowerBuildEvent>(this);
    }



    public void updateTime()
    {
        int gapTime = (int)(System.DateTime.Now - Singleton.Instance.lastEndTime).TotalSeconds;
        if (SeasonController.GetSeason() == Season.SPRING)
        {
            Singleton.Instance.timeOfSpring += gapTime;
        }
        else if (SeasonController.GetSeason() == Season.SUMMER)
        {
            Singleton.Instance.timeOfSummer += gapTime;
        }
        else if (SeasonController.GetSeason() == Season.AUTUMN)
        {
            Singleton.Instance.timeOfFall += gapTime;
        }
        else if (SeasonController.GetSeason() == Season.WINTER)
        {
            Singleton.Instance.timeOfWinter += gapTime;
        }
        Singleton.Instance.totalTime += gapTime;

        Singleton.Instance.lastEndTime = System.DateTime.Now;
    }

    public void HandleEvent(SeasonChangeEvent eventData)
    {

        if (SceneManager.GetActiveScene().name == "level1")
        {
            DataManager.buttonClickLevel1Season++;
        }
        else if (SceneManager.GetActiveScene().name == "level2")
        {
            DataManager.buttonClickLevel2Season++;
        }
        else if (SceneManager.GetActiveScene().name == "level3")
        {
            DataManager.buttonClickLevel3Season++;
        }
        else if (SceneManager.GetActiveScene().name == "level4")
        {
            DataManager.buttonClickLevel4Season++;
        }
        else if (SceneManager.GetActiveScene().name == "level5")
        {
            DataManager.buttonClickLevel5Season++;
        }
        else if (SceneManager.GetActiveScene().name == "level6")
        {
            DataManager.buttonClickLevel6Season++;
        }
        else if (SceneManager.GetActiveScene().name == "level7")
        {
            DataManager.buttonClickLevel7Season++;
        }

    }

    public void HandleEvent(WeatherEvent eventData)
    {
        if (SceneManager.GetActiveScene().name == "level1")
        {
            DataManager.buttonClickLevel1Weather++;
        }
        else if (SceneManager.GetActiveScene().name == "level2")
        {
            DataManager.buttonClickLevel2Weather++;
        }
        else if (SceneManager.GetActiveScene().name == "level3")
        {
            DataManager.buttonClickLevel3Weather++;
        }
        else if (SceneManager.GetActiveScene().name == "level4")
        {
            DataManager.buttonClickLevel4Weather++;
        }
        else if (SceneManager.GetActiveScene().name == "level5")
        {
            DataManager.buttonClickLevel5Weather++;
        }
        else if (SceneManager.GetActiveScene().name == "level6")
        {
            DataManager.buttonClickLevel6Weather++;
        }
        else if (SceneManager.GetActiveScene().name == "level7")
        {
            DataManager.buttonClickLevel7Weather++;
        }

    }

    public void HandleEvent(TowerBuildEvent eventData)
    {
        if(eventData.towerType == TowerType.CANNON_TOWER_1 || eventData.towerType == TowerType.CANNON_TOWER_2 || eventData.towerType == TowerType.CANNON_TOWER_3)
        {
            DataManager.numOfTower1++;
        }else if(eventData.towerType == TowerType.ARROW_TOWER_1 || eventData.towerType == TowerType.ARROW_TOWER_2 || eventData.towerType == TowerType.ARROW_TOWER_3)
        {
            DataManager.numOfTower2++;
        }else if(eventData.towerType == TowerType.MAGE_TOWER_1 || eventData.towerType == TowerType.MAGE_TOWER_2 || eventData.towerType == TowerType.MAGE_TOWER_3)
        {
            DataManager.numOfTower3++;
        }else if(eventData.towerType == TowerType.DESTROY_TOWER)
        {
            DataManager.numOfDestroyTower++;
        }
    }
}
