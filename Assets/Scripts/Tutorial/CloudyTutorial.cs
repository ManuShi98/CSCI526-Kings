using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TransformUtil;

public class CloudyTutorial : MonoBehaviour, IEventHandler<WeatherEvent>, IEventHandler<CollidersClickEvent>
{
    private int step;
    private Transform arrow1;
    private Transform arrow2;

    private float firstY;
    private float secondX;

    public GameObject[] blockTowers;

    public GameObject targetTower;

    // Start is called before the first frame update
    void Start()
    {
        // Event Bus
        EventBus.register<CollidersClickEvent>(this);
        EventBus.register<WeatherEvent>(this);

        step = 1;
        GameObject cloudyBtn = GameObject.Find("CloudyBtn");
        arrow1 = transform.FindDeepChild("Arrow 1");
        Vector3 pos = Camera.main.ScreenToWorldPoint(cloudyBtn.transform.position);
        pos.y -= 1.5f;
        pos.z = 0;
        arrow1.position = pos;
        arrow2 = transform.FindDeepChild("Arrow 2");
        if (arrow1 == null)
        {
            Debug.Log("Can't find arrow 1 in totiral 9");
        }
        else
        {
            firstY = arrow1.position.y;
        }
        if (arrow2 == null)
        {
            Debug.Log("Can't find arrow 2 in totiral 9");
        }
        else
        {
            secondX = arrow2.position.x;
        }
        foreach (var tower in blockTowers)
        {
            UIManager.blockTower(tower);
        }
    }
    void Update()
    {
        if (arrow1 != null && arrow1.gameObject.activeSelf == true)
        {
            arrow1.position = new Vector3(arrow1.position.x, Mathf.PingPong(Time.time, 0.5f) + firstY, arrow1.position.z);
        }
        if (arrow2 != null && arrow2.gameObject.activeSelf == true)
        {
            arrow2.position = new Vector3(Mathf.PingPong(Time.time, 0.5f) + secondX, arrow2.position.y, arrow2.position.z);
        }
    }

    void OnDestroy()
    {
        // Event Bus
        EventBus.unregister<CollidersClickEvent>(this);
        EventBus.unregister<WeatherEvent>(this);
    }

    public void HandleEvent(WeatherEvent eventData)
    {
        if (step == 1 && eventData.weather == Weather.CLOUDY)
        {
            step = 2;
            arrow1.gameObject.SetActive(false);
            arrow2.gameObject.SetActive(true);
        }
    }
    public void HandleEvent(CollidersClickEvent eventData)
    {
        if (eventData.obj == targetTower && step == 2)
        {
            step = 3;
            arrow2.gameObject.SetActive(false);
            foreach (var tower in blockTowers)
            {
                UIManager.unblockTower(tower);
            }
        }
    }
}