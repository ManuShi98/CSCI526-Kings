using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

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

  public Tile winterTree;
  public Tile summerTree;
  public Tile winterRiver;
  public Tile summerRiver;
  public Season testSeason;

  // Start is called before the first frame update
  void Start()
  {
    currentSeason = Season.SPRING;
    currentWeather = Weather.SUNNY;
    Tile tree = null;
    Tile river = null;
    maps[0].SetTile(new Vector3Int(-5, -4, 0), tree);
    maps[0].SetTile(new Vector3Int(-4, -4, 0), tree);
    maps[0].SetTile(new Vector3Int(-3, -4, 0), tree);
    maps[0].SetTile(new Vector3Int(-2, -4, 0), tree);
    maps[0].SetTile(new Vector3Int(-1, -4, 0), tree);
    maps[0].SetTile(new Vector3Int(-1, -3, 0), tree);
    maps[0].SetTile(new Vector3Int(-1, -2, 0), tree);
    maps[0].SetTile(new Vector3Int(6, 3, 0), river);
    maps[0].SetTile(new Vector3Int(6, 2, 0), river);
    maps[0].SetTile(new Vector3Int(6, 1, 0), river);
    maps[0].SetTile(new Vector3Int(6, 0, 0), river);
    maps[0].SetTile(new Vector3Int(6, -1, 0), river);
    maps[0].SetTile(new Vector3Int(6, -2, 0), river);
    maps[0].SetTile(new Vector3Int(6, -3, 0), river);
    maps[0].SetTile(new Vector3Int(6, -4, 0), river);
    maps[0].SetTile(new Vector3Int(5, -4, 0), river);
    maps[0].SetTile(new Vector3Int(4, -4, 0), river);
    maps[0].SetTile(new Vector3Int(3, -4, 0), river);
    maps[0].SetTile(new Vector3Int(3, -3, 0), river);
    maps[0].SetTile(new Vector3Int(3, -2, 0), river);
    maps[0].SetTile(new Vector3Int(3, -1, 0), river);
    maps[0].SetTile(new Vector3Int(3, -1, 0), river);
    maps[0].SetTile(new Vector3Int(2, -1, 0), river);
    maps[0].SetTile(new Vector3Int(1, -1, 0), river);
    maps[0].SetTile(new Vector3Int(0, -1, 0), river);
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
    Debug.Log(currentSeason);
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
}
