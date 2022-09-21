using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class SceneController : MonoBehaviour
{

  [SerializeField]
  private List<Tilemap> maps;

  [SerializeField]
  private List<TileData> tileDatas;

  private Dictionary<TileBase, TileData> dataFromTiles;

  // TODO: towerfab变量，按钮用到
  // [SerializeField]
  // private GameObject towerPrefab;

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

  // Start is called before the first frame update
  void Start()
  {
    currentSeason = Season.SPRING;
    currentWeather = Weather.SUNNY;
    this.ClickedBtn = null;
  }

  // Update is called once per frame
  void Update()
  {
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
          print("At position " + gridPosition + " " + tag);
          // PlaceTower(gridCenterPosition, tag);
          break;
        }
      }
    }
  }

  public void SetSeason(Season season)
  {
    currentSeason = season;
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

  // public void PlaceTower(Vector3 gridCenterPosition, string tag)
  // {
  //   if (!EventSystem.current.IsPointerOverGameObject() && this.ClickedBtn != null)
  //   {
  //     if (string.Compare(tag, "\"wall\"") == 0)
  //     {
  //       Instantiate(towerPrefab, gridCenterPosition, Quaternion.identity);
  //     }
  //     else
  //     {
  //       print("You cannot build tower on the path!");
  //     }
  //     this.ClickedBtn = null;
  //   }

  // }

  // public void PickTower(TowerBtn towerBtn)
  // {
  //   this.ClickedBtn = towerBtn;
  //   //print(this.ClickedBtn);
  // }
}
