using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TransformUtil;

public class StartTutorial : MonoBehaviour, IEventHandler<WeatherEvent>
{
    private Transform arrow1;
    private float firstY;

    private void OnEnable()
    {
        EventBus.register<WeatherEvent>(this);
    }

    private void OnDisable()
    {
        EventBus.unregister<WeatherEvent>(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        arrow1 = transform.FindDeepChild("Arrow 1");
        if (arrow1 == null)
        {
            Debug.Log("Can't find arrow 1 in totiral 6");
        }
        else
        {
            firstY = arrow1.position.y;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (arrow1 != null && arrow1.gameObject.activeSelf == true)
        {
            arrow1.position = new Vector3(arrow1.position.x, Mathf.PingPong(Time.time, 0.5f) + firstY, arrow1.position.z);
        }
    }

    // Foggy weather
    public void HandleEvent(WeatherEvent eventData)
    {
        if (eventData.weather == Weather.FOGGY)
        {
            arrow1.gameObject.SetActive(false);
        }
    }
}
