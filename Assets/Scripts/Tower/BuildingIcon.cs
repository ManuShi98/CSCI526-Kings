using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuildingIcon : MonoBehaviour, IEventHandler<UIClickEvent>
{

    public int price;
    public GameObject towerPrefab;
    public TowerType towerType;

    private TextMeshProUGUI priceText;
    private TowerRoulette towerRoulette;

    private void OnEnable()
    {
        EventBus.register<UIClickEvent>(this);
    }

    private void OnDisable()
    {
        EventBus.unregister<UIClickEvent>(this);
    }

    // Update is called once per frame
    void Awake()
    {
        towerRoulette = transform.GetComponentInParent<TowerRoulette>();
        priceText = GetComponentInChildren<TextMeshProUGUI>();
        if (towerPrefab == null)
        {
            gameObject.SetActive(false);
        }
        else
        {
            priceText.text = price.ToString();
        }
    }

    public void HandleEvent(UIClickEvent eventData)
    {
        if (eventData.obj == gameObject)
        {
            towerRoulette.Build(towerPrefab, price.ToString(), towerType);
        }
    }
}
