using UnityEngine;

public enum TowerType
{
    CANNON_TOWER_1,
    CANNON_TOWER_2,
    CANNON_TOWER_3,
    ARROW_TOWER_1,
    ARROW_TOWER_2,
    ARROW_TOWER_3,
    MAGE_TOWER_1,
    MAGE_TOWER_2,
    MAGE_TOWER_3,
    DESTROY_TOWER
}

public class TowerBuildEvent : IEventData
{
    public int price;
    public TowerType towerType;
}

public class TowerBase : MonoBehaviour, IEventHandler<CollidersClickEvent>, IEventHandler<ThunderHitEvent>, IEventHandler<WeatherEvent>
{

    // The build tree
    public GameObject towerRoulette;

    public GameObject downGradeObject;

    // 雷电充能塔，临时
    // TODO: 使用继承
    public bool canBeDestroyedByThunder;
    private int powered;

    private GameObject rangeImage;

    private Collider2D thisCollider;

    private TowerRoulette activeBuildingTree;

    private SpriteRenderer spriteRenderer;

    private Weapon weapon;

    private void OnEnable()
    {
        EventBus.register<CollidersClickEvent>(this);
        EventBus.register<ThunderHitEvent>(this);
        EventBus.register<WeatherEvent>(this);
    }

    private void OnDisable()
    {
        EventBus.unregister<CollidersClickEvent>(this);
        EventBus.unregister<ThunderHitEvent>(this);
        EventBus.unregister<WeatherEvent>(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        // 雷电充能塔，临时
        powered = 1;
        weapon = gameObject.GetComponent<Weapon>();
        if (transform.Find("Range") != null)
        {
            rangeImage = transform.Find("Range").gameObject;
        }
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        thisCollider = GetComponent<Collider2D>();
    }

    private void OpenBuildingTree()
    {
        Debug.Log("Build Tree open");
        if (towerRoulette != null)
        {
            activeBuildingTree = Instantiate<GameObject>(towerRoulette, transform).GetComponent<TowerRoulette>();
            activeBuildingTree.transform.position = transform.position;

            activeBuildingTree.myTower = this;
            thisCollider.enabled = false;
        }
    }

    private void CloseBuildingTree()
    {
        Debug.Log("Build Tree close");
        if (activeBuildingTree != null)
        {
            Destroy(activeBuildingTree.gameObject);
            thisCollider.enabled = true;
        }
    }

    public void BuildTower(GameObject towerPrefab, string price, TowerType towerType)
    {
        Debug.Log("Build Tower");
        CloseBuildingTree();
        int cost = int.Parse(price);
        if (GamingDataController.GetInstance().GetCoinCount() < cost)
        {
            Toast.INSTANCE().MakeText("No enough money!");
            return;
        }
        EventBus.post<TowerBuildEvent>(new TowerBuildEvent() { price = cost, towerType = towerType});
        GamingDataController.GetInstance().ReduceCoins(cost);
        GameObject newTower = Instantiate(towerPrefab, transform.parent);
        newTower.transform.position = transform.position;
        newTower.transform.rotation = transform.rotation;
        Destroy(gameObject);
    }

    private void ShowRange(bool flag)
    {
        if (rangeImage != null)
        {
            rangeImage.SetActive(flag);
        }
    }


    public void HandleEvent(CollidersClickEvent eventData)
    {
        if (eventData.obj == gameObject)
        {
            ShowRange(true);
            if (activeBuildingTree == null)
            {
                OpenBuildingTree();
            }
        }
        else
        {
            ShowRange(false);
            CloseBuildingTree();
        }
    }

    public void HandleEvent(ThunderHitEvent eventData)
    {
        if (eventData.obj != gameObject)
        {
            return;
        }
        if (canBeDestroyedByThunder)
        {
            CloseBuildingTree();
            GameObject newTower = Instantiate<GameObject>(downGradeObject, transform.parent);
            newTower.transform.position = transform.position;
            newTower.transform.rotation = transform.rotation;
            Destroy(gameObject);
        }
        else
        {
            if(spriteRenderer == null || weapon == null)
            {
                return;
            }
            if (powered < 4)
            {
                weapon.rate *= 2;
                powered++;
                switch (powered)
                {
                    case 2:
                        spriteRenderer.color = Color.blue;
                        break;
                    case 3:
                        spriteRenderer.color = Color.yellow;
                        break;
                    case 4:
                        spriteRenderer.color = Color.red;
                        break;
                }
            }
        }
    }

    public void HandleEvent(WeatherEvent eventData)
    {
        if (eventData.weather != Weather.RAINY)
        {
            Color color;
            ColorUtility.TryParseHtmlString("#FFFFFF", out color);
            if (color != null && spriteRenderer != null)
            {
                spriteRenderer.color = color;
            }
            if(weapon != null)
            {
                weapon.rate = 1f;
            }
        }
    }
}
