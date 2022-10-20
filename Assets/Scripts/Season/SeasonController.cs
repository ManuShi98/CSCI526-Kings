using UnityEngine;

public class SeasonChangeEvent : IEventData
{
    public Season ChangedSeason { get; set; }
}

public class SeasonController : MonoBehaviour
{
    private static Season currentSeason;

    void Start()
    {
        currentSeason = Season.SPRING;
    }

    public void PickSeason(SeasonBtn seasonBtn)
    {
        var coin = GamingDataController.GetInstance().GetCoinCount();
        if (coin < 5)
        {
            Debug.Log("No enough money");
        }
        else
        {
            // Notify that season has changed
            currentSeason = seasonBtn.season;
            GamingDataController.GetInstance().SetCoinCount(coin - 5);
            EventBus.post(new SeasonChangeEvent() { ChangedSeason = seasonBtn.season });
        }
    }

    public static Season GetSeason()
    {
        return currentSeason;
    }
}
