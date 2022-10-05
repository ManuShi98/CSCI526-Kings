using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeasonBtn : MonoBehaviour
{
  [SerializeField]
  private string season;

  public string GetSeason()
  {
    return this.season;
  }
}
