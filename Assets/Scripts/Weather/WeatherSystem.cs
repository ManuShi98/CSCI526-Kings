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

    public static void SetWeather(Weather weather)
    {
        if(weather == currentWeather)
        {
            return;
        }
        currentWeather = weather;
        // Cost all the energy when change the weather.
        GamingDataController.GetInstance().EmptyEnergy();
        EventBus.postSticky(new WeatherEvent() { weather = weather });
    }

    // Start is called before the first frame update
    void Start()
    {
        currentWeather = Weather.SUNNY;
    }

    public static void ResetWeather()
    {
        currentWeather = Weather.SUNNY;
    }
}
