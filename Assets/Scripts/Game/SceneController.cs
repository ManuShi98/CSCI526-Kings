using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class GameStartEvent : IEventData { }
public class GamePauseEvent : IEventData { }

public class SceneController : MonoBehaviour, IEventHandler<SeasonChangeEvent>
{

    [SerializeField]
    private List<Tilemap> maps;

    [SerializeField]
    private List<TileData> tileDatas;

    public static bool isPaused;

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

    private Queue<GameObject> currentSeasonalMap;

    public GameObject springMapPrefab;
    public GameObject summerMapPrefab;
    public GameObject autumnMapPrefab;
    public GameObject winterMapPrefab;

    // Start is called before the first frame update
    void Start()
    {
        isPaused = true;
        currentSeasonalMap = new Queue<GameObject>();
        ChangeSeasonalMap();
        EventBus.register(this);
    }

    public void GameBegin(GameObject ReadyBtn)
    {
        isPaused = false;
        EventBus.post(new GameStartEvent() { });
        foreach (EnemySpawner spawner in SpawnerList)
        {
            spawner.OnGenerateEnemyBtnClicked();
        }
        Destroy(ReadyBtn);
        StartGeneratingEnemies = true;
    }

    public void GamePause()
    {
        isPaused = true;
        EventBus.post(new GamePauseEvent() { });
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
