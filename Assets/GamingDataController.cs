using TMPro;
using UnityEngine;

public class GamingDataController : MonoBehaviour
{

    public volatile int coins = 20;
    public volatile int health = 3;
    public int maxRound = 1;
    public int currRound = 1;

    private bool isDataChanged = false;

    public GameObject coinDigit;
    public GameObject healthDigit;
    public GameObject maxRoundDigit;
    public GameObject currRoundDigit;

    private void Start()
    {
        maxRoundDigit.GetComponent<TextMeshProUGUI>().text = maxRound.ToString();
        updateGamingData();
    }


    // Update is called once per frame
    void Update()
    {
        if(isDataChanged)
        {
            updateGamingData();
            isDataChanged = false;
        }
    }

    public int getCoinCount()
    {
        return coins;
    }

    public void setCoinCount(int val)
    {
        coins = val;
        isDataChanged = true;
    }

    public int getHealth()
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
    }

    public void reduceCoins(int val)
    {
        int currCoins = coins - val;
        setCoinCount(currCoins);
    }

    public void updateGamingData()
    {
        coinDigit.GetComponent<TextMeshProUGUI>().text = coins.ToString();
        healthDigit.GetComponent<TextMeshProUGUI>().text = health.ToString();
        currRoundDigit.GetComponent<TextMeshProUGUI>().text = currRound.ToString();
    }
}