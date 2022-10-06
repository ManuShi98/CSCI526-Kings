using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Season
{
  SPRING,
  SUMMER,
  AUTUMN,
  WINTER
}

public class SeasonBtn : MonoBehaviour
{
  [SerializeField]
  public Season season;
}
