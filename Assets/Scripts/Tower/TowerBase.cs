using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBase : MonoBehaviour, IEventHandler<CollidersClickEvent>
{

    // The build tree
    public GameObject towerRoulette;

    private GameObject rangeImage;

    private Collider2D thisCollider;

    private TowerRoulette activeBuildingTree;

    private void OnEnable()
    {
        EventBus.register<CollidersClickEvent>(this);
    }

    private void OnDisable()
    {
        EventBus.unregister<CollidersClickEvent>(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (transform.Find("Range") != null)
        {
            rangeImage = transform.Find("Range").gameObject;
        }
        thisCollider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public void BuildTower(GameObject towerPrefab)
    {
        Debug.Log("Build Tower");
        CloseBuildingTree();
        // TODO: Add money system here
        GameObject newTower = Instantiate<GameObject>(towerPrefab, transform.parent);
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
}
