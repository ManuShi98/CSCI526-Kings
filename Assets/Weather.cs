using System.Collections;
using System.Collections.Generic;
using Random = System.Random;
using UnityEngine;

public class Weather : MonoBehaviour
{
  enum weatherMode
  {
    SUNNY,
    RAINY,
    SNOWY,
    WINDY
  }
  private static int weather;
  // Start is called before the first frame update
  void Start()
  {
    Random rd = new Random();
    int num = rd.Next(1, 5);
    if (num == 1) weather = (int)weatherMode.SUNNY;
    else if (num == 2) weather = (int)weatherMode.RAINY;
    else if (num == 3) weather = (int)weatherMode.SNOWY;
    else if (num == 4) weather = (int)weatherMode.WINDY;
    Debug.Log(weather);
  }

  void Update()
  {

  }

  public static int getWeather()
  {
    return weather;
  }
}