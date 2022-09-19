using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;
using System.Collections.Generic;

public class SceneController : MonoBehaviour
{

    [SerializeField]
    private List<Tilemap> maps;

    [SerializeField]
    private List<TileData> tileDatas;

    private Dictionary<TileBase, TileData> dataFromTiles;

    private void Awake()
    {
        dataFromTiles = new Dictionary<TileBase, TileData>();

        foreach (var tileData in tileDatas)
        {
            foreach(var tile in tileData.tiles)
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
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            foreach(var map in maps)
            {
                Vector3Int gridPosition = map.WorldToCell(mousePosition);
                TileBase clickedTile = map.GetTile(gridPosition);
                if(clickedTile != null)
                {
                    string tag = dataFromTiles[clickedTile].tag;
                    print("At position " + gridPosition + " " + tag);
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
}
