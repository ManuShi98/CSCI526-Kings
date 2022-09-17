using UnityEngine;


public class SceneController : MonoBehaviour
{
    public enum Season
    {
        SPRING,
        SUMMER,
        AUTUMN,
        WINTER
    }

    public enum Weather
    {
        SUNNY,
        RAINY,
        CLOUDY,
        SNOWY
    }

    private static Season currentSeason;
    private static Weather currentWeather;

    // Start is called before the first frame update
    void Start()
    {
        currentSeason = Season.SPRING;
        currentWeather = Weather.SUNNY;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetSeason(Season season)
    {
        currentSeason = season;
    }

    public Season GetSeason()
    {
        return currentSeason;
    }

    public void SetWeather(Weather weather)
    {
        currentWeather = weather;
    }

    public Weather GetWeather()
    {
        return currentWeather;
    }
}
