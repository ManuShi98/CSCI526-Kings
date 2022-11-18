using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamingDataController : MonoBehaviour, IEventHandler<EnemyWavesEvent>
{

    public volatile int coins;
    public volatile int health = 3;
    public static int maxRound = 7;
    public static int currRound = 1;
    public volatile int energy = 0;
    public static readonly int maxEnergy = 100;

    private bool isDataChanged = false;

    public GameObject coinDigit;
    public GameObject healthDigit;
    public GameObject maxRoundDigit;
    public GameObject currRoundDigit;
    public Slider energyBar;

    public Button springBtn;
    public Button summerBtn;
    public Button autumnBtn;
    public Button winterBtn;

    public Button sunnyBtn;
    public Button rainyBtn;
    public Button cloudyBtn;
    public Button foggyBtn;

    [SerializeField]
    private Button currSeasonBtn;
    [SerializeField]
    private Button currWeatherBtn;

    private GamingDataController() { }

    private static GamingDataController controller;
    private static readonly object locker = new();

    public static GamingDataController GetInstance()
    {
        if (controller == null)
        {
            lock (locker)
            {
                GameObject obj = GameObject.Find("GamingDataController");
                controller = obj.GetComponent<GamingDataController>();
            }
        }
        return controller;
    }

    private void Start()
    {
        EventBus.register(this);
        GetInstance();

        maxRoundDigit.GetComponent<TextMeshProUGUI>().text = maxRound.ToString();
        if (energyBar != null)
        {
            energyBar.value = energy;
        }
        if (currSeasonBtn == null)
        {
            currSeasonBtn = springBtn;
        }
        if (currWeatherBtn == null)
        {
            currWeatherBtn = sunnyBtn;
        }

        UpdateGamingData();
        UpdateButtonGroups();
    }


    // Update is called once per frame
    void Update()
    {
        if (isDataChanged)
        {
            UpdateGamingData();
            isDataChanged = false;
        }
    }

    void OnDestroy()
    {
        EventBus.unregister<EnemyWavesEvent>(this);
    }

    public int GetCoinCount()
    {
        return coins;
    }

    public void SetCoinCount(int val)
    {
        coins = val;
        isDataChanged = true;
    }

    public int GetHealth()
    {
        return health;
    }

    public void SetHealth(int val)
    {
        health = val;
        isDataChanged = true;
    }

    public int GetMaxRound()
    {
        return maxRound;
    }

    public void AddCoins(int val)
    {
        int currCoins = coins + val;
        SetCoinCount(currCoins);
        Singleton.Instance.numOfCoins += val;
    }

    public void ReduceCoins(int val)
    {
        int currCoins = coins - val;
        SetCoinCount(currCoins);
    }

    public void ReduceHealth(int val = 1)
    {
        int currHealth = Mathf.Max(health - val, 0);
        //Debug.Log("Call reduceHealth" + "   " + currHealth.ToString());
        SetHealth(currHealth);
    }

    public bool IsAlive()
    {
        return health > 0;
    }

    public void UpdateGamingData()
    {
        coinDigit.GetComponent<TextMeshProUGUI>().text = coins.ToString();
        healthDigit.GetComponent<TextMeshProUGUI>().text = health.ToString();
    }

    public int GetEnergy()
    {
        return energy;
    }

    public bool IsFullEnergy()
    {
        return energy == maxEnergy;
    }

    public void AddEnergy(int n)
    {

        if (energyBar != null)
        {
            // Energy can not exceed 100
            // todo: Need to specify the rules of this part.
            energy = Mathf.Min(energy + n, 100);
            if (energy == maxEnergy)
            {
                springBtn.interactable = true;
                summerBtn.interactable = true;
                autumnBtn.interactable = true;
                winterBtn.interactable = true;

                sunnyBtn.interactable = true;
                rainyBtn.interactable = true;
                cloudyBtn.interactable = true;
                foggyBtn.interactable = true;
            }

            energyBar.value = energy;

            Debug.Log("Current energy: " + energyBar.value);
        }
    }

    public void ReduceEnergy(int n)
    {
        if (energyBar != null)
        {
            if (energy < n)
            {
                Debug.LogError("Energy is lower than the cost!");
            }

            energy -= n;
            energyBar.value = energy;

            UpdateButtonGroups();
        }
    }

    public void EmptyEnergy()
    {
        ReduceEnergy(maxEnergy);
    }

    //public void UpdateSeasonButtonGroup()
    //{
    //    if (energy == maxEnergy)
    //    {
    //        springBtn.interactable = true;
    //        summerBtn.interactable = true;
    //        autumnBtn.interactable = true;
    //        winterBtn.interactable = true;
    //    }
    //    else
    //    {
    //        springBtn.interactable = false;
    //        summerBtn.interactable = false;
    //        autumnBtn.interactable = false;
    //        winterBtn.interactable = false;
    //    }
    //}

    //public void UpdateWeatherButtonGroup()
    //{
    //    if (energy == maxEnergy)
    //    {
    //        sunnyBtn.interactable = true;
    //        rainyBtn.interactable = true;
    //        cloudyBtn.interactable = true;
    //        foggyBtn.interactable = true;
    //    }
    //    else
    //    {
    //        sunnyBtn.interactable = false;
    //        rainyBtn.interactable = false;
    //        cloudyBtn.interactable = false;
    //        foggyBtn.interactable = false;
    //    }
    //}

    public void UpdateButtonGroups()
    {
        springBtn.interactable = false;
        summerBtn.interactable = false;
        autumnBtn.interactable = false;
        winterBtn.interactable = false;

        sunnyBtn.interactable = false;
        rainyBtn.interactable = false;
        cloudyBtn.interactable = false;
        foggyBtn.interactable = false;

        GetCurrentSeasonButton().interactable = true;
        GetCurrentWeatherButton().interactable = true;
    }

    // Enemy wave event handler
    public void HandleEvent(EnemyWavesEvent eventData)
    {
        int totalNumberOfWaves = eventData.totalNumberOfWaves;
        maxRoundDigit.GetComponent<TextMeshProUGUI>().text = totalNumberOfWaves.ToString();
        int curWave = eventData.curWave;
        currRoundDigit.GetComponent<TextMeshProUGUI>().text = curWave.ToString();
    }

    private Button GetCurrentSeasonButton()
    {
        Season season = SeasonController.GetSeason();

        if (season == Season.SPRING)
        {
            return springBtn;
        }
        else if (season == Season.SUMMER)
        {
            return summerBtn;
        }
        else if (season == Season.AUTUMN)
        {
            return autumnBtn;
        }
        else
        {
            return winterBtn;
        }
    }

    private Button GetCurrentWeatherButton()
    {
        Weather weather = WeatherSystem.GetWeather();

        if (weather == Weather.SUNNY)
        {
            return sunnyBtn;
        }
        else if (weather == Weather.RAINY)
        {
            return rainyBtn;
        }
        else if (weather == Weather.FOGGY)
        {
            return foggyBtn;
        }
        else
        {
            return cloudyBtn;
        }
    }
}
