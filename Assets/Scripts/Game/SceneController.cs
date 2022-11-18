using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using TMPro;

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

    private int speedMode = 0;

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

        // Reset the speed to normal.
        Time.timeScale = 1;
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

    // Gaming process speedup and slow down
    public void ChangeGamePlaySpeed(GameObject speedDisplayText)
    {
        speedMode = (speedMode + 1) % 3;
        // Normal speed
        if(speedMode == 0)
        {
            Time.timeScale = 1;
            speedDisplayText.GetComponent<TextMeshProUGUI>().text = "1x";
        } else if(speedMode == 1)
        {
            // 2x mode
            Time.timeScale = 2;
            speedDisplayText.GetComponent<TextMeshProUGUI>().text = "2x";
        } else
        {
            // 0.5x mode
            Time.timeScale = 0.5f;
            speedDisplayText.GetComponent<TextMeshProUGUI>().text = "0.5x";
        }

        Debug.Log("Current time scale:" + Time.timeScale);
    }
}
