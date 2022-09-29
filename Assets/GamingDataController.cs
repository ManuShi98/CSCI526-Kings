using TMPro;
using UnityEngine;
using static SceneController;

public class GamingDataController : MonoBehaviour
{

    public volatile static int coins = 20;
    public volatile static int health = 3;
    public int maxRound = 1;
    public int currRound = 1;

    private bool isDataChanged = false;

    public GameObject coinDigit;
    public GameObject healthDigit;
    public GameObject maxRoundDigit;
    public GameObject currRoundDigit;

    private GamingDataController() { }

    private static GamingDataController controller;
    private static readonly object locker = new object();

    public static GamingDataController getInstance()
    {
        if (controller == null)
        {
            lock (locker)
            {
                //GameObject obj = new GameObject();
                //obj.name = "GamingDataController";
                //controller = obj.AddComponent<GamingDataController>();
                GameObject obj = GameObject.Find("GamingDataController");
                controller = obj.GetComponent<GamingDataController>();
            }
        }
        return controller;
    }

    private void Start()
    {
        getInstance();
        maxRoundDigit.GetComponent<TextMeshProUGUI>().text = maxRound.ToString();
        updateGamingData();
    }


    // Update is called once per frame
    void Update()
    {
        if (isDataChanged)
        {
            updateGamingData();
            isDataChanged = false;
        }
    }

    public static int getCoinCount()
    {
        return coins;
    }

    public void setCoinCount(int val)
    {
        coins = val;
        isDataChanged = true;
    }

    public static int getHealth()
    {
        return health;
    }

    public void setHealth(int val)
    {
        health = val;
        isDataChanged = true;
    }

    public int getMaxRound()
    {
        return maxRound;
    }

    public void setCurrRound()
    {
        currRound = currRound + 1;
        isDataChanged = true;
    }

    public void addCoins(int val)
    {
        int currCoins = coins + val;
        setCoinCount(currCoins);
        Singleton.Instance.numOfCoins += val;
    }

    public void reduceCoins(int val)
    {
        int currCoins = coins - val;
        setCoinCount(currCoins);
    }

    public void reduceHealth(int val = 1)
    { 
        int currHealth = Mathf.Max(health - val, 0);
        //Debug.Log("Call reduceHealth" + "   " + currHealth.ToString());
        setHealth(currHealth);
    }

    public static bool isAlive()
    {
        return health > 0;
    }

    public void updateGamingData()
    {
        coinDigit.GetComponent<TextMeshProUGUI>().text = coins.ToString();
        healthDigit.GetComponent<TextMeshProUGUI>().text = health.ToString();
        currRoundDigit.GetComponent<TextMeshProUGUI>().text = currRound.ToString();
    }
}
