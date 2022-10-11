using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamingDataController : MonoBehaviour
{

    public volatile int coins;
    public volatile int health = 3;
    public static int maxRound = 7;
    public static int currRound = 1;

    private bool isDataChanged = false;

    public GameObject coinDigit;
    public GameObject healthDigit;
    public GameObject maxRoundDigit;
    public GameObject currRoundDigit;

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
        GetInstance();
        maxRoundDigit.GetComponent<TextMeshProUGUI>().text = maxRound.ToString();
        UpdateGamingData();
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

    public void SetCurrRound()
    {
        currRound = currRound + 1;
        isDataChanged = true;
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

    public bool isAlive()
    {
        return health > 0;
    }

    public void UpdateGamingData()
    {
        coinDigit.GetComponent<TextMeshProUGUI>().text = coins.ToString();
        healthDigit.GetComponent<TextMeshProUGUI>().text = health.ToString();
        UpdateLevelNumber();
    }

    private void UpdateLevelNumber()
    {
        if (SceneManager.GetActiveScene().name == "level1")
        {
            currRoundDigit.GetComponent<TextMeshProUGUI>().text = "1";
        }
        else if (SceneManager.GetActiveScene().name == "level2")
        {
            currRoundDigit.GetComponent<TextMeshProUGUI>().text = "2";
        }
        else if (SceneManager.GetActiveScene().name == "level3")
        {
            currRoundDigit.GetComponent<TextMeshProUGUI>().text = "3";
        }
        else if (SceneManager.GetActiveScene().name == "level4")
        {
            currRoundDigit.GetComponent<TextMeshProUGUI>().text = "4";
        }
        else if (SceneManager.GetActiveScene().name == "level5")
        {
            currRoundDigit.GetComponent<TextMeshProUGUI>().text = "5";
        }
        else if (SceneManager.GetActiveScene().name == "level6")
        {
            currRoundDigit.GetComponent<TextMeshProUGUI>().text = "6";
        }
        else if (SceneManager.GetActiveScene().name == "level7")
        {
            currRoundDigit.GetComponent<TextMeshProUGUI>().text = "7";
        }
    }
}
