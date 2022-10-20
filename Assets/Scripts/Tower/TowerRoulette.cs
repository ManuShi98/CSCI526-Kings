using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerRoulette : MonoBehaviour
{

    [HideInInspector]
    public TowerBase myTower;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Build(GameObject prefab, string price, TowerType towerType)
    {
        myTower.BuildTower(prefab, price, towerType);
    }
}
