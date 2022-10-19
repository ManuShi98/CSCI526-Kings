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
        // Notify that season has changed
        currentSeason = seasonBtn.season;
        EventBus.post(new SeasonChangeEvent() { ChangedSeason = seasonBtn.season });
    }

    public static Season GetSeason()
    {
        return currentSeason;
    }
}
