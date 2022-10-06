using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherIcon : MonoBehaviour, IEventHandler<UIClickEvent>
{

    public Weather weatherType;

    private void OnEnable()
    {
        EventBus.register<UIClickEvent>(this);
    }

    private void OnDisable()
    {
        EventBus.unregister<UIClickEvent>(this);
    }

    public void HandleEvent(UIClickEvent eventData)
    {
        if (eventData.obj == gameObject)
        {
            WeatherSystem.setWeather(weatherType);
        }
    }

}
