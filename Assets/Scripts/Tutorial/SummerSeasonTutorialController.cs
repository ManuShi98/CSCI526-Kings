using UnityEngine;
using TransformUtil;

public class SummerSeasonTutorialController : MonoBehaviour, IEventHandler<SeasonChangeEvent>, IEventHandler<CollidersClickEvent>, IEventHandler<UIClickEvent>
{
    private int step;
    private Transform arrow1;
    private Transform arrow2;
    private Transform arrow3;
    private float firstY;
    private float secondY;
    private float thirdY;

    public GameObject[] blockTowers;

    public GameObject targetTower;

    private void OnEnable()
    {
        EventBus.register<SeasonChangeEvent>(this);
        EventBus.register<CollidersClickEvent>(this);
        EventBus.register<UIClickEvent>(this);
    }

    private void OnDisable()
    {
        EventBus.unregister<SeasonChangeEvent>(this);
        EventBus.unregister<CollidersClickEvent>(this);
        EventBus.unregister<UIClickEvent>(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        foreach (var tower in blockTowers)
        {
            UIManager.blockTower(tower);
        }
        step = 1;
        arrow1 = transform.FindDeepChild("Arrow 1");
        arrow2 = transform.FindDeepChild("Arrow 2");
        arrow3 = transform.FindDeepChild("Arrow 3");

        firstY = arrow1.position.y;
        secondY = arrow2.position.y;
        thirdY = arrow3.position.y;
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
            arrow2.position = new Vector3(arrow2.position.x, Mathf.PingPong(Time.time, 0.5f) + secondY, arrow2.position.z);
        }
        if (arrow3 != null && arrow3.gameObject.activeSelf == true)
        {
            arrow3.position = new Vector3(arrow3.position.x, Mathf.PingPong(Time.time, 0.5f) + thirdY, arrow3.position.z);
        }
    }

    public void HandleEvent(SeasonChangeEvent eventData)
    {
        if (step == 1 && eventData.ChangedSeason == Season.SUMMER)
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
        if (eventData.obj != null && eventData.obj.CompareTag("StartButton") && step == 3)
        {
            Debug.Log(eventData.obj.tag);
            Debug.Log("eventData.obj: " + eventData.obj);
            step = 4;
            arrow3.gameObject.SetActive(false);
            foreach (var tower in blockTowers)
            {
                UIManager.unblockTower(tower);
            }
        }
    }
}
