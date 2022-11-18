using UnityEngine;
using UnityEngine.UI;

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
        // Same season
        if (currentSeason == seasonBtn.season)
            return;

        int energy = GamingDataController.GetInstance().GetEnergy();
        if (energy != GamingDataController.maxEnergy)
        {
            Toast.INSTANCE().MakeText("No enough resource to change season!");
            Debug.Log("No enough energy");
        }
        else
        {
            // Notify that season has changed
            currentSeason = seasonBtn.season;
            EventBus.post(new SeasonChangeEvent() { ChangedSeason = seasonBtn.season });

            // In this function, all the buttons will be faded(interactable = false)
            GamingDataController.GetInstance().EmptyEnergy();
            //GameObject.Find("SeasonPanelFrame").GetComponent<UIBtnScaleEffect>().ChangeBtnSeason(seasonBtn)
        }
    }

    public static Season GetSeason()
    {
        return currentSeason;
    }
}
