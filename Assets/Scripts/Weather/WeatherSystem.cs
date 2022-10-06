using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Weather
{
    SUNNY,
    RAINY,
    CLOUDY,
    FOGGY
}

public class WeatherEvent : IEventData
{
    public Weather weather;
}

public class WeatherSystem : MonoBehaviour
{

    private static Weather currentWeather;

    public static Weather GetWeather()
    {
        return currentWeather;
    }

    public static void setWeather(Weather weather)
    {
        currentWeather = weather;
        EventBus.post<WeatherEvent>(new WeatherEvent() { weather = weather });
    }

    // Start is called before the first frame update
    void Start()
    {
        currentWeather = Weather.SUNNY;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
