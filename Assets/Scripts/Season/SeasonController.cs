using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeasonChangeEvent : IEventData
{
  public Season changedSeason { get; set; }
}

public class SeasonController : MonoBehaviour
{
  // Start is called before the first frame update
  private static Season currentSeason;
  void Start()
  {
    currentSeason = Season.SPRING;
  }
  public void PickSeason(SeasonBtn seasonBtn)
  {
    // Notify that season has changed
    currentSeason = seasonBtn.season;
    EventBus.post(new SeasonChangeEvent() { changedSeason = seasonBtn.season });
  }

  public static Season GetSeason()
  {
    return currentSeason;
  }
}
