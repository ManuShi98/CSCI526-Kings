using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderHitEvent : IEventData
{
    public GameObject obj { get; set; }
}

public class ThunderSystem : MonoBehaviour, IEventHandler<WeatherEvent>
{

    public GameObject thunderObject;
    private bool paused;
    private float width;
    private float height;

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
        paused = true;
        width = 0.2f;
        height = 2000f;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void HandleEvent(WeatherEvent eventData)
    {
        Debug.Log("Thunder system ready");
        if (eventData.weather == Weather.RAINY && paused)
        {
            paused = false;
            InvokeRepeating("generateThunder", 2.0f, 10.0f);
        }
        else if(eventData.weather != Weather.RAINY && !paused)
        {
            paused = true;
            CancelInvoke();
        }
    }

    public void generateThunder()
    {
        GameObject[] towers = GameObject.FindGameObjectsWithTag("Tower");
        int target = Random.Range(0, towers.Length - 1);
        GameObject thunder = Instantiate<GameObject>(thunderObject);
        Vector2 position = towers[target].transform.position;
        position.x -= width / 2;
        position.y += height / 2;
        thunder.transform.position = position;
        Destroy(thunder, 2f);
        EventBus.post<ThunderHitEvent>(new ThunderHitEvent() { obj = towers[target] });
    }
}