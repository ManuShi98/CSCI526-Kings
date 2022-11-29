using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoggyEnemy : EnemyUnit, IEventHandler<WeatherEvent>
{
    // Start is called before the first frame update
    void Start()
    {
        OnStart();
        HandleEvent(new WeatherEvent() { weather = WeatherSystem.GetWeather() });
    }

    // Update is called once per frame
    void Update()
    {
        OnUpdate();
    }

    public override void HandleEvent(WeatherEvent eventData)
    {
        FoggyStrengthen(eventData.weather);
    }

    private void FoggyStrengthen(Weather weather)
    {
        health = health / previousHealthRate * 1f;
        if (weather == Weather.FOGGY)
        {
            previousHealthRate += 0.2f;
        } else
        {
            previousHealthRate -= 0.2f;
        }
        health *= previousHealthRate;
        bar.HPRateEffect(previousHealthRate);
    }
}
