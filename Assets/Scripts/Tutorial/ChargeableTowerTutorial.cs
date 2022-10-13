using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TransformUtil;

public class ChargeableTowerTutorial : MonoBehaviour, IEventHandler<WeatherEvent>, IEventHandler<CollidersClickEvent>, IEventHandler<UIClickEvent>
{
    private int step;
    private Transform arrow1;
    private Transform arrow2;
    private Transform arrow3;
    private float firstY;
    private float secondX;
    private float thirdX;

    public GameObject[] blockTowers;

    public GameObject targetTower;

    private void OnEnable()
    {
        EventBus.register<WeatherEvent>(this);
        EventBus.register<CollidersClickEvent>(this);
        EventBus.register<UIClickEvent>(this);
    }

    private void OnDisable()
    {
        EventBus.unregister<WeatherEvent>(this);
        EventBus.unregister<CollidersClickEvent>(this);
        EventBus.unregister<UIClickEvent>(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        foreach(var tower in blockTowers)
        {
            UIManager.blockTower(tower);
        }
        step = 1;
        arrow1 = transform.FindDeepChild("Arrow 1");
        arrow2 = transform.FindDeepChild("Arrow 2");
        arrow3 = transform.FindDeepChild("Arrow 3");
        if (arrow1 == null)
        {
            Debug.Log("Can't find arrow 1 in totiral 4");
        }
        else
        {
            firstY = arrow1.position.y;
        }
        if (arrow2 == null)
        {
            Debug.Log("Can't find arrow 2 in totiral 4");
        }
        else
        {
            secondX = arrow2.position.x;
        }
        if (arrow2 == null)
        {
            Debug.Log("Can't find arrow 3 in totiral 4");
        }
        else
        {
            thirdX = arrow3.position.x;
        }
    }

    // Update is called once per frame
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
        if (arrow3 != null && arrow3.gameObject.activeSelf == true)
        {
            arrow3.position = new Vector3(Mathf.PingPong(Time.time, 0.5f) + thirdX, arrow3.position.y, arrow3.position.z);
        }
    }

    public void HandleEvent(WeatherEvent eventData)
    {
        if(step == 1 && eventData.weather == Weather.RAINY)
        {
            step = 2;
            arrow1.gameObject.SetActive(false);
            arrow2.gameObject.SetActive(true);
        }
    }

    public void HandleEvent(CollidersClickEvent eventData)
    {
        if (eventData.obj == null && step == 3)
        {
            step = 2;
            arrow2.gameObject.SetActive(true);
            arrow3.gameObject.SetActive(false);
        }
        else if (eventData.obj == targetTower && step == 2)
        {
            step = 3;
            arrow2.gameObject.SetActive(false);
            arrow3.gameObject.SetActive(true);
        }
    }

    public void HandleEvent(UIClickEvent eventData)
    {
        if (eventData.obj != null && eventData.obj.tag == "ClickableIcon" && step == 3)
        {
            step = 4;
            arrow3.gameObject.SetActive(false);
            foreach (var tower in blockTowers)
            {
                UIManager.unblockTower(tower);
            }
        }
    }
}
