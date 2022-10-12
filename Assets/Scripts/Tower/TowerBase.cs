using UnityEngine;

public class TowerBase : MonoBehaviour, IEventHandler<CollidersClickEvent>, IEventHandler<ThunderHitEvent>
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

    private void OnEnable()
    {
        EventBus.register<CollidersClickEvent>(this);
        EventBus.register<ThunderHitEvent>(this);
    }

    private void OnDisable()
    {
        EventBus.unregister<CollidersClickEvent>(this);
        EventBus.unregister<ThunderHitEvent>(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        // 雷电充能塔，临时
        powered = 1;

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

    public void BuildTower(GameObject towerPrefab, string price)
    {
        Debug.Log("Build Tower");
        CloseBuildingTree();
        int cost = int.Parse(price);
        if (GamingDataController.GetInstance().GetCoinCount() < cost)
        {
            //TODO:添加金额不足提示
            return;
        }

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
            if (powered < 4)
            {
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
}
