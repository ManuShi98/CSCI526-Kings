using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System;

public class SceneController : MonoBehaviour
{

  [SerializeField]
  private List<Tilemap> maps;

  [SerializeField]
  private List<TileData> tileDatas;

  private Dictionary<TileBase, TileData> dataFromTiles;

  public EnemySpawner[] SpawnerList;

  public bool StartGeneratingEnemies;

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

  public enum Season
  {
    SPRING,
    SUMMER,
    AUTUMN,
    WINTER
  }

  public enum Weather
  {
    SUNNY,
    RAINY,
    CLOUDY,
    SNOWY
  }

  private static Season currentSeason;
  private static Weather currentWeather;

  // Season change sender
  public static event EventHandler OnSeasonChangeHandler;

  private Queue<GameObject> currentSeasonalMap;

  public GameObject springMapPrefab;
  public GameObject summerMapPrefab;
  public GameObject autumnMapPrefab;
  public GameObject winterMapPrefab;

  // Start is called before the first frame update
  void Start()
  {
    currentSeasonalMap = new Queue<GameObject>();
    currentSeason = Season.SPRING;//initial season is spring
    currentWeather = Weather.SUNNY;
    ChangeSeasonalMap();
  }

  // Update is called once per frame
  void Update()
  {
    GameOver();
    if (Input.GetMouseButtonDown(0))
    {
      Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
      foreach (var map in maps)
      {
        Vector3Int gridPosition = map.WorldToCell(mousePosition);
        Vector3 gridCenterPosition = map.GetCellCenterWorld(gridPosition);
        TileBase clickedTile = map.GetTile(gridPosition);
        if (clickedTile != null)
        {
          string tag = dataFromTiles[clickedTile].tag;
          // print("At position " + gridPosition + " " + tag);
          break;
        }
      }
    }

  }

  public static Season GetSeason()
  {
    return currentSeason;
  }

  public void SetWeather(Weather weather)
  {
    currentWeather = weather;
  }

  public Weather GetWeather()
  {
    return currentWeather;
  }

  private void GameOver()
  {
    if (Singleton.Instance.GetEnemyMapStatus())
    {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
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

  public void PickSeason(SeasonBtn seasonBtn)
  {
    string changedSeason = seasonBtn.GetSeason();
    // Notify that season has changed
    if (changedSeason != currentSeason.ToString().ToLower() && OnSeasonChangeHandler != null)
    {
      OnSeasonChangeHandler(gameObject, new SeasonArgs(changedSeason));
    }

    // Change current season
    if (changedSeason == "spring")
    {
      currentSeason = Season.SPRING;
    }
    else if (changedSeason == "summer")
    {
      currentSeason = Season.SUMMER;
    }
    else if (changedSeason == "autumn")
    {
      currentSeason = Season.AUTUMN;
    }
    else if (changedSeason == "winter")
    {
      currentSeason = Season.WINTER;
    }

    // Change map according current season
    ChangeSeasonalMap();
  }

  private void ChangeSeasonalMap()
  {
    if (currentSeasonalMap.Count > 0)
    {
      Destroy(currentSeasonalMap.Dequeue());
    }
    GameObject currentSeasonalGrid = null;
    switch (GetSeason())
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
}
