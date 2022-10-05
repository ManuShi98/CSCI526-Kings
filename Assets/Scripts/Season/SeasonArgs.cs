using System;
using UnityEngine;

public class SeasonArgs : EventArgs
{
  private string season;
  public string CurrentSeason
  {
    get
    {
      return season;
    }
    private set
    {
      season = value;
    }
  }
  public SeasonArgs(string season)
  {
    this.season = season;
  }
}
