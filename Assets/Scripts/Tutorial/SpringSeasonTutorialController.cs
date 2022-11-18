using UnityEngine;
using TransformUtil;

public class SpringSeasonTutorialController : MonoBehaviour, IEventHandler<CollidersClickEvent>, IEventHandler<GameStartEvent>
{
    private int step;
    private Transform arrow1;
    private Transform arrow2;
    private Transform arrow3;
    private Transform arrow4;
    private float firstX;
    private float secondY;
    private float thirdY;
    private float forthY;

    private float timer = 3;

    public GameObject[] blockTowers;

    public GameObject targetTower;

    private void OnEnable()
    {
        EventBus.register<CollidersClickEvent>(this);
        EventBus.register<GameStartEvent>(this);
    }

    private void OnDisable()
    {
        EventBus.unregister<CollidersClickEvent>(this);
        EventBus.unregister<GameStartEvent>(this);
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
        arrow4 = transform.FindDeepChild("Arrow 4");

        firstX = arrow1.position.x;
        secondY = arrow2.position.y;
        thirdY = arrow3.position.y;
        forthY = arrow4.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (arrow1 != null && arrow1.gameObject.activeSelf == true)
        {
            arrow1.position = new Vector3(firstX + Mathf.PingPong(Time.time, 0.5f), arrow1.position.y, arrow1.position.z);
        }
        if (arrow2 != null && arrow2.gameObject.activeSelf == true)
        {
            arrow2.position = new Vector3(arrow2.position.x, Mathf.PingPong(Time.time, 0.5f) + secondY, arrow2.position.z);
        }
        if (arrow3 != null && arrow3.gameObject.activeSelf == true)
        {
            arrow3.position = new Vector3(arrow3.position.x, Mathf.PingPong(Time.time, 0.5f) + thirdY, arrow3.position.z);
        }
        if (arrow4 != null && arrow4.gameObject.activeSelf == true)
        {
            arrow4.position = new Vector3(arrow4.position.x, Mathf.PingPong(Time.time, 0.5f) + forthY, arrow4.position.z);
        }

        if (timer >= 0 && (arrow1.gameObject.activeInHierarchy || arrow2.gameObject.activeInHierarchy))
        {
            timer -= Time.deltaTime;
        }
        else if (arrow1.gameObject.activeInHierarchy)
        {
            step = 2;
            arrow1.gameObject.SetActive(false);
            arrow2.gameObject.SetActive(true);
            timer = 3;
        } else if(arrow2.gameObject.activeInHierarchy)
        {
            step = 3;
            arrow2.gameObject.SetActive(false);
            arrow3.gameObject.SetActive(true);
            UIManager.unblockTower(targetTower);
        }
    }

    public void HandleEvent(CollidersClickEvent eventData)
    {
        if (eventData.obj == targetTower && step == 3)
        {
            step = 4;
            arrow3.gameObject.SetActive(false);
            arrow4.gameObject.SetActive(true);
        }
    }

    public void HandleEvent(GameStartEvent eventData)
    {
        step = 5;
        arrow1.gameObject.SetActive(false);
        arrow3.gameObject.SetActive(false);
        arrow4.gameObject.SetActive(false);
        foreach (var tower in blockTowers)
        {
            UIManager.unblockTower(tower);
        }
    }
}
