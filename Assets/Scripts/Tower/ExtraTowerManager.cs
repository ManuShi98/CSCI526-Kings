using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraTowerManager : MonoBehaviour, IEventHandler<WeatherEvent>
{

    private void Start()
    {
        EventBus.register<WeatherEvent>(this);
    }

    private void OnDestroy()
    {
        EventBus.unregister<WeatherEvent>(this);
    }

    public void HandleEvent(WeatherEvent eventData)
    {
        foreach (Transform child in this.transform)
        {
            child.gameObject.SetActive(eventData.weather == Weather.CLOUDY);
        }
    }
}
