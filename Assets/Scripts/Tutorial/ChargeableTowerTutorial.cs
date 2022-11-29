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
    private ThunderSystem thunderSystem;
    public GameObject guidancePanel;

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
        UIManager.blockByTag("Tower");
        thunderSystem = GameObject.Find("Thunder").GetComponent<ThunderSystem>();
        if(thunderSystem == null)
        {
            Debug.Log("Can't find thunder system");
        }
        
        step = 1;
        GameObject rainyBtn = GameObject.Find("RainyBtn");
        arrow1 = transform.FindDeepChild("Arrow 1");
        Vector3 pos = Camera.main.ScreenToWorldPoint(rainyBtn.transform.position);
        pos.y -= 1.5f;
        pos.z = 0;
        arrow1.position = pos;
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

    private void StrikeTower()
    {
        if(thunderSystem != null)
        {
            thunderSystem.generateCertainThunder(targetTower);
        } else
        {
            Debug.Log("Thunder System is not exists!");
        }
    }

    private void DisplayFirstGuidance()
    {
        if (guidancePanel != null)
        {
            guidancePanel.SetActive(true);
        }
        step = 2;
        arrow2.gameObject.SetActive(true);
        UIManager.unblockByTag("Tower");
        foreach (var tower in blockTowers)
        {
            UIManager.blockTower(tower);
        }
    }

    public void HandleEvent(WeatherEvent eventData)
    {
        if(step == 1 && eventData.weather == Weather.RAINY)
        {
            arrow1.gameObject.SetActive(false);
            StrikeTower();
            Invoke("DisplayFirstGuidance", 2f);
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
        else if (eventData.obj!=null && eventData.obj.CompareTag("Tower") && step == 2)
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
            PreGamePanel panel = guidancePanel.GetComponent<PreGamePanel>();
            panel.images = new Sprite[1];
            panel.images[0] = ImageUtil.GetSpriteByName("Tower_charge");
            panel.introductiuons = new string[1] { "The new tower can be charged by the thunder! Each time it get hit by lightning, it doubles its attack power" };
            guidancePanel.SetActive(true);
            panel.Start();
        }
    }
}
