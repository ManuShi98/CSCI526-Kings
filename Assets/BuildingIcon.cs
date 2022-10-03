using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuildingIcon : MonoBehaviour
{

    public int price;
    public GameObject towerPrefab;

    private TextMeshProUGUI priceText;
    private TowerRoulette towerRoulette;

    private void OnEnable()
    {
        EventBus.registerEvent("UserUIClick", UserUIClick);
    }

    private void OnDisable()
    {
        EventBus.unregisterEvent("UserUIClick", UserUIClick);
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

    private void UserUIClick(GameObject obj, string param)
    {
        if (obj == gameObject)
        {
            towerRoulette.Build(towerPrefab);
        }
    }
}
