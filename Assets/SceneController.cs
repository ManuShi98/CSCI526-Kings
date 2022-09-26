using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{

  [SerializeField]
  private List<Tilemap> maps;

  [SerializeField]
  private List<TileData> tileDatas;

  private Dictionary<TileBase, TileData> dataFromTiles;

  private TowerBtn ClickedBtn;

  private void Awake()
  {
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
          PlaceTower(gridCenterPosition, tag);
          break;
        }
      }
    }

  }

  public Season GetSeason()
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

  public void PlaceTower(Vector3 gridCenterPosition, string tag)
  {
    if (!EventSystem.current.IsPointerOverGameObject() && this.ClickedBtn != null)
    {
      if (string.Compare(tag, "wall") == 0)
      {
        Instantiate(this.ClickedBtn.TowerPrefab, gridCenterPosition, Quaternion.identity);
      }
      else
      {
        print("You cannot build tower on the path!");
      }
      this.ClickedBtn = null;
    }

  }

  public void PickTower(TowerBtn towerBtn)
  {
    this.ClickedBtn = towerBtn;
  }

  private void GameOver()
  {
    if (Singleton.Instance.GetEnemyMapStatus())
    {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
  }

  public void PickSeason(SeasonBtn seasonBtn)
  {
    string changedSeason = seasonBtn.GetSeason();
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
    ChangeSeasonalMap();
    Debug.Log(currentSeason);
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
