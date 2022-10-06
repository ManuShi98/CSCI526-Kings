using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.EventSystems;

public class SceneController : MonoBehaviour, IEventData, IEventHandler<SeasonChangeEvent>
{

  [SerializeField]
  private List<Tilemap> maps;

  [SerializeField]
  private List<TileData> tileDatas;

  private Dictionary<TileBase, TileData> dataFromTiles;

  public EnemySpawner[] SpawnerList;

  public bool StartGeneratingEnemies;

  [SerializeField]
  private GameObject PausePanelPrefab;

  private void Awake()
  {
    StartGeneratingEnemies = false;

    dataFromTiles = new Dictionary<TileBase, TileData>();

    foreach (var tileData in tileDatas)
    {
      foreach (var tile in tileData.tiles)
      {
        dataFromTiles.Add(tile, tileData);
      }
    }
  }

  public enum Weather
  {
    SUNNY,
    RAINY,
    CLOUDY,
    SNOWY
  }

  private static Weather currentWeather;

  private Queue<GameObject> currentSeasonalMap;

  public GameObject springMapPrefab;
  public GameObject summerMapPrefab;
  public GameObject autumnMapPrefab;
  public GameObject winterMapPrefab;

  // Start is called before the first frame update
  void Start()
  {
    currentSeasonalMap = new Queue<GameObject>();
    currentWeather = Weather.SUNNY;
    ChangeSeasonalMap();
    EventBus.register<SeasonChangeEvent>(this);
  }

  // Update is called once per frame
  void Update()
  {

  }

  public void SetWeather(Weather weather)
  {
    currentWeather = weather;
  }

  public Weather GetWeather()
  {
    return currentWeather;
  }

  public void GameBegin(GameObject ReadyBtn)
  {
    foreach (EnemySpawner spawner in SpawnerList)
    {
      spawner.OnGenerateEnemyBtnClicked();
    }
    Destroy(ReadyBtn);
    StartGeneratingEnemies = true;
  }

  public void GamePause()
  {
    Instantiate(PausePanelPrefab, transform.position, Quaternion.identity);
  }

  private void ChangeSeasonalMap()
  {
    if (currentSeasonalMap.Count > 0)
    {
      Destroy(currentSeasonalMap.Dequeue());
    }
    GameObject currentSeasonalGrid = null;
    switch (SeasonController.GetSeason())
    {
      case Season.SPRING:
        {
          currentSeasonalGrid = Instantiate(springMapPrefab);
        }
        break;
      case Season.SUMMER:
        {
          currentSeasonalGrid = Instantiate(summerMapPrefab);
        }
        break;
      case Season.AUTUMN:
        {
          currentSeasonalGrid = Instantiate(autumnMapPrefab);
        }
        break;
      case Season.WINTER:
        {
          currentSeasonalGrid = Instantiate(winterMapPrefab);
        }
        break;
      default:
        ;
        break;
    }
    currentSeasonalMap.Enqueue(currentSeasonalGrid);
  }

  private void UpdateTimeData()
  {
    int gapTime = (int)(System.DateTime.Now - Singleton.Instance.lastEndTime).TotalSeconds;
    Season currentSeason = SeasonController.GetSeason();
    if (currentSeason == Season.SPRING)
    {
      Singleton.Instance.timeOfSpring += gapTime;
    }
    else if (currentSeason == Season.SUMMER)
    {
      Singleton.Instance.timeOfSummer += gapTime;
    }
    else if (currentSeason == Season.AUTUMN)
    {
      Singleton.Instance.timeOfFall += gapTime;
    }
    else if (currentSeason == Season.WINTER)
    {
      Singleton.Instance.timeOfWinter += gapTime;
    }

    Singleton.Instance.lastEndTime = System.DateTime.Now;
    Singleton.Instance.totalTime += gapTime;
  }

  // Handle Season Change
  public void HandleEvent(SeasonChangeEvent eventData)
  {
    UpdateTimeData();
    ChangeSeasonalMap();
  }
}
